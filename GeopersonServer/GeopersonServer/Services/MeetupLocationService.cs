using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public class MeetupLocationService
    {
        public static List<MeetupLocation> GetByConnectionID(string ID, bool isEnabled){
            using (var context = new GeopersonContext()) {
                var data = (from ml in context.MeetupLocationDB
                            where ml.Connection.ID.ToString().Equals(ID) && ml.isEnabled== isEnabled
                            select new { ml.ID, ml.Connection, ml.isEnabled, ml.latitude, ml.longitude, ml.UpdatedBy, ml.Note }).ToList();
                var models = new List<MeetupLocation>();
                foreach (var model in data) {
                    models.Add(new MeetupLocation()
                    {
                        ID = model.ID,
                        Connection = model.Connection,
                        isEnabled = model.isEnabled,
                        latitude = model.latitude,
                        longitude = model.longitude,
                        UpdatedBy = model.UpdatedBy,
                        Note=model.Note
                    });
                }
                return models;
            }
        }
        public static MeetupLocation GetByID(string ID) {
            using (var context = new GeopersonContext()) {
                var data = (from ml in context.MeetupLocationDB
                            where ml.ID.ToString().Equals(ID)
                            select new { ml.ID, ml.Connection, ml.isEnabled, ml.latitude, ml.longitude, ml.UpdatedBy }).FirstOrDefault();
                return new MeetupLocation() {
                    ID = data.ID,
                    Connection = data.Connection,
                    isEnabled = data.isEnabled,
                    latitude = data.latitude,
                    longitude = data.longitude,
                    UpdatedBy = data.UpdatedBy
                };
            }
        }
        public static bool Insert(Guid id, Guid connectionID, bool isEnabled, float latitude, float longitude, Guid updatedBy, string note) {
            try {
                using (var context = new GeopersonContext())
                {
                    var model = new MeetupLocation()
                    {
                        ID = id,
                        Connection = (from c in context.ConnectionsDB where c.ID==connectionID select c).FirstOrDefault(),
                        isEnabled = isEnabled,
                        latitude = latitude,
                        longitude = longitude,
                        UpdatedBy = updatedBy,
                        Note=note
                    };
                    context.MeetupLocationDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(string ID, bool isEnabled, Guid UpdatedBy, float lat, float longi, string note) {
            try {
                using (var context = new GeopersonContext()) {
                    var data = (from ml in context.MeetupLocationDB where ml.ID.ToString().Equals(ID) select ml).FirstOrDefault();
                    data.Note = note;
                    data.isEnabled = isEnabled;
                    data.UpdatedBy = UpdatedBy;
                    data.latitude = lat;
                    data.longitude = longi;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}