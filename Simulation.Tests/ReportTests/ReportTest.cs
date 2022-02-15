using Simulation.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReportTests
{
    public class ShadowBolt
    {
        [Fact]
        public void DPSOver10000Milis()
        {
            double dmgDone = 0;
            Report report = new()
            {
                FightLength = 10000
            };
            Warlock wl = new();
            Wowhead wh = new();
            Spell shadowbolt = wh.GetSpell(27209);
            dmgDone += wl.CastShadowBolt(shadowbolt, report);
            dmgDone += wl.CastShadowBolt(shadowbolt, report);
            dmgDone += wl.CastShadowBolt(shadowbolt, report);
            double dps = dmgDone / (report.FightLength / 1000);

            Assert.Equal(dps, report.DPS);
        }
    }
}
