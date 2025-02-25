using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalStream.Models.Params
{
    public class SpectrogramDrawContext(CanvasDrawingSession drawingSession, float height, float width) : DrawContext(drawingSession, height, width)
    {
        public ICanvasAnimatedControl Sender { get; set; }

        public double[] LineData { get; set; }
    }
}
