using System.Linq;

namespace DiningHallServer.Entities
{
     class Order
     {
          public readonly int Id;
          public readonly double MaxWait;
          public int[] Items { get; set; }
          public int Priority { get; set; }
          public int TableId { get; set; }

          public Order(int id, int[] items, int priority, int tableId)
          {
               Id = id;
               Items = items;
               Priority = priority;
               TableId = tableId;
               MaxWait = Menu.Instance.MenuItems.Where(item => Items.Contains(item.Id)).Select(item => item.PreparationTime).Max() * 1.3;
          }
     }
}
