using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr4
{
    class Edition: INotifyPropertyChanged
    {
        protected string name;
        protected DateTime outDate;
        protected int count;

        public event PropertyChangedEventHandler PropertyChanged;

        public Edition(string name, DateTime date, int count)
        {
            this.name = name;
            outDate = date;
            this.count = count;
        }
        public Edition()
        {
            name = "";
            outDate = DateTime.Now;
            count = 0;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }

        }

        public DateTime OutDate
        {
            get
            {
                return outDate;
            }
            set
            {
                outDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OutDate"));
            }

        }

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                if (value > 0)
                {
                    count = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                }
                else
                {
                    ArgumentOutOfRangeException MyExeption = new ArgumentOutOfRangeException("_count", "Значение должно быть положительным");
                    throw MyExeption;
                }
            }

        }

        public virtual object DeepCopy()
        {
            Edition Result = new Edition();
            Result.name = (string)this.name.Clone();
            Result.outDate = this.outDate;
            Result.count = this.count;
            return (object)Result;
        }

        public override bool Equals(object obj)
        {
            Edition a = (Edition)obj;
            return (string)a.name.Clone() == (string)this.name.Clone() && a.outDate == this.outDate && a.count == this.count;
        }

        public static bool operator ==(Edition a, Edition b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Edition a, Edition b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override string ToString()
        {
            return name + " " + outDate.ToShortDateString() + " " + count;
        }

        public static Edition Scan()
        {
            Console.WriteLine("Введите название");
            string tname = Console.ReadLine();
            Console.WriteLine("Введите дату выхода");
            DateTime TDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите тираж");
            int tcount = Convert.ToInt32(Console.ReadLine());
            return new Edition((string)tname.Clone(), TDate, tcount);
        }
    }
}
