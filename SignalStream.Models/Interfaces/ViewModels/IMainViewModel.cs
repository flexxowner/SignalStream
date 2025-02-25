using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SignalStream.Models.Interfaces.ViewModels
{
    public interface IMainViewModel
    {
        event EventHandler DataUpdated;
        double[] GetCurrentLine();
        List<double[]> GetWaterfallBuffer();
    }
}
