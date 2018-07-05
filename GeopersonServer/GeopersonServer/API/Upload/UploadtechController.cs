using GeopersonServer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GeopersonServer.API.Upload
{
    public class UploadtechController : Controller
    {
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Uploads(string org, string src) {
            try {
                var fname = Guid.NewGuid();
                var path = Path.Combine(Server.MapPath("~/UPLOADS/" + org), fname.ToString() + ".png");
                byte[] bytes = Convert.FromBase64String(src);
                System.IO.File.WriteAllBytes(path, bytes);
                return Success("Upload: " + org + " " + src+" "+ Path.Combine(Server.MapPath("~/UPLOADS/" + org)));
            } catch { return Failed(MessageUtility.ServerError()); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Uploadt()
        {
            try
            {
                var org = Request.Form["org"];
                var src = Request.Form["src"];
                var fname = Guid.NewGuid();
                var path = Path.Combine(Server.MapPath("~/UPLOADS/" + org), fname.ToString() + ".png");
                byte[] bytes = Convert.FromBase64String(src);
                System.IO.File.WriteAllBytes(path, bytes);
                return Success("Upload: " + org + " " + src + " " + Path.Combine(Server.MapPath("~/UPLOADS/" + org)));
            }
            catch { return Failed(MessageUtility.ServerError()); }
        }


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