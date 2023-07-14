using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FontAwesome.Sharp;
using Skreenkinikor_Project.Model;
using Skreenkinikor_Project.Repositories;

namespace Skreenkinikor_Project.ViewModel
{
    public class MainViewModel : VMBase
    {
        //Fields
        private AccountModel _currentUser;
        private VMBase _currentVMChild;
        private string _caption;
        private IconChar _icon;
        private IUserRepo _userRepo;

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

        //Properties
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

        //Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowActorViewCommand { get; }
        public ICommand ShowMovieViewCommand { get; }
        public ICommand ShowConfectionaryViewCommand { get; }
        public ICommand ShowReportsViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

        public MainViewModel()
        {
            _userRepo = new UserRepo();
            CurrentUser = new AccountModel();

            //Initialize Commands
            ShowHomeViewCommand = new VMCommand(ExecuteHomeViewCommand);
            ShowActorViewCommand = new VMCommand(ExecuteShowActorViewCommand);
            ShowMovieViewCommand = new VMCommand(ExecuteShowMovieViewCommand);
            ShowConfectionaryViewCommand = new VMCommand(ExecuteConfectionaryViewCommand);
            ShowReportsViewCommand = new VMCommand(ExecuteReportsViewCommand);
            ShowSettingsViewCommand = new VMCommand(ExcuteSettingsViewCommand);


            ExecuteHomeViewCommand(null);
            LoadCurrent();
        }

        private void ExcuteSettingsViewCommand(object obj)
        {
            CurrentVMChild = new VMSettings();
            Caption = "Settings";
            Icon = IconChar.Gears;
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

        private void ExecuteShowMovieViewCommand(object obj)
        {
            CurrentVMChild = new VMMovies();
            Caption = "Movies";
            Icon = IconChar.Video;
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

        private void LoadCurrent()
        {
            var user = _userRepo.GetUsername(Thread.CurrentPrincipal.Identity.Name);
            if(user != null)
            {
                CurrentUser.Username = user.Username;
                CurrentUser.DisplayUser = $"{user.Name} {user.Lastname}";
                CurrentUser.pfp = null;
            }
            else
            {
                CurrentUser.DisplayUser = "Invalid user, not logged in!";
                
            }    
        }
    }
}
