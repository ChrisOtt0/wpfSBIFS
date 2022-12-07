using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfSBIFS.DataTransferObjects;
using wpfSBIFS.Models;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public interface IGroupsViewModel
    {
        public string Email { get; set; }
        public string FeedbackLabel { get; set; }
        public ObservableCollection<Group> Groups { get; set; }

        public Command Search { get; set; }
        public Command NewGroup { get; set; }
        public Command EditGroup { get; set; }

        public void OnInit();
    }
}
