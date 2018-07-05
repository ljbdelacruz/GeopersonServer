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
    public class MeetupLocationController : Controller
    {
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetByConnectionID(string ID) {
            try {
                var data = MeetupLocationService.GetByConnectionID(ID, true).FirstOrDefault();
                var model = new MeetupLocationViewModel();
                if (data != null) {
                    model = MeetupLocationViewModel.MToVM(data);
                }
                return Json(new { success = true, data = model }, JsonRequestBehavior.AllowGet);
            } catch { return Json(new { success = false, message=MessageUtility.ServerError() }, JsonRequestBehavior.AllowGet); }
        }
        //signalR to alert all users within that connection that this user is near the meetup point

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert() {
            try {
                var connID = Request.Form["connID"];
                var note = Request.Form["note"];
                var longitude = Request.Form["long"];
                var latitude = Request.Form["lat"];
                var uid = Request.Form["UID"];
                //check if meetup already exist
                var meetup = MeetupLocationService.GetByConnectionID(connID, false).FirstOrDefault();
                if (meetup == null) {
                    //if not create one
                    MeetupLocationService.Insert(Guid.NewGuid(), Guid.Parse(connID), true, float.Parse(latitude), float.Parse(longitude), Guid.Parse(uid), note);
                } else {
                    //if already exist update data
                    MeetupLocationService.Update(meetup.ID.ToString(), true, Guid.Parse(uid), float.Parse(latitude), float.Parse(longitude), note);
                }
                //invoke signalR to inform connection that this user setup a meetup point
                SignalRClients.NotifyMeetupLocationSet(note, uid, connID);
                return Json(new { success = true });
            } catch { return Json(new { success = false, message=MessageUtility.ServerError() }); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Cancel() {
            try {
                var id = Request.Form["ID"];
                var connID = Request.Form["connID"];
                var note = Request.Form["note"];
                var longitude = float.Parse(Request.Form["long"]);
                var latitude = float.Parse(Request.Form["lat"]);
                var uid = Guid.Parse(Request.Form["UID"]);
                MeetupLocationService.Update(id, false, uid, latitude, longitude, note);
                SignalRClients.NotifyMeetupLocationCancel(note, uid.ToString(), connID);
                return Json(new { success = true });
            } catch { return Json(new { success = false,message=MessageUtility.ServerError() }); }
        }
        //this signalR notifies everybody that this user is already in the location
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> NotifyNearLocation() {
            try {
                var connID = Request.Form["connID"];
                var uid = Request.Form["UID"];
                var userName = Request.Form["uname"];
                SignalRClients.NotifyAlreadyInMeetupLocation(uid, userName, connID);
                return Json(new { success = true });
            } catch { return Json(new { success = false, message=MessageUtility.ServerError() }); }
        }

    }
}