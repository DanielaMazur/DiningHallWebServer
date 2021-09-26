using DiningHallServer.Entities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace DiningHallServer.Controllers
{
     class GetTablesController : Controller
     {
          private static readonly Lazy<Controller> controllerInstance = new(() => new GetTablesController());

          public static Controller Instance { get { return controllerInstance.Value; } }

          private GetTablesController() : base("GET", "/")
          {
          }

          public override void HandleRequest(HttpListenerContext httpListenerContext)
          {
               var table = new Table(1);
               string jsonTable = JsonConvert.SerializeObject(table);
               byte[] jsonByteArray = Encoding.Default.GetBytes(jsonTable);

               var response = httpListenerContext.Response;
               response.ContentLength64 = jsonByteArray.Length;

               Stream output = response.OutputStream;
               output.Write(jsonByteArray, 0, jsonByteArray.Length);
               output.Close();
          }
     }
}
