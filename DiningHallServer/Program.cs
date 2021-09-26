using DiningHallServer.Entities;
using DiningHallServer.Enums;
using System.Collections.Generic;

namespace DiningHallServer
{
     class Program
     {
          static void Main(string[] args)
          {
               HTTPServer server = new(3000);
               server.Start();

               List<Table> tables = new() { new Table(1) };
               var waiter = new Waiter(1);

               foreach (var table in tables)
               {
                    if (table.State == TableStateEnum.WaitingToOrder)
                    {
                         waiter.SendOrder(table.GenerateOrder());
                    }
               }
          }
     }
}
