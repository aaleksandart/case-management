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
    /// Interaction logic for ShowUsersView.xaml
    /// </summary>
    public partial class ShowUsersView : UserControl
    {
        private IEnumerable<User> _userList = null!;
        private IEnumerable<Admin> _adminList = null!;
        private IUserService userService = new UserService();

        public ShowUsersView()
        {
            InitializeComponent();
            GetUsers();
            GetAdmins();
        }

        private void GetUsers()
        {
            _userList = userService.GetAllUsers();
            foreach (var user in _userList)
            {
                lvGetUsers.Items.Add(user);
            }
        }

        private void GetAdmins()
        {
            _adminList = userService.GetAllAdmins();
            foreach (var admin in _adminList)
            {
                lvGetAdmins.Items.Add(admin);
            }
        }
    }
}
