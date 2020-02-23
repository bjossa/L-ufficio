using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_ufficio
{
    abstract public class Observer
    {
        abstract public void update(List<string> stock);
    }
}
