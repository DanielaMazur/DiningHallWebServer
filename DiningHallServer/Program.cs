namespace DiningHallServer
{
     class Program
     {
          static void Main(string[] args)
          {
               HTTPServer server = new(3000);
               server.Start();

               var diningHall = DiningHall.Instance;
          }
     }
}
