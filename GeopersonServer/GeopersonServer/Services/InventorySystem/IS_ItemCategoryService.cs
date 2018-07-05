using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.InventorySystem
{
    public static class IS_ItemCategoryService
    {
        public static bool Insert(Guid id, string name, Guid api) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = Models.Geoperson.IS_ItemCategoryVM.Set(id, name, api);
                    context.IS_ItemCategoryDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid api) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.IS_ItemCategoryDB where i.ID == id && i.API == api select i).FirstOrDefault();
                    context.IS_ItemCategoryDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string name, Guid api) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.IS_ItemCategoryDB where i.ID == id && i.API == api select i).FirstOrDefault();
                    query.Name = name;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<IS_ItemCategory> GetByAPI(Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.IS_ItemCategoryDB where i.API == api select i).ToList();
                return query;
            }
        }
        public static IS_ItemCategory GetByID(Guid id, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.IS_ItemCategoryDB where i.ID == id && i.API == api select i).FirstOrDefault();
                return query;
            }
        }

    }
}