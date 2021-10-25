using System;
using System.Net;

namespace DiningHallServer.Controllers
{
     class GetOrderController : Controller
     {
          private static readonly Lazy<Controller> controllerInstance = new(() => new GetOrderController());

          public static Controller Instance { get { return controllerInstance.Value; } }

          private GetOrderController() : base("GET", "/v2/order")
          {
          }

          public override void HandleRequest(HttpListenerContext httpListenerContext)
          {
               throw new NotImplementedException();
          }
     }
}
