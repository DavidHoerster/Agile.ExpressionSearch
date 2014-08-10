using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileWays.ExpressionSearch2.Repository.Entity
{
    public class ExtendedBatter
    {
        public String Id { get; set; }
        public String PlayerId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Int32 Year { get; set; }
        public String TeamId { get; set; }
        public Int32 Games { get; set; }
        public Int32 AtBats { get; set; }
        public Int32 Runs { get; set; }
        public Int32 Hits { get; set; }
        public Int32 Doubles { get; set; }
        public Int32 Triples { get; set; }
        public Int32 HomeRuns { get; set; }
        public Int32 RunsBattedIn { get; set; }
        public Int32 Salary { get; set; }
    }
}
