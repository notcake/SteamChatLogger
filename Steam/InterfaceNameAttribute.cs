using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public class InterfaceNameAttribute : Attribute
    {
        public InterfaceNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}
