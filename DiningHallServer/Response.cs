
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DiningHallServer
{
     class Response
     {
          private byte[] _data = null;
          private string _status;
          private string _mime;

          private Response(string status, string mime, byte[] data)
          {
               _status = status;
               _mime = mime;
               _data = data;
          }

          public static Response From(Request req)
          {
               if (req == null)
               {
                    return MakeNullRequest();
               }

               //if(req.Type == "GET")
               //{
               var table = new Table(1);
               string jsonTable = JsonConvert.SerializeObject(table);
               byte[] jsonByteArray = Encoding.Unicode.GetBytes(jsonTable);

               return new Response("200", "application/json", jsonByteArray);
               //}
               //return new Response();
          }

          private static Response MakeNullRequest()
          {
               return new Response("400 Bad Request", "application/json", Array.Empty<byte>());
          }

          public void Post(NetworkStream stream)
          {
               StreamWriter writer = new(stream);
               writer.WriteLine(string.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n",
                    HTTPServer.VERSION, _status, HTTPServer.NAME, _mime, _data.Length));
               writer.Flush();
               stream.Write(_data, 0, _data.Length);
               stream.Close();
          }
     }
}
