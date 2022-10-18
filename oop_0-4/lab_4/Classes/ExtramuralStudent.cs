using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4.Classes
{
    public class ExtramuralStudent : Learner, Person
    {
        public override string getStudyPlace()
        {
            return this.studyPlace == null ? "Empty" : this.studyPlace;
        }

        public ExtramuralStudent(string name)
        {
            if (name == null || name == "")
            {
                throw new Exception("name is not valid");
            }
            _setName(name);
            this.studyPlace = "BrSTU";
        }

        public override string ToString()
        {
            return $"My name is {this.getName()}, and i study at {this.getStudyPlace()}.";
        }
    }
}