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
    /// Interaction logic for NavMenuView.xaml
    /// </summary>
    public partial class NavMenuView : UserControl
    {
        private readonly INavMenuViewModel _viewModel;

        public NavMenuView(INavMenuViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _viewModel.NavMenuPanel = this.navMenuPanel;
            this.DataContext = _viewModel;
            _viewModel.SetHome();
        }
    }
}
