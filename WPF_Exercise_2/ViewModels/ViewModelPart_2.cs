using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Exercise_2
{
    public enum OrderStatus_Person
    {
        Received = 1,
        Processing = 2,
        New = 4,
        Shipped = 8,
        None = 16
    }

    public class ViewModelPart_2 : ViewModelBase
    {
        private DAO _currentDAO = new DAO();

        private List<Person> _persons;
        public List<Person> Persons
        {
            get
            {
                for(int i = 0; i < _persons.Count; i++)
                {
                    if(_persons[i] == new Person())
                    {
                        _currentDAO.Delete(_persons[i]);
                        _persons.Remove(_persons[i]);
                    }
                }
                return _persons;
            }
            set
            {
                List<Person> lst = _currentDAO.GetAll<Person>();
                foreach(var s in value)
                {
                    if (!lst.Contains(s)) _currentDAO.Add(s);
                    else _currentDAO.Update(s);
                    if (s == new Person()) _currentDAO.Delete(s);
                }
                _persons = _currentDAO.GetAll<Person>();
                OnPropertyChanged();
            }
        }



        public ViewModelPart_2()
        {
            _persons = _currentDAO.GetAll<Person>();
        }
    }
}
