using GeopersonServer.Models.Geoperson;
using GeopersonServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static GeopersonServer.Services.AllowCrossSiteJsonAttribute;

namespace GeopersonServer.API
{
    public class RequestController : Controller
    {
        [AllowCrossSiteJson]
        [HttpGet]
        public async Task<JsonResult> GetMyRequests(string ID) {
            try {
                var data = RequestService.GetByReceipentID(ID);
                var models = new List<RequestViewModel>();
                foreach (var mod in data) {
                    models.Add(RequestViewModel.MToVM(mod));
                }
                return Json(new { success = true, data=models }, JsonRequestBehavior.AllowGet);
            } catch { return Json(new { success = false }, JsonRequestBehavior.AllowGet); }
        }
        //insert request
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> Insert(){
            try {
                var id = Guid.NewGuid();
                var rfrom = Request.Form["RF"];
                var rtID = Request.Form["RTID"];
                var connID = Request.Form["CID"];
                var connection=ConnectionServices.GetByID(connID);
                //first check if it is already existing
                var temp = RequestService.GetByConnectionIDRTID(connID, rtID);
                if (temp == null)
                {
                    if (RequestService.Insert(id, rfrom, Guid.Parse(rtID), Guid.Parse(connID), false))
                    {
                        SignalRClients.NotifyInvitationSent(connection.ConnectionName, connection.ID.ToString(), rtID);
                        return Json(new { success = true });
                    }
                }
                else if (temp.isArchived) {
                    //if it already exist and is archived then unarchive it
                    RequestService.UpdateStatus(false, temp.ID.ToString());

                    SignalRClients.NotifyInvitationSent(connection.ConnectionName, connection.ID.ToString(), rtID);
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = MessageUtility.ServerError() });
            } catch { return Json(new { success = false, message = MessageUtility.ServerError() }); }
        }

        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> AcceptRequest() {
            try {
                var rid = Request.Form["RID"];
                //connID
                var cid = Request.Form["CID"];
                //recquestTo UID
                var rtID = Request.Form["RTID"];
                //checks if it is already existing 
                var data=ConnectionServices.GetByUIDConnectionID(rtID, cid);
                if (data == null)
                {
                    //create if not existing yet
                    var id = Guid.NewGuid();
                    ConnectionMemberService.InsertMember(id, Guid.Parse(rtID), cid, DateTime.Now, false, false);
                    RequestService.UpdateStatus(true, rid);
                    //signalR notify accepted request is a standalone now if you want to invoke it please  access it via api 
                    return Json(new { success = true });
                }
                else if (data.isArchived)
                {
                    //if data already exist then unarchive it
                    ConnectionMemberService.UpdateMemberStatus(data.ID.ToString(), false);
                    RequestService.UpdateStatus(true, rid);
                    //signalR notify accepted request is a standalone now if you want to invoke it please  access it via api 
                    return Json(new { success = true });
                }
                else if(!data.isArchived){
                    RequestService.UpdateStatus(true, rid);
                    return Json(new { success = false, message = MessageUtility.AlreadyMember() });
                }

                return Json(new { success = false, message=MessageUtility.ServerError() });
            } catch { return Json(new { success = false,message=MessageUtility.ServerError()}); }
        }
        [AllowCrossSiteJson]
        [HttpPost]
        public async Task<JsonResult> CancelRequest() {
            try {
                var rid = Request.Form["RID"];
                RequestService.UpdateStatus(true, rid.ToString());
                return Json(new { success = true });
            } catch { return Json(new { success = false, message = MessageUtility.ServerError() }); }
        }


    }
}