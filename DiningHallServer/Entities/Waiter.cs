using DiningHallServer.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;

namespace DiningHallServer.Entities
{
     class Waiter 
     {
          public readonly int Id;
          private readonly Semaphore _tablesSemaphore;

          public Waiter(int id, Semaphore tablesSemaphore)
          {
               Id = id;
               _tablesSemaphore = tablesSemaphore;
          }

          public void Work()
          {
               Thread t = new(new ThreadStart(() =>
               {
                    while(true)
                    {
                         _tablesSemaphore.WaitOne();
                         foreach (var table in DiningHall.Instance.Tables.ToArray())
                         {
                              if (table.State == Enums.TableStateEnum.WaitingToOrder)
                              {
                                   SendOrder(table.GenerateOrder());
                              }
                         }
                    }
               }));

               t.Start();
          }

          public void ServeOrder(Distribution order)
          {
               var table = DiningHall.Instance.Tables.Single(table => table.Id == order.TableId);
               table.ReciveOrder(order);
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
                    PickUpTime = DateTimeOffset.Now.ToUnixTimeMilliseconds()
               });
               Console.WriteLine($"Order {order.Id} was picked up by the waiter {Id}");
               SendRequestService.SendPostRequest("http://kitchen-server-container:8000/order", jsonOrder);
          }
     }
}
