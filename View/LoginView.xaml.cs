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
using wpfSBIFS.ViewModel;

namespace wpfSBIFS.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        ILoginViewModel loginViewModel;

        public LoginView(ILoginViewModel viewModel)
        {
            InitializeComponent();

            // Set viewmodel received from DI as binding context
            loginViewModel = viewModel;
            loginViewModel.PasswordBox = pbPassword;
            this.DataContext = viewModel;
        }
    }
}
