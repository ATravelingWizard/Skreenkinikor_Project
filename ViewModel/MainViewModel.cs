using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Skreenkinikor_Project.Model;
using Skreenkinikor_Project.Repositories;

namespace Skreenkinikor_Project.ViewModel
{
    public class MainViewModel : VMBase
    {
        //Fields
        private AccountModel _currentUser;
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

        public MainViewModel()
        {
            _userRepo = new UserRepo();
            CurrentUser = new AccountModel();
            LoadCurrent();
        }

        private void LoadCurrent()
        {
            var user = _userRepo.GetUsername(Thread.CurrentPrincipal.Identity.Name);
            if(user != null)
            {
                CurrentUser.Username = user.Username;
                CurrentUser.DisplayUser = $"Welcome {user.Name} {user.Lastname}";
                CurrentUser.pfp = null;
            }
            else
            {
                CurrentUser.DisplayUser = "Invalid user, not logged in!";
                
            }    
        }
    }
}
