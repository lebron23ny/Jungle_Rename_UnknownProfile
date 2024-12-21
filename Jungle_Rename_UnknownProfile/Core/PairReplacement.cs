using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_Rename_UnknownProfile.Core
{
    public class PairReplacement
    {
        public string OldProfile { get; set; }
        public string NewProfile { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PairReplacement replacement &&
                   OldProfile == replacement.OldProfile;
        }
    }
}
