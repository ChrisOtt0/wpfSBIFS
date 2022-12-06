using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfSBIFS.Models;
using wpfSBIFS.Tools;

namespace wpfSBIFS.ViewModel
{
    public interface IActivityViewModel
    {
        public string FeedbackLabel { get; set; }
        public string ActivityDescription { get; set; }
        public double Amount { get; set; }
        public ObservableCollection<ResponsibleParticipant> ResponsibleParticipants { get; set; }
        public Command Save { get; set; }
        public Command Cancel { get; set; }
        public Command Delete { get; set; }

        public void OnInit();
    }
}
