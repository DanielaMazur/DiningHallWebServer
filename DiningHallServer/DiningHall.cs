using System;
using System.Collections.Generic;
using System.Threading;
using DiningHallServer.Entities;

namespace DiningHallServer
{
     class DiningHall
     {
          public List<int> Marks { get; set; } = new();
          public List<Table> Tables { get; set; } = new();
          public List<Waiter> Waiters { get; set; } = new();
          private Semaphore _tablesSemaphore;

          private static readonly Lazy<DiningHall> controllerInstance = new(() => new DiningHall());

          public static DiningHall Instance { get { return controllerInstance.Value; } }

          private DiningHall()
          {
               InitDiningHall(5, 2);
          }

          private void InitDiningHall(int numberOfTables, int numberOfWaiters)
          {
               _tablesSemaphore = new(numberOfTables, numberOfTables);
               for (var i = 1; i <= numberOfTables; i++)
               {
                    Tables.Add(new(i, this));
               }
               for (var i = 1; i <= numberOfWaiters; i++)
               {
                    Waiter waiter = new(i, _tablesSemaphore);
                    waiter.Work();
                    Waiters.Add(waiter);
               }
          }

          public void HandleNewOrder()
          {
               _tablesSemaphore.Release(1);
          }
     }
}
