namespace AutomationFramework.Model.Database.MLB;

public class SimulationFixtureBatterRatingsModel
{
    public string FixtureKey { get; set; }
    public string Key { get; set; }
    public string Inning { get; set; }
    public DateTime Timestamp { get; set; }
    public int Order { get; set; }
    public int PlayerID { get; set; }
    public int Team { get; set; }
    public string Type { get; set; }
    public string Bats { get; set; }
    public decimal Walk { get; set; }
    public decimal Walk_Pltn { get; set; }
    public decimal nK { get; set; }
    public decimal nK_Pltn { get; set; }
    public decimal FB { get; set; }
    public decimal FB_Pltn { get; set; }
    public decimal nWGB { get; set; }
    public decimal nWGB_Pltn { get; set; }
    public decimal GBH { get; set; }
    public decimal GBH_Pltn { get; set; }
    public decimal nWFB { get; set; }
    public decimal nWFB_Pltn { get; set; }
    public decimal FBH { get; set; }
    public decimal FBH_Pltn { get; set; }
    public decimal FBHR { get; set; }
    public decimal FBHR_Pltn { get; set; }
    public decimal GBB { get; set; }
    public decimal GBB_Pltn { get; set; }
    public decimal FBB { get; set; }
    public decimal FBB_Pltn { get; set; }
    public decimal SA { get; set; }
    public decimal SS { get; set; }
    public decimal BaseAdv_R { get; set; }
    public decimal BaseAdv_B { get; set; }
    public decimal BPPA { get; set; }
    public decimal Pos { get; set; }
}