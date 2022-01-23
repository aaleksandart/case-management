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
    /// Interaction logic for CreateCaseView.xaml
    /// </summary>
    public partial class CreateCaseView : UserControl
    {
        private IUserService userService = new UserService();
        private ICaseService caseService = new CaseService();
        private IEnumerable<User> _userList = null!;

        public CreateCaseView()
        {
            InitializeComponent();
            GetUsers();
        }

        private void btnCreateCase_Click(object sender, RoutedEventArgs e)
        {
            CreateCase();
        }
        
        //GetUsers hämtar med hjälp av UserService alla användare och skriver ut dem
        private void GetUsers()
        {
            _userList = userService.GetAllUsers();
            foreach (var _user in _userList)
            {
                lvSelectUserToCase.Items.Add(_user);
            }
        }

        //CreateCase kontrollerar att användarens input stämmer
        //Använder sedan CaseService för att skapa Cases
        private void CreateCase()
        {
            var selectedItemId = (User)lvSelectUserToCase.SelectedItem;
            
            if (!string.IsNullOrEmpty(inputHeader.Text)
                && !string.IsNullOrEmpty(inputDescription.Text)
                && !string.IsNullOrEmpty(inputAdminFirstName.Text)
                && !string.IsNullOrEmpty(inputAdminLastName.Text)
                && lvSelectUserToCase.SelectedValue != null)
            {
                CasesModel NewCase = new CasesModel
                {
                    Header = inputHeader.Text,
                    Descriptions = inputDescription.Text,
                    CreatedDate = DateTime.Now,
                    User = userService.GetUser(selectedItemId.Id),
                    CaseState = new CaseStateModel { Name = "Created"},
                    Admin = new AdminModel { FirstName = inputAdminFirstName.Text, LastName = inputAdminLastName.Text, Role = new RoleModel { Name = "Admin" } }
                };
                if (caseService.CreateCase(NewCase) == 0)
                {
                    Success();
                    tbStatusMessageCase.Text = "Created a new case succesfully.";
                }
                else
                { tbStatusMessageCase.Text = "Something went wrong. \nDo you have a case with\nthe same header already?"; }
            }
            else if (!string.IsNullOrEmpty(inputHeader.Text)
                && !string.IsNullOrEmpty(inputDescription.Text)
                && lvSelectUserToCase.SelectedValue != null)
            {
                CasesModel NewCase = new CasesModel
                {
                    Header = inputHeader.Text,
                    Descriptions = inputDescription.Text,
                    CreatedDate = DateTime.Now,
                    User = userService.GetUser(selectedItemId.Id),
                    CaseState = new CaseStateModel { Name = "Created" }
                };
                if (caseService.CreateCase(NewCase) == 0)
                {
                    Success();
                    tbStatusMessageCase.Text = "Created a new case succesfully.";
                }
                else
                {   tbStatusMessageCase.Text = "Something went wrong. \nDo you have a case with\nthe same header already?";    }
            }
            else
            {   tbStatusMessageCase.Text = "You need to fill at least \n'Header' and 'Description'.\nAlso you need to select a user.";  }
        }
        
        //Success rensar formuläret
        private void Success()
        {
            inputHeader.Text = "";
            inputDescription.Text = "";
            inputAdminFirstName.Text = "";
            inputAdminLastName.Text = "";
        }
    }
}
