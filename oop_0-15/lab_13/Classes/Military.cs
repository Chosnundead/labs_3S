using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_13.Classes
{
    [Serializable]
    public class Military : Worker, Person
    {
        [NonSerialized]
        public string test = "test";
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
        public Military() { }

        public override string ToString()
        {
            return $"My name is {this.getName()}, and i work at {this.getWorkPlace()}.";
        }
    }
}