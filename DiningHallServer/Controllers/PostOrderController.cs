using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiningHallServer.Controllers
{
     class PostOrderController : Controller
     {
          private static readonly Lazy<Controller> controllerInstance = new(() => new PostOrderController());

          public static Controller Instance { get { return controllerInstance.Value; } }

          private PostOrderController() : base("POST", "/v2/order")
          {
          }

          public override void HandleRequest(HttpListenerContext httpListenerContext)
          {
               StreamReader stream = new(httpListenerContext.Request.InputStream);
               string order = stream.ReadToEnd();

               var recivedOrder = JsonConvert.DeserializeObject<OrderEstimation>(order);

               Console.WriteLine($"Dining Hall recived the order with id-{recivedOrder.OrderId}!");

               httpListenerContext.Response.StatusCode = 200;
               httpListenerContext.Response.ContentType = "text/plain";
               byte[] responseBuffer = Encoding.UTF8.GetBytes($"Dining Hall recived the order with id-{recivedOrder.OrderId}!");
               httpListenerContext.Response.ContentLength64 = responseBuffer.Length;
               Stream output = httpListenerContext.Response.OutputStream;
               output.Write(responseBuffer, 0, responseBuffer.Length);
               output.Close();

               var waiter = DiningHall.Instance.Waiters.Single(waiter => waiter.Id == recivedOrder.WaiterId);
               waiter.ServeOrder(recivedOrder);
          }
     }
}
