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
    public class LocationSessionParticipantsController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var lsID = Guid.Parse(Request.Form["lsID"]);
                var uid = Guid.Parse(Request.Form["userID"]);
                var isAdmin = Boolean.Parse(Request.Form["isAdmin"]);
                if (LocationSessionParticipantService.Insert(id, uid, lsID, isAdmin)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch {
                return Failed(MessageUtility.ServerError());
            }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                if (LocationSessionParticipantService.Remove(id)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByUserID(string id) {
            try {
                var data = LocationSessionParticipantService.GetByUserID(Guid.Parse(id));
                return Success(LocationSessionParticipantsVM.MsToVMs(data));
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