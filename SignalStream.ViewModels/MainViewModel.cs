using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using SignalStream.Models.Constants;
using SignalStream.Models.Interfaces.Services;
using SignalStream.Models.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace SignalStream.ViewModels
{
    public partial class MainViewModel : ObservableObject, IMainViewModel
    {
        private readonly ISpectrumDataService spectrumService;
        private readonly List<double[]> waterfallBuffer = new List<double[]>();

        private double[] currentLine = new double[Settings.DataWidth];

        public event EventHandler DataUpdated;

        public MainViewModel(ISpectrumDataService spectrumService)
        {
            this.spectrumService = spectrumService;

            for (int i = 0; i < Settings.BufferLines; i++)
                waterfallBuffer.Add(new double[Settings.DataWidth]);

            spectrumService.DataGenerated += OnDataGenerated;
        }

        private void OnDataGenerated(object sender, double[] line)
        {
            this.currentLine = line;

            waterfallBuffer.Insert(0, line);
            if (waterfallBuffer.Count > Settings.BufferLines)
            {
                waterfallBuffer.RemoveAt(waterfallBuffer.Count - 1);
            }

            DataUpdated?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        private void StartRendering() => spectrumService.Start();

        [RelayCommand]
        private void StopRendering() => spectrumService.Stop();

        public double[] GetCurrentLine() => this.currentLine;

        public List<double[]> GetWaterfallBuffer() => this.waterfallBuffer;
    }
}
