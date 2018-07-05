using GeopersonServer.Context;
using GeopersonServer.Models.Geoperson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeopersonServer.Services.StatusTypeReference
{
    public static class StatusTypeReferenceService
    {
        public static bool Insert(Guid id, string name, string description) {
            try {
                using (var context = new GeopersonContext()){
                    var data = StatusTypesReferencesVM.Set(id, name, description);
                    context.StatusTypesReferencesDB.Add(data);
                    context.SaveChanges();
                    return true;
                }
            } catch { return false; }
        }
        public static StatusTypesReferences GetByID(Guid id) {
            using(var context=new GeopersonContext())
            {
                var query = (from i in context.StatusTypesReferencesDB where i.ID == id select i).FirstOrDefault();
                return query;
            }
        }
        public static List<StatusTypesReferences> GetByList() {
            using (var context = new GeopersonContext()) {
                var query = (from i in context.StatusTypesReferencesDB select i).ToList();
                return query;
            }
        }


    }
}