using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_7.Classes
{
    public interface ArrayList<T>
    {

        public void addFirstItem(T item);
        public void removeLastItem();
        public T[] getItems();
    }
}