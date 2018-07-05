using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static GeopersonServer.Models.Geoperson.UsersReviewVM;

namespace GeopersonServer.Services.LocationTracking
{
    public static class LocationStorageService
    {
        
        public static bool Insert(Guid id, string ownerID, Guid api, float longitude, float latitude, string lc, string desc) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = LocationStorageVM.Set(id, ownerID, api, longitude, latitude, lc, desc);
                    context.LocationStorageDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdateLocation(string ownerID, Guid api, float longitude, float latitude) {
            try {
                using(var context=new GeopersonContext()){
                    var query = (from i in context.LocationStorageDB where i.OwnerID.Equals(ownerID) && i.API == api select i).FirstOrDefault();
                    query.Longitude = longitude;
                    query.Latitude = latitude;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static LocationStorage GetByOwnerIDAPI(string oid, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.LocationStorageDB where i.OwnerID.Equals(oid) && i.API == api select i).FirstOrDefault();
                return query;
            }
        }
        public static LocationStorage GetByIDAPI(Guid id, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.LocationStorageDB where i.ID == id && i.API == api select i).FirstOrDefault();
                return query;
            }
        }
        public static List<LocationStorage> GetByCategory(string cat, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.LocationStorageDB where i.LocationCategory.Equals(cat) && i.API == api select i).ToList();
                return query;
            }
        }
        public static LocationStorage GetByOwnerIDCat(string id, string cat) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.LocationStorageDB where i.LocationCategory.Equals(cat) && i.OwnerID.Equals(id) select i).FirstOrDefault();
                return query;
            }
        }

        public static List<LocationStorage> GetByCategoryLocationRadius(float longitude, float latitude, string category, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.LocationStorageDB where (i.Longitude <= longitude || i.Longitude >= -longitude) && (i.Latitude <= latitude || i.Latitude >= -latitude)
                             && i.LocationCategory.Equals(category) && i.API == api select i).ToList();
                return query;
            }
        }
        public static List<LocationStorage> GetByContainCategoryLocationRadius(float longitude, float latitude, string cat, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.LocationStorageDB
                             where i.LocationCategory.Contains(cat) && (i.Longitude <= longitude || i.Longitude >= -longitude) && (i.Latitude <= latitude || i.Latitude >= -latitude) && i.API == api
                             select i).ToList();
                return query;
            }
        }

    }
}