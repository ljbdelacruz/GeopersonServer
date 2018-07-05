using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.BuyAndSellFeatures
{
    public static class ItemsImagesService
    {
        public static bool Insert(Guid id, Guid iid, Guid ilid) {
            try {
                var data = ItemsImagesVM.set(id, iid, ilid);
                using (var context = new GeopersonContext()) {
                    context.ItemsImagesDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid ilid, Guid iid) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.ItemsImagesDB where i.ImageLinkStorageID == ilid && i.ItemID == iid select i).FirstOrDefault();
                    context.ItemsImagesDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<ItemsImages> GetByItemID(Guid id) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.ItemsImagesDB where i.ItemID == id select i).ToList();
                return query;
            }
        }

    }
}