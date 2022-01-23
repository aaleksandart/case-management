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
    /// Interaction logic for ShowCasesView.xaml
    /// </summary>
    public partial class ShowCasesView : UserControl
    {
        private IEnumerable<Cases> _caseList = null!;
        private ICaseService caseService = new CaseService();

        public ShowCasesView()
        {
            InitializeComponent();
            GetCases();
        }

        //GetCases använder CaseService för att hämta och skriva ut alla Cases
        private void GetCases()
        {
            _caseList = caseService.GetAllCases();
            foreach (var c in _caseList)
            {
                lvGetCases.Items.Add(c);
            }
        }
    }
}
