using Microsoft.Graphics.Canvas;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using SignalStream.Models.Constants;
using SignalStream.Models.Interfaces.DI;
using SignalStream.Models.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace SignalStream.Core.Providers
{
    public class WaterfallProvider : IWaterfallProvider
    {
        private readonly (double Offset, Color Color)[] gradientStops =
        [
            (0.0, Color.FromArgb(255, 0, 0, 255)),
            (0.25, Color.FromArgb(255, 0, 255, 255)),
            (0.5, Color.FromArgb(255, 0, 255, 0)),
            (0.75, Color.FromArgb(255, 255, 255, 0)),
            (1.0, Color.FromArgb(255, 255, 0, 0))
        ];

        public void Draw(WaterfallDrawContext @params)
        {
            var drawingSession = @params.DrawingSession;
            var waterfallBuffer = @params.WaterfallBuffer;

            float rowHeight = @params.Height / waterfallBuffer.Count;
            float colWidth = @params.Width / Settings.DataWidth;

            DrawVertically(drawingSession, waterfallBuffer, rowHeight, colWidth);
            DrawRangeOfFrequencies(drawingSession, rowHeight, colWidth);
        }

        private void DrawVertically(CanvasDrawingSession drawingSession, List<double[]> waterfallBuffer, float rowHeight, float colWidth)
        {
            for (int row = 0; row < waterfallBuffer.Count; row++)
            {
                var line = waterfallBuffer[row];
                float yPos = row * rowHeight;

                for (int x = 0; x < line.Length; x++)
                {
                    double value = line[x];
                    double normalized = (value - Settings.MinPowerDbm) / Settings.PowerRange;
                    Color color = InterpolateColor(normalized);

                    float xPos = x * colWidth;
                    drawingSession.FillRectangle(xPos, yPos, colWidth, rowHeight, color);
                }
            }
        }

        private void DrawRangeOfFrequencies(CanvasDrawingSession drawingSession, float controlWidth, float controlHeight)
        {
            drawingSession.DrawLine(0, controlHeight, controlWidth, controlHeight, Colors.Gray, 1);

            for (double freq = Settings.FreqMin; freq <= Settings.FreqMax; freq += Settings.FreqStep)
            {
                double relative = (freq - Settings.FreqMin) / Settings.FreqRange;
                float xLabelPos = (float)(relative * controlWidth);

                drawingSession.DrawLine(xLabelPos, controlHeight, xLabelPos, controlHeight - Settings.TickHeight, Colors.Gray, 1);
            }
        }

        private Color InterpolateColor(double normalized)
        {
            normalized = Math.Max(0, Math.Min(1, normalized));
            for (int i = 0; i < gradientStops.Length - 1; i++)
            {
                var (offset1, color1) = gradientStops[i];
                var (offset2, color2) = gradientStops[i + 1];
                if (normalized >= offset1 && normalized <= offset2)
                {
                    double t = (normalized - offset1) / (offset2 - offset1);
                    byte a = (byte)(color1.A + (color2.A - color1.A) * t);
                    byte r = (byte)(color1.R + (color2.R - color1.R) * t);
                    byte g = (byte)(color1.G + (color2.G - color1.G) * t);
                    byte b = (byte)(color1.B + (color2.B - color1.B) * t);
                    return Color.FromArgb(a, r, g, b);
                }
            }

            return normalized < gradientStops.FirstOrDefault().Offset ? gradientStops.FirstOrDefault().Color : gradientStops[^1].Color;
        }
    }
}
