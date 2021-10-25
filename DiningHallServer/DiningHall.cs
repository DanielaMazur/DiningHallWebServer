using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using DiningHallServer.Entities;
using DiningHallServer.Interfaces;

namespace DiningHallServer
{
     class DiningHall
     {
          public List<int> Marks { get; set; } = new();
          public List<Table> Tables { get; set; } = new();
          public List<IWaiter> Waiters { get; set; } = new();

          public BlockingCollection<Table> TablesWithOrders = new();
          public BlockingCollection<IWaiter> AvailabeWaiters = new();

          private static readonly Lazy<DiningHall> diningHallInstance = new(() => new DiningHall());

          public static DiningHall Instance { get { return diningHallInstance.Value; } }

          private DiningHall()
          {
               InitDiningHall(10, 4);
               Work();
          }

          private void InitDiningHall(int numberOfTables, int numberOfWaiters)
          {
               for (var i = 1; i <= numberOfTables; i++)
               {
                    Tables.Add(new(i));
               }
               for (var i = 1; i <= numberOfWaiters; i++)
               {
                    Waiter waiter = new(i);
                    Waiters.Add(waiter);
                    AvailabeWaiters.TryAdd(waiter);
               }
          }

          private void Work()
          {
               new Thread(() =>
               {
                    while (TablesWithOrders.TryTake(out Table tableWithOrder, -1))
                    {
                         while (AvailabeWaiters.TryTake(out IWaiter waiter, -1))
                         {
                              waiter.PickUpOrder(tableWithOrder);
                              break;
                         }
                    }
               }).Start();
          }
     }
}
