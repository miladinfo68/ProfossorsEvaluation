using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using IAUECProfessorsEvaluation.Data.ReportModel;
using IAUECProfessorsEvaluation.Model.Models;
using System.Configuration;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class DapperRepository
    {
        public static Func<DbConnection> ConnectionFactory = () => new SqlConnection(ConnectionString.Connection);

        public static class ConnectionString
        {
            //public static string Connection = ConfigurationManager.ConnectionStrings["ProfessorsEvaluationEntities"].ConnectionString;
            //public static string Connection = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ProfessorsEvaluation;Integrated Security=True";
            //public static string Connection = @"data source=.; initial catalog=ProfessorsEvaluation; User ID=EvaluationDbUser;Password=EvaluationP@$$;MultipleActiveResultSets=true";
            public static string Connection = "data source=192.168.2.101; initial catalog=ProfessorsEvaluation; User ID=EvaluationDbUser;Password=EvaluationP@$$;MultipleActiveResultSets=true";
            //public static string Connection = "Data Source=192.168.2.101;database=ProfessorsEvaluationTest;User ID=teamuser;Password=t123*t456";
        }
        public static class SqlText
        {
            //Level 1

            #region Level 1

            //all college
            #region CollegeAll
            public static string CollegeAllCollege =
                @"select *
            from 
            (SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term
            group by c.Name,c.Id,i.Name,i.Id) as tbl3 inner join
            (select tbl.CollegeId, SUM(AvgScore) as CollegeScore from
            (SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term 
            group by c.Name,c.Id,i.Name,i.Id) as tbl
			group by tbl.CollegeId) as tbl2  on tbl3.CollegeId =tbl2.CollegeId";

            #endregion

            //not college
            #region CollegeAll
            public static string CollegeNotCollege =
                @"select *
			from 
			(SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term and c.Name in @colleges 
            group by c.Name,c.Id,i.Name,i.Id) as tbl3 inner join
			(select tbl.CollegeId, SUM(AvgScore) as CollegeScore from
			(SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term and c.Name in @colleges
            group by c.Name,c.Id,i.Name,i.Id) as tbl
			group by tbl.CollegeId) as tbl2  on tbl3.CollegeId =tbl2.CollegeId";

            #endregion



            //all college and indicatore

            #region CollegeAllCollegeWithIndicator
            public static string CollegeAllCollegeWithIndicator =
                @"select *
            from 
            (SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term and i.Id in @indicator 
            group by c.Name,c.Id,i.Name,i.Id) as tbl3 inner join
            (select tbl.CollegeId, SUM(AvgScore) as CollegeScore from
            (SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term and i.Id in @indicator 
            group by c.Name,c.Id,i.Name,i.Id) as tbl
			group by tbl.CollegeId) as tbl2  on tbl3.CollegeId =tbl2.CollegeId";

            #endregion
            //allCollege and Score

            #region CollegeAllCollegeWithScore
            public static string CollegeAllCollegeWithScore =
                @"select *
			from 
			(SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term and gs.Score_Id in @score
            group by c.Name,c.Id,i.Name,i.Id) as tbl3 inner join
			(select tbl.CollegeId, SUM(AvgScore) as CollegeScore from
			(SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term and gs.Score_Id in @score
            group by c.Name,c.Id,i.Name,i.Id) as tbl
			group by tbl.CollegeId) as tbl2  on tbl3.CollegeId =tbl2.CollegeId
";

            #endregion


            //Not All College and indicator

            #region CollegeNotAllCollegeWithIndicator
            public static string CollegeNotAllCollegeWithIndicator =
                @"select *
			from 
			(SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term and c.Name in @colleges and i.Id in @indicator
            group by c.Name,c.Id,i.Name,i.Id) as tbl3 inner join
			(select tbl.CollegeId, SUM(AvgScore) as CollegeScore from
			(SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term and c.Name in @colleges and i.Id in @indicator
            group by c.Name,c.Id,i.Name,i.Id) as tbl
			group by tbl.CollegeId) as tbl2  on tbl3.CollegeId =tbl2.CollegeId
";

            #endregion
            //Not All College and Score

            #region CollegeNotAllCollegeWithScore

            public static string CollegeNotAllCollegeWithScore =
                @"select *
			from 
			(SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term and c.Name in @colleges and gs.Score_Id in @score
            group by c.Name,c.Id,i.Name,i.Id) as tbl3 inner join
			(select tbl.CollegeId, SUM(AvgScore) as CollegeScore from
			(SELECT c.Name as CollegeName, c.Id as CollegeId,i.Name IndicatorName, i.Id as IndicatorId,AVG(gs.CurrentScore) as AvgScore
            FROM [Colleges] as c 
            inner join [EducationalGroups] as g on c.Id=g.College_Id
            left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
            left join [Scores] as s on gs.Score_Id = s.Id
            left join [Indicators] as i on s.Indicator_Id = i.Id   
            where 
            c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
            gs.Term_Id =@term and c.Name in @colleges and gs.Score_Id in @score
            group by c.Name,c.Id,i.Name,i.Id) as tbl
			group by tbl.CollegeId) as tbl2  on tbl3.CollegeId =tbl2.CollegeId";

            #endregion



            #endregion

            //Level 2

            #region Level 2

            //all college all group
            #region GroupAllCollegeAllGroup
            public static string GroupAllCollegeAllGroup =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term 
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term  
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";

            #endregion

            //all college not group
            #region GroupAllCollegeNotGroup
            public static string GroupAllCollegeNotGroup =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and g.Name in @groups
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and g.Name in @groups
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";

            #endregion

            //not college all group
            #region GroupNotCollegeAllGroup
            public static string GroupNotCollegeAllGroup =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges 
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";

            #endregion

            //not college not  group
            #region GroupNotCollegeNotGroup
            public static string GroupNotCollegeNotGroup =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges and g.Name in @groups
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges and g.Name in @groups
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";

            #endregion

            //all college and all group and indicatore

            #region GroupAllCollegeAllGroupWithIndicator
            public static string GroupAllCollegeAllGroupWithIndicator =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and i.Id in @indicator  
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and i.Id in @indicator   
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";

            #endregion
            //all college all group and score

            #region GroupAllCollegeAllGroupWithScore
            public static string GroupAllCollegeAllGroupWithScore =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and gs.Score_Id in @score  
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and gs.Score_Id in @score   
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";

            #endregion
            //all college not all group and indictor

            #region GroupAllCollegeNotAllGroupWithIndicator
            public static string GroupAllCollegeNotAllGroupWithIndicator =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and g.Name in @groups and i.Id in @indicator
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and g.Name in @groups and i.Id in @indicator
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";

            #endregion
            //all college not all group and Score

            #region GroupAllCollegeNotGroupWithScore
            public static string GroupAllCollegeNotGroupWithScore =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and g.Name in @groups and gs.Score_Id in @score
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and g.Name in @groups and gs.Score_Id in @score
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";


            #endregion


            //not college all group and indicatore

            #region GroupNotAllCollegeAllGroupwithIndicator
            public static string GroupNotAllCollegeAllGroupwithIndicator =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges and i.Id in @indicator  
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges and i.Id in @indicator   
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";

            #endregion

            //not college all group and score

            #region GroupNotAllCollegeAllGroupwithScore
            public static string GroupNotAllCollegeAllGroupwithScore =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges and gs.Score_Id in @score  
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges and gs.Score_Id in @score   
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";

            #endregion

            //not college not all group and indictor

            #region GroupNotAllCollegeNotAllGroupwithIndicator
            public static string GroupNotAllCollegeNotAllGroupwithIndicator =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges and g.Name in @groups and i.Id in @indicator
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges and g.Name in @groups and i.Id in @indicator
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";

            #endregion

            //not college not all group and Score

            #region ProfessorAllCollegeAllGroupAllProfessorWithScore
            public static string GroupNotAllCollegeNotAllGroupwithScore =
                @"select tbl1.*,tbl2.GroupScore
                from 
                (
                SELECT c.Name as CollegeName, c.Id as CollegeId,g.Name as GroupName, g.Id as GroupId,i.Name IndicatorName, i.Id as IndicatorId,gs.CurrentScore 
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges and g.Name in @groups and gs.Score_Id in @score
                    ) as tbl1 inner join 
                    (SELECT  g.Id ,SUM(gs.CurrentScore) as GroupScore
                    FROM [Colleges] as c 
                    inner join [EducationalGroups] as g on c.Id=g.College_Id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    where 
                    c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and 
                    gs.Term_Id =@term and c.Name in @colleges and g.Name in @groups and gs.Score_Id in @score
                    group by g.Id) as tbl2 on tbl1.GroupId=tbl2.Id";


            #endregion


            #endregion

            //Level 3

            #region Level 3

            //all college all group all Professor
            #region ProfessorAllCollegeAllGroupAllProfessor
            public static string ProfessorAllCollegeAllGroupAllProfessor =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //all college all group not Professor
            #region ProfessorAllCollegeAllGroupNotProfessor
            public static string ProfessorAllCollegeAllGroupNotProfessor =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and ps.Term_Id =@termId and p.Id  in @Professor
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and p.Id  in @Professor
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //all college not group all Professor
            #region ProfessorAllCollegeNotGroupAllProfessor
            public static string ProfessorAllCollegeNoGroupAllProfessor =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and ps.Term_Id =@termId and g.Name in @groups 
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //all college not group not Professor
            #region ProfessorAllCollegeNotGroupNotProfessor
            public static string ProfessorAllCollegeNotGroupNotProfessor =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups and p.Id  in @Professor 
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups and p.Id  in @Professor  
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //not college all group all Professor
            #region ProfessorNotCollegeAllGroupAllProfessor
            public static string ProfessorNotCollegeAllGroupAllProfessor =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges 
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges  
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //not college all group not Professor
            #region ProfessorNotCollegeAllGroupNotProfessor
            public static string ProfessorNotCollegeAllGroupNotProfessor =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and p.Id  in @Professor 
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and p.Id  in @Professor  
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //not college not group all Professor
            #region ProfessorNotCollegeNotGroupAllProfessor
            public static string ProfessorNotCollegeNoGroupAllProfessor =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups 
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups  
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //not college not group not Professor
            #region ProfessorNotCollegeNotGroupNotProfessor
            public static string ProfessorNotCollegeNotGroupNotProfessor =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups and p.Id  in @Professor 
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups and p.Id  in @Professor 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion



            //all college all group all Professor and indicatore

            #region ProfessorAllCollegeAllGroupAllProfessorWithScore
            public static string ProfessorAllCollegeAllGroupAllProfessorWithIndicator =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and i.Id in @indicator
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and i.Id in @indicator 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion


            //all college all group all Professor and score

            #region ProfessorAllCollegeAllGroupAllProfessorWithScore
            public static string ProfessorAllCollegeAllGroupAllProfessorWithScore =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and ps.Score_Id in @score
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and ps.Score_Id in @score 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //all college all group not Professor and indicatore

            #region ProfessorAllCollegeAllGroupNotProfessorWithIndicator
            public static string ProfessorAllCollegeAllGroupNotProfessorWithIndicator =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and p.Id  in @Professor and i.Id in @indicator
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and p.Id  in @Professor and i.Id in @indicator 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //all college all group not Professor and score
            #region ProfessorAllCollegeAllGroupNotProfessorWithScore
            public static string ProfessorAllCollegeAllGroupNotProfessorWithScore =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and p.Id  in @Professor and ps.Score_Id in @score
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and p.Id  in @Professor and gs.Score_Id in @score 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //all college not  group all Professor and indicatore

            #region ProfessorAllCollegeNotGroupAllProfessorWithIndicator
            public static string ProfessorAllCollegeNoGroupAllProfessorWithIndicator =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups and i.Id in @indicator
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups and i.Id in @indicator 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //all college not  group all Professor and score
            #region ProfessorAllCollegeNotGroupAllProfessorWithScore
            public static string ProfessorAllCollegeNoGroupAllProfessorWithScore =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups and ps.Score_Id in @score
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups and ps.Score_Id in @score
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //all college not  group not Professor and indicatore
            #region ProfessorAllCollegeNotGroupNotProfessorWithIndicator
            public static string ProfessorAllCollegeNotGroupNotProfessorWithIndicator =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups and p.Id  in @Professor and i.Id in @indicator
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups and p.Id  in @Professor and i.Id in @indicator 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //all college not  group not Professor and score
            #region ProfessorAllCollegeNotGroupNotProfessorWithScore
            public static string ProfessorAllCollegeNoGroupNotProfessorWithScore =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups and p.Id  in @Professor and ps.Score_Id in @score
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and g.Name in @groups and p.Id  in @Professor and ps.Score_Id in @score
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion




            //not college all group all Professor and indicatore

            #region ProfessorNotCollegeAllGroupAllProfessorWithIndicator
            public static string ProfessorNotCollegeAllGroupAllProfessorWithIndicator =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and i.Id in @indicator
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and i.Id in @indicator 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion


            //not college all group all Professor and score

            #region ProfessorNotCollegeAllGroupAllProfessorWithScore
            public static string ProfessorNotCollegeAllGroupAllProfessorWithScore =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and ps.Score_Id in @score
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and ps.Score_Id in @score 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //not college all group not Professor and indicatore

            #region ProfessorNotCollegeAllGroupNotProfessorWithIndicator
            public static string ProfessorNotCollegeAllGroupNotProfessorWithIndicator =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and p.Id  in @Professor and i.Id in @indicator
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and p.Id  in @Professor and i.Id in @indicator 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //not college all group not Professor and score
            #region ProfessorNotCollegeAllGroupNotProfessorWithScore
            public static string ProfessorNotCollegeAllGroupNotProfessorWithScore =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and p.Id  in @Professor and ps.Score_Id in @score
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and p.Id  in @Professor and ps.Score_Id in @score 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //not college not  group all Professor and indicatore

            #region ProfessorNotCollegeNotGroupAllProfessorWithIndicator
            public static string ProfessorNotCollegeNoGroupAllProfessorWithIndicator =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups and i.Id in @indicator
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups and i.Id in @indicator 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //not college not  group all Professor and score
            #region ProfessorNotCollegeNotGroupAllProfessorWithScore
            public static string ProfessorNotCollegeNoGroupAllProfessorWithScore =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups and ps.Score_Id in @score
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups and ps.Score_Id in @score
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //not college not  group not Professor and indicatore
            #region ProfessorNotCollegeNotGroupNotProfessorWithIndicator
            public static string ProfessorNotCollegeNotGroupNotProfessorWithIndicator =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups and p.Id  in @Professor and i.Id in @indicator
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups and p.Id  in @Professor and i.Id in @indicator 
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion

            //not college not  group not Professor and score
            #region ProfessorNotCollegeNotGroupNotProfessorWithScore
            public static string ProfessorNotCollegeNoGroupNotProfessorWithScore =
                @"select tbl1.*,tbl2.ProfessorScore
                from
                (
                SELECT 
                  c.Name as CollegeName, c.Id as CollegeId
                 ,g.Name as GroupName, g.Id as GroupId
                 ,i.Name as IndicatorName, i.Id as IndicatorId 
                 ,p.Name as ProfessorName,p.Family as ProfessorLastName
                 ,p.Id as ProfessorId,ps.CurrentScore

                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id  
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups and p.Id  in @Professor and ps.Score_Id in @score
                  ) as tbl1 inner join (
                SELECT g.Name as GroupName, p.Id as ProfessorId,SUM(ps.CurrentScore) as ProfessorScore
                  FROM [Colleges] as c 
                  inner join [EducationalGroups] as g on c.Id=g.College_Id
                  left join [EducationalClasses] as ec on g.Id=ec.EducationalGroup_Id
                  left join [Professors] as p on ec.Professor_Id = p.Id
                  left join [ProfessorScores] as ps on p.Id=ps.Professor_Id
                  left join [Scores] as s on ps.Score_Id = s.Id
                  left join [Indicators] as i on s.Indicator_Id = i.Id 
                  where 
                  c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and
                  ps.Term_Id =@termId and c.Name in @colleges and g.Name in @groups and p.Id  in @Professor and ps.Score_Id in @score
                  group by g.Name,p.Id
                  ) as tbl2 on tbl1.ProfessorId=tbl2.ProfessorId";// and p.IsActive=1


            #endregion


            #endregion
        }




        //level 1

        public IEnumerable<CollegeReportModel> GetCollegeReportAllCollege(int termId)
        {
            List<CollegeReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<CollegeReportModel>(SqlText.CollegeAllCollege
                    , new { term = termId }).ToList();
            }

            return list;
        }
        public IEnumerable<CollegeReportModel> GetCollegeReportNotCollege(int termId, List<string> collegeList)
        {
            List<CollegeReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<CollegeReportModel>(SqlText.CollegeNotCollege
                    , new { term = termId, colleges = collegeList }).ToList();
            }

            return list;
        }


        public IEnumerable<CollegeReportModel> GetCollegeReportAllCollegWithIndicator(int termId, int?[] indictorList)
        {
            List<CollegeReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<CollegeReportModel>(SqlText.CollegeAllCollegeWithIndicator, new { term = termId, indicator = indictorList }).ToList();
            }

            return list;
        }

        public IEnumerable<CollegeReportModel> GetCollegeReportAllCollegeWithScore(int termId, int?[] scoreList)
        {
            List<CollegeReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<CollegeReportModel>(SqlText.CollegeAllCollegeWithScore
                    , new { term = termId, score = scoreList }).ToList();
            }

            return list;
        }

        public IEnumerable<CollegeReportModel> GetCollegeReportNotAllCollegeWithIndicator(int termId, int?[] indictorList, List<string> collegeList)
        {
            List<CollegeReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<CollegeReportModel>(SqlText.CollegeNotAllCollegeWithIndicator,
                    new { term = termId, indicator = indictorList, colleges = collegeList }).ToList();
            }

            return list;
        }

        public IEnumerable<CollegeReportModel> GetCollegeReportNotAllCollegeWithScore(int termId, int?[] scoreList, List<string> collegeList)
        {
            List<CollegeReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<CollegeReportModel>(SqlText.CollegeNotAllCollegeWithScore, new { term = termId, score = scoreList, colleges = collegeList }).ToList();
            }

            return list;
        }


        //level 2

        //all college all group
        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeAllGroup
            (int termId)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupAllCollegeAllGroup, new { term = termId }).ToList();
            }

            return list;
        }

        //all college not group
        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeNotGroup
            (int termId, List<string> groupList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupAllCollegeNotGroup,
                    new { term = termId, groups = groupList }).ToList();
            }

            return list;
        }

        //not college all group
        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeAllGroup
            (int termId, List<string> collegeList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupNotCollegeAllGroup, new { term = termId, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college not  group
        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeNotGroup
            (int termId, List<string> groupList, List<string> collegeList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupNotCollegeNotGroup,
                    new { term = termId, groups = groupList, colleges = collegeList }).ToList();
            }

            return list;
        }



        //all college and all group and indicatore
        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeAllGroupWithIndicator(int termId, int?[] indictorList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupAllCollegeAllGroupWithIndicator, new { term = termId, indicator = indictorList }).ToList();
            }

            return list;
        }

        //all college all group and score
        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeAllGroupWithScore(int termId, int?[] scoreList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupAllCollegeAllGroupWithScore
                    , new { term = termId, score = scoreList }).ToList();
            }

            return list;
        }

        //all college not all group and indictor
        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeNotAllGroupWithIndicator(int termId, int?[] indictorList, List<string> groupList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupAllCollegeNotAllGroupWithIndicator,
                    new { term = termId, indicator = indictorList, groups = groupList }).ToList();
            }

            return list;
        }

        //all college not all group and Score
        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeNotGroupWithScore(int termId, int?[] scoreList, List<string> groupList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupAllCollegeNotGroupWithScore, new { term = termId, score = scoreList, groups = groupList }).ToList();
            }

            return list;
        }



        //not college and all group and indicatore
        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeAllGroupWithIndicator(int termId, int?[] indictorList, List<string> collegeList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupNotAllCollegeAllGroupwithIndicator, new { term = termId, indicator = indictorList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college all group and score
        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeAllGroupWithScore(int termId, int?[] scoreList, List<string> collegeList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupNotAllCollegeAllGroupwithScore, new { term = termId, score = scoreList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college not all group and indictor
        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeNotAllGroupWithIndicator(int termId, int?[] indictorList, List<string> groupList, List<string> collegeList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupNotAllCollegeNotAllGroupwithIndicator,
                    new { term = termId, indicator = indictorList, groups = groupList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college not all group and Score
        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeNotGroupWithScore(int termId, int?[] scoreList, List<string> groupList, List<string> collegeList)
        {
            List<GroupReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(SqlText.GroupNotAllCollegeNotAllGroupwithScore, new { term = termId, score = scoreList, groups = groupList, colleges = collegeList }).ToList();
            }

            return list;
        }





        //level 3

        //all college all group all Professor


        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupAllProfessor
            (int termId)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                (SqlText.ProfessorAllCollegeAllGroupAllProfessor
                    , new { termId = termId }).ToList();
            }

            return list;
        }

        //all college all group not Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupNotProfessor
            (int termId, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                (SqlText.ProfessorAllCollegeAllGroupNotProfessor
                    , new { termId = termId, Professor = professoresList }).ToList();
            }

            return list;
        }

        //all college not group all Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupAllProfessor
            (int termId, List<string> groupList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                    (SqlText.ProfessorAllCollegeNoGroupAllProfessor,
                    new { termId = termId, groups = groupList }).ToList();
            }

            return list;
        }

        //all college not group not Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupNotProfessor
            (int termId, List<string> groupList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                (SqlText.ProfessorAllCollegeNotGroupNotProfessor
                    , new { termId = termId, Professor = professoresList, groups = groupList }).ToList();
            }

            return list;
        }

        //not college all group all Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupAllProfessor
            (int termId, List<string> collegeList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                (SqlText.ProfessorNotCollegeAllGroupAllProfessor
                    , new { termId = termId, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college all group not Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupNotProfessor
            (int termId, List<string> collegeList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                (SqlText.ProfessorNotCollegeAllGroupNotProfessor
                    , new { termId = termId, Professor = professoresList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college not group all Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupAllProfessor
            (int termId, List<string> collegeList, List<string> groupList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                (SqlText.ProfessorNotCollegeNoGroupAllProfessor
                    , new { termId = termId, groups = groupList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college not group not Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupNotProfessor
            (int termId, List<string> collegeList, List<string> groupList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                (SqlText.ProfessorNotCollegeNotGroupNotProfessor
                    , new { termId = termId, Professor = professoresList, groups = groupList, colleges = collegeList }).ToList();
            }

            return list;
        }




        //all college all group all Professor and indicatore
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupAllProfessorWithIndicator
            (int termId, int?[] indictorList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                    (SqlText.ProfessorAllCollegeAllGroupAllProfessorWithIndicator, new { termId = termId, indicator = indictorList }).ToList();
            }

            return list;
        }

        //all college all group all Professor and Score
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupAllProfessorWithScore
            (int termId, int?[] scoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                    (SqlText.ProfessorAllCollegeAllGroupAllProfessorWithScore
                    , new { termId = termId, score = scoresList }).ToList();
            }

            return list;
        }

        //all college all group not Professor and indicatore
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupNotProfessorWithIndicator
            (int termId, int?[] indictorList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                    (SqlText.ProfessorAllCollegeAllGroupNotProfessorWithIndicator
                    , new { termId = termId, indicator = indictorList, Professor = professoresList }).ToList();
            }

            return list;
        }

        //all college all group not Professor and score
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupNotProfessorWithScore
            (int termId, int?[] scoreList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                    (SqlText.ProfessorAllCollegeAllGroupNotProfessorWithScore
                    , new { termId = termId, score = scoreList, Professor = professoresList }).ToList();
            }

            return list;
        }




        //all college not group all Professor and indicatore
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupAllProfessorWithIndicator
            (int termId, int?[] indictorList, List<string> groupList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>
                    (SqlText.ProfessorAllCollegeNoGroupAllProfessorWithIndicator
                    , new { termId = termId, indicator = indictorList, groups = groupList }).ToList();
            }

            return list;
        }

        //all college not group all Professor and Score
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupAllProfessorWithScore
            (int termId, int?[] scoresList, List<string> groupList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorAllCollegeNoGroupAllProfessorWithScore
                    , new { termId = termId, score = scoresList, groups = groupList }).ToList();
            }

            return list;
        }

        //all college not group not Professor and indicatore
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupNotProfessorWithIndicator
            (int termId, int?[] indictorList, List<string> groupList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorAllCollegeNotGroupNotProfessorWithIndicator
                    , new { termId = termId, indicator = indictorList, Professor = professoresList, groups = groupList }).ToList();
            }

            return list;
        }

        //all college not group not Professor and score
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupNotProfessorWithScore
            (int termId, int?[] scoreList, List<string> groupList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorAllCollegeNoGroupNotProfessorWithScore
                    , new { termId = termId, score = scoreList, Professor = professoresList, groups = groupList }).ToList();
            }

            return list;
        }








        //not college all group all Professor and indicatore
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupAllProfessorWithIndicator
            (int termId, int?[] indictorList, List<string> collegeList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorNotCollegeAllGroupAllProfessorWithIndicator
                    , new { termId = termId, indicator = indictorList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college all group all Professor and Score
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupAllProfessorWithScore
            (int termId, int?[] scoreList, List<string> collegeList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorNotCollegeAllGroupAllProfessorWithScore
                    , new { termId = termId, score = scoreList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college all group not Professor and indicatore
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupNotProfessorWithIndicator
            (int termId, int?[] indictorList, List<string> collegeList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorNotCollegeAllGroupNotProfessorWithIndicator
                    , new { termId = termId, indicator = indictorList, Professor = professoresList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college all group not Professor and score
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupNotProfessorWithScore
            (int termId, int?[] scoreList, List<string> collegeList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorNotCollegeAllGroupNotProfessorWithScore
                    , new { termId = termId, score = scoreList, Professor = professoresList, colleges = collegeList }).ToList();
            }

            return list;
        }




        //not college not group all Professor and indicatore
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupAllProfessorWithIndicator
            (int termId, int?[] indictorList, List<string> collegeList, List<string> groupList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorNotCollegeNoGroupAllProfessorWithIndicator
                    , new { termId = termId, indicator = indictorList, groups = groupList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college not group all Professor and Score
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupAllProfessorWithScore
            (int termId, int?[] scoreList, List<string> collegeList, List<string> groupList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorNotCollegeNoGroupAllProfessorWithScore
                    , new { termId = termId, score = scoreList, groups = groupList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college not group not Professor and indicatore
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupNotProfessorWithIndicator
            (int termId, int?[] indictorList, List<string> collegeList, List<string> groupList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorNotCollegeNotGroupNotProfessorWithIndicator
                    , new { termId = termId, indicator = indictorList, Professor = professoresList, groups = groupList, colleges = collegeList }).ToList();
            }

            return list;
        }

        //not college not group not Professor and score
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupNotProfessorWithScore
            (int termId, int?[] scoreList, List<string> collegeList, List<string> groupList, int[] professoresList)
        {
            List<ProfessorReportModel> list;
            using (var connection = DapperRepository.ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorReportModel>(SqlText.ProfessorNotCollegeNoGroupNotProfessorWithScore
                    , new { termId = termId, score = scoreList, Professor = professoresList, groups = groupList, colleges = collegeList }).ToList();
            }

            return list;
        }
        //Dynamic Query

        public IEnumerable<ProfessorDetialReportModel> GetProfessorReport
            (int term, int[] indictorList, int[] scoreList,
            List<int> collegeList, List<int> groupList, int[] professoresList
            , string allColleges, string allGroups, string allProfessors,
            string allProfessorIndicators, string allProfessorScores, string allProfessorForGroupsCollege)
        {
            List<ProfessorDetialReportModel> list;
            var conditionSqltxt = string.Empty;
            var specialProfessorsList = new List<int>();



            if (!string.IsNullOrEmpty(allColleges))
                conditionSqltxt = allColleges;
            if (!string.IsNullOrEmpty(allGroups))
                conditionSqltxt = conditionSqltxt + allGroups;
            if (!string.IsNullOrEmpty(allProfessors))
                conditionSqltxt = conditionSqltxt + allProfessors;
            if (!string.IsNullOrEmpty(allProfessorForGroupsCollege))
            {
                conditionSqltxt = conditionSqltxt + allProfessorForGroupsCollege;


            }

            if (!string.IsNullOrEmpty(allProfessorScores))
            {
                var selectProfessorSqlText = @"
                select distinct t1.Id
                from
                (select p.Id ,i.Id as indicatorId, AVG(ps.CurrentScore) as avgScore
                from Professors as p
                left join ProfessorScores as ps on p.Id=ps.Professor_Id
                inner join EducationalClasses as ec on p.Id=ec.Professor_Id
                inner join EducationalGroups as g on ec.EducationalGroup_Id=g.Id
                inner join Colleges as c on c.Id = g.College_Id
                left join Scores as s on ps.Score_Id = s.Id
                left join Indicators as i on i.Id=s.Indicator_Id
                where ps.Term_Id =@termId and g.IsActive=1 and s.IsActive=1 and c.IsActive =1 and i.IsActive=1 and ec.IsActive=1 "// and p.IsActive=1
                + conditionSqltxt + @" and ps.Score_Id in @scores " +
                @" group by p.Id,i.Id) as t1 inner join Professors as p on t1.Id=p.Id";

                using (var connection = ConnectionFactory())
                {
                    connection.Open();
                    specialProfessorsList = connection.Query<int>(selectProfessorSqlText,
                        new
                        {
                            termId = term,
                            colleges = collegeList,
                            groups = groupList,
                            professores = professoresList,
                            //indicators = indictorList,
                            scores = scoreList
                        }).ToList();
                }

                if (specialProfessorsList.Any())
                    conditionSqltxt = conditionSqltxt + allProfessorScores;
                else
                {
                    return new List<ProfessorDetialReportModel>();
                }
            }
            if (!string.IsNullOrEmpty(allProfessorIndicators))
                conditionSqltxt = conditionSqltxt + allProfessorIndicators;


            var sqlText = @"
                select distinct t1.Id as ProfessorId ,t1.FullName,SUM(t1.avgScore) over (partition by t1.Id) as ProfessorScore,p.NationalCode,p.ProfessorCode,p.Gender,p.Name,p.Family
                from
                (select p.Id,(p.Name+' '+p.Family) as FullName,i.Id as indicatorId, AVG(ps.CurrentScore) as avgScore
                from Professors as p
                left join ProfessorScores as ps on p.Id=ps.Professor_Id
                inner join EducationalClasses as ec on p.Id=ec.Professor_Id
                inner join EducationalGroups as g on ec.EducationalGroup_Id=g.Id 
                inner join Colleges as c on c.Id = g.College_Id
                left join Scores as s on ps.Score_Id = s.Id
                left join Indicators as i on i.Id=s.Indicator_Id
                where ps.Term_Id =@termId and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and c.IsActive =1 and ec.IsActive=1 "// and p.IsActive=1
                + conditionSqltxt
                + @" group by p.Id,(p.Name+' '+p.Family),i.Id ,p.Name,p.Family) as t1 inner join Professors as p on t1.Id=p.Id";



            using (var connection = ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<ProfessorDetialReportModel>(sqlText, new
                {
                    termId = term,
                    colleges = collegeList,
                    groups = groupList,
                    professores = professoresList,
                    indicators = indictorList,
                    specialProfessors = specialProfessorsList
                }).ToList();
            }

            return list;
        }

        public List<ProfessorDetialReportModel> GetProfessorDetials(int term
            , int[] indictorList,
            List<int> collegeList, List<int> groupList
            , string allColleges, string allGroups,
            string allProfessorIndicators)
        {


            var conditionSqltxt = string.Empty;
            if (!string.IsNullOrEmpty(allColleges))
                conditionSqltxt = allColleges;
            if (!string.IsNullOrEmpty(allGroups))
                conditionSqltxt = conditionSqltxt + allGroups;
            if (!string.IsNullOrEmpty(allProfessorIndicators))
                conditionSqltxt = conditionSqltxt + allProfessorIndicators;


            List<ProfessorDetialReportModel> resualtList;

            var sqlText = @"
            select p.Id as ProfessorId,(p.Name+' '+P.Family) as FullName,p.NationalCode,p.ProfessorCode,i.Id as indicatorId,i.Name as IndicatorName, (select top 1 Point from Scores where Indicator_Id=i.Id and Point>=(AVG(ps.CurrentScore)/r.Point) order by Point )*r.Point as AvgScore,r.Point as RationValue,r.Name as RationName,p.Gender,p.Name,P.Family
            from Professors as p
            left join ProfessorScores as ps on p.Id=ps.Professor_Id and p.Term_Id = @termId
            inner join EducationalClasses as ec on p.Id=ec.Professor_Id
            inner join EducationalGroups as g on ec.EducationalGroup_Id=g.Id
            inner join Colleges as c on c.Id=g.College_Id
            left join Scores as s on ps.Score_Id = s.Id
            left join Indicators as i on i.Id=s.Indicator_Id
			inner join Ratios as r on i.Ratio_Id =r.Id
            where ps.Term_Id =@termId and c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and ec.IsActive=1 and r.IsActive=1 "// and p.IsActive=1
                          + conditionSqltxt
            + @" group by p.Id,(p.Name+' '+P.Family),p.NationalCode,p.ProfessorCode,p.Name,i.Id,i.Name,r.Point,r.Name,p.Gender,p.Name,P.Family";
            using (var connection = ConnectionFactory())
            {
                connection.Open();
                resualtList = connection.Query<ProfessorDetialReportModel>(sqlText,
                    new
                    {
                        termId = term,
                        colleges = collegeList,
                        groups = groupList,
                        indicators = indictorList,
                        //professor= professorId
                    }).ToList();

            }
         
            return resualtList;
        }


        public IEnumerable<GroupReportModel> GetGroupReport
        (int term, List<int> indictorList, List<int> scoreList,
            List<int> collegeList, List<int> groupList
            , string allColleges, string allGroups,
            string allGroupIndicators, string allGroupScores)
        {
            List<GroupReportModel> list;
            var conditionSqltxt = string.Empty;
            var specialgroupList = new List<int>();



            if (!string.IsNullOrEmpty(allColleges))
                conditionSqltxt = allColleges;
            if (!string.IsNullOrEmpty(allGroups))
                conditionSqltxt = conditionSqltxt + allGroups;
            if (!string.IsNullOrEmpty(allGroupScores))
            {
                var selectGroupSqlText = @"
                    SELECT distinct g.Id 
                    FROM  
                     [EducationalGroups] as g 
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
					inner join Colleges as c on c.Id = g.College_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id   
                    left join [Professors] as p on g.GroupManger_Id = p.id
                    where gs.Term_Id =@termId and c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1  "
                    + conditionSqltxt + @" and gs.Score_Id in @scores ";

                using (var connection = ConnectionFactory())
                {
                    connection.Open();
                    specialgroupList = connection.Query<int>(selectGroupSqlText,
                        new
                        {
                            termId = term,
                            colleges = collegeList,
                            groups = groupList,
                            //indicators = indictorList,
                            scores = scoreList
                        }).ToList();
                }

                if (specialgroupList.Any())
                    conditionSqltxt = conditionSqltxt + allGroupScores;
                else
                {
                    return new List<GroupReportModel>();
                }
            }


            if (!string.IsNullOrEmpty(allGroupIndicators))
                conditionSqltxt = conditionSqltxt + allGroupIndicators;

            var sqlText = @"
					SELECT distinct g.Id ,SUM(gs.CurrentScore) over(partition by g.Id) as GroupScore
					,c.Name as CollegeName,g.Name as GroupName,g.EducationalGroupCode as GroupCode,g.Id as GroupId
					,i.Id as IndicatorId,i.Name as IndicatorName,gs.CurrentScore,r.Point as RatioValue, r.Name as RatioName,
					c.CollegeCode as CollegeCode,c.Id as CollegeId,c.Name as CollegeName, p.Name as GroupManagerFirstName, p.Family as GroupManagerLastName
                     FROM  
                     [EducationalGroups] as g 
                    left join [Professors] as p on g.GroupManger_Id = p.id
                    left join [EducationalGroupScores] as gs on g.Id=gs.EducationalGroup_Id
					inner join Colleges as c on c.Id = g.College_Id
                    left join [Scores] as s on gs.Score_Id = s.Id
                    left join [Indicators] as i on s.Indicator_Id = i.Id  
					left join Ratios as r on r.Id= i.Ratio_Id
                    where gs.Term_Id =@termId and c.IsActive=1 and g.IsActive=1 and s.IsActive=1 and i.IsActive=1  "
                    + conditionSqltxt;


            using (var connection = ConnectionFactory())
            {
                connection.Open();
                list = connection.Query<GroupReportModel>(sqlText, new
                {
                    termId = term,
                    colleges = collegeList,
                    groups = groupList,
                    indicators = indictorList,
                    specialGroupsList = specialgroupList
                }).ToList();
            }

            var professorSqlText = @"select distinct AVG(t1.avgScore) over(partition by t1.GroupId) AvgProfessorScoreGroup
                                    from 
                                    (select tbl1.Professor_Id as Id,tbl1.avgScore as avgScore,tbl1.GroupId as GroupId
                                    from (

                                    select distinct ps.Professor_Id, ps.EducationalGroup_Id as GroupId, 
                                    sum(ps.CurrentScore) over(partition by ps.Professor_Id) as avgScore
                                    from  ProfessorScores as ps
                                    where ps.Term_Id=@termId and ps.EducationalGroup_Id=@groups
                                    ) as tbl1 inner join 
                                    (
                                    select distinct p.Id,ps.EducationalGroup_Id as GroupId
                                    from Professors as p
                                    left join ProfessorScores as ps on p.Id=ps.Professor_Id
                                    inner join EducationalClasses as ec on p.Id=ec.Professor_Id
                                    inner join EducationalGroups as g on ec.EducationalGroup_Id=g.Id 
                                    inner join Colleges as c on c.Id = g.College_Id
                                    left join Scores as s on ps.Score_Id = s.Id
                                    left join Indicators as i on i.Id=s.Indicator_Id
                                    where ps.Term_Id =@termId and ps.EducationalGroup_Id = @groups 
                                    and g.IsActive=1 and s.IsActive=1 and i.IsActive=1 and c.IsActive =1 
                                     and ec.IsActive=1 
                                    ) as tbl2
                                    on tbl1.Professor_Id=tbl2.Id and tbl1.GroupId = tbl2.GroupId ) as t1 ";//and p.IsActive=1
            foreach (var item in list)
            {
                List<int> avgProfessorScoreGroup;
                using (var connection = ConnectionFactory())
                {

                    connection.Open();
                    avgProfessorScoreGroup = connection.Query<int>(professorSqlText,
                        new
                        {
                            termId = term,
                            //colleges = collegeList,
                            groups = item.GroupId
                            //professores = professoresList,
                            //indicators = indictorList,
                            //scores = scoreList
                        }).ToList();
                }

                item.GroupScore = item.GroupScore + avgProfessorScoreGroup.FirstOrDefault();
                item.AvgProfessorScoreGroup = avgProfessorScoreGroup.FirstOrDefault();
                if (avgProfessorScoreGroup.FirstOrDefault() > 0)
                {
                    var tt = avgProfessorScoreGroup.FirstOrDefault();
                }
            }
            return list;

        }




        public IEnumerable<GroupReportModel> GetCollegeReport
        (int term, List<int> indictorList, List<int> scoreList,
            List<int> collegeList, string allColleges,
            string allCollegeIndicators, string allCollegeScores)
        {
            var conditionSqltxt = string.Empty;
            var specialCollegesList = new List<int>();

            var resualtOfGroup = GetGroupReport(term, indictorList, scoreList,
                  collegeList, null
                  , allColleges, null,
                  allCollegeIndicators, allCollegeScores);
            var collegeScores = resualtOfGroup.GroupBy(x => x.CollegeId).Select(x => new GroupReportModel
            {
                CollegeScore = Convert.ToInt32(x.ToList().Average(p => p.GroupScore)),
                CollegeId = Convert.ToInt32(x.Key)
            });

            var resualt = resualtOfGroup.Join(collegeScores, x => x.CollegeId, y => y.CollegeId, (x, y) => new GroupReportModel
            {
                CollegeScore = y.CollegeScore,
                RowNumber = x.RowNumber,
                CollegeId = x.CollegeId,
                CollegeCode = x.CollegeCode,
                CollegeName = x.CollegeName,
                GroupId = x.GroupId,
                GroupCode = x.GroupCode,
                GroupName = x.GroupName,
                GroupScore = x.GroupScore
            });
            return resualt;
        }
    }
}
