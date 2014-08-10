using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GridSearchOperationGroups
    {
        /// <summary>
        /// All operations
        /// </summary>
        public const GridSearchOperation Everything = GridSearchOperation.BN | GridSearchOperation.BW | GridSearchOperation.CN | GridSearchOperation.EN | GridSearchOperation.EQ
                                                        | GridSearchOperation.EW | GridSearchOperation.GE | GridSearchOperation.GT | GridSearchOperation.LE | GridSearchOperation.LT
                                                        | GridSearchOperation.NC | GridSearchOperation.NE;
        public const GridSearchOperation AllStrings = GridSearchOperation.BN | GridSearchOperation.BW | GridSearchOperation.CN | GridSearchOperation.EN | GridSearchOperation.EQ
                                                        | GridSearchOperation.EW | GridSearchOperation.NC | GridSearchOperation.NE;
        public const GridSearchOperation AllNumbers = GridSearchOperation.EQ | GridSearchOperation.GE | GridSearchOperation.GT
                                                        | GridSearchOperation.LE | GridSearchOperation.LT | GridSearchOperation.NE;
        public const GridSearchOperation AllDates = AllNumbers;

        public static string[] GetToLoweredStringArray(GridSearchOperation op)
        {
            return op.ToString()
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim().ToLower())
                        .ToArray();
        }
    }
}