using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Service.Service;
using System.Security.Cryptography;
using System.Text;

namespace IAUECProfessorsEvaluation.Web.Models.Utility
{
    public static class UtilityFunction
    {
        public static string GetScoreName(this int score, int indicatorId)
        {



            IndicatorService indicatorService = new IndicatorService(new IndicatorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));

            return indicatorService.Get(x => x.Id == indicatorId).Scores.OrderBy(s => s.Point)
                .FirstOrDefault(s => s.Point >= score)
                ?.Name;


            //var scoreService = new ScoreService(new ScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            //return scoreService.Get(x => x.Id == scoreId.Value).Name;
        }

        public static string GetScoreName(this int score, int indicatorId, string professorCode, int termId)
        {
            var psCode = Convert.ToInt32(professorCode);

            if (indicatorId != 4)
            {
                IndicatorService indicatorService = new IndicatorService(new IndicatorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));

                return indicatorService.Get(x => x.Id == indicatorId).Scores.OrderBy(s => s.Point)
                    .FirstOrDefault(s => s.Point >= score)
                    ?.Name;
            }
            else
            {
                var ps = new ProfessorService(new ProfessorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));

                var universityStudyPlace = ps.Get(x => x.ProfessorCode == psCode && x.Term.Id == termId).UniversityStudyPlace;
                var ss = new ScoreService(new ScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));


                var ulm = new UniversityLevelMappingService(new UniversityLevelMappingRepository(new DatabaseFactory()),
                    new UnitOfWork(new DatabaseFactory()));
                if (universityStudyPlace != null && universityStudyPlace > 0)
                {
                    var scoreId = ulm.Get(x => x.UniversityId == universityStudyPlace).Score.Id;
                    var score1 = ss.Get(x => x.Id == scoreId);
                    if (score1 != null)
                        return score1.Name;
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }


            //var scoreService = new ScoreService(new ScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            //return scoreService.Get(x => x.Id == scoreId.Value).Name;
        }
        public static string GetCssClassForProgressBar(this int score)
        {
            string resualt;
            if (score > 75)
            {
                resualt = "progress-bar-success";
            }
            else
            {
                if (score <= 75 && score > 50)
                {
                    resualt = "progress-bar-info";
                }
                else
                {
                    if (score <= 50 && score > 25)
                    {
                        resualt = "progress-bar-warning";
                    }
                    else
                    {
                        resualt = "progress-bar-danger";
                    }

                }

            }

            return resualt;
        }

        public static string GetMd5Hash(this string input)
        {
            var md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        public static bool VerifyMd5Hash(this string input, string hash)
        {
            string hashOfInput = input.GetMd5Hash();
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            else
                return false;
        }

        public static int GetValuOfRatio(this string input)
        {
            if (!string.IsNullOrEmpty(input))
                switch (input.Trim())
                {
                    case "خیلی زیاد":
                        return 5;
                    case "زیاد":
                        return 4;
                    case "متوسط":
                        return 3;
                    case "کم":
                        return 2;
                    case "خیلی کم":
                        return 1;
                    default:
                        return 0;
                }
            else return 0;
        }

        public static double GetTimeLapse(decimal timeLapse, string timeLapseMeasurement)
        {
            decimal res = 0;
            switch (timeLapseMeasurement)
            {
                case "m":
                    res = timeLapse;
                    break;
                case "h":
                    res = timeLapse * 60;
                    break;
                case "D":
                    res = timeLapse * 60 * 24;
                    break;
                case "M":
                    res = timeLapse * 60 * 24 * 30;
                    break;
                case "Y":
                    res = timeLapse * 60 * 24 * 30 * 12;
                    break;
            }
            return Convert.ToDouble(res);
        }
    }
}