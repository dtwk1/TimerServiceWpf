using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerServiceWpf.Common
{
    class TimerViewModelFactory
    {
        public TimerViewModel Build(double rate)
        {
            return new TimerViewModel
                           {
                               Rate=rate
                           };

        }

    }
}
