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
    public class LocationSessionController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var name = Request.Form["name"];
                var api = Guid.Parse(Request.Form["api"]);
                if (LocationSessionService.Insert(id, name, api)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id, string api) {
            try {
                var data = LocationSessionService.GetByID(Guid.Parse(id), Guid.Parse(api));
                return Success(LocationSessionVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #region util
        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message) {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}