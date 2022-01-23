using CaseManagement_App.Entities;
using CaseManagement_App.Services;
using System;
using System.Collections.Generic;
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

namespace CaseManagement_App.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private ICaseService caseService = new CaseService();
        private IEnumerable<Cases> _caseList = null!;

        public HomeView()
        {
            InitializeComponent();
            ShowLatest();
            GetStatistics();
        }

        //ShowLatest använder CaseService för att hämta och skriva ut senaste skapade Cases
        private void ShowLatest()
        {
            _caseList = caseService.GetLastCases();
            foreach (var c in _caseList)
            {
                lvLastCreatedCases.Items.Add(c);
            }
        }

        //GetStatistics använder CaseService för att hämta antalet Cases med specifika CaseStates
        private void GetStatistics()
        {
            tbCreated.Text = $"At the moment we have {caseService.Get_Statistics(1)} cases that are created.";
            tbInProgress.Text = $"There is {caseService.Get_Statistics(2)} cases in progress.";
            tbClosed.Text = $"{caseService.Get_Statistics(3)} cases are closed.";
        }
    }
}
