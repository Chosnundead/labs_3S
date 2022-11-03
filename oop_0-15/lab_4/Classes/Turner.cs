using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4.Classes
{
    public class Turner : Worker, PersonFor4Task
    {
        public override string getWorkPlace()
        {
            return this.workPlace == null ? "Empty" : this.workPlace;
        }

        public Turner(string name)
        {
            if (name == null || name == "")
            {
                throw new Exception("name is not valid");
            }
            _setName(name);
            this.workPlace = "Basement";
        }

        public override string ToString()
        {
            return $"My name is {this.getName()}, and i work at {this.getWorkPlace()}.";
        }
    }
}