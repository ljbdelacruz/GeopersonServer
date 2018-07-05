using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static GeopersonServer.Models.Geoperson.UsersReviewVM;

namespace GeopersonServer.Services.LocationTracking
{
    public static class LocationSessionInvitationService
    {
        public static bool Insert(Guid id, Guid lsID, Guid uid, Guid api) {
            try {
                using (var context = new GeopersonContext()){
                    var model = LocationSessionInvitationVM.Set(id, lsID, uid, api);
                    context.LocationSessionInvitationDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.LocationSessionInvitationDB where i.ID == id select i).FirstOrDefault();
                    context.LocationSessionInvitationDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<LocationSessionInvitation> GetByUserID(Guid id, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.LocationSessionInvitationDB where i.UserID == id && i.API == api select i).ToList();
                return query;
            }
        }
    }
}