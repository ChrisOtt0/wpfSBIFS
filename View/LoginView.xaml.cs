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
using wpfSBIFS.Model;
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
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //get login data from the ui
            string userName = tbUserName.Text;
            string passWord = pbPassword.Password;


            //hard coded values for now
            string hostName = "localhost";
            int port = 8080;


            ServerConnectionAdapter sva = new ServerConnectionAdapter(hostName, port);
            sva.Login(userName, passWord);

        }
    }
}
