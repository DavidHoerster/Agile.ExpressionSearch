using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileWays.ExpressionSearch2.Repository.Entity
{
    public class Batting
    {
        public String Id { get; set; }
        public String PlayerId { get; set; }
        public Int32 Year { get; set; }
        public Int32 Stint { get; set; }
        public String TeamId { get; set; }
        public String LeagueId { get; set; }
        public Int32 Games { get; set; }
        public Int32 GamesAsBatter { get; set; }
        public Int32 AtBats { get; set; }
        public Int32 Runs { get; set; }
        public Int32 Hits { get; set; }
        public Int32 Doubles { get; set; }
        public Int32 Triples { get; set; }
        public Int32 HomeRuns { get; set; }
        public Int32 RunsBattedIn { get; set; }
        public Int32 StolenBases { get; set; }
        public Int32 CaughtStealing { get; set; }
        public Int32 BaseOnBalls { get; set; }
        public Int32 Strikeouts { get; set; }
        public Int32 IntentionalWalks { get; set; }
        public Int32 HitByPitch { get; set; }
        public Int32 SacrificeHits { get; set; }
        public Int32 SacrificeFlies { get; set; }
        public Int32 GroundedIntoDoublePlays { get; set; }
    }
}
