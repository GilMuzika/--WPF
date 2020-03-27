using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPF_Exercise_2
{
    public partial class MainWindow
    {
        private void Initialize()
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

    }
}
