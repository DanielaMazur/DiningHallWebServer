using DiningHallServer.Services;
using Newtonsoft.Json;
using System;

namespace DiningHallServer.Entities
{
     class Waiter
     {
          public readonly int Id;
          public Waiter(int id)
          {
               Id = id;
          }

          public void SendOrder(Order order)
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
               SendRequestService.SendPostRequest("http://localhost:8000/order", jsonOrder);
          }
     }
}
