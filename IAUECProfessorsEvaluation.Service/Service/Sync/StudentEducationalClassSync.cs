using System.Collections.Generic;
using System.Linq;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service.Sync
{
    public static class StudentEducationalClassSync
    {
        //دانشجو-کلاس-Add Or Update
        public static void SyncAddOrUpdateStudentEducationalClass(
            IStudentEducationalClassService studentEducationalClassService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user,string termCode)
        {
            var studentEducationalClasses =
                ClientHelper.GetValue<StudentEducationalClassSyncModel>(StaticValue.StudentEducationalClass + $"/{termCode}");
            //log get From service
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_دانشجو_کلاس_از_سرویس);


            var addOrUpdateResualt = new Dictionary<string, int>();
            var counter = 1;
            studentEducationalClasses.ForEach(x =>
            {
                if (x != null)
                    if (HasNullValue(x))
                    {
                        addOrUpdateResualt.Add($"{x.EducationalClassId}-{x.StudentId}-{counter}", 4);
                    }
                    else
                    {
                        var r = studentEducationalClassService.AddOrUpdate(x);
                        addOrUpdateResualt.Add($"{x.EducationalClassId}-{x.StudentId}-{counter}", r);
                    }
                ++counter;

            });

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

            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دانشجو_کلاس_اضافه_گردید, $"تعداد {added}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دانشجو_کلاس_آپدیت_گردید, $"تعداد {updatetd}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_در_بروزرسانی_دانشجو_کلاس, stringWarrning);
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_به_دلیل_عدم_وجود_حداقل_یک_مقدار_از_دانشجو_کلاس, stringNotFounded);


            // return addOrUpdateResualt;
        }

        private static bool HasNullValue(StudentEducationalClassSyncModel x)
        {
            return x.EducationalClassId == null || x.EducationalClassId == 0 || x.StudentId == null || x.StudentId == 0 || string.IsNullOrEmpty(x.Term);
        }

        //دانشجو-کلاس-Remove
        public static Dictionary<string, string> SyncRemoveStudentEducationalClass(
            IStudentEducationalClassService studentEducationalClassService)
        {
            var studentEducationalClasses =
                ClientHelper.GetValue<StudentEducationalClassSyncModel>(StaticValue.StudentEducationalClass);
            var addOrUpdateResualt = new Dictionary<string, string>();
            var counter = 1;
            studentEducationalClasses.ForEach(x =>
            {
                if (x != null)
                {
                    if (studentEducationalClassService.IsExist(y => y.Term.TermCode == x.Term && y.EducationalClass.CodeClass == x.EducationalClassId && y.StudentId == x.StudentId))
                    {


                        var r = studentEducationalClassService.Remove(x);

                        addOrUpdateResualt.Add($"{x.EducationalClassId}-{x.StudentId}-{counter}", r.GetDeleteResualt());
                        ++counter;
                    }
                    else
                    {

                        addOrUpdateResualt.Add($"{x.EducationalClassId}-{x.StudentId}-{counter}", 0.GetDeleteResualt());
                        ++counter;

                    }
                }
            });
            return addOrUpdateResualt;
        }


        public static void Run(IStudentEducationalClassService studentEducationalClassService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user,string termCode)
        {
            //The order of the execution on the commands is important
            //First-remove
            //Second-addOrUpdate


            //log start
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_سینک_دانشجو_کلاس);


            //var remove = SyncRemoveStudentEducationalClass(studentEducationalClassService);

            SyncAddOrUpdateStudentEducationalClass(studentEducationalClassService, logService, logTypeService, userService, user,termCode);

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