using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SignalStream.Models.Constants;
using SignalStream.Models.Interfaces.DI;
using SignalStream.Models.Interfaces.ViewModels;
using SignalStream.Models.Params;
using SignalStream.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

namespace SignalStream.Controls
{
    public sealed partial class WaterfallControl : UserControl
    {
        private readonly IMainViewModel viewModel;
        private readonly IWaterfallProvider waterfallProvider;

        public WaterfallControl()
        {
            this.InitializeComponent();

            waterfallProvider = Ioc.Default.GetService<IWaterfallProvider>();

            viewModel = Ioc.Default.GetService<IMainViewModel>();
            viewModel.DataUpdated += (s, e) => this.Waterfall.Invalidate();
        }

        private void DrawWaterfall(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            var drawingSession = args.DrawingSession;
            float width = (float)sender.Size.Width;
            float height = (float)sender.Size.Width;

            var waterfallBuffer = viewModel?.GetWaterfallBuffer();
            if (waterfallBuffer == null) return;

            var @params = new WaterfallDrawContext(drawingSession, height, width)
            {
                WaterfallBuffer = waterfallBuffer,
            };
            waterfallProvider.Draw(@params);
        }
    }
}
