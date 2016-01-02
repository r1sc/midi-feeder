using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Midi;
using vJoyInterfaceWrap;
using Control = System.Windows.Forms.Control;

namespace MidiFeeder
{
    public partial class Form1 : Form
    {
        private bool _running = true;

        private List<JoystickControl> _joystickControls = new List<JoystickControl>();
        private int _numButtons;
        private vJoy.JoystickState _state;
        private Dictionary<HID_USAGES, long> _maxValues = new Dictionary<HID_USAGES, long>();

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            InitializeJoystick();
            
            foreach (var installedDevice in InputDevice.InstalledDevices)
            {
                installedDevice.Open();
                installedDevice.StartReceiving(null);
                installedDevice.ControlChange += OnControlChange;
            }

            var t = new Thread(() =>
            {
                while (_running)
                {
                    Program.Joystick.UpdateVJD(1, ref _state);
                    Thread.Sleep(10);
                }
            });
            t.Start();
        }

        private void InitializeJoystick()
        {
            switch (Program.Joystick.GetVJDStatus(1))
            {
                case VjdStat.VJD_STAT_OWN:
                    MessageBox.Show("Virtual joystick already aquired. This shouldn't happen.");
                    break;
                case VjdStat.VJD_STAT_FREE:
                    if (!Program.Joystick.AcquireVJD(1))
                        MessageBox.Show("Couldn't aquire joystick");
                    Program.Joystick.ResetVJD(1);
                    var values = Enum.GetValues(typeof(HID_USAGES));
                    foreach (HID_USAGES value in values)
                    {
                        long max = 0;
                        Program.Joystick.GetVJDAxisMax(1, value, ref max);
                        _maxValues.Add(value, max);

                        _joystickControls.Add(new JoystickControl
                        {
                            HidUsage = value
                        });
                    }

                    _numButtons = Program.Joystick.GetVJDButtonNumber(1);
                    for (int i = 0; i < _numButtons; i++)
                    {
                        _joystickControls.Add(new JoystickControl
                        {
                            IsButton = true,
                            ButtonNumber = i
                        });
                    }

                    break;
                case VjdStat.VJD_STAT_BUSY:
                    MessageBox.Show("The virtual joystick is already receiving data from another application. Close the offending application or choose another virtual joystick.");
                    break;
                case VjdStat.VJD_STAT_MISS:
                    MessageBox.Show("The vJoy driver doesn't seem to be installed. Make sure you download and install vJoy before running vJoy MIDI Mapper.");
                    break;
                case VjdStat.VJD_STAT_UNKN:
                    MessageBox.Show("vJoy unknown generic joystick error. An unhandled error occured. Don't worry, make sure vJoy is running and the driver is properly installed.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _running = false;
            foreach (var installedDevice in InputDevice.InstalledDevices)
            {
                if (installedDevice.IsOpen)
                    installedDevice.Close();
            }
            Program.Joystick.RelinquishVJD(1);
        }

        private void OnControlChange(ControlChangeMessage msg)
        {
            Invoke((Action<ControlChangeMessage>)delegate(ControlChangeMessage message)
            {
                Change(InputDevice.InstalledDevices.IndexOf((InputDevice)message.Device),
                    (int)message.Control, message.Value);
            }, msg);
        }

        private int _currentDeviceId, _currentControlId;
        private void Change(int deviceId, int controlOrPitch, int value)
        {
            // Redraw UI etc.

            JoystickControl joystickControl = _joystickControls.SingleOrDefault(x => x.Mapping != null && (x.Mapping.DeviceId == deviceId && x.Mapping.ControlNumber == controlOrPitch));
            if (_currentDeviceId != deviceId || _currentControlId != controlOrPitch)
            {
                _currentDeviceId = deviceId;
                _currentControlId = controlOrPitch;
                lblMidiDevice.Text = "MIDI Device: " + InputDevice.InstalledDevices[deviceId].Name;
                lblMidiControlNumber.Text = "Control #: " + controlOrPitch;

                if (joystickControl != null)
                {
                    radioTypeButton.Checked = joystickControl.IsButton;
                    radioTypeAxis.Checked = !joystickControl.IsButton;
                    button1.Enabled = true;
                }
                else
                {
                    radioTypeAxis.Checked = radioTypeButton.Checked = false;
                    button1.Enabled = false;
                }
            }
            progessMidiData.Value = (value+1);
            progessMidiData.Value = value;
            
            if (joystickControl != null)
            {
                UpdateJoystick(joystickControl, value);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
        }

        private void UpdateJoystick(JoystickControl joystickControl, int value)
        {

            if (joystickControl.IsButton == false)
            {
                var percentage = value / 127.0f;
                long min = 0, max = 0;
                var axisVal = (int)(percentage * _maxValues[joystickControl.HidUsage]);
                switch (joystickControl.HidUsage)
                {
                    case HID_USAGES.HID_USAGE_X:
                        _state.AxisX = axisVal;
                        break;
                    case HID_USAGES.HID_USAGE_Y:
                        _state.AxisY = axisVal;
                        break;
                    case HID_USAGES.HID_USAGE_Z:
                        _state.AxisZ = axisVal;
                        break;
                    case HID_USAGES.HID_USAGE_RX:
                        _state.AxisXRot = axisVal;
                        break;
                    case HID_USAGES.HID_USAGE_RY:
                        _state.AxisYRot = axisVal;
                        break;
                    case HID_USAGES.HID_USAGE_RZ:
                        _state.AxisZRot = axisVal;
                        break;
                    case HID_USAGES.HID_USAGE_SL0:
                        _state.Slider = axisVal;
                        break;
                    case HID_USAGES.HID_USAGE_SL1:
                        _state.Dial = axisVal;
                        break;
                    case HID_USAGES.HID_USAGE_WHL:
                        _state.Wheel = axisVal;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                var pressed = value > 0;
                if (pressed)
                    _state.Buttons |= (uint)(1 << joystickControl.ButtonNumber);
                else
                    _state.Buttons &= ~(uint)(1 << joystickControl.ButtonNumber);
            }
        }

        private void radioTypeButton_CheckedChanged(object sender, EventArgs e)
        {
            var currentMapping = _joystickControls.SingleOrDefault(x => x.Mapping != null && (x.Mapping.DeviceId == _currentDeviceId && x.Mapping.ControlNumber == _currentControlId));
            if (!radioTypeButton.Checked)
                return;
            var unmapped = _joystickControls.FirstOrDefault(x => x.IsButton && x.Mapping == null);
            
            if (currentMapping == null || currentMapping.IsButton == false)
            {
                if (currentMapping != null)
                    currentMapping.Mapping = null;
                if (unmapped == null)
                {
                    MessageBox.Show("There are no unmapped buttons left. Unmap another button in order to map this control.");
                }
                else
                {
                    unmapped.Mapping = new Mapping
                    {
                        ControlNumber = _currentControlId,
                        DeviceId = _currentDeviceId
                    };
                }
            }
            
        }

        private void radioTypeAxis_CheckedChanged(object sender, EventArgs e)
        {
            var currentMapping = _joystickControls.SingleOrDefault(x => x.Mapping != null && (x.Mapping.DeviceId == _currentDeviceId && x.Mapping.ControlNumber == _currentControlId));
            if (!radioTypeAxis.Checked)
                return;
            var unmapped = _joystickControls.FirstOrDefault(x => x.IsButton == false && x.Mapping == null);
            
            if (currentMapping == null ||  currentMapping.IsButton)
            {
                if(currentMapping != null)
                    currentMapping.Mapping = null;
                if (unmapped == null)
                {
                    MessageBox.Show("There are no unmapped axes left. Unmap another axis in order to map this control.");
                }
                else
                {
                    unmapped.Mapping = new Mapping
                    {
                        ControlNumber = _currentControlId,
                        DeviceId = _currentDeviceId
                    };
                }
            }
        }
    }

    public class JoystickControl
    {
        public bool IsButton { get; set; }
        public int ButtonNumber { get; set; }
        public HID_USAGES HidUsage { get; set; }
        public Mapping Mapping { get; set; }

        public override string ToString()
        {
            if (IsButton)
                return "Button " + (ButtonNumber + 1);

            switch (HidUsage)
            {
                case HID_USAGES.HID_USAGE_X:
                    return "X Axis";
                case HID_USAGES.HID_USAGE_Y:
                    return "Y Axis";
                case HID_USAGES.HID_USAGE_Z:
                    return "Z Axis";
                case HID_USAGES.HID_USAGE_RX:
                    return "X Axis Rotation";
                case HID_USAGES.HID_USAGE_RY:
                    return "Y Axis Rotation";
                case HID_USAGES.HID_USAGE_RZ:
                    return "Z Axis Rotation";
                case HID_USAGES.HID_USAGE_SL0:
                    return "Slider 1";
                case HID_USAGES.HID_USAGE_SL1:
                    return "Slider 2";
                case HID_USAGES.HID_USAGE_WHL:
                    return "Wheel";
                case HID_USAGES.HID_USAGE_POV:
                    return "POV Hat";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class Mapping
    {
        public int DeviceId { get; set; }
        public int ControlNumber { get; set; }
    }
}
