using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace SignalStream.Models.Constants
{
    public static class Settings
    {
        public const int DataWidth = 1024;
        public const int BufferLines = 200;

        public const double FreqMin = 90;
        public const double FreqMax = 110;
        public const double FreqRange = FreqMax - FreqMin;
        public const int FreqStep = 5;
        public const float TickHeight = 5;

        public const double MinPowerDbm = -120;
        public const double MaxPowerDbm = -20;
        public const double PowerRange = 100;
    }
}
