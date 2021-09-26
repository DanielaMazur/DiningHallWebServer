using System;

namespace DiningHallServer.Entities
{
     class Distribution
     {
          public int OrderId { get; set; }
          public int TableId { get; set; }
          public int WaiterId { get; set; }
          public int[] Items { get; set; }
          public int Priority { get; set; }
          public double MaxWait { get; set; }
          public long PickUpTime { get; set; }
          public long CoockingTime { get; set; }
          public override string ToString()
          {
               return $"Order-{OrderId} was taken by the waiter-{WaiterId} from the table-{TableId} at {UnixTimeStampToDateTime(PickUpTime)}.";
          }

          private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
          {
               DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
               dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
               return dateTime;
          }
     }
}
