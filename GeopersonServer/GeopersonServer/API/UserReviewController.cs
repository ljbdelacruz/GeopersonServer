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
    public class UserReviewController : Controller
    {
        #region get
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByID(string id, string api) {
            try {
                var data = UsersReviewService.GetByID(Guid.Parse(id), Guid.Parse(api));
                return Success(UsersReviewVM.MToVM(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> CalculateUserReviews(string id, string api) {
            try {
                var data = UsersReviewService.GetByUserID(Guid.Parse(id), Guid.Parse(api));
                var average = 0;
                if (data.Count > 0) {
                    int totalStars = 0;
                    foreach (var model in data)
                    {
                        totalStars += model.Stars;
                    }
                    average = totalStars / data.Count;
                }
                return Success(""+average);
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        #endregion


        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByUserID(string id, string aid) {
            try {
                var data = UsersReviewService.GetByUserID(Guid.Parse(id), Guid.Parse(aid));
                return Success(UsersReviewVM.MsToVMs(data));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var id = Guid.NewGuid();
                var comment = Request.Form["comment"];
                var uid = Guid.Parse(Request.Form["uid"]);
                var senderID = Guid.Parse(Request.Form["sid"]);
                var api = Guid.Parse(Request.Form["api"]);
                var stars = int.Parse(Request.Form["stars"]);
                if (UsersReviewService.Insert(id, comment, uid, senderID, api, DateTime.Now, stars)){
                    return Success(id.ToString());
                }
                return Success(MessageUtility.ServerError());
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Remove() {
            try {
                var id = Guid.Parse(Request.Form["id"]);
                var aid = Guid.Parse(Request.Form["aid"]);
                if (UsersReviewService.Remove(id, aid)) {
                    return Success("");
                }
                return Failed(MessageUtility.ServerError());
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