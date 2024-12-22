using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TSM = Tekla.Structures.Model;
using TSC = Tekla.Structures.Catalogs;
using Jungle_Rename_UnknownProfile.Core;
using System.IO;

namespace Jungle_Rename_UnknownProfile
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ControlWindow controlWindow;
        private string mapPath = "mekhanobr_mapping.csv";
        List<PairReplacement> replacementList;
        TSM.Model Model { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Model = new TSM.Model();
            if(Model.GetConnectionStatus())
                Connct_Bar.Jungle_Connect = true;
            controlWindow = new ControlWindow();
            this.DataContext = controlWindow;
            replacementList = GetReplacementList();
            controlWindow.ListReport = ReadUnknownProfile(); 
            
        }

        private void Mekhanobr_Title_bar_Jungle_Application_Close(object arg1, EventArgs arg2)
        {
            this.Close();
        }

        private void Mekhanobr_Title_bar_Jungle_Application_DragMove(object arg1, EventArgs arg2)
        {
            this.DragMove();
        }


        public ObservableCollection<ReportClass> ReadUnknownProfile()
        {
            ObservableCollection<ReportClass> reportCollection = new ObservableCollection<ReportClass>();
            List<ReportClass> listRepot = new List<ReportClass>();
            TSM.Model model = new TSM.Model();
            if (model.GetConnectionStatus())
            {
                TSM.ModelObjectSelector modelObjectSelector = model.GetModelObjectSelector();
                var beams = modelObjectSelector.GetAllObjectsWithType(TSM.ModelObject.ModelObjectEnum.BEAM);
                List<string> unknownProfile = new List<string>();
                TSC.LibraryProfileItem libraryProfileItem = new TSC.LibraryProfileItem();
                while (beams.MoveNext())
                {
                    TSM.Beam beam = beams.Current as TSM.Beam;
                    string profile = beam.Profile.ProfileString;
                    if (!libraryProfileItem.Select(profile))
                    {
                        if (!reportCollection.Contains(new ReportClass { Flag = true, UnknownProfile = profile, NewProfile = "dssds" }))
                        {
                            string newProf = GetNewProfile(profile);
                            if(newProf != string.Empty)
                            {
                                reportCollection.Add(new ReportClass { Flag = true, UnknownProfile = profile, NewProfile = GetNewProfile(profile) });
                                listRepot.Add(new ReportClass { Flag = true, UnknownProfile = profile, NewProfile = GetNewProfile(profile) });
                            }
                                
                            else
                            {
                                reportCollection.Add(new ReportClass { Flag = false, UnknownProfile = profile, NewProfile = GetNewProfile(profile) });
                                listRepot.Add(new ReportClass { Flag = false, UnknownProfile = profile, NewProfile = GetNewProfile(profile) });
                            }
                                
                        }
                            


                    }
                }
            }

            listRepot.Sort();
            reportCollection = new ObservableCollection<ReportClass>(listRepot);
            return reportCollection;
        }


        public List<PairReplacement> GetReplacementList()
        {
            List<PairReplacement> pairs = new List<PairReplacement>();
            if(File.Exists(mapPath))
            {
                string[] lines = File.ReadAllLines(mapPath);
                foreach (string line in lines)
                {
                    var values = line.Split(';');
                    if (!pairs.Contains(new PairReplacement { OldProfile = values[0], NewProfile = values[1] }))
                        pairs.Add(new PairReplacement { OldProfile = values[0], NewProfile = values[1] });
                }
            }




            return pairs;
        }

        private string GetNewProfile(string oldProf)
        {
            string prof = string.Empty;
            if(replacementList.Where(pair=>pair.OldProfile == oldProf).FirstOrDefault() != null)
                return replacementList.Where(pair=> pair.OldProfile == oldProf).FirstOrDefault().NewProfile;
            return prof;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<ReportClass> listReplace = controlWindow.ListReport.Where(r => r.Flag == true).ToList();
            foreach (ReportClass reportClass in listReplace)
            {
                string profOld = reportClass.UnknownProfile;
                string profNew = reportClass.NewProfile;
                if(profNew != string.Empty)
                {
                    TSM.ModelObjectEnumerator modelObjectEnumerator = Tools.GetModelObjectEnumerator(profOld);
                    while (modelObjectEnumerator.MoveNext())
                    {
                        if (modelObjectEnumerator.Current is TSM.Beam)
                        {
                            TSM.Beam beam = modelObjectEnumerator.Current as TSM.Beam;
                            beam.Profile.ProfileString = profNew;
                            beam.Modify();
                            reportClass.UnknownProfile = profNew;
                        }
                    }
                }


            }
            
            Model.CommitChanges();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            replacementList = GetReplacementList();
            controlWindow.ListReport = ReadUnknownProfile();
        }
    }
}
