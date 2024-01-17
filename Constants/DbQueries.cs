namespace AutomationFramework.Constants
{
    internal static class DbQueries
    {

        public static readonly string NflPlayerDetails = @"( select PlayerId, '1990-01-01' as DOB, FirstName as FirstName, Surname AS Surname, 'P' as StatType,mPosition as Position,Starter,OnField,Status 
       from ( 
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status, D.Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, QBonField As OnField 
              FROM sportname.tblSquads_PFF S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X ON X.PlayerID=S.PlayerID 
              WHERE  NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) 
              AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')  and M.Position='QB' and S.Team='teamname' 
              order by D.Rank asc limit 3
             )
             Union 
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status, MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, BACKonFieldrush As OnField 
              FROM sportname.tblSquads_PFF S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X ON X.PlayerID=S.PlayerID 
              WHERE  NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) 
              AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')  and M.Position='RB' and S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by (BACKonFieldRush+BACKonFieldPass)/2 DESC, MIN(IFNULL(D.Rank, 10)) asc limit 4
             )
             Union
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status, MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, TEonFieldpass As OnField 
              FROM sportname.tblSquads_PFF S
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X ON X.PlayerID=S.PlayerID 
              WHERE  NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) 
              AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')   and M.Position='TE'  and S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by (TEonFieldRush+TEonFieldPass)/2 DESC, MIN(IFNULL(D.Rank, 10)) asc limit 4
             ) 
             Union
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status, MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, WRonField As OnField
              FROM sportname.tblSquads_PFF S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X ON X.PlayerID=S.PlayerID 
              WHERE NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) 
              AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')  and M.Position='WR' and S.Team='teamname' 
              GROUP BY S.PlayerID order by WRonField DESC, MIN(IFNULL(D.Rank, 10)) asc limit 7
             ) 
             Union 
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status,  MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, OLonField As OnField 
              FROM sportname.tblSquads_PFF S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X ON X.PlayerID=S.PlayerID 
              WHERE  NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) 
              AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')  and M.Position='OLine'  and S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by MIN(IFNULL(D.Rank, 2.5)) asc, OLonField DESC  limit 7
             ) 
             Union 
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status, MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, DLonFieldPass As OnField 
              FROM sportname.tblSquads_PFF S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X ON X.PlayerID=S.PlayerID 
              WHERE  NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport 
              WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')  
              and M.Position='DLINE'  and S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by  (DLonFieldRush+DLonFieldPass)/2 DESC, MIN(IFNULL(D.Rank, 10)) asc limit 9
             ) 
             Union 
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status,   MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, LBonFieldPAss As OnField 
              FROM sportname.tblSquads_PFF S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X ON X.PlayerID=S.PlayerID 
              WHERE  NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) 
              AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')  and M.Position='LB'  and S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by (LBonFieldRush+LBonFieldPass)/2 DESC, MIN(IFNULL(D.Rank, 10)) asc limit 8
             )
             Union
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status,  MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, DBonFieldPass As OnField 
              FROM sportname.tblSquads_PFF S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X     ON X.PlayerID=S.PlayerID 
              WHERE  NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) 
              AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')  and M.Position='DB' and S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by (DBonFieldRush+DBonFieldPass)/2 DESC, MIN(IFNULL(D.Rank, 10)) asc limit 8
             ) 
             Union 
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status,  MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, DBonFieldPass As OnField 
              FROM sportname.tblSquads_PFF S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X     ON X.PlayerID=S.PlayerID 
              WHERE  NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) 
              AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')  and M.Position='FB' and S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by (BACKonFieldRush+BACKonFieldPass)/2 DESC, MIN(IFNULL(D.Rank, 10)) asc limit 1
             ) 
             Union 
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status, MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, DBonFieldPass As OnField 
              FROM sportname.tblSquads_PFF S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X     ON X.PlayerID=S.PlayerID 
              WHERE  NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) 
              AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')  and M.Position='K' and S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by MIN(IFNULL(D.Rank, 10)) asc limit 2
             ) 
             Union 
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status,   MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, DBonFieldPass As OnField 
              FROM sportname.tblSquads_PFF S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X     ON X.PlayerID=S.PlayerID 
              WHERE  NOT (Stattype='P' and S.PlayerId in (SELECT PlayerId FROM sportname.tblPendingFixtureInactivePlayers) ) 
              AND NOT (StatType='P' AND S.PlayerID in (SELECT PlayerID FROM sportname.vwPracticeReport WHERE Status='Out' OR Status='Doubtful' OR Status='Inactive')) 
              AND IFNULL(D.Position,'')<>'LS'  AND (Status='A' OR Status='I' OR Status='DFD')  and M.Position='P' and S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by MIN(IFNULL(D.Rank, 10)) asc limit 2
             ) 
             Union 
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status,   MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, DBonFieldPass As OnField 
              FROM (SELECT P.Team, R.PlayerId, P.Status 
                    FROM sportname.tblFixtureRosters_SR R  
                    JOIN sportname.tblFixtures_SR F ON F.FixtureID=R.FixtureId 
                    JOIN (SELECT X.Season, MAX(Week) AS Week 
                          FROM sportname.tblFixtures_SR F 
                          JOIN (SELECT DISTINCT RIGHT(FixtureKey, 4) AS Season 
                                FROM sportname.tblSeasonSchedule WHERE FixtureKey LIKE '%Sep%') X 
                          WHERE F.Season=X.Season AND (Home='teamname' OR Away='teamname') ) X ON X.Season=F.Season AND X.Week=F.Week AND R.Team='teamname' 
                    JOIN sportname.tblSquads_PFF P ON P.Team =R.Team AND P.Status ='P' AND P.PlayerID =R.PlayerId
                   ) S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X     ON X.PlayerID=S.PlayerID 
              WHERE  S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by MIN(IFNULL(D.Rank, 10)) asc
             ) 
             Union 
             (SELECT S.Team, S.PlayerID, '1990-01-01', P.FirstName as FirstName, P.Surname AS Surname, S.Status,   MIN(IFNULL(D.Rank, 10)) AS Rank,M.Position as mPosition, CASE WHEN X.Starter IS NULL THEN 0 ELSE X.Starter END AS Starter, DBonFieldPass As OnField 
              FROM (SELECT R.Team, R.PlayerId, CASE WHEN P.Status IS NULL THEN 'A' ELSE P.Status END AS Status 
                    FROM sportname.tblFixtureManualSquadChange R 
                    JOIN (SELECT X.Season, 0 AS Week 
                          FROM sportname.tblFixtures_SR F 
                          JOIN (SELECT DISTINCT RIGHT(FixtureKey, 4) AS Season FROM sportname.tblSeasonSchedule WHERE FixtureKey LIKE '%Sep%') X 
                          WHERE F.Season=X.Season AND (Home='teamname' OR Away='teamname') ) X ON X.Season=R.Season AND X.Week=R.Week AND R.Team='teamname' 
                    LEFT JOIN sportname.tblSquads_PFF P ON P.PlayerID =R.PlayerId
                   ) S 
              JOIN sportname.tblRatingsCurrentOnField M on M.PlayerId=S.PlayerId and M.StatType='P' 
              JOIN sportname.tblPlayers P on M.PlayerId=P.PlayerId 
              LEFT JOIN sportname.tblDepthCharts_RW D ON D.PlayerId=S.PlayerId 
              LEFT JOIN (SELECT Team, PlayerID, CASE WHEN COUNT(*)>1 THEN 0 ELSE 0 END AS Starter FROM sportname.tblStartingUnits_PFF GROUP BY Team, PlayerID ) X     ON X.PlayerID=S.PlayerID 
              WHERE  S.Team='teamname' 
              GROUP BY S.PlayerID 
              order by MIN(IFNULL(D.Rank, 10)) asc
             ) 
         )s WHERE TEAM='teamname'
      Union 
        (Select CC.CoachId,'1990-01-01',SUBSTRING_INDEX(C.CoachName, ' ', 1) AS FirstName, SUBSTRING_INDEX(SUBSTRING_INDEX(C.CoachName, ' ', 2), ' ', -1) AS Surname ,'C','Coach',0,0, '' FROM sportname.tblCurrentCoaches AS CC join sportname.tblCoaches AS C on CC.CoachId = C.CoachId AND CC.TEAM='teamname') 
)X 
WHERE Position in (positioncondition) 
Order BY CASE WHEN Position='Coach' then 0 WHEN Position='QB' THEN 1 WHEN Position='OLINE' THEN 2 WHEN Position='RB' OR Position='FB' THEN 4 
            WHEN Position='TE' THEN 5 WHEN Position='WR' THEN 6     WHEN Position='DLINE' THEN 7 WHEN Position='LB' THEN 8     WHEN Position='DB' THEN 9 
            WHEN Position='K' THEN 10 WHEN Position='P' THEN 11 WHEN Position='LS' then 12 else 13 end,starter desc,OnField DESC limit 28;";

        public static readonly string MlbBattersDetail = @"select p.PlayerID, CONCAT(FirstName, ' ', Surname) as Name, FieldingPosition, BattingPosition,n.Bats
                 From sportname.tblPendingFixtureLineups_NEW  p   join sportname.tblPlayers n on p.playerid=n.playerid
                 where p.team = 'teamname' AND p.source = 'Rotowire' AND p.Completed=1 AND p.FixtureKey = 'fixturename'
                  UNION   SELECT b.PlayerId,CONCAT(FirstName, ' ', Surname) as Name, 
                 REPLACE(REPLACE(b.FieldingPosition,'OF','LF/CF/RF'),'INF','3B/SS/2B/1B') as FieldingPosition, b.BattingOrder as BattingPosition,n.Bats
                 FROM sportname.tblStandardBattingLineups b join sportname.tblPlayers n on b.playerid=n.playerid
                 where b.team = 'teamname' and n.Active=1 and lineupType='lineupname'
                 and b.PlayerID not in ( select PlayerID From sportname.tblPendingFixtureLineups_NEW 
                 where team = 'teamname' and source = 'Rotowire' and Completed=1 
                 and FixtureKey = 'fixturename')
                  order by BattingPosition limit 13";

        public static readonly string MlbPitchersDetail =
                            @"SELECT d.PlayerId,CONCAT(FirstName, ' ', Surname) as pName,Throws,s.FixtureKey,ifnull(p.PitchingPosition,99) as PitchingPosition,DC.Position
                 FROM (Select Distinct PlayerId from sportname.tblDepthCharts where Position in ('SP','RP') and Team='teamname'
                 UNION (SELECT distinct PlayerId from sportname.tblPlayers where Position='P' and Team='teamname')
                 UNION (select Distinct Batter from sportname.tblFixtureBattingStats_BBRef where Position='P' and Team='teamname' and substring(FixtureURL,15,4)=year(now()))
                 UNION (select Distinct Pitcher from sportname.tblStartingPitchers where Team='teamname')
                 UNION (select Distinct PlayerId from sportname.tblStandardPitchingLineups where Team='teamname')
                 UNION (SELECT Distinct Pitcher from sportname.tblStartingPitchers_RW where Team='teamname')  ) d
                 LEFT JOIN (Select * from sportname.tblStartingPitchers where Team='teamname' 
                 UNION (SELECT Fixturekey,Team,Pitcher from sportname.tblStartingPitchers_RW where Type<>'PP' and Team='teamname')) s
                 ON d.PlayerID = s.Pitcher
                 left join sportname.tblStandardPitchingLineups p
                 on d.PlayerId=p.PlayerId
                 join sportname.tblPlayers n on n.PlayerId=d.PlayerId 
                 join sportname.tblDepthCharts DC on n.PlayerId=DC.PlayerId 
                 order by ifnull(p.PitchingPosition,99)";
    }
}
