using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfSBIFS.DataTransferObjects
{
    public class GroupUserDto : IJson
    {
        public GroupDto GroupRequest { get; set; } = new GroupDto();
        public EmailDto UserRequest { get; set; } = new EmailDto();
    }
}
