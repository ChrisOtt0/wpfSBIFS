using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfSBIFS.DataTransferObjects;
using wpfSBIFS.Models;
using wpfSBIFS.Tools;
using wpfSBIFS.View;

namespace wpfSBIFS.ViewModel
{
    public interface IGroupViewModel
    {
        public int GroupID { get; }
        public Group Group { get; set; }
        public string GroupName { get; set; }
        public string SearchEmail { get; set; }
        public string FeedbackLabel { get; set; }
        public bool PopupIsOpen { get; set; }
        public Command Save { get; set; }
        public Command Cancel { get; set; }
        public Command Calculate { get; set; }
        public Command AddParticipant { get; set; }
        public Command RemoveParticipant { get; set; }
        public Command AddActivity { get; set; }
        public Command EditActivity { get; set; }
        public Command SelectionChanged { get; set; }
        public GroupView View { get; set; }

        public ObservableCollection<User> Participants { get; set; }
        public ObservableCollection<Activity> Activities { get; set; }
        public ObservableCollection<UserDto> SearchPopup { get; set; }

        public void OnInit();
        public void SearchEmailChanged();
    }
}
