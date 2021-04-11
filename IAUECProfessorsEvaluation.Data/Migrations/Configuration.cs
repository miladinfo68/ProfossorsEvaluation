using System;
using System.Collections.Generic;
using System.Linq;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ContextEntities.ProfessorsEvaluationEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContextEntities.ProfessorsEvaluationEntities context)
        {
            #region First Seed Data
            //new List<Term>()
            //{
            //    new Term{Name ="ترم دوم سال 96 ", TermCode = "95-96-2",IsActive = true,CreationDate = DateTime.Now},
            //    new Term{Name ="ترم اول سال 97", TermCode = "96-97-1",IsActive = true,CreationDate = DateTime.Now},
            //    new Term{Name ="ترم دوم سال 97", TermCode = "96-97-2",IsActive = true,CreationDate = DateTime.Now}

            //}.ForEach(m => context.Terms.AddOrUpdate(x => x.Name, m));
            //new List<Ratio>
            //{
            //    new Ratio{Name = "خیلی زیاد",CreationDate = DateTime.Now,Point = 5,IsActive = true},
            //    new Ratio{Name = "زیاد",CreationDate = DateTime.Now,Point = 4,IsActive = true},
            //    new Ratio{Name = "متوسط",CreationDate = DateTime.Now,Point = 3,IsActive = true},
            //    new Ratio{Name = "کم",CreationDate = DateTime.Now,Point = 2,IsActive = true},
            //    new Ratio{Name = "خیلی کم",CreationDate = DateTime.Now,Point = 1,IsActive = true},
            //}.ForEach(m => context.Ratios.AddOrUpdate(x => x.Name, m));
            //new List<ObjectType>
            //{
            //    new ObjectType{Name = "اساتید",CreationDate = DateTime.Now,IsActive = true},
            //    new ObjectType{Name = "گروهای آموزشی",CreationDate = DateTime.Now,IsActive = true},
            //    new ObjectType{Name = "دانشکده",CreationDate = DateTime.Now,IsActive = true}
            //}.ForEach(m => context.ObjectTypes.AddOrUpdate(x => x.Name, m));
            #endregion

            #region Second Seed Data
            //new List<Indicator>
            //{
            //    new Indicator
            //    {
            //        Name = "مرتبه علمی",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="خیلی زیاد")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "استاد",IsActive = true,CreationDate = DateTime.Now,Point = 100 },
            //            new Score{Name = "دانشیار",IsActive = true,CreationDate = DateTime.Now,Point = 95 },
            //            new Score{Name = "استادیار",IsActive = true,CreationDate = DateTime.Now,Point = 90 },
            //            new Score{Name = "مربی",IsActive = true,CreationDate = DateTime.Now,Point = 70 },
            //            new Score{Name = "فاقد مرتبه علمی",IsActive = true,CreationDate = DateTime.Now,Point = 60 }
            //        }
            //    },
            //    new Indicator
            //    {
            //        Name = "سابقه تدریس",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="زیاد")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "یک الی پنج",IsActive = true,CreationDate = DateTime.Now,Point = 80, MinValue = 1,MaxValue = 5},
            //            new Score{Name = "شش الی ده",IsActive = true,CreationDate = DateTime.Now,Point = 85 ,MinValue = 6,MaxValue = 10},
            //            new Score{Name = "یازده الی پانزده",IsActive = true,CreationDate = DateTime.Now,Point = 90 ,MinValue = 11,MaxValue = 15},
            //            new Score{Name = "شانزده الی بیست",IsActive = true,CreationDate = DateTime.Now,Point = 95 ,MinValue = 16,MaxValue = 20},
            //            new Score{Name = "بیست و یک به بالا",IsActive = true,CreationDate = DateTime.Now,Point = 100 ,MinValue = 21}
            //        }
            //    },
            //    new Indicator
            //    {
            //        Name = "میزان تحصیلات",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="خیلی زیاد")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "دکتری",IsActive = true,CreationDate = DateTime.Now,Point = 100 },
            //            new Score{Name = "دانشجوی دکتری بعد از جامع",IsActive = true,CreationDate = DateTime.Now,Point = 80 },
            //            new Score{Name = "دانشجوی دکترا قبل از جامع",IsActive = true,CreationDate = DateTime.Now,Point = 70 },
            //            new Score{Name = "کارشناسی ارشد",IsActive = true,CreationDate = DateTime.Now,Point = 40 }
            //        }
            //    },
            //    new Indicator
            //    {
            //        Name = "رتبه دانشگاه محل تحصیل",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="متوسط")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "دولتی-سطح یک",IsActive = true,CreationDate = DateTime.Now,Point = 100 },
            //            new Score{Name = "دولتی-سطح دو",IsActive = true,CreationDate = DateTime.Now,Point = 90 },
            //            new Score{Name = "دولتی-سطح سه",IsActive = true,CreationDate = DateTime.Now,Point = 70 },
            //            new Score{Name = "دولتی-سطح چهار",IsActive = true,CreationDate = DateTime.Now,Point = 60 },
            //            new Score{Name = "آزاد-سطح یک",IsActive = true,CreationDate = DateTime.Now,Point = 100 },
            //            new Score{Name = "آزاد-سطح دو",IsActive = true,CreationDate = DateTime.Now,Point = 90 },
            //            new Score{Name = "آزاد-سطح سه",IsActive = true,CreationDate = DateTime.Now,Point = 80 },
            //            new Score{Name = "آزاد-سطح جهار",IsActive = true,CreationDate = DateTime.Now,Point = 70 },
            //            new Score{Name = "آزاد-سطح پنج",IsActive = true,CreationDate = DateTime.Now,Point = 60 },
            //            new Score{Name = "آزاد-سطح شش",IsActive = true,CreationDate = DateTime.Now,Point = 50 },
            //            new Score{Name = "حوزه",IsActive = true,CreationDate = DateTime.Now,Point = 100 },
            //            new Score{Name = "خارج از کشور",IsActive = true,CreationDate = DateTime.Now,Point = 90 },
            //            new Score{Name = "سایر",IsActive = true,CreationDate = DateTime.Now,Point = 70 },
            //        }
            //    },
            //    new Indicator
            //    {
            //        Name = "دانشگاه محل خدمت",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="زیاد")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "آزاد",IsActive = true,CreationDate = DateTime.Now,Point = 100 },
            //            new Score{Name = "دولتی",IsActive = true,CreationDate = DateTime.Now,Point = 90 },
            //            new Score{Name = "سایر",IsActive = true,CreationDate = DateTime.Now,Point = 60 },
            //            new Score{Name = "غیر هیات علمی",IsActive = true,CreationDate = DateTime.Now,Point = 50 },
            //        }
            //    },
            //    new Indicator
            //    {
            //        Name = "ارزشیابی پایان ترم",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="خیلی زیاد")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "کمتر از پانزده و نیم",IsActive = true,CreationDate = DateTime.Now,Point = 0, MinValue = 0,MaxValue = 15/49},
            //            new Score{Name = "پانزده و نیم الی پانزده و نود نه",IsActive = true,CreationDate = DateTime.Now,Point = 40 ,MinValue = 15/5,MaxValue = 15/99},
            //            new Score{Name = "شانزده الی شانزده و نود ونه",IsActive = true,CreationDate = DateTime.Now,Point = 70 ,MinValue = 16,MaxValue = 16/99},
            //            new Score{Name = "هفتده الی هفتده و نود و نه",IsActive = true,CreationDate = DateTime.Now,Point = 80 ,MinValue = 17,MaxValue = 17/99},
            //            new Score{Name = "هجده الی هجده و نود ونه",IsActive = true,CreationDate = DateTime.Now,Point = 90 ,MinValue =18,MaxValue = 18/99},
            //            new Score{Name = "نوزده الی نوزده و نود ونه",IsActive = true,CreationDate = DateTime.Now,Point = 100 ,MinValue = 19 , MaxValue = 20}
            //        }

            //    },
            //    new Indicator
            //    {
            //        Name = "تعداد جلسات برگزار شده کلاس",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="خیلی زیاد")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "پانزده جلسه به بالا",IsActive = true,CreationDate = DateTime.Now,Point = 100, MinValue = 15 },
            //            new Score{Name = "دوازده الی چهارده",IsActive = true,CreationDate = DateTime.Now,Point = 80 ,MinValue = 12,MaxValue = 14},
            //            new Score{Name = "نه الی یازده",IsActive = true,CreationDate = DateTime.Now,Point = 60 ,MinValue = 9,MaxValue = 11},
            //            new Score{Name = "شش الی هشت",IsActive = true,CreationDate = DateTime.Now,Point = 30 ,MinValue = 6,MaxValue = 8},
            //            new Score{Name = "کمتر از شش",IsActive = true,CreationDate = DateTime.Now,Point = 0 ,MinValue = 0,MaxValue = 6}
            //        }
            //    },
            //    new Indicator
            //    {
            //        Name = "وضعیت ورود و خروج استاد در کلاس",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="خیلی زیاد")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "کمتر از ده",IsActive = true,CreationDate = DateTime.Now,Point = 100,MinValue = 0,MaxValue = 9/99},
            //            new Score{Name = "بین ده تا بیست",IsActive = true,CreationDate = DateTime.Now,Point = 90 ,MinValue = 10,MaxValue = 19/99},
            //            new Score{Name = "بین بیست تا سی",IsActive = true,CreationDate = DateTime.Now,Point = 80 ,MinValue = 20,MaxValue = 29/99},
            //            new Score{Name = "بین سی تا جهل",IsActive = true,CreationDate = DateTime.Now,Point = 60 ,MinValue = 30,MaxValue = 39/99},
            //            new Score{Name = "چهل و بیشتر",IsActive = true,CreationDate = DateTime.Now,Point = 20 ,MinValue = 40}
            //        }
            //    },
            //    new Indicator
            //    {
            //        Name = "وضعیت اعلام نمرات",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="خیلی زیاد")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "بین چهارده الی بیست و یک",IsActive = true,CreationDate = DateTime.Now,Point = 100, MinValue = 14,MaxValue = 20/99},
            //            new Score{Name = "بین بیست و یک تا بیست و هشت",IsActive = true,CreationDate = DateTime.Now,Point = 80 ,MinValue = 21,MaxValue = 27/99},
            //            new Score{Name = "بین بیست و هشت تا سی و پنج",IsActive = true,CreationDate = DateTime.Now,Point = 60 ,MinValue = 28,MaxValue = 34/99},
            //            new Score{Name = "بیشتر از سی و پنج",IsActive = true,CreationDate = DateTime.Now,Point = 20 ,MinValue = 35},
            //        }
            //    },
            //    new Indicator
            //    {
            //        Name = "ارائه به موقع سوالات امتحانی",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="زیاد")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "کمتر از هفت",IsActive = true,CreationDate = DateTime.Now,Point = 100,MinValue = 0,MaxValue = 6/99},
            //            new Score{Name = "بین هفت تا چهارده",IsActive = true,CreationDate = DateTime.Now,Point = 80 ,MinValue = 7,MaxValue = 13/99},
            //            new Score{Name = "چهارده و بیشتر",IsActive = true,CreationDate = DateTime.Now,Point = 20 ,MinValue = 14},
            //        }
            //    },
            //    new Indicator
            //    {
            //        Name = "وضعیت دسترسی به ساتاد و تعامل با دانشکده",IsActive = true,CreationDate = DateTime.Now,ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید"),Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="زیاد")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "عالی",IsActive = true,CreationDate = DateTime.Now,Point = 100 },
            //            new Score{Name = "بسیار خوب",IsActive = true,CreationDate = DateTime.Now,Point = 90 },
            //            new Score{Name = "خوب",IsActive = true,CreationDate = DateTime.Now,Point = 80 },
            //            new Score{Name = "متوسط",IsActive = true,CreationDate = DateTime.Now,Point = 70 },
            //            new Score{Name = "ضعیف",IsActive = true,CreationDate = DateTime.Now,Point = 40 }
            //        }
            //    },
            //    new Indicator
            //    {
            //        Name = "نظر مدیر گروه",IsActive = true,CreationDate = DateTime.Now,Ratio = context.Ratios.FirstOrDefault(x=>x.Name=="زیاد"),ObjectType = context.ObjectTypes.FirstOrDefault(x=>x.Name=="اساتید")
            //        ,Scores = new List<Score>()
            //        {
            //            new Score{Name = "عالی",IsActive = true,CreationDate = DateTime.Now,Point = 100 },
            //            new Score{Name = "بسیار خوب",IsActive = true,CreationDate = DateTime.Now,Point = 90 },
            //            new Score{Name = "خوب",IsActive = true,CreationDate = DateTime.Now,Point = 70 },
            //            new Score{Name = "متوسط",IsActive = true,CreationDate = DateTime.Now,Point = 50 },
            //            new Score{Name = "ضعیف",IsActive = true,CreationDate = DateTime.Now,Point = 0 }
            //        }
            //    },
            //}.ForEach(m => context.Indicators.AddOrUpdate(x => x.Name, m));
            #endregion

            #region Third Seed Data
            //new List<College>()
            //{
            //    new College{Name = "علوم انسانی",IsActive = true,CreationDate = DateTime.Now,CollegeCode = 1},
            //    new College{Name = "فنی مهندسی",IsActive = true,CreationDate = DateTime.Now,CollegeCode = 2}
            //}.ForEach(m => context.Colleges.AddOrUpdate(x => x.Name, m));
            #endregion

            #region  Fourth Seed Data

            //new List<EducationalGroup>()
            //{
            //        new EducationalGroup{Name = "حقوق",IsActive = true,CreationDate = DateTime.Now,EducationalGroupCode = 1,College = context.Colleges.FirstOrDefault(x=>x.CollegeCode==1)},
            //        new EducationalGroup{Name = "رونشناسی",IsActive = true,CreationDate = DateTime.Now,EducationalGroupCode = 2,College = context.Colleges.FirstOrDefault(x=>x.CollegeCode==1)},
            //        new EducationalGroup{Name = "مدیریت",IsActive = true,CreationDate = DateTime.Now,EducationalGroupCode = 3,College = context.Colleges.FirstOrDefault(x=>x.CollegeCode==1)},

            //        new EducationalGroup{Name = "کامپیوتر و فناوری اطلاعات",IsActive = true,CreationDate = DateTime.Now,EducationalGroupCode = 4,College = context.Colleges.FirstOrDefault(x=>x.CollegeCode==2),},
            //        new EducationalGroup{Name = "عمران",IsActive = true,CreationDate = DateTime.Now,EducationalGroupCode = 5,College = context.Colleges.FirstOrDefault(x=>x.CollegeCode==2)},
            //        new EducationalGroup{Name = "برق",IsActive = true,CreationDate = DateTime.Now,EducationalGroupCode = 6,College = context.Colleges.FirstOrDefault(x=>x.CollegeCode==2)},
            //}.ForEach(m => context.EducationalGroups.AddOrUpdate(x => x.Name, m));


            #endregion

            #region Fithe Seed Data

            //new List<Professor>()
            //{
            //    new Professor{Name = "استاد 1",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 1",ProfessorCode = 1, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 1).ToList()},
            //    new Professor{Name = "استاد 2",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 2",ProfessorCode = 2, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 2).ToList()},
            //    new Professor{Name = "استاد 3",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 3",ProfessorCode = 3, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 3).ToList()},
            //    new Professor{Name = "استاد 4",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 4",ProfessorCode = 4, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 4).ToList()},

            //    new Professor{Name = "استاد 5",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 5",ProfessorCode = 5, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 5).ToList()},
            //    new Professor{Name = "استاد 6",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 6",ProfessorCode = 6, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 6).ToList()},
            //    new Professor{Name = "استاد 7",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 7",ProfessorCode = 7, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 7).ToList()},
            //    new Professor{Name = "استاد 8",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 8",ProfessorCode = 8, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 8).ToList()},

            //    new Professor{Name = "استاد 9",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 9",ProfessorCode = 9, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 9).ToList()},
            //    new Professor{Name = "استاد 10",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 10",ProfessorCode = 10, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 10).ToList()},
            //    new Professor{Name = "استاد 11",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 11",ProfessorCode = 11, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 11).ToList()},
            //    new Professor{Name = "استاد 12",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 12",ProfessorCode = 12, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 12).ToList()},


            //    new Professor{Name = "استاد 13",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 13",ProfessorCode = 13, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 13).ToList()},
            //    new Professor{Name = "استاد 14",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 14",ProfessorCode = 14, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 14).ToList()},
            //    new Professor{Name = "استاد 15",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 15",ProfessorCode = 15, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 15).ToList()},
            //    new Professor{Name = "استاد 16",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 16",ProfessorCode = 16, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 16).ToList()},


            //    new Professor{Name = "استاد 17",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 17",ProfessorCode = 17, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 17).ToList()},
            //    new Professor{Name = "استاد 18",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 18",ProfessorCode = 18, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 18).ToList()},
            //    new Professor{Name = "استاد 19",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 19",ProfessorCode = 19, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 19).ToList()},
            //    new Professor{Name = "استاد 20",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 20",ProfessorCode = 20, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 20).ToList()},


            //    new Professor{Name = "استاد 21",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 21",ProfessorCode = 21, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 21).ToList()},
            //    new Professor{Name = "استاد 22",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 22",ProfessorCode = 22, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 22).ToList()},
            //    new Professor{Name = "استاد 23",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 23",ProfessorCode = 23, ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 23).ToList()},
            //    new Professor{Name = "استاد 24",IsActive = true,CreationDate = DateTime.Now,Family = "فامیل 24",ProfessorCode = 24 , ProfessorScores = context.ProfessorScores.Where(x=>x.Professor.Id == 24).ToList()}




            //}.ForEach(m => context.Professors.AddOrUpdate(x => x.ProfessorCode, m));

            #endregion

            #region Six Seed Data

            //new List<EducationalClass>()
            //{

            //new EducationalClass{Name = "کلاس 1",IsActive = true,CreationDate = DateTime.Now,CodeClass = 1,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==1),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 2",IsActive = true,CreationDate = DateTime.Now,CodeClass = 2,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==1),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 3",IsActive = true,CreationDate = DateTime.Now,CodeClass = 3,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==1),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 4",IsActive = true,CreationDate = DateTime.Now,CodeClass = 4,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==1),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},

            //new EducationalClass{Name = "کلاس 5",IsActive = true,CreationDate = DateTime.Now,CodeClass = 5,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==2),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 6",IsActive = true,CreationDate = DateTime.Now,CodeClass = 6,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==2),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 7",IsActive = true,CreationDate = DateTime.Now,CodeClass = 7,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==2),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 8",IsActive = true,CreationDate = DateTime.Now,CodeClass = 8,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==2),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},

            //new EducationalClass{Name = "کلاس 9",IsActive = true,CreationDate = DateTime.Now,CodeClass = 9,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==3),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 10",IsActive = true,CreationDate = DateTime.Now,CodeClass = 10,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==3),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 11",IsActive = true,CreationDate = DateTime.Now,CodeClass = 11,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==3),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 12",IsActive = true,CreationDate = DateTime.Now,CodeClass = 12,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==3),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},

            //new EducationalClass{Name = "کلاس 13",IsActive = true,CreationDate = DateTime.Now,CodeClass = 13,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==4),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 14",IsActive = true,CreationDate = DateTime.Now,CodeClass = 14,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==4),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 15",IsActive = true,CreationDate = DateTime.Now,CodeClass = 15,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==4),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 16",IsActive = true,CreationDate = DateTime.Now,CodeClass = 16,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==4),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==1).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},





            //new EducationalClass{Name = "کلاس 17",IsActive = true,CreationDate = DateTime.Now,CodeClass = 17,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==5),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 18",IsActive = true,CreationDate = DateTime.Now,CodeClass = 18,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==5),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 19",IsActive = true,CreationDate = DateTime.Now,CodeClass = 19,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==5),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 20",IsActive = true,CreationDate = DateTime.Now,CodeClass = 20,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==5),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},

            //new EducationalClass{Name = "کلاس 21",IsActive = true,CreationDate = DateTime.Now,CodeClass = 21,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==6),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 22",IsActive = true,CreationDate = DateTime.Now,CodeClass = 22,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==6),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 23",IsActive = true,CreationDate = DateTime.Now,CodeClass = 23,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==6),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 24",IsActive = true,CreationDate = DateTime.Now,CodeClass = 24,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==6),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},

            //new EducationalClass{Name = "کلاس 25",IsActive = true,CreationDate = DateTime.Now,CodeClass = 25,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==7),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 26",IsActive = true,CreationDate = DateTime.Now,CodeClass = 26,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==7),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 27",IsActive = true,CreationDate = DateTime.Now,CodeClass = 27,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==7),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 28",IsActive = true,CreationDate = DateTime.Now,CodeClass = 28,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==7),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},

            //new EducationalClass{Name = "کلاس 29",IsActive = true,CreationDate = DateTime.Now,CodeClass = 29,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==8),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 30",IsActive = true,CreationDate = DateTime.Now,CodeClass = 30,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==8),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 31",IsActive = true,CreationDate = DateTime.Now,CodeClass = 31,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==8),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 32",IsActive = true,CreationDate = DateTime.Now,CodeClass = 32,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==8),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==2).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},





            //new EducationalClass{Name = "کلاس 33",IsActive = true,CreationDate = DateTime.Now,CodeClass = 33,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==9),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 34",IsActive = true,CreationDate = DateTime.Now,CodeClass = 34,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==9),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 35",IsActive = true,CreationDate = DateTime.Now,CodeClass = 35,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==9),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 36",IsActive = true,CreationDate = DateTime.Now,CodeClass = 36,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==9),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},

            //new EducationalClass{Name = "کلاس 37",IsActive = true,CreationDate = DateTime.Now,CodeClass = 37,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==10),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 38",IsActive = true,CreationDate = DateTime.Now,CodeClass = 38,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==10),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 39",IsActive = true,CreationDate = DateTime.Now,CodeClass = 39,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==10),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 40",IsActive = true,CreationDate = DateTime.Now,CodeClass = 40,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==10),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},

            //new EducationalClass{Name = "کلاس 41",IsActive = true,CreationDate = DateTime.Now,CodeClass = 41,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==11),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 42",IsActive = true,CreationDate = DateTime.Now,CodeClass = 42,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==11),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 43",IsActive = true,CreationDate = DateTime.Now,CodeClass = 43,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==11),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 44",IsActive = true,CreationDate = DateTime.Now,CodeClass = 44,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==11),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},

            //new EducationalClass{Name = "کلاس 45",IsActive = true,CreationDate = DateTime.Now,CodeClass = 45,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==12),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 46",IsActive = true,CreationDate = DateTime.Now,CodeClass = 46,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==12),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 47",IsActive = true,CreationDate = DateTime.Now,CodeClass = 47,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==12),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},
            //new EducationalClass{Name = "کلاس 48",IsActive = true,CreationDate = DateTime.Now,CodeClass = 48,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==12),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==3).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==1).ToList()
            //},











            //new EducationalClass{Name = "کلاس 49",IsActive = true,CreationDate = DateTime.Now,CodeClass = 49,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==13),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 50",IsActive = true,CreationDate = DateTime.Now,CodeClass = 50,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==13),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 51",IsActive = true,CreationDate = DateTime.Now,CodeClass = 51,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==13),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 52",IsActive = true,CreationDate = DateTime.Now,CodeClass = 52,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==13),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},

            //new EducationalClass{Name = "کلاس 53",IsActive = true,CreationDate = DateTime.Now,CodeClass = 53,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==14),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 54",IsActive = true,CreationDate = DateTime.Now,CodeClass = 54,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==14),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 55",IsActive = true,CreationDate = DateTime.Now,CodeClass = 55,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==14),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 56",IsActive = true,CreationDate = DateTime.Now,CodeClass = 56,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==14),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},

            //new EducationalClass{Name = "کلاس 57",IsActive = true,CreationDate = DateTime.Now,CodeClass = 57,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==15),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 58",IsActive = true,CreationDate = DateTime.Now,CodeClass = 58,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==15),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 59",IsActive = true,CreationDate = DateTime.Now,CodeClass = 59,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==15),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 60",IsActive = true,CreationDate = DateTime.Now,CodeClass = 60,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==15),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},

            //new EducationalClass{Name = "کلاس 61",IsActive = true,CreationDate = DateTime.Now,CodeClass = 61,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==16),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 62",IsActive = true,CreationDate = DateTime.Now,CodeClass = 62,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==16),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 63",IsActive = true,CreationDate = DateTime.Now,CodeClass = 63,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==16),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 64",IsActive = true,CreationDate = DateTime.Now,CodeClass = 64,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==16),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==4).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},





            //new EducationalClass{Name = "کلاس 65",IsActive = true,CreationDate = DateTime.Now,CodeClass = 65,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==17),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 66",IsActive = true,CreationDate = DateTime.Now,CodeClass = 66,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==17),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 67",IsActive = true,CreationDate = DateTime.Now,CodeClass = 67,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==17),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 68",IsActive = true,CreationDate = DateTime.Now,CodeClass = 68,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==17),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},

            //new EducationalClass{Name = "کلاس 69",IsActive = true,CreationDate = DateTime.Now,CodeClass = 69,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==18),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 70",IsActive = true,CreationDate = DateTime.Now,CodeClass = 70,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==18),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 71",IsActive = true,CreationDate = DateTime.Now,CodeClass = 71,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==18),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 72",IsActive = true,CreationDate = DateTime.Now,CodeClass = 72,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==18),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},

            //new EducationalClass{Name = "کلاس 73",IsActive = true,CreationDate = DateTime.Now,CodeClass = 73,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==19),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 74",IsActive = true,CreationDate = DateTime.Now,CodeClass = 74,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==19),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 75",IsActive = true,CreationDate = DateTime.Now,CodeClass = 75,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==19),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 76",IsActive = true,CreationDate = DateTime.Now,CodeClass = 76,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==19),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},

            //new EducationalClass{Name = "کلاس 77",IsActive = true,CreationDate = DateTime.Now,CodeClass = 77,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==20),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 78",IsActive = true,CreationDate = DateTime.Now,CodeClass = 78,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==20),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 79",IsActive = true,CreationDate = DateTime.Now,CodeClass = 79,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==20),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 80",IsActive = true,CreationDate = DateTime.Now,CodeClass = 80,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==20),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==5).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},





            //new EducationalClass{Name = "کلاس 81",IsActive = true,CreationDate = DateTime.Now,CodeClass = 81,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==21),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 82",IsActive = true,CreationDate = DateTime.Now,CodeClass = 82,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==21),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 83",IsActive = true,CreationDate = DateTime.Now,CodeClass = 83,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==21),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 84",IsActive = true,CreationDate = DateTime.Now,CodeClass = 84,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==21),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},

            //new EducationalClass{Name = "کلاس 85",IsActive = true,CreationDate = DateTime.Now,CodeClass = 85,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==22),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 86",IsActive = true,CreationDate = DateTime.Now,CodeClass = 86,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==22),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 87",IsActive = true,CreationDate = DateTime.Now,CodeClass = 87,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==22),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 88",IsActive = true,CreationDate = DateTime.Now,CodeClass = 88,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==22),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},

            //new EducationalClass{Name = "کلاس 89",IsActive = true,CreationDate = DateTime.Now,CodeClass = 89,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==23),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 90",IsActive = true,CreationDate = DateTime.Now,CodeClass = 90,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==23),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 91",IsActive = true,CreationDate = DateTime.Now,CodeClass = 91,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==23),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 92",IsActive = true,CreationDate = DateTime.Now,CodeClass = 92,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==23),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},

            //new EducationalClass{Name = "کلاس 93",IsActive = true,CreationDate = DateTime.Now,CodeClass = 93,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==24),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 94",IsActive = true,CreationDate = DateTime.Now,CodeClass = 94,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==24),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 95",IsActive = true,CreationDate = DateTime.Now,CodeClass = 95,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==24),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //},
            //new EducationalClass{Name = "کلاس 96",IsActive = true,CreationDate = DateTime.Now,CodeClass = 96,
            //    Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==24),
            //    EducationalGroups = context.EducationalGroups.Where(x=>x.EducationalGroupCode==6).ToList(),
            //    Colleges = context.Colleges.Where(x=>x.CollegeCode==2).ToList()
            //}
            //}.ForEach(m => context.EducationalClasses.AddOrUpdate(x => x.Name, m));

            #endregion

            #region Seven Seed Data

            //new List<ProfessorScore>()
            //{
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==1),Score = context.Scores.FirstOrDefault(x=>x.Name =="استاد"),CurrentScore = 100*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==2),Score = context.Scores.FirstOrDefault(x=>x.Name =="دانشیار"),CurrentScore = 95*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==3),Score = context.Scores.FirstOrDefault(x=>x.Name =="استادیار"),CurrentScore = 90*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==4),Score = context.Scores.FirstOrDefault(x=>x.Name =="مربی"),CurrentScore = 70*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==5),Score = context.Scores.FirstOrDefault(x=>x.Name =="فاقد مرتبه علمی"),CurrentScore = 60*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},


            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==1),Score = context.Scores.FirstOrDefault(x=>x.Name =="یک الی پنج"),CurrentScore = 80*4, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==2),Score = context.Scores.FirstOrDefault(x=>x.Name =="شش الی ده"),CurrentScore = 85*4, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==3),Score = context.Scores.FirstOrDefault(x=>x.Name =="یازده الی پانزده"),CurrentScore = 90*4, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==4),Score = context.Scores.FirstOrDefault(x=>x.Name =="شانزده الی بیست"),CurrentScore = 95*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==5),Score = context.Scores.FirstOrDefault(x=>x.Name =="بیست و یک به بالا"),CurrentScore = 100*4, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},

            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==1),Score = context.Scores.FirstOrDefault(x=>x.Name =="دکتری"),CurrentScore = 100*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==2),Score = context.Scores.FirstOrDefault(x=>x.Name =="دانشجوی دکتری بعد از جامع"),CurrentScore = 80*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==3),Score = context.Scores.FirstOrDefault(x=>x.Name =="دانشجوی دکترا قبل از جامع"),CurrentScore = 70*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==4),Score = context.Scores.FirstOrDefault(x=>x.Name =="کارشناسی ارشد"),CurrentScore = 40*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==5),Score = context.Scores.FirstOrDefault(x=>x.Name =="دکتری"),CurrentScore = 100*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},



            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==1),Score = context.Scores.FirstOrDefault(x=>x.Name =="آزاد"),CurrentScore = 100*4, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==2),Score = context.Scores.FirstOrDefault(x=>x.Name =="دولتی"),CurrentScore = 90*4, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==3),Score = context.Scores.FirstOrDefault(x=>x.Name =="سایر"),CurrentScore = 60*4, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==4),Score = context.Scores.FirstOrDefault(x=>x.Name =="غیر هیات علمی"),CurrentScore = 50*5, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},
            //    new ProfessorScore(){Professor = context.Professors.FirstOrDefault(x=>x.ProfessorCode==5),Score = context.Scores.FirstOrDefault(x=>x.Name =="آزاد"),CurrentScore = 100*4, Term = context.Terms.FirstOrDefault(x=>x.TermCode == "96-97-2")},









            //}.ForEach(m => context.ProfessorScores.AddOrUpdate(x => x.Id, m));

            #endregion

        }
    }
}
