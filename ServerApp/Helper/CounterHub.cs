using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ServerApp.Helper
{
   // [HubName("Counter")]
    public class CounterHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<CounterHub>();

        // Send the data to all clients (may be called from client JS - hub.client.broadcastCommonData)
        public void BroadcastCountData(string data)
        {
            Clients.All.addedNewCountData(data);
        }

        // Send the data to all clients (may be called from server C#)
        // In this example, called by TestController on data update (see the Post method)
        public static void BroadcastCountDataStatic(string data)
        {
            hubContext.Clients.All.addedNewCountData(data);
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}