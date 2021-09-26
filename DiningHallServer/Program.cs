using DiningHallServer.Entities;
using System.Collections.Generic;

namespace DiningHallServer
{
     class Program
     {
          static void Main(string[] args)
          {
               HTTPServer server = new(3000);
               server.Start();

               List<Table> tables = new() { new Table(1), new Table(2), new Table(3), new Table(4), new Table(5) };
               List<Waiter> waiters = new() { new Waiter(1), new Waiter(2) };

               var diningHall =  DiningHall.Instance;
               diningHall.Waiters = waiters;
               diningHall.Tables = tables;
          }
     }
}
