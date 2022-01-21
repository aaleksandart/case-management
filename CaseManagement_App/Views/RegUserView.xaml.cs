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
    /// Interaction logic for RegUserView.xaml
    /// </summary>
    public partial class RegUserView : UserControl
    {
        private IUserService userService = new UserService();

        public RegUserView()
        {
            InitializeComponent();
        }

        private void btnRegisterUser_Click(object sender, RoutedEventArgs e)
        {
            RegisterUser();   
        }

        private void RegisterUser()
        {
            if (!string.IsNullOrEmpty(inputFirstName.Text)
                && !string.IsNullOrEmpty(inputLastName.Text)
                && !string.IsNullOrEmpty(inputEmail.Text)
                && !string.IsNullOrEmpty(inputPhoneNumber.Text)
                && !string.IsNullOrEmpty(inputStreetName.Text)
                && !string.IsNullOrEmpty(inputPostalCode.Text)
                && !string.IsNullOrEmpty(inputCity.Text)
                && !string.IsNullOrEmpty(inputCountry.Text))
            {
                UserModel NewUser = new UserModel
                {
                    FirstName = inputFirstName.Text,
                    LastName = inputLastName.Text,
                    Role = new RoleModel { Name = "User" },
                    ContactInfo = new ContactInfoModel { Email = inputEmail.Text, PhoneNumber = inputPhoneNumber.Text },
                    Address = new AddressModel { StreetName = inputStreetName.Text, PostalCode = inputPostalCode.Text, City = inputCity.Text, Country = inputCountry.Text }
                };
                if (userService.CreateUser(NewUser) != 0)
                {
                    Success();
                    tbStatusMessage.Text = "Saved new user succesfully.";
                }
                else
                {   tbStatusMessage.Text = "There is some problem with your info. Try another email address.";  }
            }
        }

        private void Success()
        {
            inputFirstName.Text = "";
            inputLastName.Text = "";
            inputEmail.Text = "";
            inputPhoneNumber.Text = "";
            inputStreetName.Text = "";
            inputPostalCode.Text = "";
            inputCity.Text = "";
            inputCountry.Text = "";
        }
    }
}
