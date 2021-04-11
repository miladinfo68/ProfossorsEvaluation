using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service.Sync
{
    public static class TermSync
    {
        //ترم-Add Or Update
        public static void SyncAddOrUpdateTerms(ITermService termService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user)
        {

            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_سینک_ترم);

            var terms = new List<Term>();
            try
            {
                terms = ClientHelper.GetValue<Term>(StaticValue.TermRelativeAddress);
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_ترم_از_سرویس);

            }
            catch (Exception e)
            {
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عدم_دریافت_ترم_از_سرویس);
            }


            var resualt = new Dictionary<string, int>();
            var counter = 1;
            //SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_اضافه_و_آپدیت_نمودن_ترم);

            terms.ForEach(x =>
            {
                if (x != null)
                {
                    if (string.IsNullOrEmpty(x.TermCode))
                        resualt.Add($"{x.Name}-{counter}", 3);
                    else
                    {
                        var r = termService.AddOrUpdate(x);

                        // resualt.Add($"{x.Name}-{counter}", r.GetAddOrUpdateResualt());
                        resualt.Add($"{x.Name}-{counter}", r);
                    }
                }
                ++counter;

            });
            var added = resualt.Count(x => x.Value == 1);
            var updatetd = resualt.Count(x => x.Value == 2);
            var warrning = resualt.Count(x => x.Value == 3);
            var stringWarrning = string.Empty;

            foreach (var s in resualt.Where(x => x.Value == 3).Select(x => x.Key))
            {
                stringWarrning += $"تعداد {warrning}" + " || " + s + " | ";
            }
            stringWarrning = !string.IsNullOrEmpty(stringWarrning.Trim()) ? stringWarrning : "بدون مشکل";
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.ترم_اضافه_شد, $"تعداد {added}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.ترم_آپدیت_شد, $"تعداد {updatetd}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.مشکل_در_اضافه_و_آپدیت_کردن_ترم, stringWarrning);


            //SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.پایان_اضافه_و_آپدیت_نمودن_ترم);

            // return resualt;
        }

    }
}
