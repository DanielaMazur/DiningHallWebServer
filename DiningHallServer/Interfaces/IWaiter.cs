using DiningHallServer.Entities;

namespace DiningHallServer.Interfaces
{
     interface IWaiter
     {
          int Id { get; }
          void PickUpOrder(Table table);
          void ServeOrder(Distribution order);
     }
}
