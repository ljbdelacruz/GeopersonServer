using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public static class ItemSubCategoryService
    {
        public static ItemSubCategory GetByID(Guid id) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.ItemSubCategoryDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static List<ItemSubCategory> GetByCategoryID(Guid catID) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.ItemSubCategoryDB where i.Category == catID && i.isArchived==false select i).ToList();
                return query;
            }
        }
        public static bool Insert(Guid id, string name, Guid catID) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = ItemSubCategoryVM.Set(id, name, catID, false);
                    context.ItemSubCategoryDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string name, bool isArchived) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.ItemSubCategoryDB where i.ID == id select i).FirstOrDefault();
                    query.Name = name;
                    query.isArchived = isArchived;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }


    }
}