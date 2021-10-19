using DiningHallServer.Enums;
using System;
using System.Linq;
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
                         Task.Delay(new Random().Next(30, 100) * Constants.TIME_UNIT).ContinueWith((task) =>
                         {
                             _state = TableStateEnum.WaitingToOrder;
                              DiningHall.Instance.TablesWithOrders.TryAdd(this);
                         });
                    }
                    _state = value;
               }
          }

          public readonly int Id;
          public Table(int id)
          {
               Id = id;
               State = TableStateEnum.Free;
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
               if (order.TableId == this.Id)
               {
                    State = TableStateEnum.Free;
                    var stars = GetOrderStar(order);
                    DiningHall.Instance.Marks.Add(stars);
                    for (var i = 0; i < stars; i++)
                    {
                         Console.Write("*");
                    }
                    Console.WriteLine("\nAvarage rating => {0} from {1} estimations", (float)DiningHall.Instance.Marks.Sum() / DiningHall.Instance.Marks.Count, DiningHall.Instance.Marks.Count);
               }
          }

          private int GetOrderStar(Distribution order)
          {
               var orderTotalTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - order.PickUpTime;
               Console.WriteLine(orderTotalTime + "    " + order.MaxWait);
               if (orderTotalTime < order.MaxWait) return 5;
               if (orderTotalTime < order.MaxWait * 1.1) return 4;
               if (orderTotalTime < order.MaxWait * 1.2) return 3;
               if (orderTotalTime < order.MaxWait * 1.3) return 2;
               if (orderTotalTime < order.MaxWait * 1.4) return 1;
               return 0;
          }
     }
}
