using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalStream.Models.Params
{
    public class WaterfallDrawContext(CanvasDrawingSession drawingSession, float height, float width) : DrawContext(drawingSession, height, width)
    {
        public List<double[]> WaterfallBuffer { get; set; }
    }
}
