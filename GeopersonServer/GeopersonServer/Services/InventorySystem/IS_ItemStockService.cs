using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.InventorySystem
{
    public static class IS_ItemStockService
    {
        public static bool Insert(Guid id, string bcode, Guid itemID, Guid status){
            try {
                using (var context = new GeopersonContext()) {
                    var model = IS_ItemStockVM.set(id, bcode, itemID, status);
                    context.IS_ItemStockDB.Add(model);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static bool Remove(Guid id, Guid itemID) {
            try {
                using (var context = new GeopersonContext()){
                    var query = (from i in context.IS_ItemStockDB where i.ID == id && i.IS_ItemID == itemID select i).FirstOrDefault();
                    context.IS_ItemStockDB.Remove(query);
                    context.SaveChanges();
                    return true;
                }
            }catch { return false; }
        }
        public static bool UpdateStatus(Guid id, Guid IS_iID, Guid status) {
            try {
                using (var context = new GeopersonContext()) {
                    var query = (from i in context.IS_ItemStockDB where i.ID == id && i.IS_ItemID == IS_iID select i).FirstOrDefault();
                    query.IS_ItemStockStatus = status;
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }

        public static List<IS_ItemStock> GetByItemID(Guid id, Guid status) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.IS_ItemStockDB where i.IS_ItemID == id && i.IS_ItemStockStatus == status select i).ToList();
                return query;
            }
        }
        public static IS_ItemStock GetByID(Guid id, Guid itemID, Guid status) {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.IS_ItemStockDB where i.ID == id && i.IS_ItemID == itemID && i.IS_ItemStockStatus==status select i).FirstOrDefault();
                return query;
            }
        }


    }
}