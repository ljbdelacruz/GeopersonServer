using GeopersonServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API
{
    public class SignalController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> NotifyLeaveConnection() {
            try {
                var cid = Request.Form["cid"];
                var mid = Request.Form["mid"];
                var username = Request.Form["uname"];
                SignalRClients.NotifyConnectionLeave(cid, mid, username);
                return Json(new { success = true });
            } catch { return Json(new { success = false, message = MessageUtility.ServerError() }); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> NotifyInvitationAccepted() {
            try
            {
                var cid = Request.Form["cid"];
                var username = Request.Form["uname"];
                SignalRClients.NotifyInvitationAccepted(cid, username);
                return Json(new { success = true });
            } catch { return Json(new { success = false, message = MessageUtility.ServerError() }); }

        }
    }
}