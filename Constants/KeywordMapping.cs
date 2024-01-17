namespace AutomationFramework.Constants
{
    internal static class KeywordMapping
    {
        private static readonly Dictionary<string, string> SportMapping = new()
        {
            { "mlb", "Baseball" },
            { "nfl", "NFL" },
            { "nba", "Basketball" },
            { "ncaaf", "NFL" },
            { "ncaab", "NBA" }
        };

        public static readonly Dictionary<string, int> SimDelay = new()
        {
            { "qa", 4 },
            { "dev", 8 }
        };

        private static readonly Dictionary<string, Dictionary<string, string>> EnvironmentSportWebApiUrlMapping = new()
        {
            {
                "dev", new Dictionary<string, string>()
                {
                    {"nfl","http://10.30.1.137"},
                    {"mlb","http://10.30.1.207"},
                    {"ncaaf","http://10.30.101.190"},
                    {"nba","http://10.30.101.252"}
                }
            },
            {
                "qa", new Dictionary<string, string>()
                {
                    {"nfl","http://10.30.1.247"},
                    {"mlb","http://10.30.101.237"},
                    {"ncaaf","http://10.30.101.171"},
                    {"nba","http://10.30.1.157"}
                }
            }
        };

        public static readonly Dictionary<string, Dictionary<string, List<string>>> FixtureMarketAllowedMarkets = new()
        {
            {
                "nfl", new Dictionary<string, List<string>>()
                {
                    {
                        "mm", ["m", "s","t","h","a","hsh","hsq","htf","hwq","awq","hwh","awh","mov","moe"]
                    },
                    {
                        "1h", ["m", "s","t","h","a"]
                    },
                    {
                        "2h", ["m", "s","t","h","a"]
                    },
                    {
                        "1q", ["m", "s","t","h","a"]
                    },
                    {
                        "2q", ["m", "s","t","h","a"]
                    },
                    {
                        "3q", ["m", "s","t","h","a"]
                    },
                    {
                        "4q", ["m", "s","t","h","a"]
                    }
                }
            },
            {
                "mlb", new Dictionary<string, List<string>>()
                {
                    {
                        "mm", ["m", "s","t","h","a"]//,"mei","mot","mst","mwm"
                    },
                    {
                        "1h", ["m", "s","t","h","a"]
                    }//,
                    //{
                    //    "1i", ["r", "t"]
                    //},
                    //{
                    //    "3i", ["m", "s","t"]
                    //},
                    //{
                    //    "7i", ["m", "s","t"]
                    //}
                }
            }
        };

        public static readonly Dictionary<string, List<string>> AllowedPeriodsPerSport = new()
        {
            {
                "nfl", ["mm", "1h", "2h", "1q", "2q", "3q", "4q"]
            },
            {
                "mlb", ["mm", "1h"]
            }
        };

        public static readonly string[] NflSuspensionReason = 
        {
            "0",
            "1",
            "4294967296",
            "2",
            "8589934592",
            "4",
            "17179869184",
            "8",
            "34359738368",
            "16",
            "68719476736",
            "32",
            "137438953472",
            "64",
            "274877906944",
            "128",
            "549755813888",
            "256",
            "1099511627776",
            "512",
            "2199023255552",
            "1024",
            "4398046511104",
            "2048",
            "8796093022208",
            "4096",
            "17592186044416",
            "8192",
            "35184372088832",
            "16384",
            "70368744177664",
            "32768",
            "140737488355328",
            "65536",
            "281474976710656",
            "131072",
            "562949953421312",
            "262144",
            "1125899906842624",
            "524288",
            "36893488147419103232",
            "1048576",
            "36028797018963968",
            "2097152",
            "2305843009213693952",
            "4194304",
            "72057594037927936",
            "8388608",
            "1152921504606846976",
            "16777216",
            "4611686018427387904",
            "33554432",
            "9223372036854775808",
            "67108864",
            "18446744073709551616",
            "134217728",
            "147573952589676412928",
            "268435456",
            "73786976294838206464",
            "536870912",
            "1073741824",
            "2147483648",
            "295147905179352825856",
            "18014398509481984",
            "590295810358705651712",
            "144115188075855872",
            "1180591620717411303424",
            "4722366482869645213696",
            "1208925819614629174706176",
            "9444732965739290427392",
            "2417851639229258349412352",
            "18889465931478580854784",
            "4835703278458516698824704",
            "37778931862957161709568",
            "9671406556917033397649408",
            "75557863725914323419136",
            "19342813113834066795298816",
            "151115727451828646838272",
            "38685626227668133590597632",
            "302231454903657293676544",
            "77371252455336267181195264",
            "604462909807314587353088",
            "154742504910672534362390528",
            "618970019642690137449562112",
            "1237940039285380274899124224",
            "2475880078570760549798248448",
            "4951760157141521099596496896",
            "9903520314283042199192993792",
            "39614081257132168796771975168",
            "19807040628566084398385987584",
            "79228162514264337593543950336",
            "158456325028528675187087900672",
            "316912650057057350374175801344",
            "633825300114114700748351602688",
            "1267650600228229401496703205376",
            "2535301200456458802993406410752",
            "5070602400912917605986812821504",
            "10141204801825835211973625643008",
            "20282409603651670423947251286016",
            "40564819207303340847894502572032",
            "81129638414606681695789005144064",
            "162259276829213363391578010288128",
            "324518553658426726783156020576256",
            "4503599627370496",
            "10000000000000",
            "9007199254740992",
            "20000000000000",
            "309485009821345068724781056",
            "10000000000000000000000"
        };

        public enum MarketTypes
        {
            FixtureMarkets,
            CurrentDriveMarkets,
            FirstLastNextMarkets,
            FixtureCorrectScoreMarkets,
            PlayerCombinedMarkets,
            PlayerMarkets,
            PlayerMatchupHandicapMarkets,
            PlayerMatchupMarkets,
            PlayerMilestoneMarkets,
            PlayerMostMarkets,
            PlayerRaceToMarkets,
            BetBuilderMarkets,
            CurrentAtBat,
            HalfInningMarkets
        }

        public enum MarketSuspension
        {
            UnSuspended = 0,
            Suspended =1,
            
        }

        public static string GetSportMapping(string sport)
        {
            return SportMapping[sport.ToLower()];
        }

        public static string GetWebApiUrl(string sport)
        {
            return EnvironmentSportWebApiUrlMapping[Base.Base.Configuration.Environment][sport.ToLower()];
        }
    }
}
