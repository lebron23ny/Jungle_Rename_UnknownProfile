using Jungle_Rename_UnknownProfile.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_Rename_UnknownProfile
{
    public class ControlWindow : BaseViewModel
    {
        public bool AllSet {  get; set; } = true;
        public ObservableCollection<ReportClass> ListReport { get; set; } = new ObservableCollection<ReportClass>();
    }
}
