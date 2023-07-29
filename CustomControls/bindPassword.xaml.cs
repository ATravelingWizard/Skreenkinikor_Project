using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace Skreenkinikor_Project.CustomControls
{
    /// <summary>
    /// Interaction logic for bindPassword.xaml
    /// </summary>
    public partial class bindPassword : UserControl
    {
        public static readonly DependencyProperty PasswordProperty = 
            DependencyProperty.Register("Password", typeof(SecureString), typeof(bindPassword)); //Makes password secure
        //Only way to retrieve text from a secure string 
        public SecureString Password
        {
            get
            {
                return (SecureString)GetValue(PasswordProperty);
            }
            set
            {
                SetValue(PasswordProperty, value);
            }
        }
        //Sets password as it is being entered
        public bindPassword()
        {
            InitializeComponent();
            txtPassword.PasswordChanged += OnPassChange;
        }
        //Makes password secure while typing
        private void OnPassChange(object sender, RoutedEventArgs e)
        {
            Password = txtPassword.SecurePassword;
        }
    }
}
