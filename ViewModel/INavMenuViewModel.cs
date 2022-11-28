using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace wpfSBIFS.ViewModel
{
    public interface INavMenuViewModel
    {
        public ContentControl NavMenuPanel { get; set; }

        public void SetHome(UserControl view);
    }
}
