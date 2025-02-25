using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using SignalStream.Models.Constants;
using SignalStream.Models.Interfaces.DI;
using SignalStream.Models.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalStream.Core.Providers
{
    public class SpectrogramProvider : ISpectrogramProvider
    {
        public void Draw(SpectrogramDrawContext @params)
        {
            var lineData = @params.LineData;
            var height = @params.Height;
            var width = @params.Width;
            var drawingSession = @params.DrawingSession;

            drawingSession.DrawLine(0, height, width, height, Colors.Gray, 1);
            drawingSession.DrawLine(0, 0, 0, height, Colors.Gray, 1);

            if (!lineData?.Any() ?? true)
                return;

            using (var pathBuilder = new CanvasPathBuilder(@params.Sender))
            {
                float x0 = 0;
                float y0 = (float)(@params.Height - ((lineData.FirstOrDefault() - Settings.MinPowerDbm) / Settings.PowerRange * @params.Height));
                pathBuilder.BeginFigure(x0, y0);

                for (int i = 1; i < lineData.Length; i++)
                {
                    float x = i * @params.Width / (lineData.Length - 1);
                    float y = (float)(height - ((lineData[i] - Settings.MinPowerDbm) / Settings.PowerRange * height));

                    pathBuilder.AddLine(x, y);
                }

                pathBuilder.EndFigure(CanvasFigureLoop.Open);
                using (var geometry = CanvasGeometry.CreatePath(pathBuilder))
                {
                    drawingSession.DrawGeometry(geometry, Colors.Lime, 2);
                }
            }

            DrawRangeOfFrequencies(@params);
        }

        private void DrawRangeOfFrequencies(SpectrogramDrawContext @params)
        {
            var height = @params.Height;

            for (int freq = (int)Settings.FreqMin; freq <= Settings.FreqMax; freq += Settings.FreqStep)
            {
                double relative = (freq - Settings.FreqMin) / Settings.FreqRange;

                float xPos = (float)(relative * @params.Width);

                @params.DrawingSession.DrawLine(xPos, height, xPos, height - Settings.TickHeight, Colors.Gray, 1);
            }
        }
    }
}
