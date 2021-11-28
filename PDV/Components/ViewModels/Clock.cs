using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Timers;

namespace PDV.Components.ViewModels
{
    public class Clock : ObservableObject
    {
        private static readonly TimeSpan TickTimeSpan = TimeSpan.FromMinutes(1);
        private readonly Timer _timer = new(TickTimeSpan.TotalMilliseconds);
        private DateTime _currentTime = DateTime.Now;

        public Clock()
        {
            _timer.Elapsed += (_, _) => OnTick();
            _timer.BeginInit();
            _timer.Start();
        }

        public DateTime CurrentTime
        {
            get => _currentTime;
            private set
            {
                SetProperty(ref _currentTime, value);
            }
        }

        private void OnTick()
        {
            CurrentTime += TickTimeSpan;
        }
    }
}