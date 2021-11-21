using System;
using System.Timers;
using PDV.Mvvm;

namespace PDV.Components.ViewModels
{
    public class Clock : NotifyPropertyChanged
    {
        private static readonly TimeSpan TickTimeSpan = TimeSpan.FromMinutes(1);
        private readonly Timer _timer = new(TickTimeSpan.TotalMilliseconds);
        private DateTime _currentTime = DateTime.Now;

        public Clock()
        {
            _timer.Elapsed += (_, _) => OnTick();
            _timer.Start();
        }

        public DateTime CurrentTime
        {
            get => _currentTime;
            private set
            {
                _currentTime = value;
                NotifyChanged();
            }
        }

        private void OnTick()
        {
            CurrentTime += TickTimeSpan;
        }
    }
}