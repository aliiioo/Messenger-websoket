namespace Multi_Shop.App_source
{
    using Microsoft.AspNetCore.Builder;
    using System;

    public class WebConfig
    {
        public static int buffersize = 4 * 1024;
        public static WebSocketOptions GetOptions()
        {
            WebSocketOptions wso = new WebSocketOptions()
            {
                ReceiveBufferSize = buffersize,
                KeepAliveInterval = TimeSpan.FromSeconds(120)
            };
            return wso;
        }

    }
}
