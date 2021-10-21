using GrpcClient;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace GrpcClientUi
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        StopwatchClientHelper _StopwatchClientHelper;
        private Timer _RefreshTimer;
        public MainWindowViewModel()
        {
            Status = "";
            Time = "";

            _StopwatchClientHelper = new StopwatchClientHelper();

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

        string _Status;
        public string Status
        {
            get => _Status;
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _Time;
        public string Time
        {
            get => _Time;
            set
            {
                if (_Time != value)
                {
                    _Time = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DelegateCommand StopCommand { get; set; }
        public DelegateCommand StartCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InitRefreshTimer()
        {
            _RefreshTimer = new Timer(200);
            _RefreshTimer.Elapsed += RefreshTimerEvent;
            _RefreshTimer.AutoReset = true;
            _RefreshTimer.Enabled = true;
        }

        private async void RefreshTimerEvent(Object source, ElapsedEventArgs e)
        {
            var isRunning = await _StopwatchClientHelper.IsRunning();
            var time = await _StopwatchClientHelper.GetTimeInMS();

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
            _StopwatchClientHelper.Start();
        }

        public void StopButtonKlick()
        {
            _StopwatchClientHelper.Stop();
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
