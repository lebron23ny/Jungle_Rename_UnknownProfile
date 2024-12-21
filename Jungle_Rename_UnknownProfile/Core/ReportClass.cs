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
            ReportClass a = this;
            ReportClass b = obj as ReportClass;
            return string.Compare(a.UnknownProfile, b.UnknownProfile);
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
