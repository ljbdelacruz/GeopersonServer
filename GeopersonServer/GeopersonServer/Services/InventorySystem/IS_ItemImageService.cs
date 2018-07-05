using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.InventorySystem
{
    public static class IS_ItemImageService
    {
        public static bool Insert(Guid id, string source, Guid itemID) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = IS_ItemImagesVM.Set(id, source, itemID);
                    context.IS_ItemImagesDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid itemID) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.IS_ItemImagesDB where i.ID == id && i.IS_ItemID == itemID select i).FirstOrDefault();
                    context.IS_ItemImagesDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string source, Guid itemID) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.IS_ItemImagesDB where i.ID == id && i.IS_ItemID == itemID select i).FirstOrDefault();
                    query.Source = source;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<IS_ItemImages> GetByItemID(Guid itemID) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.IS_ItemImagesDB where i.IS_ItemID == itemID select i).ToList();
                return query;
            }
        }
        public static IS_ItemImages GetByID(Guid id, Guid itemID) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.IS_ItemImagesDB where i.ID == id && i.IS_ItemID == itemID select i).FirstOrDefault();
                return query;
            }
        }
    }
}