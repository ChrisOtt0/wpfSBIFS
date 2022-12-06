using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfSBIFS.Tools;
using wpfSBIFS.ViewModel;

namespace wpfSBIFS.Services.NavigationService
{
    public class NavigationService : INavigationService
    {
        public Command MoveToAccountView { get; set; }
        public Command MoveToGroupsView { get; set; }
        public Command MoveToGroupView { get; set; }
        public Command MoveToActivityView { get; set; }
    }
}
