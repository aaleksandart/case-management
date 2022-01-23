using CaseManagement_App.Entities;
using CaseManagement_App.Models;
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
    /// Interaction logic for UpdateCaseView.xaml
    /// </summary>
    public partial class UpdateCaseView : UserControl
    {
        private ICaseService caseService = new CaseService();
        private IEnumerable<Cases> _caseList = null!;
        private IEnumerable<CaseState> _caseStateList = null!;

        public UpdateCaseView()
        {
            InitializeComponent();
            GetAllCases();
            GetStates();
        }

        private void btnUpdateCase_Click(object sender, RoutedEventArgs e)
        {
            UpdateCase();
        }

        //GetAllCases använder CaseService för att hämta och skriva ut alla Cases
        private void GetAllCases()
        {
            _caseList = caseService.GetAllCases();
            foreach (var c in _caseList)
            {
                lvSelectCaseUpdate.Items.Add(c);
            }
        }

        //GetStates använder CaseService för att hämta och skriva ut alla CaseStates förutom Created
        private void GetStates()
        {
            _caseStateList = caseService.GetStates();
            foreach (var cs in _caseStateList)
            {
                if (cs.Name == "In-progress")
                {
                    rBtnAddState.Content = cs.Name;
                }
                    
                else if (cs.Name == "Closed")
                    rBtnAddState2.Content = cs.Name;
            };
        }

        //UpdateCase tar in och kontrollerar uppdaterad info om ett Case
        //Använder CaseService för att sedan uppdatera valt Case
        private void UpdateCase()
        {
            string _updateHeader = updateHeader.Text;
            string _updateDescription = updateDescription.Text;

            if (lvSelectCaseUpdate.SelectedValue != null && (rBtnAddState.IsChecked == true || rBtnAddState2.IsChecked == true))
            {
                var selectedCase = (Cases)lvSelectCaseUpdate.SelectedItem;
                CaseState selectedState;

                if (rBtnAddState.IsChecked == true)
                    selectedState = caseService.GetState(2);
                    
                else
                    selectedState = caseService.GetState(3);

                if (!string.IsNullOrEmpty(_updateHeader) && !string.IsNullOrEmpty(_updateDescription))
                {
                    caseService.UpdateCase(_updateHeader, _updateDescription, selectedState.Id, selectedCase.Id);
                }
                else
                {
                    if (!string.IsNullOrEmpty(_updateDescription))
                    {
                        caseService.UpdateCase(selectedCase.Header, _updateDescription, selectedState.Id, selectedCase.Id);
                    }
                    else if(!string.IsNullOrEmpty(_updateHeader))
                    {
                        caseService.UpdateCase(_updateHeader, selectedCase.Descriptions, selectedState.Id, selectedCase.Id);
                    }
                    else
                        caseService.UpdateCase(selectedCase.Header, selectedCase.Descriptions, selectedState.Id, selectedCase.Id);
                }
                updateHeader.Text = "";
                updateDescription.Text = "";
                processState.Text = "";
                lvSelectCaseUpdate.Items.Clear();
                GetAllCases();
            }
            else
            {
                processState.Text = "You must choose one case and one state.";
            }
        }
    }
}
