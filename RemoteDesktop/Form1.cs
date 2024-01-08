using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class mainFrm : Form
    {
        static List<Screen> screens;
        public mainFrm()
        {
            screens = getScreenListSorted();
            InitializeComponent();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hideBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            var cursorPos = Cursor.Position;
            Screen currentScreen = Screen.FromPoint(cursorPos);
            int index = screens.FindIndex(screen => screen.Bounds.X==currentScreen.Bounds.X);
            if (index < screens.Count-1)
                SetCursorPos(screens[index+1].Bounds.X + (int)(screens[index+1].Bounds.Width / 2), cursorPos.Y);
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            var cursorPos = Cursor.Position;
            Screen currentScreen = Screen.FromPoint(cursorPos);
            int index = screens.FindIndex(screen => screen.Bounds == currentScreen.Bounds);
            if (index > 0)
                SetCursorPos(screens[index - 1].Bounds.X + (int)(screens[index - 1].Bounds.Width / 2), cursorPos.Y);
        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private List<Screen> getScreenListSorted()
        {
            var list = new List<Screen>();
            foreach(var screen in Screen.AllScreens)
            {
                list.Add(screen);
            }

            var sortedList = list.OrderBy(screen=>screen.Bounds.X);
            return sortedList.ToList();
        }

    }
}
