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
    /// Interaction logic for SearchCaseView.xaml
    /// </summary>
    public partial class SearchCaseView : UserControl
    {
        private ICaseService caseService = new CaseService();
        private IEnumerable<Cases> _caseList = null!;
        public SearchCaseView()
        {
            InitializeComponent();
            GetAllCases();
        }

        private void btnSeeDetails_Click(object sender, RoutedEventArgs e)
        {
            lvCaseDetails.Items.Clear();
            GetCaseDetails();
        }

        private void GetAllCases()
        {
            _caseList = caseService.GetAllCases();
            foreach(var c in _caseList)
            {
                lvCaseChoice.Items.Add(c);
            }
        }

        private void GetCaseDetails()
        {
            var selectedItem = (Cases)lvCaseChoice.SelectedItem;
            var item = caseService.GetCase(selectedItem.Id);

            if(lvCaseChoice.SelectedValue != null)
                lvCaseDetails.Items.Add(item);
            else
            { }
        }
    }
}
