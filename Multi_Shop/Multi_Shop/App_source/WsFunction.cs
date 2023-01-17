using Microsoft.AspNetCore.Http;
using Multi_Shop.Context;
using Multi_Shop.Repository;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Shop.App_source
{
    public class WsFunction
    {
        public static string full_message;
        public static string usernameing;


        public async Task listenAcceptAsync(HttpContext context)
        {
            WebSocket ws = await context.WebSockets.AcceptWebSocketAsync();
            string Uname = context.Request.Query["name"];
            userWebSocke uws = new userWebSocke();
            uws.Username = Uname;
            uws.Userws = ws;
            usernameing = Uname;
            UserList.listdic.Add(Uname, uws);
            await ReciveAsync(uws);
        }

        public async Task ReciveAsync(userWebSocke uws)
        {
            WebSocket ws = uws.Userws;
            string Uname = uws.Username;
            byte[] buff;
            while (ws.State == WebSocketState.Open)
            {
                buff = new byte[WebConfig.buffersize];
                WebSocketReceiveResult result = await ws.ReceiveAsync(new System.ArraySegment<byte>(buff, 0, buff.Length), System.Threading.CancellationToken.None);
                if (result != null)
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string strmsg = Encoding.UTF8.GetString(buff);
                        Send_recivemessage.Recivemessage Resmsg =JsonConvert.DeserializeObject<Send_recivemessage.Recivemessage>(strmsg);
                        userWebSocke Recums = UserList.listdic[Resmsg.Recive];
                        Send_recivemessage.SndMessage SendMsg = new Send_recivemessage.SndMessage()
                        {
                            message = Resmsg.Message,
                            sender = Uname
                        };
                        await SendAsync(Recums.Userws,SendMsg);
                    }
                    if (result.MessageType == WebSocketMessageType.Close) ////////////////////////////////////////
                    {
                        full_message=null;
                        //youre code 
                        return;
                    }
                    if (result.MessageType == WebSocketMessageType.Binary)
                    {
                        if (File.Exists("Herrr"))
                        {
                            byte[] fileup = File.ReadAllBytes("Herrr");
                            byte[] Newbuff = new byte[fileup.Length + result.Count];
                            Buffer.BlockCopy(fileup,0,Newbuff,0,fileup.Length);
                            Buffer.BlockCopy(buff, 0, Newbuff, fileup.Length, result.Count);
                            File.WriteAllBytes("Herrr", Newbuff);
                        }
                        else
                        {
                            byte[] fileup = new byte[result.Count];
                            Buffer.BlockCopy(buff,0,fileup,0,result.Count);
                            File.WriteAllBytes("Herrr", fileup);
                        }
                    }
                }

            }
        }

        public async Task SendAsync(WebSocket ws, Send_recivemessage.SndMessage message)
        {
            full_message += message+"<br>";
            string sremsg = JsonConvert.SerializeObject(message);
            byte[] buff = Encoding.UTF8.GetBytes(sremsg);
            await ws.SendAsync(
                new System.ArraySegment<byte>(buff,0,buff.Length)
              , WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
        }




    }
}
