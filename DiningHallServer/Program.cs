namespace DiningHallServer
{
     class Program
     {
          static void Main(string[] args)
          {
               HTTPServer server = new();
               server.Start();

               var diningHall = DiningHall.Instance;
          }
     }
}
