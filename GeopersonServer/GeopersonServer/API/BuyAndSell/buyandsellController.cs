using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using GeopersonServer.Services.BuyAndSellFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API.BuyAndSell
{
    public class buyandsellController : Controller
    {
        #region util
        private JsonResult Success(dynamic data)
        {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message)
        {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ItemsImages
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> IIInsert() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var iid = Guid.Parse(Request.Form["iid"]);
                var ilid = Guid.Parse(Request.Form["ilid"]);
                if (ItemsImagesService.Insert(id, iid, ilid)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> IIRemove() {
            try {
                var ilid = Guid.Parse(Request.Form["ilid"]);
                var iid = Guid.Parse(Request.Form["iid"]);
                if (ItemsImagesService.Remove(ilid, iid)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> IIGetByIID(string id) {
            try {
                var data = ItemsImagesService.GetByItemID(Guid.Parse(id));
                return Success(ItemsImagesVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #endregion
    }
}