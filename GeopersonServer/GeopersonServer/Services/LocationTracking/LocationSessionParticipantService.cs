using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static GeopersonServer.Models.Geoperson.UsersReviewVM;

namespace GeopersonServer.Services.LocationTracking
{
    public static class LocationSessionParticipantService
    {
        public static bool Insert(Guid id, Guid uid, Guid locationSessionID, bool isAdmin) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = LocationSessionParticipantsVM.Set(id, uid, locationSessionID, isAdmin);
                    context.LocationSessionParticipantsDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.LocationSessionParticipantsDB where i.ID == id select i).FirstOrDefault();
                    context.LocationSessionParticipantsDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<LocationSessionParticipants> GetByLSID(Guid id) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.LocationSessionParticipantsDB where i.LocationSessionID == id select i).ToList();
                return query;
            }
        }
        public static List<LocationSessionParticipants> GetByUserID(Guid id) {
            using (var context = new GeopersonContext())
            {
                var query = (from i in context.LocationSessionParticipantsDB where i.UserID == id select i).ToList();
                return query;
            }
        }


    }
}