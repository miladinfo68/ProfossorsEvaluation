using System.Collections.Generic;
using System.Linq;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service.Sync
{
    public static class EducationalClassSync
    {
        //کلاس-Add Or Update
        public static void SyncAddOrUpdateEducationalClass(IEducationalClassService educationalClassService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user,string termCode)
        {
            var educationalClasses = ClientHelper.GetValue<EducationalClassSyncModel>(StaticValue.EducationalClassRelativeAddress + $"/{termCode}");

            //log get From service
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_کلاس_از_سرویس);

            var addOrUpdateResualt = new Dictionary<string, int>();
            var counter = 1;
            var preventedContentType = new List<int?> { 10, 11, 24, 7, 6, 49, 50, 38 };
            var preventedHoldingType = new List<decimal?> { 5, 7 };
            if (educationalClasses != null)
            {
                var exceptions = educationalClasses.Where(w => preventedContentType.Contains(w.ContentType) ||
                preventedHoldingType.Contains(w.HoldingType) || w.HoldingExamDate == null);
                educationalClasses.Except(exceptions).ToList().ForEach(x =>
                {
                    if (x.ContentType != null && !preventedContentType.Contains(x.ContentType)
                    && x.HoldingExamDate != null
                    && x.HoldingType != null && !preventedHoldingType.Contains(x.HoldingType))
                    {
                        if (x != null)
                            if (HasNullProperty(x))
                            {
                                addOrUpdateResualt.Add($"{x.GroupId}-{x.CodeClass}-{x.Name}-{counter}", 4);

                            }
                            else
                            {
                                var r = educationalClassService.AddOrUpdate(x);
                                addOrUpdateResualt.Add($"{x.GroupId}-{x.CodeClass}-{x.Name}-{counter}", r);
                            }
                        ++counter;
                    }
                });
            }
            var added = addOrUpdateResualt.Count(x => x.Value == 1);
            var updatetd = addOrUpdateResualt.Count(x => x.Value == 2);
            var warrning = addOrUpdateResualt.Count(x => x.Value == 3);
            var notFounded = addOrUpdateResualt.Count(x => x.Value == 4);
            var stringWarrning = string.Empty;
            var stringNotFounded = string.Empty;

            foreach (var s in addOrUpdateResualt.Where(x => x.Value == 3).Select(x => x.Key))
            {
                stringWarrning = $"تعداد {warrning}" + " || " + s + " | ";
            }
            foreach (var s in addOrUpdateResualt.Where(x => x.Value == 4).Select(x => x.Key))
            {
                stringNotFounded = $"تعداد {notFounded}" + " || " + s + " | ";
            }

            stringWarrning = !string.IsNullOrEmpty(stringWarrning.Trim()) ? stringWarrning : "بدون مشکل";
            stringNotFounded = !string.IsNullOrEmpty(stringNotFounded.Trim()) ? stringNotFounded : "بدون مشکل";

            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.کلاس_اضافه_گردید, $"تعداد {added}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.کلاس_آپدیت_گردید, $"تعداد {updatetd}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_در_بروزرسانی_کلاس, stringWarrning);
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_به_دلیل_عدم_وجود_حداقل_یک_مقدار_از_کلاس, stringNotFounded);

            //return result;
        }

        private static bool HasNullProperty(EducationalClassSyncModel x)
        {
            return x.CodeClass == null || x.CodeClass == 0 || string.IsNullOrEmpty(x.Term) || x.GroupId == null
                                || x.GroupId == 0 || x.ProfessorId == null || x.ProfessorId == 0;
        }


        //کلاس-Remove
        public static Dictionary<string, string> SyncRemoveEducationalClass(IEducationalClassService educationalClassService)
        {
            var educationalClasses = ClientHelper.GetValue<EducationalClassSyncModel>(StaticValue.EducationalClassRelativeAddress);
            var result = new Dictionary<string, string>();
            var counter = 1;
            educationalClasses.ForEach(x =>
            {
                if (x != null)
                {
                    if (educationalClassService.IsExist(y => y.CodeClass == x.CodeClass && y.Term.TermCode == x.Term))
                    {


                        var r = educationalClassService.Remove(x);
                        result.Add($"{x.Name}-{counter}", r.GetDeleteResualt());
                        ++counter;
                    }
                    else
                    {
                        result.Add($"{x.Name}-{counter}", 0.GetDeleteResualt());
                        ++counter;
                    }
                }
            });
            return result;
        }

        public static void Run(IEducationalClassService educationalClassService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user,string termCode)
        {
            //The order of the execution on the commands is important
            //First-remove
            //Second-addOrUpdate

            //log start
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_سینک_کلاس);


            // var remove = SyncRemoveEducationalClass(educationalClassService);
             SyncAddOrUpdateEducationalClass(educationalClassService, logService, logTypeService, userService, user,termCode);

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