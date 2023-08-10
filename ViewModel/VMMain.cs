using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FontAwesome.Sharp;
using Skreenkinikor_Project.Model;
using Skreenkinikor_Project.Repositories;
using Skreenkinikor_Project.View;

namespace Skreenkinikor_Project.ViewModel
{
    public class VMMain : VMBase
    {
        //Fields to be used in methods
        private AccountModel _currentUser;
        private VMBase _currentVMChild;
        private string _caption;
        private IconChar _icon;
        private IUserRepo _userRepo;
        private IsAdminRepo _isAdminRepo;
        //Selects current logged in user
        public AccountModel CurrentUser 
        {
            get
            {
                return _currentUser;
            } 
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }


        //Properties used by exec methods
        public VMBase CurrentVMChild
        {
            get
            {
                return _currentVMChild;
            }
            set
            {
                _currentVMChild = value;
                OnPropertyChanged(nameof(CurrentVMChild));
            }
        }
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        //Init Commands (uses these commands in xaml to call children
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowActorViewCommand { get; }
        public ICommand ShowScheduleViewCommand { get; }
        public ICommand ShowConfectionaryViewCommand { get; }
        public ICommand ShowReportsViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }
        public ICommand ShowMoviesViewCommand { get; }
        public ICommand ShowTicketViewCommand { get; }
        public ICommand ShowStockViewCommand { get; }
        //Logout
        public ICommand LogoutCommand { get; }

        public VMMain()
        {
            //Variables
            _userRepo = new UserRepo();
            CurrentUser = new AccountModel();
            _isAdminRepo = new IsAdminRepo();

            //Initialize Show Commands
            ShowHomeViewCommand = new VMCommand(ExecuteHomeViewCommand);
            ShowActorViewCommand = new VMCommand(ExecuteShowActorViewCommand);
            ShowScheduleViewCommand = new VMCommand(ExecuteShowScheduleViewCommand);
            ShowConfectionaryViewCommand = new VMCommand(ExecuteConfectionaryViewCommand);
            ShowReportsViewCommand = new VMCommand(ExecuteReportsViewCommand);
            ShowSettingsViewCommand = new VMCommand(ExcuteSettingsViewCommand);
            ShowMoviesViewCommand = new VMCommand(ExecuteMoviesViewCommand);
            ShowTicketViewCommand = new VMCommand(ExecuteTicketViewCommand);
            ShowStockViewCommand = new VMCommand(ExecuteStockViewCommand);

            //Default methods on startup
            ExecuteHomeViewCommand(null);
            LoadCurrent();
        }
       //Execute Display Window commands

        private void ExcuteSettingsViewCommand(object obj)
        {
            CurrentVMChild = new VMSettings(); //Sets Child of MainView to chosen view
            Caption = "Settings"; //Caption (displayed in navbar and at the top of content
            Icon = IconChar.Gears; //Icon (displayed in navbar and next to content title
        }
        private void ExecuteTicketViewCommand(object obj)
        {
            CurrentVMChild = new VMTickets();
            Caption = "Ticket Sales";
            Icon = IconChar.Ticket;
        }
        private void ExecuteStockViewCommand(object obj)
        {
            CurrentVMChild = new VMStock();
            Caption = "Manage Stock";
            Icon = IconChar.BasketShopping;
        }
        private void ExecuteMoviesViewCommand(object obj)
        {
            CurrentVMChild = new VMMovies();
            Caption = "Manage Movies";
            Icon = IconChar.VideoCamera;
        }

        private void ExecuteReportsViewCommand(object obj)
        {
            CurrentVMChild = new VMReports();
            Caption = "Reports";
            Icon = IconChar.File;
        }

        private void ExecuteConfectionaryViewCommand(object obj)
        {
            CurrentVMChild = new VMConfectionary();
            Caption = "Confectionary";
            Icon = IconChar.BowlFood;
        }

        private void ExecuteShowScheduleViewCommand(object obj)
        {
            CurrentVMChild = new VMSchedule();
            Caption = "Schedule";
            Icon = IconChar.Calendar;
        }

        private void ExecuteShowActorViewCommand(object obj)
        {
            CurrentVMChild = new VMActors();
            Caption = "Actors";
            Icon = IconChar.Person;
        }

        private void ExecuteHomeViewCommand(object obj)
        {
            CurrentVMChild = new VMHome();
            Caption = "Home";
            Icon = IconChar.Home;
        }
        //Load current logged in user
        private void LoadCurrent()
        {
            var user = _userRepo.GetUsername(Thread.CurrentPrincipal.Identity.Name);
            if(user != null)
            {
                CurrentUser.Username = user.Username;
                CurrentUser.DisplayUser = $"{user.Name} {user.Lastname}";

                bool isAdmin = _isAdminRepo.GetAdmin(user.Username);
                IsAdmin = isAdmin;
            }
            else
            {
                CurrentUser.DisplayUser = "Invalid user, not logged in!";
                
            }    
        }

        //Visibility Modifier
        private CheckAdminModel _checkAdmin = new CheckAdminModel();
        public bool IsAdmin
        {
            get { return _checkAdmin.IsAdmin; }
            set
            {
                _checkAdmin.IsAdmin = value;
            }
        }
    }
}
