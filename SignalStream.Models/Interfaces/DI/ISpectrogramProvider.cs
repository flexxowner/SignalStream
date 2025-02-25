using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using SignalStream.Models.Params;

namespace SignalStream.Models.Interfaces.DI
{
    public interface ISpectrogramProvider
    {
        void Draw(SpectrogramDrawContext @params);
    }
}
