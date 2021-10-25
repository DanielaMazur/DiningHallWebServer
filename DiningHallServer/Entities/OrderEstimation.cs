namespace DiningHallServer.Entities
{
     class OrderEstimation
     {
          public string RestaurantAddress { get; set; }
          public int RestaurantId { get; set; }
          public int OrderId { get; set; }
          public double EstimatedWaitingTime { get; set; }
          public double CreatedTime { get; set; } 
          public double RegisteredTime { get; set; }
     }
}
