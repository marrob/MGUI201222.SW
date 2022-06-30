using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konvolucio.MGUI201222.Controls
{
    public class KnvIoEventArg:EventArgs
    {
        public int Index { get; private set; }
        public bool state { get; private set; }

        public KnvIoEventArg(int index, bool state)
        {
            Index = index;
            this.state = state;
        }
    }
}
