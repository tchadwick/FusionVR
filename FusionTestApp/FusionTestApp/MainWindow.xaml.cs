using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Windows;
using System.Windows.Forms;

namespace FusionTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string folderPath = string.Empty;
        private const int DefaultBaudRate = 9600;
        private SerialPort arduinoPort = new SerialPort();
        private string selectedComPortName = string.Empty;
        private bool isFirstRead = true;
        private StreamWriter loggingStream;
        private bool IsLoggingEnabled = true;
        private ObservableCollection<DataPoint> Collection = new ObservableCollection<DataPoint>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void InitializeArduinoPortSettings(int baudRate)
        {
            arduinoPort.BaudRate = baudRate;
            arduinoPort.DtrEnable = true;
            arduinoPort.Parity = Parity.None;

            // Set the read/write timeouts
            arduinoPort.ReadTimeout = 500;
            arduinoPort.WriteTimeout = 500;

            arduinoPort.DataReceived += ArduinoPort_DataReceived;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeArduinoPortSettings(DefaultBaudRate);
            comPortDropdown.ItemsSource = SerialPort.GetPortNames();
            comPortDropdown.SelectionChanged += comPortDropdown_Selected;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClosingCleanup();
        }

        private void ClosingCleanup()
        {
            CloseArduinoConnection();
            if (loggingStream != null)
            {
                loggingStream.Close();
            }
        }

        private void comPortDropdown_DropDownOpened(object sender, EventArgs e)
        {
            comPortDropdown.ItemsSource = null;
            comPortDropdown.ItemsSource = SerialPort.GetPortNames();
        }

        private void comPortDropdown_Selected(object sender, RoutedEventArgs e)
        {
            selectedComPortName = (sender as System.Windows.Controls.ComboBox).SelectedItem as string;
        }

        private void BeginCommunication()
        {
            OpenLoggingFileStream();
            OpenArduinoConnection();
            isFirstRead = true;
        }

        private void OpenLoggingFileStream()
        {
            if (IsLoggingEnabled &&
               !string.IsNullOrEmpty(folderPath))
            {
                if (loggingStream != null)
                {
                    loggingStream.Close();
                }
                string filePath = Path.Combine(folderPath, string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt} Output.csv", DateTime.Now));
                loggingStream = new StreamWriter(filePath, false);
            }
        }

        private void OpenArduinoConnection()
        {
            CloseArduinoConnection();
            try
            {
                comErrorLbl.Visibility = Visibility.Collapsed;
                arduinoPort.PortName = selectedComPortName;
                arduinoPort.BaudRate = int.Parse(baudRateTxtBx.Text);
                arduinoPort.Open();
            }
            catch (Exception)
            {
                comErrorLbl.Visibility = Visibility.Visible;
            }
        }

        private void CloseArduinoConnection()
        {
            if (arduinoPort.IsOpen)
            {
                arduinoPort.Close();
            }
        }

        private void ArduinoPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string incomingData = arduinoPort.ReadLine();
            if (isFirstRead)
            {
                isFirstRead = false;
                return; // Throw away the first line, may be incomplete
            }

            if (IsLoggingEnabled)
            {
                loggingStream.WriteLine(incomingData);
            }

            string[] dataPoints = incomingData.Replace(" ", string.Empty).Split(',');

        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            BeginCommunication();
        }

        private void CSVLocationButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = fbd.SelectedPath;
            }
        }

        private void enableLoggingChk_Click(object sender, RoutedEventArgs e)
        {
            IsLoggingEnabled = enableLoggingChk.IsChecked ?? false;
        }
    }
}
