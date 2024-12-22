using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_Rename_UnknownProfile.Core
{
    public class ReportClass:BaseViewModel, IComparable
    {
        public bool Flag { get; set; }

        public string UnknownProfile { get; set; }
        public string NewProfile { get; set; }

        public int CompareTo(object obj)
        {
            ReportClass other = obj as ReportClass;
            return UnknownProfile.CompareTo(other.UnknownProfile);
        }

        public override bool Equals(object obj)
        {
            return obj is ReportClass @class &&
                   UnknownProfile == @class.UnknownProfile;
        }

        public override string ToString()
        {
            return UnknownProfile;
        }
    }
}
