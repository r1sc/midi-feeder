using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using vJoyInterfaceWrap;

namespace MidiFeeder
{
    static class Program
    {
        public static vJoy Joystick;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Joystick = new vJoy();

            if (!Joystick.vJoyEnabled())
            {
                MessageBox.Show("vJoy is not enabled. You'll need to install vJoy before you can use this program.", "MIDIFeeder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
