using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reflow_Controller_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

        public void InitialializeUSB()
        {
            
        }

    }
}
