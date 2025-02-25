using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalStream.Models.Params
{
    public class DrawContext
    {
        public DrawContext(CanvasDrawingSession drawingSession, float height, float width)
        {
            DrawingSession = drawingSession;
            Height = height;
            Width = width;
        }

        public CanvasDrawingSession DrawingSession { get; set; }

        public float Height { get; set; }

        public float Width { get; set; }
    }
}
