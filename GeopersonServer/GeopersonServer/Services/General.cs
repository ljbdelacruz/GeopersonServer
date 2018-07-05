using GeopersonServer.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.Services
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }

    public class SignalRClients
    {
        //Notifies connection if request is accepted
        public static void NotifyInvitationAccepted(string connectionID, string userWhoAccepted)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<RequestHub>();
            hubContext.Clients.All.InvitationAccepted(connectionID, userWhoAccepted);
        }
        //notify request invitation declined
        public static void NotifyInvitationCancelled(string connectionID, string requestToName)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<RequestHub>();
            hubContext.Clients.All.InvitationCancelled(connectionID, requestToName);
        }

        //connection connectionID, memberUserID
        public static void NotifyConnectionLeave(string connectionID, string muid, string usernameWhoLeft)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<RequestHub>();
            hubContext.Clients.All.GroupLeave(connectionID, muid, usernameWhoLeft);
        }
        public static void NotifyInvitationSent(string connectionName, string cid, string rtID)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<RequestHub>();
            hubContext.Clients.All.RequestSent(connectionName, cid, rtID);
        }

        //meetup location
        public static void NotifyMeetupLocationSet(string note, string uid, string connectionID)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<RequestHub>();
            hubContext.Clients.All.MeetupSetup(note, uid, connectionID);
        }
        public static void NotifyMeetupLocationCancel(string note, string uid, string connectionID)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<RequestHub>();
            hubContext.Clients.All.MeetupCancel(note, uid, connectionID);
        }
        //notify that user is already in the meetup location
        public static void NotifyAlreadyInMeetupLocation(string uid, string uname, string connectionID)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<RequestHub>();
            hubContext.Clients.All.MeetupSetup(uid, uname, connectionID);
        }
    }
}