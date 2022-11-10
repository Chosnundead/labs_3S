using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4.Classes
{
    public class Programmer : Worker, PersonFor4Task
    {
        public override string getWorkPlace()
        {
            return this.workPlace == null ? "Empty" : this.workPlace;
        }

        public Programmer(string name) : base()
        {
            if (name == null || name == "")
            {
                throw new Exception("name is not valid");
            }
            _setName(name);
            this.workPlace = "Street";
        }

        public override string ToString()
        {
            return $"My name is {this.getName()}, and i work at {this.getWorkPlace()}.";
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            int counter = 1;
            foreach (var item in this.getName())
            {
                hashCode += counter * Convert.ToInt32(item);
                counter += 1;
            }
            foreach (var item in this.getWorkPlace())
            {
                hashCode += counter * Convert.ToInt32(item);
                counter += 1;
            }
            return hashCode;
        }

        public override bool Equals(object? obj)
        {
            try
            {
                return (obj.GetHashCode() == this.GetHashCode() && obj.GetType() == this.GetType()) ? true : false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}