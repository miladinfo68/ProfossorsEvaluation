using System;
using System.Collections.Generic;
using System.Linq;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service.Sync
{
    public static class ProfessorSync
    {
        //استاد-Add Or Update
        public static void SyncAddOrUpdateProfessor(
            IProfessorService professorService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user, string termCode)
        {
            //log start
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_سینک_اساتید);



            var professores = ClientHelper.GetValue<ProfessorSyncModel>(StaticValue.ProfessorRelativeAddress + $"/?termCode={termCode}");
            //log get From service
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_اساتید_از_سرویس);


            var addOrUpdateResualt = new Dictionary<string, int>();
            var counter = 1;
            professores.ForEach(x =>
            {
                if (x != null)
                    if (x.ProfessoreCode == null || x.ProfessoreCode == 0 || string.IsNullOrEmpty(x.Term))
                    {
                        addOrUpdateResualt.Add($"{x.ProfessoreCode}-{x.Family}-{x.Name}-{counter}", 4);
                    }
                    else
                    {
                        var addOrUpdateReusalt = professorService.AddOrUpdate(x);
                        addOrUpdateResualt.Add($"{x.ProfessoreCode}-{x.Family}-{x.Name}-{counter}", addOrUpdateReusalt);

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
                stringWarrning += $"تعداد {warrning}" + " || " + s + " | ";
            }
            foreach (var s in addOrUpdateResualt.Where(x => x.Value == 3).Select(x => x.Key))
            {
                stringNotFounded += $"تعداد {notFounded}" + " || " + s + " | ";
            }

            stringWarrning = !string.IsNullOrEmpty(stringWarrning.Trim()) ? stringWarrning : "بدون مشکل";
            stringNotFounded = !string.IsNullOrEmpty(stringNotFounded.Trim()) ? stringNotFounded : "بدون مشکل";

            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.اساتید_اضافه_گردید, $"تعداد {added}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.اساتید_آپدیت_گردید, $"تعداد {updatetd}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_در_بروزرسانی_اساتید, stringWarrning);
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_به_دلیل_عدم_وجود_حداقل_یک_مقدار_از_اساتید, stringNotFounded);


            //return addOrUpdateResualt;
        }

        //استاد-Remove
        public static Dictionary<string, string> SyncRemoveProfessor(
            IProfessorService professorService)
        {
            var professores = ClientHelper.GetValue<ProfessorSyncModel>(StaticValue.ProfessorRelativeAddress);
            var addOrUpdateResualt = new Dictionary<string, string>();
            var counter = 1;
            professores.ForEach(x =>
            {
                if (x != null)
                {
                    if (professorService.IsExist(y => y.ProfessorCode == x.ProfessoreCode && y.Term.TermCode == x.Term))
                    {
                        var professor = professorService
                            .GetMany(y => y.ProfessorCode == x.ProfessoreCode && y.Term.TermCode == x.Term)
                            .FirstOrDefault();

                        var addOrUpdateReusalt = professorService.Remove(professor);
                        addOrUpdateResualt.Add($"{x.Family}-{x.Name}-{counter}", addOrUpdateReusalt.GetAddOrUpdateResualt());
                        ++counter;
                    }
                    else
                    {
                        addOrUpdateResualt.Add($"{x.Family}-{x.Name}-{counter}", 0.GetDeleteResualt());
                        ++counter;

                    }
                }
            });

            return addOrUpdateResualt;
        }


        //استاد-Remove Score

        public static void SyncProfessorRemoveScore(
            IProfessorService professorService, ILogService logService, ILogTypeService logTypeService, IUserService userService, User user, string termCode)
        {
            //log start
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_عملیات_حذف_امتیازات_اساتید);

            var professores = ClientHelper.GetValue<ProfessorSyncModel>(StaticValue.ProfessorRelativeAddress + $"/{termCode}");
            //log get From service
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_اساتید_از_سرویس);

            var removeResualt = new Dictionary<string, int>();
            var counter = 1;
            professores.ForEach(x =>
            {
                var professor = professorService
                    .GetMany(y => y.ProfessorCode == x.ProfessoreCode && y.Term.TermCode == x.Term)
                    .FirstOrDefault();
                if (x != null)
                    if (professor != null)
                    {
                        var pr = professorService.RemoveProfessorScore(x.ProfessoreCode, x.Term);
                        removeResualt.Add($"{x.Term}-{x.ProfessoreCode}-{x.Family}-{x.Name}-{counter}", pr);
                    }
                    else
                    {
                        if (x.ProfessoreCode == null || x.ProfessoreCode == 0 || string.IsNullOrEmpty(x.Term))
                        {
                            removeResualt.Add($"{x.Term}-{x.ProfessoreCode}-{x.Family}-{x.Name}-{counter}", -2000);
                        }
                        else
                            removeResualt.Add($"{x.Term}-{x.ProfessoreCode}-{x.Family}-{x.Name}-{counter}", -3000);
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

            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue._حذف_امتیازات_اساتید, $"تعداد {removed}");
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_به_دلیل_عدم_وجود_استاد, stringWarrning);
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_به_دلیل_عدم_وجود_حداقل_یک_مقدار_از_فیلد_های_استاد, stringNotFounded);


            //return removeResualt;
        }


        //استاد- Add Score

        public static void SyncProfessorAddScore(
            IProfessorService professorService,
            IProfessorScoreService professorScoreService,
            IIndicatorService indicatorService,
            ITermService termService,
            IMappingService mappingService,
            IMappingTypeService mappingTypeService,
            IEducationalClassService educationalClassService,
            IUniversityLevelMappingService universityLevelMappingService
            , ILogService logService, ILogTypeService logTypeService, IUserService userService, User user, string termCode)
        {
            //log start
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.شروع_عملیات_ثبت_امتیازات_اساتید);

            var professores = ClientHelper.GetValue<ProfessorSyncModel>(StaticValue.ProfessorRelativeAddress + $"/{termCode}");

            //log get From service
            SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.دریافت_اساتید_از_سرویس);


            //var addScoreResualt = new Dictionary<string, List<KeyValuePair<string, string>>>();
            var counter = 1;
            professores.ForEach(x =>
            {
                if (x != null)
                {
                    AddProfessorScore(professorService, professorScoreService
                       , indicatorService, termService, mappingService, mappingTypeService
                       , educationalClassService, universityLevelMappingService, x,
                       logService, logTypeService, userService, user);

                    //addScoreResualt.Add($"{x.Family}-{x.Name}-{counter}", resualt);

                }
                ++counter;

            });

            //return addScoreResualt;
        }



        public static void AddProfessorScore(IProfessorService professorService, IProfessorScoreService professorScoreService, IIndicatorService indicatorService, ITermService termService, IMappingService mappingService, IMappingTypeService mappingTypeService, IEducationalClassService educationalClassService,
            IUniversityLevelMappingService universityLevelMappingService, ProfessorSyncModel x
            , ILogService logService, ILogTypeService logTypeService, IUserService userService, User user)
        {
            //var resualList = new List<KeyValuePair<string, string>>();
            

            try
            {
                var description = string.Empty;
                var logFlag = false;
                var preventedContentType = new List<int?> { 10, 11, 24, 7, 6, 49, 50, 38 };
                var preventedHoldingType = new List<decimal?> { 5, 7 };
                var groupList = educationalClassService.GetMany(y => y.Professor.ProfessorCode == x.ProfessoreCode && y.Term.TermCode == x.Term
                    && !preventedContentType.Contains(y.ContentType)
                                && y.HoldingExamDate != null
                                && !preventedHoldingType.Contains(y.HoldingType)
                                && y.IsActive == true)
                    .Select(y => y.EducationalGroup).Distinct();
                if (!termService.IsExist(y => y.TermCode == x.Term))
                {
                    description += "عدم وجود ترم-";
                    SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_استاد, $"{x.ProfessoreCode}-{x.Term}");
                    return;
                }
                if (!groupList.Any())
                {
                    description += "عدم عضویت در گروه-";
                    logFlag = true;
                    //resualList.Add(new KeyValuePair<string, string>("خطا-گروه", "استاد زیرمجموعه هیچ گروه آموزشی نمی باشد"));
                }
                var classList = educationalClassService.GetMany(y =>
                                y.Professor.ProfessorCode == x.ProfessoreCode
                                && !preventedContentType.Contains(y.ContentType)
                                && y.HoldingExamDate != null
                                && !preventedHoldingType.Contains(y.HoldingType)
                                && y.Term.TermCode == x.Term 
                                && y.IsActive == true)
                    .Select(p => new
                    {
                        Group = p.EducationalGroup,
                        ClassStudentList = p.StudentEducationalClasses,
                        OnlineHelding = p.OnlineHeldingCount,
                        PersentHelding = p.PersentHeldingCount,
                        OtherHelding = p.OthersHeldingCount,
                        DelayAndEarlier = p.ProfessorDelayAndEarlier,
                        DeclaingScoreDate = p.DeclaringScoreDate,
                        HoldingExamDate = p.HoldingExamDate,
                        LoadingQuestionDate = p.LoadingQuestionDate,
                        Class = p.CodeClass,
                        ExamPapersRestoreDate = p.ReceiveExamPaperDate,
                        ExamPapersAggregationDate = p.AggregationExamPaperDate,
                        LessonPlanSendDate = p.LessonPlanSendDate
                    }).GroupBy(z => z.Group).ToList();

                if (!classList.Any())
                {
                    description += "عدم داشتن کلاس-";
                    logFlag = true;
                }


                #region مرتبه علمی

                try
                {
                    if (x.ScientificRank != null)
                    {
                        var indicator1 = indicatorService.Get(y => y.CountOfType ==
                                                                   ("p" + (int)IndicatorProfessorName.مرتبه_علمی));
                        var scores1 = indicator1.Scores;
                        Mapping map = null;
                        if (x.ScientificRank != 0)
                            map = mappingService
                                .GetMany(y => y.MappingType.Name == "مرتبه علمی")
                                .FirstOrDefault(y => y.TypeId == x.ScientificRank);
                        else if (x.ScientificRank == 0 && (x.AcademicDegree == 1 || x.AcademicDegree == 2 || x.AcademicDegree == 3 || x.AcademicDegree == 4))
                            map = mappingService
                                .GetMany(y => y.MappingType.Name == "مرتبه علمی")
                                .FirstOrDefault(y => y.TypeId == 1);
                        var score1 = scores1.FirstOrDefault(y => y.Name == map?.TypeName);
                        if (groupList.Any() && score1 != null)
                        {
                            foreach (var i in groupList)
                            {

                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score1,
                                    CurrentScore = (int?)(indicator1.Ratio.Point * score1.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                    z.Professor.Id == professorScore.Professor.Id &&
                                    z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                    z.Term.Id == professorScore.Term.Id &&
                                    z.Score.Id == professorScore.Score.Id &&
                                    z.CurrentScore == professorScore.CurrentScore
                                    ))
                                    professorScoreService.Add(professorScore);
                            }

                            //resualList.Add(new KeyValuePair<string, string>(indicator1.Name, "انجام گردید"));
                        }
                        else
                        {
                            description += "مرتبه علمی-";

                            logFlag = true;

                        }
                    }
                    else
                    {
                        description += "مرتبه علمی-";

                        logFlag = true;
                    }
                }
                catch (Exception ex)
                {
                    description += "مرتبه علمی-";

                    logFlag = true;
                }

                #endregion

                #region  سابقه تدریس

                try
                {
                    if (x.TeachingExperience != null && x.TeachingExperience != 0)
                    {
                        var indicator2 = indicatorService.Get(y => y.CountOfType ==
                                                                   ("p" + (int)IndicatorProfessorName.سابقه_تدریس));

                        var scores2 = indicator2.Scores;
                        var score2 = scores2
                            .FirstOrDefault(y => y.MinValue <= x.TeachingExperience && y.MaxValue >= x.TeachingExperience);
                        if (groupList.Any() && score2 != null)
                        {
                            foreach (var i in groupList)
                            {
                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score2,
                                    CurrentScore = (int?)(indicator2.Ratio.Point * score2.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);

                            }
                            //resualList.Add(new KeyValuePair<string, string>(indicator2.Name, "انجام گردید"));
                        }
                        else
                        {
                            description += "سابقه تدریس-";

                            logFlag = true;

                        }
                    }
                    else
                    {
                        description += "سابقه تدریس-";
                        logFlag = true;

                    }

                }
                catch (Exception)
                {
                    description += "سابقه تدریس-";
                    logFlag = true;
                }
                #endregion

                #region میزان تحصیلات
                try
                {
                    if (x.AcademicDegree != null && x.AcademicDegree != 0)
                    {
                        var indicator3 = indicatorService.Get(y => y.CountOfType ==
                                                                   ("p" + (int)IndicatorProfessorName.میزان_تحصیلات));
                        var map3 = mappingService
                            .GetMany(y => y.MappingType.Name == "میزان تحصیلات")
                            .FirstOrDefault(y => y.TypeId == x.AcademicDegree);
                        var scores3 = indicator3.Scores;
                        var score3 = scores3.FirstOrDefault(y => y.Name == map3?.TypeName);
                        if (groupList.Any() && score3 != null)
                        {
                            foreach (var i in groupList)
                            {
                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score3,
                                    CurrentScore = (int?)(indicator3.Ratio.Point * score3.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }

                            //resualList.Add(new KeyValuePair<string, string>(indicator3.Name, "انجام گردید"));
                        }
                        else
                        {
                            description += "میزان تحصیلات-";

                            logFlag = true;

                        }

                    }
                    else
                    {
                        description += "میزان تحصیلات-";

                        logFlag = true;
                    }

                }
                catch (Exception)
                {
                    description += "میزان تحصیلات-";

                    logFlag = true;
                }
                #endregion

                // dependency - UniversityLevelMappings
                #region رتبه دانشگاه محل تحصیل
                try
                {
                    if (x.UniversityStudyPlace != null && x.UniversityStudyPlace != 0)
                    {
                        var indicator4 = indicatorService
                            .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.رتبه_دانشگاه_محل_تحصیل));

                        //var uid = mappingService.Get(g => g.MappingType.Id == 4 && g.TypeId == x.UniversityStudyPlace).Id;
                        var score4 = universityLevelMappingService
                            .GetMany(y => y.UniversityId == x.UniversityStudyPlace)//x.UniversityStudyPlace
                            .FirstOrDefault()?.Score;
                        if (groupList.Any() && score4 != null)
                        {
                            foreach (var i in groupList)
                            {
                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score4,
                                    CurrentScore = (int?)(indicator4.Ratio.Point * score4.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }

                            //resualList.Add(new KeyValuePair<string, string>(indicator4.Name, "انجام گردید"));
                        }
                        else
                        {
                            description += "رتبه دانشگاه محل تحصیل-";

                            logFlag = true;
                        }

                    }
                    else
                    {
                        description += "رتبه دانشگاه محل تحصیل-";
                        logFlag = true;
                    }

                }
                catch (Exception)
                {
                    description += "رتبه دانشگاه محل تحصیل-";
                    logFlag = true;
                }
                #endregion

                // dependency - UniversityLevelMappings
                #region دانشگاه محل خدمت

                try
                {
                    if (x.UniversityWorkPlace != null && x.UniversityWorkPlace != 0)
                    {
                        var indicator5 = indicatorService
                            .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.دانشگاه_محل_خدمت));


                        var map5 = mappingService
                            .GetMany(y => y.MappingType.Name == "نوع دانشگاه")
                            .FirstOrDefault(y => y.TypeId == x.UniversityWorkPlace);
                        var scores5 = indicator5.Scores;
                        var score5 = scores5.FirstOrDefault(y => y.Name == map5?.TypeName);

                        if (groupList.Any() && score5 != null)
                        {
                            foreach (var i in groupList)
                            {

                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score5,
                                    CurrentScore = (int?)(indicator5.Ratio.Point * score5.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }

                            //resualList.Add(new KeyValuePair<string, string>(indicator5.Name, "انجام گردید"));
                        }
                        else
                        {
                            description += "دانشگاه محل خدمت-";

                            logFlag = true;
                        }

                    }
                    else
                    {
                        description += "دانشگاه محل خدمت-";

                        logFlag = true;
                    }

                }
                catch (Exception)
                {
                    description += "دانشگاه محل خدمت-";

                    logFlag = true;
                }
                #endregion

                //* dependebcy - StudentEducationalClasses
                #region ارزشیابی پایان ترم

                try
                {
                    var indicator6 = indicatorService
                        .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.ارزشیابی_پایان_ترم));



                    var evlautionScore = new List<KeyValuePair<int?, decimal?>>();
                    if (classList.Any())
                    {
                        foreach (var scList in classList)
                        {
                            var avgScoreGroup = new List<decimal?>();
                            foreach (var sc in scList)
                            {
                                if (sc.ClassStudentList.Any())
                                {
                                    var avg = sc.ClassStudentList.Sum(s => s.ProfessorEvaluationScore) / sc.ClassStudentList.Where(w => w.ProfessorEvaluationScore != null
                                     && w.ProfessorEvaluationScore > 0).Count(); //sc.ClassStudentList.Average(z => z.ProfessorEvaluationScore);
                                    if (avg != null && avg > 0)
                                        avgScoreGroup.Add(avg);
                                }
                            }
                            var average = avgScoreGroup.Average(p => p);
                            if (average > 0)
                                evlautionScore.Add(new KeyValuePair<int?, decimal?>(scList.Key.Id, average));
                            else
                            {
                                description += "ارزشیابی پایان ترم-";

                                logFlag = true;

                            }
                        }
                    }
                    else
                    {
                        description += "ارزشیابی پایان ترم-";

                        logFlag = true;
                    }
                    var scores6 = indicator6.Scores;

                    if (groupList.Any())
                    {
                        foreach (var i in groupList)
                        {
                            var evalScore = decimal.Round(evlautionScore.FirstOrDefault(z => z.Key == i.Id).Value != null ?
                                    (decimal)evlautionScore.FirstOrDefault(z => z.Key == i.Id).Value : 0m, 2);

                            var score6 = scores6.FirstOrDefault(p => p.MinValue
                                                                     <= evalScore &&
                                                                     p.MaxValue >= evalScore);
                            if (score6 != null)
                            {

                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score6,
                                    CurrentScore = (int?)(indicator6.Ratio.Point * score6.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }
                            else
                            {
                                description += "ارزشیابی پایان ترم-";

                                logFlag = true;

                            }
                        }

                        //resualList.Add(new KeyValuePair<string, string>(indicator6.Name, "انجام گردید"));
                    }
                    else
                    {
                        description += "ارزشیابی پایان ترم-";

                        logFlag = true;

                    }

                }
                catch (Exception)
                {
                    description += "ارزشیابی پایان ترم-";

                    logFlag = true;
                }

                #endregion

                #region تعداد جلسات برگزار شده کلاس
                try
                {

                    var indicator7 = indicatorService
                        .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.تعداد_جلسات_برگزار_شده_کلاس));
                    var scores7 = indicator7.Scores;



                    var heldingCount = new List<KeyValuePair<int?, decimal?>>();
                    if (classList.Any())
                    {
                        foreach (var scList in classList)
                        {
                            var sumHeldingGroup = new List<decimal?>();
                            foreach (var sc in scList)
                            {
                                if (sc.HoldingExamDate == null)
                                {
                                    sumHeldingGroup.Add((scores7.FirstOrDefault(p => p.Point == 80).MinValue) + 1);
                                }
                                else if (((sc.OnlineHelding ?? 0) + (sc.PersentHelding ?? 0) + (sc.OtherHelding ?? 0)) > 0)
                                {
                                    var sum = (sc.OnlineHelding ?? 0) + (sc.PersentHelding ?? 0) + (sc.OtherHelding ?? 0);
                                    if (sum > 0)
                                        sumHeldingGroup.Add(sum);
                                }
                            }
                            var average = sumHeldingGroup.Average(p => p);
                            if (average > 0)
                                heldingCount.Add(new KeyValuePair<int?, decimal?>(scList.Key.Id, average));
                            else
                            {
                                description += "تعداد جلسات برگزار شده کلاس-";

                                logFlag = true;

                            }
                        }
                    }
                    else
                    {
                        description += "تعداد جلسات برگزار شده کلاس-";

                        logFlag = true;

                    }

                    if (groupList.Any())
                    {
                        foreach (var i in groupList)
                        {
                            var heldScore = Math.Round(heldingCount.FirstOrDefault(z => z.Key == i.Id).Value != null ?
                                (decimal)heldingCount.FirstOrDefault(z => z.Key == i.Id).Value : 0m);


                            var score7 = scores7
                                .FirstOrDefault(p => p.MinValue <= heldScore
                                                     && p.MaxValue >= heldScore
                                );

                            if (score7 != null)
                            {

                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score7,
                                    CurrentScore = (int?)(indicator7.Ratio.Point * score7.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }
                            else
                            {
                                description += "تعداد جلسات برگزار شده کلاس-";

                                logFlag = true;

                            }
                        }

                        //resualList.Add(new KeyValuePair<string, string>(indicator7.Name, "انجام گردید"));
                    }
                    else
                    {
                        description += "تعداد جلسات برگزار شده کلاس-";

                        logFlag = true;

                    }


                }
                catch (Exception)
                {
                    description += "تعداد جلسات برگزار شده کلاس-";

                    logFlag = true;
                }
                #endregion

                #region وضعیت ورود و خروج استاد در کلاس

                try
                {
                    var indicator8 = indicatorService
                        .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.وضعیت_ورود_و_خروج_استاد_در_کلاس));
                    var scores8 = indicator8.Scores;

                    var delayAndEarlies = new List<KeyValuePair<int?, decimal?>>();
                    if (classList.Any())
                    {
                        foreach (var scList in classList)
                        {
                            var avg = new List<decimal?>();
                            foreach (var sc in scList)
                            {
                                if (sc.HoldingExamDate == null)
                                {
                                    avg.Add((scores8.FirstOrDefault(p => p.Point == 90).MinValue) + 1);
                                }
                                else if (sc.DelayAndEarlier > 0)
                                    avg.Add(sc.DelayAndEarlier);
                            }
                            var average = avg.Average(p => p);
                            if (average > 0)
                                delayAndEarlies.Add(new KeyValuePair<int?, decimal?>(scList.Key.Id, average));
                            else
                            {
                                description += "وضعیت ورود و خروج استاد در کلاس-";

                                logFlag = true;

                            }
                        }
                    }

                    if (groupList.Any())
                    {
                        foreach (var i in groupList)
                        {
                            var reoundedDelayAndEarlies = Math.Round((decimal)delayAndEarlies.FirstOrDefault(z => z.Key == i.Id).Value);
                            var score8 = scores8
                                .FirstOrDefault(p => p.MinValue <= reoundedDelayAndEarlies
                                                     && p.MaxValue >= reoundedDelayAndEarlies);
                            if (score8 != null)
                            {

                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score8,
                                    CurrentScore = (int?)(indicator8.Ratio.Point * score8.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }
                            else
                            {
                                description += "وضعیت ورود و خروج استاد در کلاس-";

                                logFlag = true;

                            }
                        }

                        //resualList.Add(new KeyValuePair<string, string>(indicator8.Name, "انجام گردید"));
                    }
                    else
                    {
                        description += "وضعیت ورود و خروج استاد در کلاس-";

                        logFlag = true;


                    }

                }
                catch (Exception)
                {
                    description += "وضعیت ورود و خروج استاد در کلاس-";

                    logFlag = true;
                }
                #endregion

                #region وضعیت اعلام نمرات

                try
                {
                    var indicator9 = indicatorService
                        .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.وضعیت_اعلام_نمرات));

                    var declaringScore = new List<KeyValuePair<int?, decimal?>>();
                    var scores9 = indicator9.Scores;
                    if (classList.Any())
                    {
                        foreach (var scList in classList)
                        {
                            var avg = new List<decimal?>();
                            foreach (var sc in scList)
                            {
                                if (sc.DeclaingScoreDate != null && sc.ExamPapersAggregationDate != null)
                                {
                                    var dayDiff = (sc.DeclaingScoreDate - sc.ExamPapersAggregationDate).Value.Days;

                                    //if (dayDiff >= 0)
                                    avg.Add(dayDiff);
                                }
                                else if (sc.DeclaingScoreDate == null)
                                    avg.Add((scores9.FirstOrDefault(f => f.Point == 0).MinValue) + 1);

                                //else if (sc.ExamPapersAggregationDate == null)
                                //    avg.Add((scores9.FirstOrDefault(f => f.Point == 80).MinValue) + 1);
                            }
                            var average = avg.Average(p => p);
                            //if (average >= 0)
                            declaringScore.Add(new KeyValuePair<int?, decimal?>(scList.Key.Id, average));
                            //else
                            //{
                            //    description += "وضعیت اعلام نمرات-";

                            //    logFlag = true;


                            //}
                        }
                    }
                    else
                    {
                        description += "وضعیت اعلام نمرات-";

                        logFlag = true;


                    }

                    if (groupList.Any())
                    {
                        foreach (var i in groupList)
                        {
                            var roundedDeclaringScore = Math.Round((decimal)declaringScore.FirstOrDefault(z => z.Key == i.Id).Value);
                            var score9 = scores9
                                .FirstOrDefault(p => p.MinValue <= roundedDeclaringScore
                                                     && p.MaxValue >= roundedDeclaringScore);
                            if (score9 != null)
                            {

                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score9,
                                    CurrentScore = (int?)(indicator9.Ratio.Point * score9.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }
                            else
                            {
                                description += "وضعیت اعلام نمرات-";

                                logFlag = true;


                            }
                        }

                        //resualList.Add(new KeyValuePair<string, string>(indicator9.Name, "انجام گردید"));
                    }
                    else
                    {
                        description += "وضعیت اعلام نمرات-";

                        logFlag = true;

                    }

                }
                catch (Exception)
                {
                    description += "وضعیت اعلام نمرات-";

                    logFlag = true;
                }
                #endregion

                #region ارائه به موقع سوالات امتحانی

                try
                {
                    var indicator10 = indicatorService
                        .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.ارائه_به_موقع_سوالات_امتحانی));
                    var scores10 = indicator10.Scores;

                    var loadingQuestion = new List<KeyValuePair<int?, decimal?>>();
                    if (classList.Any())
                    {
                        foreach (var scList in classList)
                        {
                            var avg = new List<decimal?>();
                            foreach (var sc in scList)
                            {
                                if (sc.LoadingQuestionDate != null && sc.HoldingExamDate != null)
                                {
                                    var dayDiff = (sc.HoldingExamDate - sc.LoadingQuestionDate).Value.Days;

                                    //if (dayDiff >= 0)
                                    avg.Add(dayDiff);
                                }
                                else
                                    avg.Add((scores10.FirstOrDefault(f => f.Point == 90).MinValue) + 1);

                            }
                            var average = avg.Average(p => p);
                            //if (average >= 0)
                            loadingQuestion.Add(new KeyValuePair<int?, decimal?>(scList.Key.Id, average));
                            //else
                            //{
                            //    description += "ارائه به موقع سوالات امتحانی-";

                            //    logFlag = true;

                            //}
                        }
                    }
                    else
                    {
                        description += "ارائه به موقع سوالات امتحانی-";

                        logFlag = true;


                    }

                    if (groupList.Any())
                    {
                        foreach (var i in groupList)
                        {
                            var roundedLoadingQuestion = Math.Round((decimal)loadingQuestion.FirstOrDefault(z => z.Key == i.Id).Value);
                            var score10 = scores10
                                .FirstOrDefault(p => p.MinValue <= roundedLoadingQuestion
                                                     && p.MaxValue >= roundedLoadingQuestion);
                            if (score10 != null)
                            {

                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score10,
                                    CurrentScore = (int?)(indicator10.Ratio.Point * score10.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }
                            else
                            {
                                description += "ارائه به موقع سوالات امتحانی-";

                                logFlag = true;


                            }
                        }
                        // resualList.Add(new KeyValuePair<string, string>(indicator10.Name, "انجام گردید"));
                    }
                    else
                    {
                        description += "ارائه به موقع سوالات امتحانی-";

                        logFlag = true;


                    }

                }
                catch (Exception ex)
                {
                    description += "ارائه به موقع سوالات امتحانی-";

                    logFlag = true;
                }

                if (logFlag) SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_استاد, $"{x.ProfessoreCode}-{x.Term}-{description}");

                #endregion

                #region عودت اوراق امتحانی
                try
                {
                    var indicator = indicatorService
                        .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.عودت_اوراق_امتحانی));

                    var declaringScore = new List<KeyValuePair<int?, decimal?>>();
                    var scores = indicator.Scores;
                    if (classList.Any())
                    {
                        foreach (var scList in classList)
                        {
                            var avg = new List<decimal?>();
                            foreach (var sc in scList)
                            {
                                if (sc.HoldingExamDate != null)
                                {
                                    if (sc.ExamPapersRestoreDate != null && sc.ExamPapersAggregationDate != null)// && sc.DeclaingScoreDate != null
                                    {
                                        var dayDiff = (sc.ExamPapersRestoreDate - sc.ExamPapersAggregationDate).Value.Days;

                                        //if (dayDiff >= 0)
                                        avg.Add(dayDiff);
                                    }
                                    else
                                    {
                                        avg.Add(scores.FirstOrDefault(f => f.Point == 0).MinValue);
                                    }
                                }
                            }
                            var average = avg.Average(p => p);
                            //if (average >= 0)
                            declaringScore.Add(new KeyValuePair<int?, decimal?>(scList.Key.Id, average));
                            //else
                            //{
                            //    description += "عودت اوراق امتحانی-";
                            //    logFlag = true;
                            //}
                        }
                    }
                    else
                    {
                        description += "عودت اوراق امتحانی-";
                        logFlag = true;
                    }
                    if (groupList.Any())
                    {
                        foreach (var i in groupList)
                        {
                            var roundedDeclaringScore = Math.Round((decimal)declaringScore.FirstOrDefault(z => z.Key == i.Id).Value);
                            var score = scores
                                .FirstOrDefault(p => p.MinValue <= roundedDeclaringScore
                                                     && p.MaxValue >= roundedDeclaringScore);
                            if (score != null)
                            {

                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score,
                                    CurrentScore = (int?)(indicator.Ratio.Point * score.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }
                            else
                            {
                                description += "عودت اوراق امتحانی-";

                                logFlag = true;


                            }
                        }
                    }
                    else
                    {
                        description += "عودت اوراق امتحانی-";
                        logFlag = true;
                    }
                }
                catch (Exception)
                {
                    description += "عودت اوراق امتحانی-";
                    logFlag = true;
                }
                #endregion

                #region امضاء قرارداد آموزشی
                try
                {
                    var term = termService.Get(y => y.TermCode == x.Term);
                    if (term.ExamStartDate != null)
                    {
                        var indicator = indicatorService.Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.امضاء_قرارداد_آموزشی));
                        var scores = indicator.Scores;
                        //var dayDiff = ((TimeSpan)(term.ExamStartDate - x.ContractSignDate)).Days;
                        var point = 0;

                        if(x.UniversityWorkPlaceId != null && x.UniversityWorkPlaceId == 263)
                        {
                            point = 0;
                        }
                        else if (x.ContractSignDate != null)
                        {
                            if (((TimeSpan)(term.ExamStartDate - x.ContractSignDate)).Days >= 14)
                                point = 0;
                            else if (((TimeSpan)(x.ContractSignDate - term.ExamEndDate)).Days >= 10)
                                point = 1;
                            else
                                point = 2;
                        }
                        else
                            point = 2;
                        if (groupList.Any())
                        {
                            foreach (var i in groupList)
                            {
                                var score = scores.FirstOrDefault(p => p.MinValue <= point && p.MaxValue >= point);
                                if (score != null)
                                {
                                    var professorScore = new ProfessorScore()
                                    {
                                        Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                              y.Term.TermCode == x.Term),
                                        EducationalGroup = i,
                                        Term = term,
                                        Score = score,
                                        CurrentScore = (int?)(indicator.Ratio.Point * score.Point)
                                    };
                                    if (!professorScoreService.IsExist(
                                        z =>
                                            z.Professor.Id == professorScore.Professor.Id &&
                                            z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                            z.Term.Id == professorScore.Term.Id &&
                                            z.Score.Id == professorScore.Score.Id &&
                                            z.CurrentScore == professorScore.CurrentScore
                                    ))
                                        professorScoreService.Add(professorScore);
                                }
                                else
                                {
                                    description += "امضاء قرارداد آموزشی-";
                                    logFlag = true;
                                }
                            }
                        }
                        else
                        {
                            description += "امضاء قرارداد آموزشی-";
                            logFlag = true;
                        }
                    }
                    else
                    {
                        description += "امضاء قرارداد آموزشی-";
                        logFlag = true;
                    }
                }
                catch (Exception)
                {
                    description += "امضاء قرارداد آموزشی-";
                    logFlag = true;
                }
                #endregion

                #region ارائه طرح درس
                try
                {
                    var indicator = indicatorService.Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.ارائه_طرح_درس));
                    var scores = indicator.Scores;
                    var hasLessonPlan = 0;
                    var classCount = 0;
                    if (classList.Any())
                    {
                        foreach (var scList in classList)
                        {
                            foreach (var sc in scList)
                            {
                                classCount++;
                                if (sc.LessonPlanSendDate != null)
                                    hasLessonPlan++;
                            }
                        }
                    }
                    else
                    {
                        description += "ارائه طرح درس-";
                        logFlag = true;
                    }

                    if (groupList.Any())
                    {
                        foreach (var i in groupList)
                        {
                            var percent = hasLessonPlan / classCount * 100;
                            var score = scores.FirstOrDefault(p => p.MinValue <= percent && p.MaxValue >= percent);
                            if (score != null)
                            {
                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode && y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score,
                                    CurrentScore = (int?)(indicator.Ratio.Point * score.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }
                            else
                            {
                                description += "ارائه طرح درس-";
                                logFlag = true;
                            }
                        }
                    }
                    else
                    {
                        description += "ارائه طرح درس-";
                        logFlag = true;
                    }
                }
                catch (Exception)
                {
                    description += "ارائه طرح درس-";
                    logFlag = true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_استاد, $"{x.ProfessoreCode}-{x.Term}");
            }


        }

        public static void SyncGroupManagerProfessorScores(IProfessorService professorService, IProfessorScoreService professorScoreService, IIndicatorService indicatorService
            , ITermService termService, IMappingService mappingService, IMappingTypeService mappingTypeService, IEducationalClassService educationalClassService
            , IUniversityLevelMappingService universityLevelMappingService, ProfessorSyncModel x, ILogService logService, ILogTypeService logTypeService, IUserService userService
            , User user, IEducationalGroupService educationalGroupService)
        {
            var description = string.Empty;
            var logFlag = false;
            try
            {
                #region مرتبه علمی

                try
                {
                    if (x.ScientificRank != null)
                    {
                        var indicator1 = indicatorService.Get(y => y.CountOfType ==
                                                                   ("p" + (int)IndicatorProfessorName.مرتبه_علمی));
                        var scores1 = indicator1.Scores;
                        Mapping map = null;
                        if (x.ScientificRank != 0)
                            map = mappingService
                                .GetMany(y => y.MappingType.Name == "مرتبه علمی")
                                .FirstOrDefault(y => y.TypeId == x.ScientificRank);
                        else if (x.ScientificRank == 0 && (x.AcademicDegree == 1 || x.AcademicDegree == 2 || x.AcademicDegree == 3 || x.AcademicDegree == 4))
                            map = mappingService
                                .GetMany(y => y.MappingType.Name == "مرتبه علمی")
                                .FirstOrDefault(y => y.TypeId == 1);
                        var score1 = scores1.FirstOrDefault(y => y.Name == map?.TypeName);
                        var groupList = educationalGroupService.GetMany(w => w.GroupManger.ProfessorCode == x.ProfessoreCode && w.Term.TermCode == x.Term);
                        if (groupList.Any() && score1 != null)
                        {
                            foreach (var i in groupList)
                            {

                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score1,
                                    CurrentScore = (int?)(indicator1.Ratio.Point * score1.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                    z.Professor.Id == professorScore.Professor.Id &&
                                    z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                    z.Term.Id == professorScore.Term.Id &&
                                    z.Score.Id == professorScore.Score.Id &&
                                    z.CurrentScore == professorScore.CurrentScore
                                    ))
                                    professorScoreService.Add(professorScore);
                            }

                            //resualList.Add(new KeyValuePair<string, string>(indicator1.Name, "انجام گردید"));
                        }
                        else
                        {
                            description += "مرتبه علمی-";

                            logFlag = true;

                        }
                    }
                    else
                    {
                        description += "مرتبه علمی-";

                        logFlag = true;
                    }
                }
                catch (Exception ex)
                {
                    description += "مرتبه علمی-";

                    logFlag = true;
                }

                #endregion

                #region  سابقه تدریس

                try
                {
                    if (x.TeachingExperience != null && x.TeachingExperience != 0)
                    {
                        var indicator2 = indicatorService.Get(y => y.CountOfType ==
                                                                   ("p" + (int)IndicatorProfessorName.سابقه_تدریس));

                        var scores2 = indicator2.Scores;
                        var score2 = scores2
                            .FirstOrDefault(y => y.MinValue <= x.TeachingExperience && y.MaxValue >= x.TeachingExperience);
                        var groupList = educationalGroupService.GetMany(w => w.GroupManger.ProfessorCode == x.ProfessoreCode && w.Term.TermCode == x.Term);
                        if (groupList.Any() && score2 != null)
                        {
                            foreach (var i in groupList)
                            {
                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score2,
                                    CurrentScore = (int?)(indicator2.Ratio.Point * score2.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);

                            }
                            //resualList.Add(new KeyValuePair<string, string>(indicator2.Name, "انجام گردید"));
                        }
                        else
                        {
                            description += "سابقه تدریس-";

                            logFlag = true;

                        }
                    }
                    else
                    {
                        description += "سابقه تدریس-";
                        logFlag = true;

                    }

                }
                catch (Exception)
                {
                    description += "سابقه تدریس-";
                    logFlag = true;
                }
                #endregion

                #region میزان تحصیلات
                try
                {
                    if (x.AcademicDegree != null && x.AcademicDegree != 0)
                    {
                        var indicator3 = indicatorService.Get(y => y.CountOfType ==
                                                                   ("p" + (int)IndicatorProfessorName.میزان_تحصیلات));
                        var map3 = mappingService
                            .GetMany(y => y.MappingType.Name == "میزان تحصیلات")
                            .FirstOrDefault(y => y.TypeId == x.AcademicDegree);
                        var scores3 = indicator3.Scores;
                        var score3 = scores3.FirstOrDefault(y => y.Name == map3?.TypeName);
                        var groupList = educationalGroupService.GetMany(w => w.GroupManger.ProfessorCode == x.ProfessoreCode && w.Term.TermCode == x.Term);
                        if (groupList.Any() && score3 != null)
                        {
                            foreach (var i in groupList)
                            {
                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score3,
                                    CurrentScore = (int?)(indicator3.Ratio.Point * score3.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }

                            //resualList.Add(new KeyValuePair<string, string>(indicator3.Name, "انجام گردید"));
                        }
                        else
                        {
                            description += "میزان تحصیلات-";

                            logFlag = true;

                        }

                    }
                    else
                    {
                        description += "میزان تحصیلات-";

                        logFlag = true;
                    }

                }
                catch (Exception)
                {
                    description += "میزان تحصیلات-";

                    logFlag = true;
                }
                #endregion

                // dependency - UniversityLevelMappings
                #region رتبه دانشگاه محل تحصیل
                try
                {
                    if (x.UniversityStudyPlace != null && x.UniversityStudyPlace != 0)
                    {
                        var indicator4 = indicatorService
                            .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.رتبه_دانشگاه_محل_تحصیل));

                        //var uid = mappingService.Get(g => g.MappingType.Id == 4 && g.TypeId == x.UniversityStudyPlace).Id;
                        var score4 = universityLevelMappingService
                            .GetMany(y => y.UniversityId == x.UniversityStudyPlace)//x.UniversityStudyPlace
                            .FirstOrDefault()?.Score;
                        var groupList = educationalGroupService.GetMany(w => w.GroupManger.ProfessorCode == x.ProfessoreCode && w.Term.TermCode == x.Term);
                        if (groupList.Any() && score4 != null)
                        {
                            foreach (var i in groupList)
                            {
                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score4,
                                    CurrentScore = (int?)(indicator4.Ratio.Point * score4.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }

                            //resualList.Add(new KeyValuePair<string, string>(indicator4.Name, "انجام گردید"));
                        }
                        else
                        {
                            description += "رتبه دانشگاه محل تحصیل-";

                            logFlag = true;
                        }

                    }
                    else
                    {
                        description += "رتبه دانشگاه محل تحصیل-";
                        logFlag = true;
                    }

                }
                catch (Exception)
                {
                    description += "رتبه دانشگاه محل تحصیل-";
                    logFlag = true;
                }
                #endregion

                // dependency - UniversityLevelMappings
                #region دانشگاه محل خدمت

                try
                {
                    if (x.UniversityWorkPlace != null && x.UniversityWorkPlace != 0)
                    {
                        var indicator5 = indicatorService
                            .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.دانشگاه_محل_خدمت));


                        var map5 = mappingService
                            .GetMany(y => y.MappingType.Name == "نوع دانشگاه")
                            .FirstOrDefault(y => y.TypeId == x.UniversityWorkPlace);
                        var scores5 = indicator5.Scores;
                        var score5 = scores5.FirstOrDefault(y => y.Name == map5?.TypeName);
                        var groupList = educationalGroupService.GetMany(w => w.GroupManger.ProfessorCode == x.ProfessoreCode && w.Term.TermCode == x.Term);
                        if (groupList.Any() && score5 != null)
                        {
                            foreach (var i in groupList)
                            {

                                var professorScore = new ProfessorScore()
                                {
                                    Professor = professorService.Get(y => y.ProfessorCode == x.ProfessoreCode &&
                                                                          y.Term.TermCode == x.Term),
                                    EducationalGroup = i,
                                    Term = termService.Get(y => y.TermCode == x.Term),
                                    Score = score5,
                                    CurrentScore = (int?)(indicator5.Ratio.Point * score5.Point)
                                };
                                if (!professorScoreService.IsExist(
                                    z =>
                                        z.Professor.Id == professorScore.Professor.Id &&
                                        z.EducationalGroup.Id == professorScore.EducationalGroup.Id &&
                                        z.Term.Id == professorScore.Term.Id &&
                                        z.Score.Id == professorScore.Score.Id &&
                                        z.CurrentScore == professorScore.CurrentScore
                                ))
                                    professorScoreService.Add(professorScore);
                            }

                            //resualList.Add(new KeyValuePair<string, string>(indicator5.Name, "انجام گردید"));
                        }
                        else
                        {
                            description += "دانشگاه محل خدمت-";

                            logFlag = true;
                        }

                    }
                    else
                    {
                        description += "دانشگاه محل خدمت-";

                        logFlag = true;
                    }

                }
                catch (Exception)
                {
                    description += "دانشگاه محل خدمت-";

                    logFlag = true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                SyncService.LogSync(logService, logTypeService, userService, user, (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_استاد, $"{x.ProfessoreCode}-{x.Term}");
            }
        }



        //public static ReturnValue Run(IProfessorService professorService,
        //    IProfessorScoreService professorScoreService,
        //    IIndicatorService indicatorService,
        //    ITermService termService,
        //    IMappingService mappingService,
        //    IMappingTypeService mappingTypeService,
        //    IEducationalClassService educationalClassService,
        //    IUniversityLevelMappingService universityLevelMappingService
        //    , ILogService logService, ILogTypeService logTypeService, IUserService userService, User user)
        //{
        //    //The order of the execution on the commands is important
        //    //First-remove
        //    //Second-addOrUpdate
        //    //Third-remove score
        //    //fourth-addOrUpdate score

        //    //var remove = SyncRemoveProfessor(professorService);

        //    SyncAddOrUpdateProfessor(professorService, logService, logTypeService, userService, user);

        //     SyncProfessorRemoveScore(professorService, logService, logTypeService, userService, user);

        //    SyncProfessorAddScore(professorService, professorScoreService, indicatorService, termService, mappingService, mappingTypeService, educationalClassService, universityLevelMappingService, logService, logTypeService, userService, user);


        //   // var resualt = new ReturnValue
        //    //{
        //        //Remove = remove,
        //        //AddOrUpdate = addOrUpdate,
        //        //RemoveScore = removeScore,
        //       // AddOrUpdateScore = addOrUpdateScore
        //    //};
        //   // return resualt;
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