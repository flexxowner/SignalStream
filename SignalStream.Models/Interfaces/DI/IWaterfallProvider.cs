using Microsoft.Graphics.Canvas;
using SignalStream.Models.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalStream.Models.Interfaces.DI
{
    public interface IWaterfallProvider
    {
        void Draw(WaterfallDrawContext @params);
    }
}
