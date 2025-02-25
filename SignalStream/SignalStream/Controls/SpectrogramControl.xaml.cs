using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using DependencyPropertyGenerator;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.UI;
using SignalStream.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using SignalStream.Models.Interfaces.ViewModels;
using SignalStream.Models.Interfaces.DI;
using SignalStream.Core.Providers;
using SignalStream.Models.Params;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SignalStream.Controls
{
    public sealed partial class SpectrogramControl : UserControl
    {
        private readonly IMainViewModel viewModel;
        private readonly ISpectrogramProvider spectrogramProvider;

        public SpectrogramControl()
        {
            this.InitializeComponent();

            spectrogramProvider = Ioc.Default.GetService<ISpectrogramProvider>();

            viewModel = Ioc.Default.GetService<IMainViewModel>();
            viewModel.DataUpdated += (s, e) => Spectrogram.Invalidate();
        }

        private void DrawSpectrogram(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var drawingSession = args.DrawingSession;
            float width = (float)sender.Size.Width;
            float height = (float)sender.Size.Height;

            var lineData = viewModel?.GetCurrentLine();
            if (lineData == null || lineData.Length < 2) return;

            var @params = new SpectrogramDrawContext(drawingSession, height, width)
            {
                LineData = lineData,
                Sender = sender
            };
            spectrogramProvider?.Draw(@params);
        }
    }
}
