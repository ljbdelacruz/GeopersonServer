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
    public class IS_ItemStockController : Controller
    {
        #region request post
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var bcode = Request.Form["bcode"];
                var itemID = Guid.Parse(Request.Form["ii"]);
                var sid = Guid.Parse(Request.Form["sid"]);
                if(IS_ItemStockService.Insert(id, bcode, itemID, sid))
                {
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
                if (IS_ItemStockService.Remove(id, itemID)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Update() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var itemID = Guid.Parse(Request.Form["itemID"]);
                var status = Guid.Parse(Request.Form["status"]);
                if (IS_ItemStockService.UpdateStatus(id, itemID, status)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
        #region web
        #endregion
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByItemID(string id, string sid) {
            try {
                var data = IS_ItemStockService.GetByItemID(Guid.Parse(id), Guid.Parse(sid));
                return Success(IS_ItemStockVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id, string ii, string sid) {
            try {
                var data = IS_ItemStockService.GetByID(Guid.Parse(id), Guid.Parse(ii), Guid.Parse(sid));
                return Success(IS_ItemStockVM.MToVM(data)); 
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion
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
    }
}