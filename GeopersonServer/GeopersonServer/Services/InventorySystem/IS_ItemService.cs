using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.InventorySystem
{
    public static class IS_ItemService
    {
        public static bool Insert(Guid id, string title, string description, Guid api, Guid catID, bool isCount, int quantity, Guid storeAPI) {
            try {
                using (var context = new GeopersonContext()){
                    var model = IS_ItemVM.Set(id, title, description, api, catID, isCount, quantity, storeAPI);
                    context.IS_ItemDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid api) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.IS_ItemDB where i.ID == id && i.API == api select i).FirstOrDefault();
                    context.IS_ItemDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Update(Guid id, string title, string description, Guid api) {
            try {
                using(var context=new GeopersonContext())
                {
                    var query = (from i in context.IS_ItemDB where i.ID == id && i.API == api select i).FirstOrDefault();
                    query.Title = title;
                    query.Description = description;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static List<IS_Item> GetByAPI(Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.IS_ItemDB where i.API == api select i).ToList();
                return query;
            }
        }
        public static List<IS_Item> GetByItemCategory(Guid api, Guid category) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.IS_ItemDB where i.API == api && i.IS_ItemCategoryID == category select i).ToList();
                return query;
            }
        }
        public static IS_Item GetByID(Guid id, Guid api) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.IS_ItemDB where i.ID == id && i.API == api select i).FirstOrDefault();
                return query;
            }
        }


    }
}