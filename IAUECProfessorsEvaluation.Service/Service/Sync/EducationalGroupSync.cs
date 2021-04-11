using System;
using System.Collections.Generic;
using System.Linq;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service.Sync
{
    public class EducationalGroupSync
    {
        //گروه-Remove Score
        public static void SyncEducationalGroupRemoveScore(
            IEducationalGroupService grp
            , ILogService logService, ILogTypeService logTypeService, IUserService userService, User user, string termCode)
        {
            //log start
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_عملیات_حذف_امتیازات_گروه);

            var groups = ClientHelper.GetValue<GroupSyncModel>(StaticValue.GroupRelativeAddress + $"/{termCode}");

            //log get From service
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_گروه_از_سرویس);

            var removeResualt = new Dictionary<string, int>();
            var counter = 1;
            groups.ForEach(x =>
            {
                if (x != null)
                {
                    var group = grp.Get(y => y.EducationalGroupCode == x.EducationalGroupCode
                                             && y.Term.TermCode == x.Term);
                    if (group != null)
                    {
                        if (x.EducationalGroupCode == null || x.EducationalGroupCode == 0 ||
                            string.IsNullOrEmpty(x.Term))
                        {
                            removeResualt.Add($"{x.EducationalGroupCode}-{x.Term}-{x.Name}-{counter}", -2000);
                        }
                        else
                        {
                            var gr = grp.RemoveGroupScore(x.EducationalGroupCode, x.Term);
                            removeResualt.Add($"{x.EducationalGroupCode}-{x.Term}-{x.Name}-{counter}", gr);
                        }
                    }
                    else
                    {
                        removeResualt.Add($"{x.EducationalGroupCode}-{x.Term}-{x.Name}-{counter}", -3000);

                    }
                }



                ++counter;
            });


            var removed = removeResualt.Count(x => x.Value == 1);
            var warrning = removeResualt.Count(x => x.Value == -3000);
            var notFounded = removeResualt.Count(x => x.Value == -2000);
            var stringWarrning = string.Empty;
            var stringNotFounded = string.Empty;

            foreach (var s in removeResualt.Where(x => x.Value == -3000).Select(x => x.Key))
            {
                stringWarrning = $"تعداد {warrning}" + " || " + s + " | ";
            }
            foreach (var s in removeResualt.Where(x => x.Value == -2000).Select(x => x.Key))
            {
                stringNotFounded = $"تعداد {notFounded}" + " || " + s + " | ";
            }

            stringWarrning = !string.IsNullOrEmpty(stringWarrning.Trim()) ? stringWarrning : "بدون مشکل";
            stringNotFounded = !string.IsNullOrEmpty(stringNotFounded.Trim()) ? stringNotFounded : "بدون مشکل";

            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.حذف_امتیازات_گروه, $"تعداد {removed}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_به_دلیل_عدم_وجود_گروه, stringWarrning);
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_به_دلیل_عدم_وجود_حداقل_یک_مقدار_از_فیلد_های_گروه, stringNotFounded);

            //return removeResualt;
        }

        //گروه-Add Score
        public static void SyncEducationalGroupAddScore(
            IEducationalGroupService grp,
            IEducationalGroupScoreService grpSrc,
            IIndicatorService ind,
            ITermService term,
            IProfessorService professorService,
            IProfessorScoreService professorScoreService,
            IEducationalClassService educationalClassService
            , ILogService logService, ILogTypeService logTypeService, IUserService userService, User user, string termCode)
        {
            //log start
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_عملیات_ثبت_امتیازات_گروه);

            var groups = ClientHelper.GetValue<GroupSyncModel>(StaticValue.GroupRelativeAddress + $"/{termCode}");

            //log get From service
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_گروه_از_سرویس);


            // var addScoreResualt = new Dictionary<string, List<KeyValuePair<string, string>>>();
            groups.ForEach(x =>
            {
                if (x != null)
                {
                    AddGroupScore(grp, grpSrc, ind, term, professorService, professorScoreService, x, educationalClassService
                      , logService, logTypeService, userService, user);
                    //addScoreResualt.Add($"{x.Name}-{counter}", resualt);
                }
            });
            //return addScoreResualt;
        }

        //گروه -Add Or Update
        public static void SyncAddOrUpdateEducationalGroup(
            IEducationalGroupService grp, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user, string termCode)
        {

            try
            {
                //log start
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_سینک_گروه);


                var groups = ClientHelper.GetValue<GroupSyncModel>(StaticValue.GroupRelativeAddress + $"/{termCode}");

                //log get From service
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_گروه_از_سرویس);



                var addOrUpdateResualt = new Dictionary<string, int>();
                var counter = 1;
                groups.ForEach(x =>
                {
                    if (x != null)
                        if (HasNullProperty(x))
                        {
                            addOrUpdateResualt.Add($"{x.EducationalGroupCode}-{x.Name}-{counter}", 4);
                        }
                        else
                        {
                            var g = grp.AddOrUpdate(x);
                            addOrUpdateResualt.Add($"{x.EducationalGroupCode}-{x.Name}-{counter}", g);
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
                foreach (var s in addOrUpdateResualt.Where(x => x.Value == 3).Select(x => x.Key))
                {
                    stringNotFounded = $"تعداد {notFounded}" + " || " + s + " | ";
                }

                stringWarrning = !string.IsNullOrEmpty(stringWarrning.Trim()) ? stringWarrning : "بدون مشکل";
                stringNotFounded = !string.IsNullOrEmpty(stringNotFounded.Trim()) ? stringNotFounded : "بدون مشکل";

                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.گروه_اضافه_گردید, $"تعداد {added}");
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.گروه_آپدیت_گردید, $"تعداد {updatetd}");
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_در_بروزرسانی_گروه, stringWarrning);
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_به_دلیل_عدم_وجود_حداقل_یک_مقدار_از_فیلد_های_گروه, stringNotFounded);


                //return addOrUpdateResualt;

            }
            catch (Exception ex)
            {
             //   System.IO.File.AppendAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "log.txt", new string[] { ex.Message + " --> " + ex.InnerException });
            }
        }

        private static bool HasNullProperty(GroupSyncModel x)
        {
            return x.EducationalGroupCode == null || x.EducationalGroupCode == 0 || x.CollegeId == 0 || x.CollegeId == null || string.IsNullOrEmpty(x.Term);
        }

        //گروه -Remove
        public static Dictionary<string, string> SyncRemoveEducationalGroup(
            IEducationalGroupService grp)
        {
            var groups = ClientHelper.GetValue<GroupSyncModel>(StaticValue.GroupRelativeAddress);
            var addOrUpdateResualt = new Dictionary<string, string>();
            var counter = 1;
            groups.ForEach(x =>
            {
                if (x != null)
                {
                    if (grp.IsExist(y => y.EducationalGroupCode == x.EducationalGroupCode && y.Term.TermCode == x.Term))
                    {
                        var selectedGrp = grp.GetMany(y => y.EducationalGroupCode == x.EducationalGroupCode && y.Term.TermCode == x.Term)
                            .FirstOrDefault();

                        var g = grp.Remove(selectedGrp);
                        addOrUpdateResualt.Add($"{x.Name}-{counter}", g.GetDeleteResualt());
                        ++counter;
                    }
                    else
                    {
                        addOrUpdateResualt.Add($"{x.Name}-{counter}", 0.GetDeleteResualt());
                        ++counter;
                    }
                }
            });
            return addOrUpdateResualt;
        }



        public static void AddGroupScore(
    IEducationalGroupService grp,
    IEducationalGroupScoreService grpScr,
    IIndicatorService ind,
    ITermService term,
    IProfessorService professorService,
    IProfessorScoreService professorScoreService,
    GroupSyncModel x,
    IEducationalClassService educationalClassService
    , ILogService logService, ILogTypeService logTypeService, IUserService userService, User user)
        {
            //var resualList = new List<KeyValuePair<string, string>>();
            var description = string.Empty;
            var logFlag = false;
            try
            {
                if (!term.IsExist(y => y.TermCode == x.Term))
                {
                    SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_گروه, $"{x.EducationalGroupCode}-{x.Term}-عدم وجود ترم");
                    return;
                }

                if (!grp.IsExist(y => y.EducationalGroupCode == x.EducationalGroupCode && y.Term.TermCode == x.Term))
                {
                    SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_گروه, $"{x.EducationalGroupCode}-{x.Term}-عدم وجود گروه");
                    return;
                }

                var group = grp.Get(y => y.EducationalGroupCode == x.EducationalGroupCode &&
                                         y.Term.TermCode == x.Term);
                var groupTerm = term.Get(y => y.TermCode == x.Term);



                #region وضعیت حضور آنلاین مدیر گروه
                try
                {
                    var indicator1 = ind.Get(y => y.CountOfType ==
                                                  ("g" + (int)IndicatorGroupName.وضعیت_حضور_آنلاین_مدیر_گروه) && y.IsActive == true);

                    if (indicator1 != null)
                    {

                        if (x.OnlinePresenceTime != null)
                        {
                            var scores1 = indicator1.Scores;
                            var score1 = scores1.FirstOrDefault(y => y.MinValue <= x.OnlinePresenceTime
                                                                     && y.MaxValue >= x.OnlinePresenceTime);
                            if (score1 != null)
                            {

                                var grpScoModel1 = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score1,
                                    CurrentScore = (int?)(indicator1.Ratio.Point * score1.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel1.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel1.Term.Id &&
                                        z.Score.Id == grpScoModel1.Score.Id &&
                                        z.CurrentScore == grpScoModel1.CurrentScore
                                ))
                                    grpScr.Add(grpScoModel1);
                            }
                            else
                            {
                                description += "وضعیت حضور آنلاین مدیر گروه-";
                                logFlag = true;


                            }
                        }
                        else
                        {
                            description += "وضعیت حضور آنلاین مدیر گروه-";
                            logFlag = true;

                        }
                    }

                }
                catch (Exception)
                {
                    description += "وضعیت حضور آنلاین مدیر گروه-";
                    logFlag = true;

                }
                #endregion

                #region وضعیت حضور فیزیکی مدیران گروه
                //try
                //{
                //    var indicator2 = ind.Get(y => y.CountOfType ==
                //                                  ("g" + (int)IndicatorGroupName.وضعیت_حضور_فیزیکی_مدیران_گروه));

                //    if (x.PhysicalPresenceTime != null)
                //    {
                //        var scores2 = indicator2.Scores;
                //        var score2 = scores2.FirstOrDefault(y => y.MinValue <= x.PhysicalPresenceTime
                //                                                 && y.MaxValue > x.PhysicalPresenceTime);
                //        if (score2 != null)
                //        {

                //            var grpScoModel2 = new EducationalGroupScore
                //            {
                //                EducationalGroup = group,
                //                Term = groupTerm,
                //                Score = score2,
                //                CurrentScore = (int?)(indicator2.Ratio.Point * score2.Point)
                //            };

                //            grpScr.Add(grpScoModel2);
                //        }
                //        else
                //        {
                //            description += "وضعیت حضور فیزیکی مدیران گروه-";

                //            logFlag = true;


                //        }
                //    }
                //    else
                //    {
                //        description += "وضعیت حضور فیزیکی مدیران گروه-";

                //        logFlag = true;

                //    }


                //}
                //catch (Exception)
                //{
                //    description += "وضعیت حضور فیزیکی مدیران گروه-";

                //    logFlag = true;

                //}
                #endregion

                #region میانگین نمرات دانشجو گروه در یک ترم مقطعه لیسانس
                try
                {
                    var indicator3 = ind.Get(y => y.CountOfType ==
                                                  ("g" + (int)IndicatorGroupName.میانگین_نمرات_دانشجو_گروه_در_یک_ترم_مقطعه_لیسانس) && y.IsActive == true);

                    if (indicator3 != null)
                    {

                        if (x.BachelorStudentAverageScores != null)
                        {
                            var scores3 = indicator3.Scores;
                            var roundedBachelorStudentAvreageScores =
                                decimal.Round((decimal)x.BachelorStudentAverageScores, 2);
                            var score3 = scores3.FirstOrDefault(y => y.MinValue <= roundedBachelorStudentAvreageScores
                                                                     && y.MaxValue >= roundedBachelorStudentAvreageScores);
                            if (score3 != null)
                            {

                                var grpScoModel3 = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score3,
                                    CurrentScore = (int?)(indicator3.Ratio.Point * score3.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel3.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel3.Term.Id &&
                                        z.Score.Id == grpScoModel3.Score.Id &&
                                        z.CurrentScore == grpScoModel3.CurrentScore
                                ))

                                    grpScr.Add(grpScoModel3);
                            }
                            else
                            {
                                description += "میانگین نمرات دانشجو گروه در یک ترم مقطعه لیسانس-";

                                logFlag = true;


                            }
                        }
                        else
                        {
                            description += "میانگین نمرات دانشجو گروه در یک ترم مقطعه لیسانس-";

                            logFlag = true;

                        }
                    }

                }
                catch (Exception)
                {

                    description += "میانگین نمرات دانشجو گروه در یک ترم مقطعه لیسانس-";
                    logFlag = true;

                }
                #endregion

                #region میانگین نمرات دانشجو گروه در یک ترم مقطعه فوق لیسانس
                try
                {
                    var indicator4 = ind.Get(y => y.CountOfType ==
                                                  ("g" + (int)IndicatorGroupName.میانگین_نمرات_دانشجو_گروه_در_یک_ترم_مقطعه_فوق_لیسانس) && y.IsActive == true);

                    if (indicator4 != null)
                    {

                        if (x.MaStudentAverageScores != null)
                        {
                            var roundedMaStudentAvreageScores =
                                decimal.Round((decimal)x.MaStudentAverageScores, 2);

                            var scores4 = indicator4.Scores;
                            var score4 = scores4.FirstOrDefault(y => y.MinValue <= roundedMaStudentAvreageScores
                                                                     && y.MaxValue >= roundedMaStudentAvreageScores);
                            if (score4 != null)
                            {

                                var grpScoModel4 = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score4,
                                    CurrentScore = (int?)(indicator4.Ratio.Point * score4.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel4.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel4.Term.Id &&
                                        z.Score.Id == grpScoModel4.Score.Id &&
                                        z.CurrentScore == grpScoModel4.CurrentScore
                                ))

                                    grpScr.Add(grpScoModel4);
                            }
                            else
                            {
                                description += "میانگین نمرات دانشجو گروه در یک ترم مقطعه فوق لیسانس-";

                                logFlag = true;


                            }
                        }
                        else
                        {
                            description += "میانگین نمرات دانشجو گروه در یک ترم مقطعه فوق لیسانس-";


                            logFlag = true;

                        }
                    }

                }
                catch (Exception)
                {
                    description += "میانگین نمرات دانشجو گروه در یک ترم مقطعه فوق لیسانس-";


                    logFlag = true;
                }
                #endregion

                #region درصد دانشجویان اخراجی
                try
                {
                    var indicator5 = ind.Get(y => y.CountOfType ==
                                                  ("g" + (int)IndicatorGroupName.درصد_دانشجویان_اخراجی) && y.IsActive == true);

                    if (indicator5 != null)
                    {

                        if (x.DismissedstudentsCount != null && x.TotalStudentsCount != null)
                        {
                            var scores5 = indicator5.Scores;
                            var roundedDismissedstudentsCount =
                                decimal.Round((decimal)((x.DismissedstudentsCount * 100) / x.TotalStudentsCount), 2);
                            var score5 = scores5.FirstOrDefault(y => y.MinValue <= roundedDismissedstudentsCount
                                                                     && y.MaxValue >= roundedDismissedstudentsCount);
                            if (score5 != null)
                            {

                                var grpScoModel5 = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score5,
                                    CurrentScore = (int?)(indicator5.Ratio.Point * score5.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel5.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel5.Term.Id &&
                                        z.Score.Id == grpScoModel5.Score.Id &&
                                        z.CurrentScore == grpScoModel5.CurrentScore
                                ))

                                    grpScr.Add(grpScoModel5);
                            }
                            else
                            {
                                description += "درصد دانشجویان اخراجی-";

                                logFlag = true;


                            }
                        }
                        else
                        {
                            description += "درصد دانشجویان اخراجی-";

                            logFlag = true;

                        }
                    }

                }
                catch (Exception)
                {
                    description += "درصد دانشجویان اخراجی-";

                    logFlag = true;
                }
                #endregion

                #region درصد دانشجویان انصرافی
                try
                {
                    var indicator6 = ind.Get(y => y.CountOfType ==
                                                  ("g" + (int)IndicatorGroupName.درصد_دانشجویان_انصرافی) && y.IsActive == true);

                    if (indicator6 != null)
                    {

                        if (x.CancellationStudentsCount != null && x.TotalStudentsCount != null)
                        {
                            var scores6 = indicator6.Scores;

                            var roundedCancledStudentsCount =
                                decimal.Round((decimal)((x.CancellationStudentsCount * 100) / x.TotalStudentsCount), 2);


                            var score6 = scores6.FirstOrDefault(y => y.MinValue <= roundedCancledStudentsCount
                                                                     && y.MaxValue >= roundedCancledStudentsCount);
                            if (score6 != null)
                            {

                                var grpScoModel6 = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score6,
                                    CurrentScore = (int?)(indicator6.Ratio.Point * score6.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel6.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel6.Term.Id &&
                                        z.Score.Id == grpScoModel6.Score.Id &&
                                        z.CurrentScore == grpScoModel6.CurrentScore
                                ))


                                    grpScr.Add(grpScoModel6);
                            }
                            else
                            {
                                description += "درصد دانشجویان انصرافی-";

                                logFlag = true;


                            }
                        }
                        else
                        {
                            description += "درصد دانشجویان انصرافی-";

                            logFlag = true;

                        }
                    }

                }
                catch (Exception)
                {
                    description += "درصد دانشجویان انصرافی-";

                    logFlag = true;
                }
                #endregion

                #region نسبت استاد به دانشجو کارشناسی

                try
                {
                    var indicator7 = ind.Get(y => y.CountOfType ==
                                                  ("g" + (int)IndicatorGroupName.نسبت_استاد_به_دانشجو_کارشناسی) && y.IsActive == true);

                    if (indicator7 != null)
                    {

                        if (x.BachelorStudentCount != null && x.BachelorProfessorsCount != null)
                        {
                            var scores7 = indicator7.Scores;

                            var roundedBachelorStudentCount = decimal.Round((decimal)(x.BachelorStudentCount / x.BachelorProfessorsCount), 2);

                            var score7 = scores7.FirstOrDefault(y => y.MinValue <= roundedBachelorStudentCount
                                                                     && y.MaxValue >= roundedBachelorStudentCount);
                            if (score7 != null)
                            {

                                var grpScoModel7 = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score7,
                                    CurrentScore = (int?)(indicator7.Ratio.Point * score7.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel7.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel7.Term.Id &&
                                        z.Score.Id == grpScoModel7.Score.Id &&
                                        z.CurrentScore == grpScoModel7.CurrentScore
                                ))

                                    grpScr.Add(grpScoModel7);
                            }
                            else
                            {
                                description += "نسبت استاد به دانشجو کارشناسی-";

                                logFlag = true;


                            }
                        }
                        else
                        {
                            description += "نسبت استاد به دانشجو کارشناسی-";

                            logFlag = true;

                        }
                    }

                }
                catch (Exception)
                {
                    SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_گروه, $"{x.EducationalGroupCode}-{x.Term}");
                }
                #endregion

                #region پروپوزال های تصویب شده در گروه
                try
                {
                    var indicator8 = ind.Get(y => y.CountOfType ==
                                                  ("g" + (int)IndicatorGroupName.پروپوزال_های_تصویب_شده_در_گروه) && y.IsActive == true);

                    if (indicator8 != null)
                    {

                        if (x.ApprovedProposals != null && x.TotalProposals != null)
                        {
                            var scores8 = indicator8.Scores;
                            var roundedApprovedProposals =
                                decimal.Round((decimal)((x.ApprovedProposals * 100) / x.TotalProposals), 2);

                            var score8 = scores8.FirstOrDefault(y => y.MinValue <= roundedApprovedProposals
                                                                     && y.MaxValue >= roundedApprovedProposals);
                            if (score8 != null)
                            {

                                var grpScoModel8 = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score8,
                                    CurrentScore = (int?)(indicator8.Ratio.Point * score8.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel8.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel8.Term.Id &&
                                        z.Score.Id == grpScoModel8.Score.Id &&
                                        z.CurrentScore == grpScoModel8.CurrentScore
                                ))

                                    grpScr.Add(grpScoModel8);
                            }
                            else
                            {
                                description += "پروپوزال های تصویب شده در گروه-";

                                logFlag = true;


                            }
                        }
                        else
                        {
                            description += "پروپوزال های تصویب شده در گروه-";

                            logFlag = true;

                        }
                    }
                }
                catch (Exception)
                {
                    description += "پروپوزال های تصویب شده در گروه-";

                    logFlag = true;
                }
                #endregion

                #region نسبت استاد به دانشجو کارشناسی ارشد
                try
                {
                    var indicator9 = ind.Get(y => y.CountOfType ==
                                                  ("g" + (int)IndicatorGroupName.نسبت_استاد_به_دانشجو_کارشناسی_ارشد) && y.IsActive == true);

                    if (indicator9 != null)
                    {
                        if (x.MaStudentCount != null && x.MaProfessorsCount != null)
                        {
                            var roundedMaStudentCount = decimal.Round((decimal)(x.MaStudentCount / x.MaProfessorsCount), 2);

                            var scores9 = indicator9.Scores;
                            var score9 = scores9.FirstOrDefault(y => y.MinValue <= roundedMaStudentCount
                                                                     && y.MaxValue >= roundedMaStudentCount);
                            if (score9 != null)
                            {

                                var grpScoModel9 = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score9,
                                    CurrentScore = (int?)(indicator9.Ratio.Point * score9.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel9.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel9.Term.Id &&
                                        z.Score.Id == grpScoModel9.Score.Id &&
                                        z.CurrentScore == grpScoModel9.CurrentScore
                                ))

                                    grpScr.Add(grpScoModel9);
                            }
                            else
                            {
                                description += "نسبت استاد به دانشجو کارشناسی ارشد-";

                                logFlag = true;


                            }
                        }
                        else
                        {
                            description += "نسبت استاد به دانشجو کارشناسی ارشد-";

                            logFlag = true;

                        }
                    }
                }
                catch (Exception)
                {
                    description += "نسبت استاد به دانشجو کارشناسی ارشد-";

                    logFlag = true;
                }
                #endregion

                #region ضریب مدیر گروه


                try
                {
                    var indicator10 = ind.Get(y => y.CountOfType ==
                                                   ("g" + (int)IndicatorGroupName.ضریب_مدیر_گروه) && y.IsActive == true);

                    if (indicator10 != null)
                    {

                        if (x.GroupMangerId != null)
                        {
                            if (professorService.IsExist(y => y.ProfessorCode == x.GroupMangerId && y.Term.TermCode == x.Term))
                            {
                                var score10 = indicator10.Scores.FirstOrDefault();

                                var groupManger = group.GroupManger;

                                //پنج تا شاخص برای ضریب مدیر گروه 

                                var indicatorForProfessor = ind.GetMany(y =>
                                    y.CountOfType == ("p" + (int)IndicatorProfessorName.مرتبه_علمی) ||
                                    y.CountOfType == ("p" + (int)IndicatorProfessorName.سابقه_تدریس) ||
                                    y.CountOfType == ("p" + (int)IndicatorProfessorName.میزان_تحصیلات) ||
                                    y.CountOfType == ("p" + (int)IndicatorProfessorName.رتبه_دانشگاه_محل_تحصیل) ||
                                    y.CountOfType == ("p" + (int)IndicatorProfessorName.دانشگاه_محل_خدمت)).ToList().Select(z => z.Id);

                                var prfId = professorService.Get(y => y.ProfessorCode == x.GroupMangerId && y.Term.TermCode == x.Term).Id;
                                var professoreScore = professorScoreService.GetMany(y => y.EducationalGroup.Id == group.Id &&
                                y.Term.Id == groupTerm.Id && prfId == y.Professor.Id &&
                                indicatorForProfessor.Contains(y.Score.Indicator.Id)).Select(y => y.CurrentScore).Sum();

                                if (professoreScore == 0)
                                {
                                    var groupId = professorScoreService.GetMany(y =>
                                            y.Term.Id == groupTerm.Id && prfId == y.Professor.Id &&
                                            indicatorForProfessor.Contains(y.Score.Indicator.Id))
                                        .Select(y => y.EducationalGroup.Id)
                                        .FirstOrDefault();
                                    professoreScore = professorScoreService.GetMany(y => y.EducationalGroup.Id == groupId &&
                                                                                         y.Term.Id == groupTerm.Id && prfId == y.Professor.Id &&
                                                                                         indicatorForProfessor.Contains(y.Score.Indicator.Id)).Select(y => y.CurrentScore).Sum();
                                }


                                if (professoreScore != null && professoreScore != 0)
                                {
                                    var fiveProfessorIndicatorScore = (professoreScore * 1.4);
                                    if (score10 != null)
                                    {

                                        var grpScoModel10 = new EducationalGroupScore
                                        {
                                            EducationalGroup = group,
                                            Term = groupTerm,
                                            Score = score10,
                                            CurrentScore = (int?)fiveProfessorIndicatorScore
                                        };
                                        if (!grpScr.IsExist(
                                            z =>
                                                z.EducationalGroup.Id == grpScoModel10.EducationalGroup.Id &&
                                                z.Term.Id == grpScoModel10.Term.Id &&
                                                z.Score.Id == grpScoModel10.Score.Id &&
                                                z.CurrentScore == grpScoModel10.CurrentScore
                                        ))

                                            grpScr.Add(grpScoModel10);
                                    }
                                    else
                                    {
                                        description += "ضریب مدیر گروه-";

                                        logFlag = true;


                                    }
                                }
                                else
                                {
                                    description += "ضریب مدیر گروه-";

                                    logFlag = true;


                                }
                            }
                            else
                            {
                                description += "ضریب مدیر گروه-";

                                logFlag = true;

                            }

                        }
                        else
                        {
                            description += "ضریب مدیر گروه-";

                            logFlag = true;

                        }
                    }

                }
                catch (Exception)
                {
                    description += "ضریب مدیر گروه-";

                    logFlag = true;
                }

                #endregion

                #region نسبت تعداد مشخصه به دانشجو
                try
                {
                    var indicator = ind.Get(y => y.CountOfType ==
                                                   ("g" + (int)IndicatorGroupName.نسبت_تعداد_مشخصه_به_دانشجو) && y.IsActive == true);
                    if (indicator != null)
                    {
                        var gorupClasses = educationalClassService.GetMany(g => g.EducationalGroup.EducationalGroupCode == x.EducationalGroupCode && g.Term.TermCode == x.Term)
                            .Select(s => s.CodeClass).Distinct().ToList();
                        if (x.TotalStudentsCount != null && gorupClasses.Count() > 0)
                        {
                            var roundedMaStudentCount = decimal.Round((decimal)(x.TotalStudentsCount / gorupClasses.Count()), 2);

                            var scores = indicator.Scores;
                            var score = scores.FirstOrDefault(y => y.MinValue <= roundedMaStudentCount
                                                                     && y.MaxValue >= roundedMaStudentCount);
                            if (score != null)
                            {
                                var grpScoModel = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score,
                                    CurrentScore = (int?)(indicator.Ratio.Point * score.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel.Term.Id &&
                                        z.Score.Id == grpScoModel.Score.Id &&
                                        z.CurrentScore == grpScoModel.CurrentScore
                                ))

                                    grpScr.Add(grpScoModel);
                            }
                            else
                            {
                                description += "نسبت تعداد مشخصه به دانشجو-";
                                logFlag = true;
                            }
                        }
                        else
                        {
                            description += "نسبت تعداد مشخصه به دانشجو-";
                            logFlag = true;
                        }
                    }
                }
                catch (Exception)
                {
                    description += "نسبت تعداد مشخصه به دانشجو-";
                    logFlag = true;
                }
                #endregion

                #region اساتید پژوهشی فعال گروه
                try
                {
                    var indicator = ind.Get(y => y.CountOfType ==
                                                   ("g" + (int)IndicatorGroupName.اساتید_پژوهشی_فعال_گروه) && y.IsActive == true);
                    if (indicator != null)
                    {
                        if (x.ActiveResearchProfessorCount != null)
                        {
                            var scores = indicator.Scores;
                            var score = scores.FirstOrDefault(y => y.MinValue <= x.ActiveResearchProfessorCount
                                                                     && y.MaxValue >= x.ActiveResearchProfessorCount);
                            if (score != null)
                            {
                                var grpScoModel = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score,
                                    CurrentScore = (int?)(indicator.Ratio.Point * score.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel.Term.Id &&
                                        z.Score.Id == grpScoModel.Score.Id &&
                                        z.CurrentScore == grpScoModel.CurrentScore
                                ))

                                    grpScr.Add(grpScoModel);
                            }
                            else
                            {
                                description += "اساتید پژوهشی فعال گروه-";
                                logFlag = true;
                            }
                        }
                        else
                        {
                            description += "اساتید پژوهشی فعال گروه-";
                            logFlag = true;
                        }
                    }
                }
                catch (Exception)
                {
                    description += "اساتید پژوهشی فعال گروه-";
                    logFlag = true;
                }
                #endregion

                #region زمان انتظار پروپوزال دانشجو
                try
                {
                    var indicator = ind.Get(y => y.CountOfType ==
                                                   ("g" + (int)IndicatorGroupName.زمان_انتظار_پروپوزال_دانشجو) && y.IsActive == true);
                    if (indicator != null)
                    {
                        if (x.AverageProposalWaitingTime != null)
                        {
                            var scores = indicator.Scores;
                            var score = scores.FirstOrDefault(y => y.MinValue <= x.AverageProposalWaitingTime
                                                                     && y.MaxValue >= x.AverageProposalWaitingTime);
                            if (score != null)
                            {
                                var grpScoModel = new EducationalGroupScore
                                {
                                    EducationalGroup = group,
                                    Term = groupTerm,
                                    Score = score,
                                    CurrentScore = (int?)(indicator.Ratio.Point * score.Point)
                                };
                                if (!grpScr.IsExist(
                                    z =>
                                        z.EducationalGroup.Id == grpScoModel.EducationalGroup.Id &&
                                        z.Term.Id == grpScoModel.Term.Id &&
                                        z.Score.Id == grpScoModel.Score.Id &&
                                        z.CurrentScore == grpScoModel.CurrentScore
                                ))

                                    grpScr.Add(grpScoModel);
                            }
                            else
                            {
                                description += "زمان انتظار پروپوزال دانشجو-";
                                logFlag = true;
                            }
                        }
                        else
                        {
                            description += "زمان انتظار پروپوزال دانشجو-";
                            logFlag = true;
                        }
                    }
                }
                catch (Exception)
                {
                    description += "زمان انتظار پروپوزال دانشجو-";
                    logFlag = true;
                }
                #endregion

                if (logFlag) SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_گروه, $"{x.EducationalGroupCode}-{x.Term}-{description}");


            }
            catch (Exception)
            {
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_گروه, $"{x.EducationalGroupCode}-{x.Term}");

            }

            //return resualList;

        }
        public static void SyncGroupManagerEducationalGroupScores(
    IEducationalGroupService grp,
    IEducationalGroupScoreService grpScr,
    IIndicatorService ind,
    ITermService term,
    IProfessorService professorService,
    IProfessorScoreService professorScoreService,
    GroupSyncModel x,
    IEducationalClassService educationalClassService
    , ILogService logService, ILogTypeService logTypeService, IUserService userService, User user)
        {
            var description = string.Empty;
            var logFlag = false;
            var group = grp.Get(y => y.EducationalGroupCode == x.EducationalGroupCode &&
                                         y.Term.TermCode == x.Term);
            var groupTerm = term.Get(y => y.TermCode == x.Term);
            try
            {
                #region ضریب مدیر گروه
                try
                {
                    var indicator10 = ind.Get(y => y.CountOfType ==
                                                   ("g" + (int)IndicatorGroupName.ضریب_مدیر_گروه) && y.IsActive == true);

                    if (indicator10 != null)
                    {

                        if (x.GroupMangerId != null)
                        {
                            if (professorService.IsExist(y => y.ProfessorCode == x.GroupMangerId && y.Term.TermCode == x.Term))
                            {
                                var score10 = indicator10.Scores.FirstOrDefault();

                                var groupManger = group.GroupManger;

                                //پنج تا شاخص برای ضریب مدیر گروه 

                                var indicatorForProfessor = ind.GetMany(y =>
                                    y.CountOfType == ("p" + (int)IndicatorProfessorName.مرتبه_علمی) ||
                                    y.CountOfType == ("p" + (int)IndicatorProfessorName.سابقه_تدریس) ||
                                    y.CountOfType == ("p" + (int)IndicatorProfessorName.میزان_تحصیلات) ||
                                    y.CountOfType == ("p" + (int)IndicatorProfessorName.رتبه_دانشگاه_محل_تحصیل) ||
                                    y.CountOfType == ("p" + (int)IndicatorProfessorName.دانشگاه_محل_خدمت)).ToList().Select(z => z.Id);

                                var prfId = professorService.Get(y => y.ProfessorCode == x.GroupMangerId && y.Term.TermCode == x.Term).Id;
                                var professoreScore = professorScoreService.GetMany(y => y.EducationalGroup.Id == group.Id &&
                                y.Term.Id == groupTerm.Id && prfId == y.Professor.Id &&
                                indicatorForProfessor.Contains(y.Score.Indicator.Id)).Select(y => y.CurrentScore).Sum();

                                if (professoreScore == 0)
                                {
                                    var groupId = professorScoreService.GetMany(y =>
                                            y.Term.Id == groupTerm.Id && prfId == y.Professor.Id &&
                                            indicatorForProfessor.Contains(y.Score.Indicator.Id))
                                        .Select(y => y.EducationalGroup.Id)
                                        .FirstOrDefault();
                                    professoreScore = professorScoreService.GetMany(y => y.EducationalGroup.Id == groupId &&
                                                                                         y.Term.Id == groupTerm.Id && prfId == y.Professor.Id &&
                                                                                         indicatorForProfessor.Contains(y.Score.Indicator.Id)).Select(y => y.CurrentScore).Sum();
                                }


                                if (professoreScore != null && professoreScore != 0)
                                {
                                    var fiveProfessorIndicatorScore = (professoreScore * 1.4);
                                    if (score10 != null)
                                    {

                                        var grpScoModel10 = new EducationalGroupScore
                                        {
                                            EducationalGroup = group,
                                            Term = groupTerm,
                                            Score = score10,
                                            CurrentScore = (int?)fiveProfessorIndicatorScore
                                        };
                                        if (!grpScr.IsExist(
                                            z =>
                                                z.EducationalGroup.Id == grpScoModel10.EducationalGroup.Id &&
                                                z.Term.Id == grpScoModel10.Term.Id &&
                                                z.Score.Id == grpScoModel10.Score.Id &&
                                                z.CurrentScore == grpScoModel10.CurrentScore
                                        ))

                                            grpScr.Add(grpScoModel10);
                                    }
                                    else
                                    {
                                        description += "ضریب مدیر گروه-";

                                        logFlag = true;


                                    }
                                }
                                else
                                {
                                    description += "ضریب مدیر گروه-";

                                    logFlag = true;


                                }
                            }
                            else
                            {
                                description += "ضریب مدیر گروه-";

                                logFlag = true;

                            }

                        }
                        else
                        {
                            description += "ضریب مدیر گروه-";

                            logFlag = true;

                        }
                    }

                }
                catch (Exception)
                {
                    description += "ضریب مدیر گروه-";

                    logFlag = true;
                }
                #endregion
            }
            catch (Exception)
            {
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_گروه, $"{x.EducationalGroupCode}-{x.Term}");
            }
        }
        




        //public static ReturnValue Run(IEducationalGroupService grp,
        //    IEducationalGroupScoreService grpSrc,
        //    IIndicatorService ind,
        //    ITermService term,
        //    IProfessorService professorService,
        //    IProfessorScoreService professorScoreService
        //   )
        //{
        //    //The order of the execution on the commands is important
        //    //First-remove
        //    //Second-addOrUpdate
        //    //Third-remove score
        //    //fourth-addOrUpdate score

        //    //var remove = SyncRemoveEducationalGroup(grp);

        //    var addOrUpdate = SyncAddOrUpdateEducationalGroup(grp,);

        //    var removeScore = SyncEducationalGroupRemoveScore(grp);

        //    var addOrUpdateScore = SyncEducationalGroupAddScore(grp, grpSrc, ind, term, professorService, professorScoreService);

        //    var resualt = new ReturnValue
        //    {
        //        //Remove = remove,
        //        AddOrUpdate = addOrUpdate,
        //        RemoveScore = removeScore,
        //        AddOrUpdateScore = addOrUpdateScore
        //    };
        //    return resualt;
        //}

        public class ReturnValue
        {
            public Dictionary<string, string> Remove { get; set; }
            public Dictionary<string, string> AddOrUpdate { get; set; }
            public Dictionary<string, string> RemoveScore { get; set; }
            public Dictionary<string, List<KeyValuePair<string, string>>> AddOrUpdateScore { get; set; }
        }

    }
}