using DiningHallServer.Enums;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningHallServer.Entities
{
     class Table
     {
          private TableStateEnum _state;
          public TableStateEnum State
          {
               get
               {
                    return _state;
               }
               set
               {
                    if (value == TableStateEnum.Free)
                    {
                         Task.Delay(new Random().Next(100, 500)).ContinueWith((task) =>
                         {
                              _state = TableStateEnum.WaitingToOrder;
                              _diningHall.HandleNewOrder();
                         });
                    }
                    _state = value;
               }
          }

          public readonly int Id;
          private DiningHall _diningHall;
          public Table(int id, DiningHall diningHall)
          {
               Id = id;
               State = TableStateEnum.Free;
               _diningHall = diningHall;
          }

          public Order GenerateOrder()
          {
               var random = new Random();
               var numberOfFoods = random.Next(1, 10);
               int[] foods = new int[numberOfFoods];
               for (var i = 0; i < numberOfFoods; i++)
               {
                    var foodId = random.Next(10);
                    while (foods.Contains(foodId))
                    {
                         foodId = random.Next(1, 10);
                    }
                    foods[i] = foodId;
               }
               State = TableStateEnum.WaitingToBeServed;
               var orderId = Guid.NewGuid().GetHashCode();
               return new Order(orderId, foods, random.Next(1, 5), Id);
          }

          public void ReciveOrder(Distribution order)
          {
               if(order.TableId == this.Id)
               {
                    State = TableStateEnum.Free;
                    var stars = GetOrderStar(order);
                    DiningHall.Instance.Marks.Add(stars);
                    Console.OutputEncoding = Encoding.UTF8;
                    for (var i =0; i< stars; i++)
                    {
                         Console.Write("⭐");
                    }
                    Console.WriteLine("");
               }
          }

          private int GetOrderStar(Distribution order)
          {
               var orderTotalTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - order.PickUpTime;
               Console.WriteLine(orderTotalTime + "    " + order.MaxWait);
               if (orderTotalTime  < order.MaxWait) return 5;
               if (orderTotalTime * 1.1 < order.MaxWait) return 4;
               if (orderTotalTime * 1.2 < order.MaxWait) return 3;
               if (orderTotalTime * 1.3 < order.MaxWait) return 2;
               if (orderTotalTime * 1.4 < order.MaxWait) return 1;
               return 1;
          }
     }
}
