using DiningHallServer.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiningHallServer.Entities
{
     class Table
     {
          private TableStateEnum _state = TableStateEnum.WaitingToOrder;
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
                              _state = TableStateEnum.WaitingToBeServed;
                         });
                    }
                    _state = value;
               }
          }
          public readonly int Id;
          public Table(int id)
          {
               Id = id;
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
     }
}
