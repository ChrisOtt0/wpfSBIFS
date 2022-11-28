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
    /// Interaction logic for GroupView.xaml
    /// </summary>
    public partial class GroupView : UserControl
    {
        private readonly IGroupViewModel _groupViewModel;

        public GroupView(IGroupViewModel groupViewModel)
        {
            InitializeComponent();
            _groupViewModel = groupViewModel;
            this.DataContext = _groupViewModel;
        }
    }
}
