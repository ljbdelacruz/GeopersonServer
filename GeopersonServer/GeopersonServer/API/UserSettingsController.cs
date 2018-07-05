using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API
{
    public class UserSettingsController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var uid = Guid.Parse(Request.Form["uid"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if(UserSettingService.Insert(id, uid, aid, DateTime.Now)){
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByUID(string id) {
            try {
                var data = UserSettingService.GetByUID(Guid.Parse(id));
                return Success(UserSettingsVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }

        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message) {
            return Json(new { success = false, message = MessageUtility.ServerError() }, JsonRequestBehavior.AllowGet);
        }
    }
}