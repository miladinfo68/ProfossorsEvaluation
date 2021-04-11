using System.Collections.Generic;
using System.Linq;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service.Sync
{
    public static class CollegeSync
    {
        //دانشکده-Add Or Update
        public static void SyncAddOrUpdateColleges(ICollegeService collegeService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user)
        {
            var colleges = ClientHelper.GetValue<College>(StaticValue.CollegeRelativeAddress);
            //2.log
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_دانشکده_از_سرویس);


            var resualt = new Dictionary<string, int>();
            var counter = 1;
            colleges.ForEach(x =>
            {
                if (x != null)
                {
                    if (x.CollegeCode == null || x.CollegeCode == 0 || string.IsNullOrEmpty(x.Name))
                    {

                        resualt.Add($"{x.Name}-{x.CollegeCode}-{counter}", 4);

                    }
                    var r = collegeService.AddOrUpdate(x);
                    resualt.Add($"{x.Name}-{x.CollegeCode}-{counter}", r);
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
            foreach (var s in resualt.Where(x => x.Value == 3).Select(x => x.Key))
            {
                stringNotFounded += $"تعداد {notFounded}" + " || " + s + " | ";
            }

            stringWarrning = !string.IsNullOrEmpty(stringWarrning.Trim()) ? stringWarrning : "بدون مشکل";
            stringNotFounded = !string.IsNullOrEmpty(stringNotFounded.Trim()) ? stringNotFounded : "بدون مشکل";

            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دانشکده_اضافه_گردید, $"تعداد {added}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دانشکده_آپدیت_گردید, $"تعداد {updatetd}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_در_بروزرسانی_دانشکده, stringWarrning);
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_به_دلیل_عدم_وجود_حداقل_یک_مقدار_از_دانشکده, stringNotFounded);


            //return resualt;
        }

        //دانشکده-Remove
        public static Dictionary<string, string> SyncRemoveColleges(ICollegeService collegeService)
        {
            var colleges = ClientHelper.GetValue<College>(StaticValue.CollegeRelativeAddress);

            var resualt = new Dictionary<string, string>();
            var counter = 1;
            colleges.ForEach(x =>
            {
                if (x != null)
                {
                    var r = collegeService.Remove(x);
                    resualt.Add($"{x.Name}-{counter}", r.GetDeleteResualt());
                    ++counter;
                }
            });
            return resualt;
        }

        public static void Run(ICollegeService collegeService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user)
        {
            //The order of the execution on the commands is important
            //First-remove
            //Second-addOrUpdate

            //1.log
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_سینک_دانشکده);


            //var remove = SyncRemoveColleges(collegeService);
            SyncAddOrUpdateColleges(collegeService, logService, logTypeService, userService, user);

            //var resualt = new ReturnValue
            //{
            //    //Remove = remove,
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
