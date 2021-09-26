using System;
using System.Collections.Generic;
using DiningHallServer.Entities;

namespace DiningHallServer
{
     class DiningHall
     {
          public List<Table> Tables { get; set; } = new();
          public List<Waiter> Waiters { get; set; } = new();

          private static readonly Lazy<DiningHall> controllerInstance = new(() => new DiningHall());

          public static DiningHall Instance { get { return controllerInstance.Value; } }

          private DiningHall() 
          {
          }
     }
}
