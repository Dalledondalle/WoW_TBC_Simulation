using Simulation.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Simulation.Tests
{
    public class ReportTest
    {
        [Fact]
        public void ReportShadowBolt()
        {
            double dmgDone = 0;
            Report report = new()
            {
                FightLength = 10000
            };
            Warlock wl = new();
            dmgDone += wl.CastShadowBolt(11, report);
            dmgDone += wl.CastShadowBolt(11, report);
            dmgDone += wl.CastShadowBolt(11, report);
            double dps = dmgDone / (report.FightLength / 1000);

            Assert.Equal(dps, report.DPS);
        }
    }
}
