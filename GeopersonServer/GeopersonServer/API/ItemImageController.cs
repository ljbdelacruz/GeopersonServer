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
    public class ItemImageController : Controller
    {
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByItemID(string id) {
            try {
                var data = ItemImageService.GetByItemsID(Guid.Parse(id));
                return Success(ItemImageVM.MsToVMs(data));
            } catch { return Failed(); }
        }

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var source = Request.Form["source"];
                var itemID = Guid.Parse(Request.Form["itemID"]);
                if (ItemImageService.Insert(id, source, itemID))
                {
                    return Success(id.ToString());
                }
                return Failed();
            } catch { return Failed(); }   
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                if (ItemImageService.Remove(id)) {
                    return Success("");
                }
                return Failed();
            } catch { return Failed(); }
        }

        private JsonResult Success(dynamic data) {
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult Failed() {
            return Json(new { success = false, message = MessageUtility.ServerError() }, JsonRequestBehavior.AllowGet);
        }

    }
}