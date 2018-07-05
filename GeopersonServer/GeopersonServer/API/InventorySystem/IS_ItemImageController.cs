using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using GeopersonServer.Services.InventorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API.InventorySystem
{
    public class IS_ItemImageController : Controller
    {
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var source = Request.Form["source"];
                var itemID = Guid.Parse(Request.Form["ii"]);
                if (IS_ItemImageService.Insert(id, source, itemID)) {
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
                var itemID = Guid.Parse(Request.Form["ii"]);
                if (IS_ItemImageService.Remove(id, itemID)) {
                    return Success(id.ToString());
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByItemID(string itemID) {
            try {
                var data = IS_ItemImageService.GetByItemID(Guid.Parse(itemID));
                return Success(IS_ItemImagesVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id, string iid) {
            try {
                var data = IS_ItemImageService.GetByID(Guid.Parse(id), Guid.Parse(iid));
                return Success(IS_ItemImagesVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        private JsonResult Success(dynamic data)
        {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed(string message)
        {
            return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}