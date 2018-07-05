using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace GeopersonServer.Hubs
{
    [HubName("requestHub")]
    public class RequestHub : Hub
    {
        private readonly IHubContext _uptimeHub;
        public RequestHub()
        {
            _uptimeHub = GlobalHost.ConnectionManager.GetHubContext<RequestHub>();
        }
        public void Notify(string ID)
        {
            Clients.All.Notify(ID);
        }

    }
}