using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Unity;
using wpfSBIFS.Services.HttpService;
using wpfSBIFS.Services.TokenService;
using wpfSBIFS.ViewModel;

namespace wpfSBIFS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // UnityContainer - DI
        public static IUnityContainer container = new UnityContainer();

        // Reference to main windows content control
        public ContentControl ccRef { get; set; } = null;

        public App()
        {
            ConfigureServices();
        }

        // Method to change view
        public void ChangeUserControl(UserControl view)
        {
            this.ccRef.Content = view;
        }

        // Configure DI service provider
        private void ConfigureServices()
        {
            container.RegisterType<ILoginViewModel, LoginViewModel>();
            container.RegisterSingleton<INavMenuViewModel, NavMenuViewModel>();
            container.RegisterSingleton<IAccountViewModel, AccountViewModel>();
            container.RegisterSingleton<IGroupViewModel, GroupViewModel>();
            container.RegisterSingleton<IHttpService, HttpService>();
            container.RegisterSingleton<ITokenService, TokenService>();
            
        }
    }
}
