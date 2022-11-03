using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4.Classes
{
    sealed class Military : Worker, Person
    {
        public override string getWorkPlace()
        {
            return this.workPlace == null ? "Empty" : this.workPlace;
        }

        public Military(string name)
        {
            if (name == null || name == "")
            {
                throw new Exception("name is not valid");
            }
            _setName(name);
            this.workPlace = "Army";
        }

        public override string ToString()
        {
            return $"My name is {this.getName()}, and i work at {this.getWorkPlace()}.";
        }
    }
}