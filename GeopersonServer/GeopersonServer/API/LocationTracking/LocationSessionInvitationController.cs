using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using GeopersonServer.Services.LocationTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API.LocationTracking
{
    public class LocationSessionInvitationController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var lsID = Guid.Parse(Request.Form["lsID"]);
                var uid = Guid.Parse(Request.Form["uid"]);
                var api = Guid.Parse(Request.Form["api"]);
                if (LocationSessionInvitationService.Insert(id, lsID, uid, api)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                if (LocationSessionInvitationService.Remove(id)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByUserID(string id, string api) {
            try {
                var data = LocationSessionInvitationService.GetByUserID(Guid.Parse(id), Guid.Parse(api));
                return Success(LocationSessionInvitationVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message) {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}