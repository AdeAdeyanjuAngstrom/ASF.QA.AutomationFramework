namespace AutomationFramework.Model.WorkbookWebApi
{
    public class NflMarketPublishLinesRequestModel
    {
        public string FixtureKey { get; set; }
        public string SimulationKey { get; set; }
        public string ClientKey { get; set; }
        public int Q1M { get; set; } // first quarter match odds Lines
        public int Q1T { get; set; } // first quarter Total Lines
        public int Q1S { get; set; } // first quarter handicap Lines
        public int Q1H { get; set; } // first quarter Home team total Lines
        public int Q1A { get; set; } // first quarter Away team total Lines
        public int Q1C { get; set; } // first quarter correct score Lines
        public int Q2M { get; set; } // second quarter match odds Lines
        public int Q2T { get; set; } // second quarter Total Lines
        public int Q2S { get; set; } // second quarter handicap Lines
        public int Q2H { get; set; } // second quarter Home team total Lines
        public int Q2A { get; set; } // second quarter Away team total Lines
        public int Q2C { get; set; } // second quarter correct score Lines
        public int Q3M { get; set; } // third quarter match odds Lines
        public int Q3T { get; set; } // third quarter Total Lines
        public int Q3S { get; set; } // third quarter handicap Lines
        public int Q3H { get; set; } // third quarter Home team total Lines
        public int Q3A { get; set; } // third quarter Away team total Lines
        public int Q3C { get; set; } // third quarter correct score Lines
        public int Q4M { get; set; } // fourth quarter match odds Lines
        public int Q4T { get; set; } // fourth quarter Total Lines
        public int Q4S { get; set; } // fourth quarter handicap Lines
        public int Q4H { get; set; } // fourth quarter Home team total Lines
        public int Q4A { get; set; } // fourth quarter Away team total Lines
        public int Q4C { get; set; } // fourth quarter correct score Lines
        public int H1M { get; set; } //first half match odds Lines
        public int H1T { get; set; } //first half Total Lines
        public int H1S { get; set; } //first half Handicap Lines
        public int H1H { get; set; } //first half Home team total Lines
        public int H1A { get; set; } //first half Away team total Lines
        public int H1C { get; set; } //first half correct score total Lines
        public int H2M { get; set; } //second half match odds Lines
        public int H2T { get; set; } //second half Total Lines
        public int H2S { get; set; } //second half Handicap Lines
        public int H2H { get; set; } //second half Home team total Lines
        public int H2A { get; set; } //second half Away team total Lines
        public int H2C { get; set; } //second half correct score total Lines
        public int MMM { get; set; } //match match odds Lines
        public int MMT { get; set; } //matchTotal Lines
        public int MMS { get; set; } //matchhandicap Lines
        public int MMH { get; set; } //match Home team total Lines
        public int MMA { get; set; } //match Away team total Lines
        public int MMC { get; set; } //match correct score total Lines
        public int TRY { get; set; } //Total Rushing Yards Lines
        public int TRT { get; set; } //Total Rushing Touchdowns Lines
        public int TPY { get; set; } //Total Passing Yards Lines
        public int TPD { get; set; } //Total Passing Touchdowns Lines
        public int TPC { get; set; } //Total Passing Completions Lines
        public int TCY { get; set; } //Total Receiving Yards Lines
        public int TCT { get; set; } //Total Receiving Touchdowns Lines
        public int TCR { get; set; } //Total Receptions Lines
        public int TTD { get; set; } //Total Touchdowns Lines
        public int TCM { get; set; } //Total Field Goals Lines
        public int TRR { get; set; } //Total Rushing and Reception Yards
        public int TIN { get; set; } //Total Interceptions
        public int TIP { get; set; } //Total Incomplete Passes
        public int TPA { get; set; } //Total Pass Attempts
        public int TSK { get; set; } //Total Sacks
        public int TDI { get; set; } //Total Defensive Interceptions
        public int TDS { get; set; } //Total Defensive Sacks
        public int TDK { get; set; } //Total Defensive Tackles
        public int TDT { get; set; } //Defensive Tackles + Assists
        public int TCP { get; set; } //Kicking Points
        #region FirstLastNextMarket
        #region NFL
        public int FMT { get; set; } //First Touchdown Scorer
        public int LMT { get; set; } //Last Touchdown Scorer
        public int NMT { get; set; } //Next Touchdown Scorer
        public int FMH { get; set; } //First Home Team Touchdown Scorer
        public int LMH { get; set; } //Last Home Team Touchdown Scorer
        public int NMH { get; set; } //Next Home Team Touchdown Scorer
        public int FMA { get; set; } //First Away Team Touchdown Scorer
        public int LMA { get; set; } //Last Away Team Touchdown Scorer
        public int NMA { get; set; } //Next Away Team Touchdown Scorer
        #endregion

        #region NCAAF
        public int MMF { get; set; } //First Team To Score 2 Way
        public int MML { get; set; } //Last Team To Score 2 Way

        #endregion

        #endregion

        #region PlayerMatchupHandicap
        public int SPY { get; set; } //Most Passing Yards Handicap

        public int SPA { get; set; } //Most Passing Attempts Handicap

        public int SCM { get; set; } //Most Passing Completions Handicap

        public int SPD { get; set; } //Most Passing Touchdowns Handicap

        public int SIN { get; set; } //Most Interceptions Handicap

        public int SCY { get; set; } //Most Reception Yards Handicap

        public int SCR { get; set; } //Most Receptions Handicap

        public int STA { get; set; } //Most 'Targets' (Receptions Attempted) Handicap

        public int SRY { get; set; } //Most Rushing Yards Handicap

        public int SRA { get; set; } //Most Rushing Attempts Handicap

        public int SRR { get; set; } //Most Rushing and Reception Yards Handicap

        public int STD { get; set; } //Most Touchdowns Handicap

        public int SKM { get; set; } //Most Field Goals Made Handicap
        #endregion PlayerMatchupHandicap

        #region PlayerMilestoneMarkets
        public int MPY { get; set; } //Passing Yards Milestones
        public int MCY { get; set; } //Reception Yards Milestones
        public int MRY { get; set; } //Rushing Yards Milestones
        public int MRR { get; set; } //Rushing and Reception Yards Milestones
        #endregion PlayerMilestoneMarkets

        #region PlayerMostMarkets
        public int HRY { get; set; } //Rushing Yards
        public int HCY { get; set; } //Receiving Yards
        #endregion PlayerMostMarkets

        #region PlayerRaceToMarkets
        public int RRY { get; set; } //Rushing Yards
        public int RCY { get; set; } //Receiving Yards
        #endregion PlayerRaceToMarkets

        #region CurrentDriveMarkets
        public int DPR { get; set; } //Most Yards Pass v Rush
        public int D4C { get; set; } //Will a 4th Down Be Converted
        public int D1G { get; set; } //Will First Down Be Gained
        public int DSK { get; set; } //Sack
        public int DRZ { get; set; } //Reach Redzone
        public int D50 { get; set; } //Reach 50 Yard Line
        public int D30 { get; set; } //Reach 30 Yard Lines
        public int DIN { get; set; } //Interception
        public int DLP { get; set; } //Longest Play
        public int DNP { get; set; } //Number of Plays
        public int DN1 { get; set; } //Number of First Downs
        public int DNC { get; set; } //Number of Pass Completions
        public int DLC { get; set; } //Longest Reception
        public int DL3 { get; set; } //Longest 3rd Down
        public int DC3 { get; set; } //Longest 3rd Down Converted
        public int DOC { get; set; } //Drive Outcome
        #endregion CurrentDriveMarkets

        #region PlayerCombinedMarkets
        public int XPY { get; set; } // Total Passing Yards
        public int XPA { get; set; } // Total Passing Attempts
        public int XCM { get; set; } // Total Passing Completions
        public int XPD { get; set; } // Total Passing Touchdowns
        public int XIN { get; set; } // Total Interceptions
        public int XCY { get; set; } // Total Reception Yards
        public int XCR { get; set; } // Total Receptions
        public int XTA { get; set; } // Total 'Targets' (Receptions Attempted)
        public int XRY { get; set; } // Total Rushing Yards
        public int XRA { get; set; } // Total Rushing Attempts
        public int XRR { get; set; } // Total Rushing and Reception Yards
        public int XTD { get; set; } // Total Touchdowns
        public int XKM { get; set; } // Total Field Goals Made
        #endregion PlayerCombinedMarkets

        #region PlayerMatchupMarkets

        public int PPY { get; set; } // Most Passing Yards
        public int PPA { get; set; } // Most Passing Attempts
        public int PCM { get; set; } // Most Passing Completions
        public int PPD { get; set; } // Most Passing Touchdowns
        public int PIN { get; set; } // Most Interceptions
        public int PCT { get; set; } // Most Reception Touchdowns
        public int PCY { get; set; } // Most Receptions Yards
        public int PCR { get; set; } // Most Receptions
        public int PTA { get; set; } // Most 'Targets' (Receptions Attempted)
        public int PRT { get; set; } // Most Rushing Touchdowns
        public int PRY { get; set; } // Most Rushing Yards
        public int PRA { get; set; } // Most Rushing Attempts
        public int PRR { get; set; } // Most Rushing and Reception Yards
        public int PTD { get; set; } // Most Touchdowns
        public int PKM { get; set; } // Most Field Goals Made
        public int PKP { get; set; } // Most Kicking Points
        public int PDI { get; set; } // Most Defensive Interceptions
        public int PDS { get; set; } // Most Defensive Sacks
        public int PDT { get; set; } // Most Defensive Tackles + Assists

        #endregion
        public int PIP { get; set; }
        public int TFL { get; set; }

        public int TLP {get;set;}
        public int TLR {get;set;}
        public int TRA {get;set;}
        public int PAT {get;set;}
        public int GFG {get;set;}
        public int HSH {get;set;}
        public int HSQ {get;set;}
        public int HTF {get;set;}
        public int HWQ {get;set;}
        public int AWQ {get;set;}
        public int HWH {get;set;}
        public int AWH {get;set;}
        public int RMT {get;set;}
        public int OYN {get;set;}
        public int POE {get;set;}
        public int GRY {get;set;}
        public int GRA {get;set;}
        public int GPY {get;set;}
        public int GPA {get;set;}
        public int GPT {get;set;}
        public int GRT {get;set;}
        public int GDT {get;set;}
    }
}