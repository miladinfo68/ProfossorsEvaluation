using System;
using System.Collections.Generic;
using System.Linq;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service.Sync
{
    public static class MappingSync
    {
        //مپینگ-AddOrUpdate
        public static void SyncAddOrUpdateMappings(IMappingService mappingService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user)
        {

            var mappings = ClientHelper.GetValue<MappingSyncModel>(StaticValue.MappingRelativeAddress);
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_مپینگ_از_سرویس);

            var resualt = new Dictionary<string, int>();

            var counter = 1;


            mappings.ForEach(x =>
            {
                if (x != null)
                {
                    if (x.TypeId == 0 || x.MappingTypeId == 0 || string.IsNullOrEmpty(x.TypeName))
                    {
                        resualt.Add($"{counter}- کد نوع نگاشت:{x.MappingTypeId}-{x.TypeId}-{x.TypeName}", 4);
                    }
                    else
                    {
                        var r = mappingService.AddOrUpdate(x);
                        resualt.Add($"{counter}- کد نوع نگاشت:{x.MappingTypeId}-{x.TypeId}-{x.TypeName}", r);
                    }
                }
                ++counter;

            });




            var added = resualt.Count(x => x.Value == 1);
            var updatetd = resualt.Count(x => x.Value == 2);
            var warrning = resualt.Count(x => x.Value == 3);
            var notFounded = resualt.Count(x => x.Value == 4);
            var stringWarrning = string.Empty;
            var stringNotFounded = string.Empty;

            foreach (var s in resualt.Where(x => x.Value == 3).Select(x => x.Key))
            {
                stringWarrning += $"تعداد {warrning}" + " || " + s + " | ";
            }
            foreach (var s in resualt.Where(x => x.Value == 4).Select(x => x.Key))
            {
                stringNotFounded += $"تعداد {notFounded}" + " || " + s + " | ";
            }

            stringWarrning = !string.IsNullOrEmpty(stringWarrning.Trim()) ? stringWarrning : "بدون مشکل";
            stringNotFounded = !string.IsNullOrEmpty(stringNotFounded.Trim()) ? stringNotFounded : "بدون مشکل";

            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.مپینگ_اضافه_گردید, $"تعداد {added}");
            //SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.مپینگ_آپدیت_گردید, $"تعداد {updatetd}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.مشکل_در_اضافه_و_آپدیت_کردن_مپینگ, stringWarrning);
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.یکی_از_مقادیر_نگاشت_موجود_نمی, stringNotFounded);




            // return result;
        }

        //مپینگ-Remove
        public static Dictionary<string, string> SyncRemoveMappings(IMappingService mappingService)
        {

            var mappings = ClientHelper.GetValue<MappingSyncModel>(StaticValue.MappingRelativeAddress);
            var result = new Dictionary<string, string>();

            var counter = 1;
            mappings.ForEach(x =>
            {
                if (x != null)
                {
                    var r = mappingService.Remove(x);
                    result.Add($"{counter}- کد نوع نگاشت:{x.MappingTypeId}-{x.TypeName}", r.GetDeleteResualt());
                    ++counter;
                }
            });
            return result;
        }

        //مپینگ-RemoveAll

        public static void SyncRemoveAllMappings(IMappingService mappingService)
        {

            mappingService.DeleteMany(x => true);


            //var r = new Dictionary<string, string> {{"نگاشت", "تمامی نگاشت ها با موفقیت حذف گردید"}};
            //return r;
        }
        public static void Run(IMappingService mappingService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user)
        {
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_سینک_مپینگ);

            //The order of the execution on the commands is important
            //First-remove
            //Second-addOrUpdate

            try
            {
                SyncRemoveAllMappings(mappingService);
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.حذف_مپینگ_ها);

            }
            catch (Exception e)
            {
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.مشکل_در_حذف_مپینگ_ها);

            }

            // SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_اضافه_و_آپدیت_نمودن_مپینگ);

            SyncAddOrUpdateMappings(mappingService, logService, logTypeService, userService, user);

            //  SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.پایان_اضافه_و_آپدیت_نمودن_مپینگ);



            //var resualt = new ReturnValue
            //{
            //    Remove = remove,
            //    AddOrUpdate = addOrUpdate
            //};
            //return resualt;
        }

        public class ReturnValue
        {
            public Dictionary<string, string> Remove { get; set; }
            public Dictionary<string, string> AddOrUpdate { get; set; }
        }

    }
}