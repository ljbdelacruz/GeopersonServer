using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public static class ItemAssignCategoryService
    {
        public static List<ItemAssignCategory> GetByItemID(Guid id) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.ItemAssignCategoryDB where i.ItemID == id select i).ToList();
                return query;
            }
        }
        public static bool Insert(Guid id, Guid itemID, Guid subCatID) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = ItemAssignCategoryVM.Set(id, itemID, subCatID);
                    context.ItemAssignCategoryDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.ItemAssignCategoryDB where i.ID == id select i).FirstOrDefault();
                    context.ItemAssignCategoryDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool UpdateByItemID(Guid id, Guid subCat) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.ItemAssignCategoryDB where i.ItemID == id select i).FirstOrDefault();
                    query.SubCategory = subCat;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}