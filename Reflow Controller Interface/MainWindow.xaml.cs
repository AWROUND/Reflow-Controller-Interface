using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UsbHid;
using UsbHid.USB.Classes.Messaging;

namespace Reflow_Controller_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UsbHidDevice Device;

        public MainWindow()
        {
            InitializeComponent();
            InitialializeLabels();
        }

        public void InitialializeLabels()
        {
            TemperatureLabel.Text = "000" + "\u00b0" + "C";
            SetpointLabel.Text = "000" + "\u00b0" + "C";
            StageTimeLabel.Text = "N/A";
            StageLabel.Text = "WAITING";
            ElapsedTimeLabel.Text = "00:00:00";
            OvenStatusLabel.Text = "OFF";
            FanStatusLabel.Text = "OFF";
            AuxStatusLabel.Text = "OFF";
            //toolStripStatusLabel1.Text = "Status: Not connected to port";*/
        }

        private void InitialializeUSB(object sender, EventArgs e)
        {
            Device = new UsbHidDevice(0x04DB, 0x1234);
            Device.OnConnected += DeviceOnConnected;
            Device.OnDisConnected += DeviceOnDisConnected;
            Device.DataReceived += DeviceDataReceived;
            Device.Connect();
        }

        private void DeviceDataReceived(byte[] data)
        {
            AppendText(ByteArrayToString(data));
        }

        private void AppendText(string p)
        {
            ThreadSafe(() => textBox1.AppendText(p + Environment.NewLine));
        }

        private void DeviceOnDisConnected()
        {
            ThreadSafe(() => checkBox1.Enabled = false);

        }

        private void DeviceOnConnected()
        {
            ThreadSafe(() => checkBox1.Enabled = true);
        }

        private void ThreadSafe(MethodInvoker method)
        {
            if (InvokeRequired)
                Invoke(method);
            else
                method();
        }

        private static string ByteArrayToString(ICollection<byte> input)
        {
            var result = string.Empty;

            if (input != null && input.Count > 0)
            {
                var isFirst = true;
                foreach (var b in input)
                {
                    result += isFirst ? string.Empty : ",";
                    result += b.ToString("X2");
                    isFirst = false;
                }
            }
            return result;
        }

        private void Button1Click(object sender, EventArgs e)
        {
            if (!Device.IsDeviceConnected) return;

            var command = new CommandMessage(0x86, new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 });
            Device.SendMessage(command);
        }
        

    }
}
