using DiningHallServer.Services;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace DiningHallServer.Entities
{
     class Waiter
     {
          public readonly int Id;
          private DiningHall _diningHall = DiningHall.Instance;
          public Waiter(int id)
          {
               Id = id;
               Work();
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
                    PickUpTime = DateTimeOffset.Now.ToUnixTimeSeconds()
               });
               Console.WriteLine($"Order {order.Id} was picked up by the waiter {Id}");
               SendRequestService.SendPostRequest("http://kitchen-server-container:8000/order", jsonOrder);
          }

          public void Work()
          {
               Thread t = new(new ThreadStart(() =>
               {
                    while (true)
                    {
                         foreach (var table in _diningHall.Tables.ToArray())
                         {
                              if(table.State == Enums.TableStateEnum.WaitingToOrder)
                              {
                                   SendOrder(table.GenerateOrder());
                              }
                         }
                    }
               }));

               t.Start();
          }
     }
}
