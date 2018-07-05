using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public static class ItemService
    {
        public static List<Items> GetByMostViewedItem(int takeCount, float longitude, float latitude) {
            using (var context = new GeopersonContext()) {
                var query = (from c in context.ItemsDB
                             where (c.longitude <= longitude || c.longitude >= -longitude) && (c.latitude <= latitude || c.latitude >= -latitude) && c.isArchived == false
                             orderby c.TimesViewed descending
                             select c).ToList().Take(takeCount).ToList();

                return query;
            }
        }

        public static Items GetByID(Guid ID, Guid api, bool isArchived) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.ItemsDB where i.ID == ID && i.API==api && i.isArchived==isArchived select i).FirstOrDefault();
                return query;
            }
        }
        public static List<Items> GetByOwnerID(Guid ID, bool isArchived) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.ItemsDB where i.OwnerID == ID && i.isArchived==isArchived select i).ToList();
                return query;
            }
        }
        //get location of items by radius
        public static List<Items> GetByLocationRadius(float longitude, float latitude) {
            using (var context = new GeopersonContext()) {
                var query = (from c in context.ItemsDB where (c.longitude <= longitude || c.longitude >= -longitude) && (c.latitude <= latitude || c.latitude >= -latitude)
                             && c.isArchived==false
                             select c).ToList();
                return query;
            }
        }
        public static bool Insert(Guid id, string title, string description, float price, Guid OwnerID, Guid CategoryID, float longitude, float latitude, bool isArchived, Guid API, DateTime createdAt, DateTime updatedAt, int ptype) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = ItemsViewModel.Set(id, title, description, price, OwnerID, CategoryID, longitude, latitude, isArchived, API, createdAt, updatedAt, ptype);
                    context.ItemsDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string title, string description, Guid categoryID, bool isArchived, float price, int postType) {
            try {
                using (var context = new GeopersonContext()) {
                    var data = (from i in context.ItemsDB where i.ID == id select i).FirstOrDefault();
                    data.Title = title;
                    data.Price = price;
                    data.PostType = postType;
                    data.Description = description;
                    data.isArchived = isArchived;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Archive(Guid ID, bool isArchived) {
            try {
                using (var context = new GeopersonContext())
                {
                    var query = (from i in context.ItemsDB where i.ID == ID select i).FirstOrDefault();
                    query.isArchived = isArchived;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdateTimesViewed(Guid id, int addon) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.ItemsDB where i.ID == id select i).FirstOrDefault();
                    query.TimesViewed = query.TimesViewed+addon;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
    }
}