using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services
{
    public static class ItemCategoryService
    {
        public static List<ItemCategory> GetAll() {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.ItemCategoryDB where i.isArchived==false select i).ToList();
                return query;
            }
        }
        public static ItemCategory GetByID(Guid ID) {
            using (var context = new GeopersonContext()) {
                var query = (from ic in context.ItemCategoryDB where ic.ID == ID select ic).FirstOrDefault();
                return query;
            }
        }
        public static bool Insert(Guid id, string name) {
            try {
                using (var context = new GeopersonContext()) {
                    var model = ItemCategoryVM.Set(id, name);
                    context.ItemCategoryDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string name, bool archive) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.ItemCategoryDB where i.ID == id select i).FirstOrDefault();
                    query.Name = name;
                    query.isArchived = archive;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

    }
}