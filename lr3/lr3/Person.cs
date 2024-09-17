using System;

namespace lr3
{
    class Person
    {
        private string FirstName;
        private string LastName;
        private System.DateTime BirthDate;

        public Person(string f, string l, DateTime d)
        {
            FirstName = f;
            LastName = l;
            BirthDate = d;
        }

        public Person()
        {
            FirstName = "Daria";
            LastName = "Nikitina";
            BirthDate = DateTime.Now;
        }

        public string FName
        {
            get
            {
                return FirstName;
            }
            set
            {
                FirstName = value;
            }
        }

        public string LName
        {
            get
            {
                return LastName;
            }
            set
            {
                LastName = value;
            }
        }

        public DateTime BDate
        {
            get
            {
                return BirthDate;
            }
            set
            {
                BirthDate = value;
            }
        }

        public int YearBirth
        {
            get
            {
                return BirthDate.Year;
            }
            set
            {
                BirthDate = new DateTime(value, BirthDate.Month, BirthDate.Day);
            }
        }

        public override string ToString()
        {
            return "Имя: " + FirstName + "\n" + "Фамилия: " + LastName + "\n" + BirthDate.ToString() + "\n";
        }

        public virtual string ToShortString()
        {
            return "Имя: " + FirstName + "\n" + "Фамилия: " + LastName + "\n";
        }

        public override bool Equals(object obj)
        {
            Person pobj = (Person)obj;
            return (string)this.FirstName.Clone() == (string)pobj.FirstName.Clone() && (string)this.LastName.Clone() == (string)pobj.LastName.Clone() && this.BirthDate == pobj.BirthDate;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static bool operator ==(Person a, Person b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Person a, Person b)
        {
            return !a.Equals(b);
        }

        public object DeepCopy()
        {
            Person Result = new Person();
            Result.LastName = (string)LastName.Clone();
            Result.FirstName = (string)FirstName.Clone();
            Result.BirthDate = BirthDate;
            return (object)Result;
        }

        public static Person Scan()
        {
            Console.WriteLine("Введите имя");
            string tname = Console.ReadLine();
            Console.WriteLine("Введите фамилию");
            string tfname = Console.ReadLine();
            Console.WriteLine("Введите дату рождения");
            DateTime tdateofbirth = DateTime.Parse(Console.ReadLine());
            return new Person(tname, tfname, tdateofbirth);
        }

    }
}
