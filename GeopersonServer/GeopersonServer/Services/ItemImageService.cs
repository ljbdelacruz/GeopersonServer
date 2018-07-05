using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public static class ItemImageService
    {
        public static List<ItemImage> GetByItemsID(Guid ID) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.ItemImageDB where i.Item.ID == ID select i).ToList();
                return query;
            }
        }
        public static ItemImage GetByID(Guid ID) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.ItemImageDB where i.ID == ID select i).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid id, string source, Guid itemsID) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = ItemImageVM.Set(id, source, context.ItemsDB.Where(x => x.ID == itemsID).FirstOrDefault());
                    context.ItemImageDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid ID) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.ItemImageDB where i.ID == ID select i).FirstOrDefault();
                    context.ItemImageDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}