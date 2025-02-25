using Microsoft.UI.Xaml;
using SignalStream.Models.Constants;
using SignalStream.Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalStream.Core.Services
{
    public class SpectrumDataService : ISpectrumDataService
    {
        private readonly DispatcherTimer timer;
        private readonly Random random = new Random();

        public event EventHandler<double[]> DataGenerated;

        public SpectrumDataService()
        {
            timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(50) };
            timer.Tick += (s, e) => GenerateRandomPowers();
        }

        public void Start() => timer.Start();

        public void Stop() => timer.Stop();

        private void GenerateRandomPowers()
        {
            double[] line = new double[Settings.DataWidth];

            for (int i = 0; i < line.Length; i++)
            {
                line[i] = Settings.MinPowerDbm + random.NextDouble() * Settings.PowerRange; // Pseudo-random power generation -120...-20 dBm
            }

            DataGenerated?.Invoke(this, line);
        }
    }
}
