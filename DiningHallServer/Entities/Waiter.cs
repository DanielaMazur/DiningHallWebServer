using DiningHallServer.Interfaces;
using DiningHallServer.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace DiningHallServer.Entities
{
     class Waiter : IWaiter
     {
          public int Id { get; }
          private BlockingCollection<Action> _tasksCollection = new();

          public Waiter(int id)
          {
               Id = id;
               new Thread(() =>
               {
                   while (_tasksCollection.TryTake(out Action action, -1))
                   {
                        action.Invoke();
                   }
               }).Start();
          }

          public void PickUpOrder(Table table)
          {
               this._tasksCollection.TryAdd(() => this.HandlePickUpOrder(table));
          }

          public void ServeOrder(Distribution order)
          {
               this._tasksCollection.TryAdd(() => this.HandleServeOrder(order));
          }

          private void HandleServeOrder(Distribution order)
          {
               var table = DiningHall.Instance.Tables.Single(table => table.Id == order.TableId);
               table.ReciveOrder(order);
          }

          private void HandlePickUpOrder(Table table)
          {
               Thread.Sleep(new Random().Next(2, 4) * Constants.TIME_UNIT);
               SendOrder(table.GenerateOrder());
               DiningHall.Instance.AvailabeWaiters.TryAdd(this);
          }

          private void SendOrder(Order order)
          {
               string jsonOrder = JsonConvert.SerializeObject(new
               {
                    OrderId = order.Id,
                    WaiterId = this.Id,
                    order.TableId,
                    order.Items,
                    order.Priority,
                    order.MaxWait,
                    PickUpTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() * Constants.TIME_UNIT
               });
               Console.WriteLine($"Order {order.Id} was picked up by the waiter {Id}");
               SendRequestService.SendPostRequest("http://kitchen-server-container:8000/order", jsonOrder);
          }
     }
}
