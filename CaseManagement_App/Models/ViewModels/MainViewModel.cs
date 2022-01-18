using CaseManagement_App.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement_App.Models.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        private object _currentView;

        public MainViewModel()
        {
            RegUserViewModel = new RegUserViewModel();
            CreateCaseViewModel = new CreateCaseViewModel();
            ShowUsersViewModel = new ShowUsersViewModel();
            ShowCasesViewModel = new ShowCasesViewModel();
            SearchUserViewModel = new SearchUserViewModel();
            SearchCaseViewModel = new SearchCaseViewModel();
            UpdateCaseViewModel = new UpdateCaseViewModel();
            HomeViewModel = new HomeViewModel();
            CurrentView = HomeViewModel;

            RegUserViewCommand = new RelayCommand(x => CurrentView = RegUserViewModel);
            CreateCaseViewCommand = new RelayCommand(x => CurrentView = CreateCaseViewModel);
            ShowUsersViewCommand = new RelayCommand(x => CurrentView = ShowUsersViewModel);
            ShowCasesViewCommand = new RelayCommand(x => CurrentView = ShowCasesViewModel);
            SearchUsersViewCommand = new RelayCommand(x => CurrentView = SearchUserViewModel);
            SearchCaseViewCommand = new RelayCommand(x => CurrentView = SearchCaseViewModel);
            UpdateCaseViewCommand = new RelayCommand(x => CurrentView = UpdateCaseViewModel);
            HomeViewCommand = new RelayCommand(x => CurrentView = HomeViewModel);
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand RegUserViewCommand { get; set; }
        public RegUserViewModel RegUserViewModel { get; set; }

        public RelayCommand CreateCaseViewCommand { get; set; }
        public CreateCaseViewModel CreateCaseViewModel { get; set; }

        public RelayCommand ShowUsersViewCommand { get; set; }
        public ShowUsersViewModel ShowUsersViewModel { get; set; }

        public RelayCommand ShowCasesViewCommand { get; set; }
        public ShowCasesViewModel ShowCasesViewModel { get; set; }

        public RelayCommand SearchUsersViewCommand { get; set; }
        public SearchUserViewModel SearchUserViewModel { get; set; }

        public RelayCommand SearchCaseViewCommand { get; set; }
        public SearchCaseViewModel SearchCaseViewModel { get; set; }

        public RelayCommand UpdateCaseViewCommand { get; set; }
        public UpdateCaseViewModel UpdateCaseViewModel { get; set; }

        public RelayCommand HomeViewCommand { get; set; }
        public HomeViewModel HomeViewModel { get; set; }
    }
}
