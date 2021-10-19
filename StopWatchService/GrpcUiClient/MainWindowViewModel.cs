using GrpcClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace GrpcClientUi
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        StopwatchClientHelper stopwatchClientHelper;
        private Timer refreshTimer;
        public MainWindowViewModel()
        {
            Status = "";
            Time = "";

            stopwatchClientHelper = new StopwatchClientHelper();

            StopCommand = new DelegateCommand(
                o => true,
                o => { StopButtonKlick(); }
                );

            StartCommand = new DelegateCommand(
                o => true,
                o => { StartButtonKlick(); }
                );

            InitRefreshTimer();
        }

        string status;
        public string Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    RaisePropertyChanged();
                }
            }
        }

        string time;
        public string Time
        {
            get => time;
            set
            {
                if (time != value)
                {
                    time = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DelegateCommand StopCommand { get; set; }
        public DelegateCommand StartCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InitRefreshTimer()
        {
            // Create a timer with a 200 milliseconds interval.
            refreshTimer = new Timer(200);
            // Hook up the Elapsed event for the timer. 
            refreshTimer.Elapsed += RefreshTimerEvent;
            refreshTimer.AutoReset = true;
            refreshTimer.Enabled = true;
        }

        private async void RefreshTimerEvent(Object source, ElapsedEventArgs e)
        {
            var isRunning = await stopwatchClientHelper.IsRunning();
            var time = await stopwatchClientHelper.GetTimeInMS();

            Time = FormatTime(time);
            Status = isRunning ? "Running" : "Stopped";
        }

        private string FormatTime(long timeInMs)
        {
            TimeSpan ts = TimeSpan.FromMilliseconds(timeInMs);
            return ts.ToString(@"hh\:mm\:ss");
        }

        public void StartButtonKlick()
        {
            stopwatchClientHelper.Start();
        }

        public void StopButtonKlick()
        {
            stopwatchClientHelper.Stop();
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
