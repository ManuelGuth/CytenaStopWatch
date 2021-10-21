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

        /// <summary>
        /// A timer is used to frquently connect to the server and update the status as a quick solution.
        /// </summary>
        public void InitRefreshTimer()
        {
            _RefreshTimer = new Timer(200);
            _RefreshTimer.Elapsed += RefreshTimerEvent;
            _RefreshTimer.AutoReset = true;
            _RefreshTimer.Enabled = true;
        }

        /// <summary>
        /// gets the current time and status from the server.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private async void RefreshTimerEvent(Object source, ElapsedEventArgs e)
        {
            var isRunning = await _StopwatchClientHelper.IsRunning();
            var time = await _StopwatchClientHelper.GetTimeInMS();

            Time = FormatTime(time);
            Status = isRunning ? "Running" : "Stopped";
        }

        /// <summary>
        /// Formats the time to hour:minute:second format
        /// </summary>
        /// <param name="timeInMs"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Notifies the UI that the passed property has changede and needs to be updated in the view
        /// </summary>
        /// <param name="propertyName"></param>
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
