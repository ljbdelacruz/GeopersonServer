using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static GeopersonServer.Models.Geoperson.UsersReviewVM;

namespace GeopersonServer.Services.LocationTracking
{
    public static class LocationSessionService
    {
        public static LocationSession GetByID(Guid id, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.LocationSessionDB where i.ID == id && i.API==api select i).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid id, string name, Guid api) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = LocationSessionVM.Set(id, name, api);
                    context.LocationSessionDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
    }


}