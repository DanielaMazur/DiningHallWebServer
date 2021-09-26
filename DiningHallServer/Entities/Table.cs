using DiningHallServer.Enums;
using System;
using System.Linq;

namespace DiningHallServer.Entities
{
     class Table
     {
          public TableStateEnum State { get; private set; } = TableStateEnum.WaitingToOrder;
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
               for(var i = 0; i < numberOfFoods; i++)
               {
                    var foodId = random.Next(10);
                    while (foods.Contains(foodId))
                    {
                         foodId = random.Next(1, 10);
                    }
                    foods[i] = foodId;
               }
               State = TableStateEnum.WaitingToBeServed;
               return new Order(Guid.NewGuid().GetHashCode(), foods, random.Next(1, 5), Id);
          }
     }
}
