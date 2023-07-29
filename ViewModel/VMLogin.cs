using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Windows.Input;
using Skreenkinikor_Project.Model;
using Skreenkinikor_Project.Repositories;
using System.Net;
using System.Threading;
using System.Security.Principal;

namespace Skreenkinikor_Project.ViewModel
{
    public class VMLogin : VMBase
    {
        //Fields to use for database
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isVisible = true;

        private IUserRepo userRep;

        //Property Methods
        public string Username 
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
        //Commands 
        public ICommand LoginCommand { get; }
        public ICommand ProblemCommand { get; }
        public ICommand ShowPassCommand { get; }

        //Constructors
        public VMLogin()
        {
            userRep = new UserRepo();
            LoginCommand = new VMCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            ProblemCommand = new VMCommand(p => ExecuteErrorWithLoginCommand(""));
        }
        //Checks if password and username param's are met
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }
        //Displays error message
        private void ExecuteLoginCommand(object obj)
        {
            var isUserValid = userRep.AuthUser(new NetworkCredential(Username, Password));
            if(isUserValid == true)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                IsVisible = false;
            }
            else
            {
                ErrorMessage = "* Invalid Username and/or Password!";
            }
        }
        //Not yet coded
        private void ExecuteErrorWithLoginCommand(string username)
        {
            throw new NotImplementedException();
        }
    }
}
