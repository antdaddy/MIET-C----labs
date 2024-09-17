using System;
using System.Collections;
using System.Collections.Generic;

namespace lr2
{
    interface IRateAndCopy
    {
        double Score
        {
            get;
        }
        object DeepCopy();
    }
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
            FirstName = "Dmitriy";
            LastName = "Dzhugeli";
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

    enum Frequency
    {
        Weekly = 1, Monthly = 2, Yearly = 3
    };

    class Article : IRateAndCopy
    {
        public Person Author
        {
            get;
            set;
        }
        public String Name
        {
            get;
            set;
        }
        public double Score
        {
            get;
            set;
        }

        public Article(Person a, string b, double c)
        {
            Author = a;
            Name = b;
            Score = c;
        }

        public Article()
        {
            Author = new Person();
            Name = "";
            Score = 0;
        }

        public override string ToString()
        {
            return Author.ToShortString() + "\n" + "Название статьи: " + Name + "\n" + "Рейтинг " + Score.ToString() + "\n";
        }

        public object DeepCopy()
        {
            Article Result = new Article();
            Result.Author = (Person)Author.DeepCopy();
            Result.Name = (string)Name.Clone();
            Result.Score = Score;
            return (object)Result;
        }

        public static Article Scan()
        {

            Person tauthor = (Person)Person.Scan().DeepCopy();
            Console.WriteLine("Введите название");
            string tname = Console.ReadLine();
            Console.WriteLine("Введите рейтинг статьи");
            double tRating = Convert.ToDouble(Console.ReadLine());
            return new Article(tauthor, tname, tRating);
        }
    }

    class Edition
    {
        protected string Name;
        protected DateTime OutDate;
        protected int Count;

        public Edition(string name, DateTime date, int count)
        {
            Name = name;
            OutDate = date;
            Count = count;
        }
        public Edition()
        {
            Name = "";
            OutDate = DateTime.Now;
            Count = 0;
        }

        public string NameS
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }

        }

        public DateTime OutDateS
        {
            get
            {
                return OutDate;
            }
            set
            {
                OutDate = value;
            }

        }

        public int count
        {
            get
            {
                return Count;
            }
            set
            {
                if (value > 0)
                    Count = value;
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
            Result.Name = (string)this.Name.Clone();
            Result.OutDate = this.OutDate;
            Result.Count = this.Count;
            return (object)Result;
        }

        public override bool Equals(object obj)
        {
            Edition a = (Edition)obj;
            return (string)a.Name.Clone() == (string)this.Name.Clone() && a.OutDate == this.OutDate && a.Count == this.Count;
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
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name + " " + OutDate.ToShortDateString() + " " + Count;
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

    class MagazineEnumerator : IEnumerator
    {

        Magazine magazine;
        object CurrenaArticle;
        int ind = 0;
        public MagazineEnumerator(Magazine a)
        {
            magazine = a;
        }
        public object Current
        {
            get
            {
                return CurrenaArticle;
            }
        }

        public bool MoveNext()
        {
            ind++;
            for (int i = ind; i < magazine.ArticlesS.Count; i++)
            {
                Article article = (Article)magazine.ArticlesS[i];
                Person author = article.Author;
                if (magazine.EditorsS.LastIndexOf(author) == -1)
                {
                    CurrenaArticle = magazine.ArticlesS[i];
                    break;
                }
            }
            return (ind < magazine.ArticlesS.Count);

        }

        public void Reset()
        {
            ind = 0;
        }
    }

    class Magazine : Edition, IRateAndCopy, IEnumerable
    {
        private Frequency Period;
        private ArrayList Editors;
        private ArrayList Articles;
        public double Score
        {
            get;
            set;
        }

        public Magazine(string a, Frequency b, DateTime c, int d)
        {
            Name = (string)a.Clone();
            Period = b;
            OutDate = c;
            Count = d;
        }

        public Magazine()
        {
            Name = "";
            Period = Frequency.Monthly;
            OutDate = DateTime.Today;
            Count = 0;
            Editors = new System.Collections.ArrayList();
            Articles = new System.Collections.ArrayList();
        }

        public new String NameS
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }

        public Frequency PeriodS
        {
            get
            {
                return Period;
            }
            set
            {
                Period = value;
            }
        }

        public DateTime OutdateS
        {
            get
            {
                return OutDate;
            }
            set
            {
                OutDate = value;
            }
        }

        public int CountS
        {
            get
            {
                return Count;
            }
            set
            {
                Count = value;
            }
        }

        public System.Collections.ArrayList ArticlesS
        {
            get { return (System.Collections.ArrayList)Articles.Clone(); }
            set { Articles = (System.Collections.ArrayList)value.Clone(); }
        }

        public System.Collections.ArrayList EditorsS
        {
            get { return (System.Collections.ArrayList)Editors.Clone(); }
            set { Editors = (System.Collections.ArrayList)value.Clone(); }
        }

        public bool this[Frequency a]
        {
            get
            {
                return Period.Equals(a);
            }
        }

        public void AddArticles(params Article[] a)
        {
            foreach (Article x in a)
                Articles.Add(x.DeepCopy());
        }

        public void AddEditors(params Person[] a)
        {
            foreach (Person x in a)
                Editors.Add(x.DeepCopy());
        }

        public override string ToString()
        {
            string result;
            result = "Название: " + Name + "\n" + "Периодичность: " + Period.ToString() + "\n" + "Дата издания: " + OutDate.ToShortDateString() + "\n" + "Тираж: " + Count + "\n";
            result += "Информация о статьях: \n";
            foreach (Article a in Articles)
                result = result + a.ToString() + "\n";
            foreach (Person a in Editors)
                result = result + a.ToString() + "\n";
            return result;
        }

        public double AverageScore
        {
            get
            {
                double sum = 0;
                foreach (Article a in Articles)
                    sum += a.Score;
                return sum / Articles.Count;
            }
        }

        public virtual string ToShortString()
        {
            return Name + " " + Period.ToString() + " " + OutDate.ToShortDateString() + " " + Count + "\n" + "Средний рейтинг: " + this.AverageScore;
        }

        public override object DeepCopy()
        {
            Magazine Result = new Magazine();
            Result.Name = (string)Name.Clone();
            Result.Period = Period;
            Result.OutDate = OutDate;
            Result.Count = Count;
            foreach (Article a in Articles)
                Result.Articles.Add(a.DeepCopy());
            foreach (Person a in Editors)
                Result.Editors.Add(a.DeepCopy());
            return Result;
        }

        public Edition Editions
        {
            get
            {
                return new Edition((string)Name.Clone(), OutDate, Count);
            }
            set
            {
                Name = (string)value.NameS.Clone();
                OutDate = value.OutDateS;
                Count = value.count;
            }
        }

        public IEnumerable<Article> ArticlesByRate(double x)
        {
            foreach (Article a in Articles)
            {
                if (a.Score > x)
                    yield return (Article)a.DeepCopy();
            }
        }

        public IEnumerable<Article> ArticlesByString(string x)
        {
            foreach (Article a in Articles)
            {
                if (a.Name.Contains(x))
                    yield return (Article)a.DeepCopy();
            }
        }

        public IEnumerable<Article> ArticlesByEditors()
        {
            foreach (Article a in Articles)
            {
                if (Editors.LastIndexOf(a.Author) != -1)
                    yield return (Article)a.DeepCopy();
            }
        }

        public IEnumerable<Person> OnlyEditors()
        {

            foreach (Person a in Editors)
            {
                bool ans = true;
                foreach (Article art in Articles)
                {
                    if (art.Author == a)
                        ans = false;
                }
                if (ans)
                    yield return (Person)a.DeepCopy();
            }

        }

        public static new Magazine Scan()
        {
            Magazine Result = new Magazine();
            Result.Editions = Edition.Scan();
            Console.WriteLine("Введите переодичность");
            Result.Period = (Frequency)Frequency.Parse(typeof(Frequency), Console.ReadLine(), true);
            Console.WriteLine("Введите количество статей");
            long tcount = Convert.ToInt64(Console.ReadLine());
            for (int i = 0; i < tcount; i++)
                Result.AddArticles(Article.Scan());
            Console.WriteLine("Введите количество редакторов");
            tcount = Convert.ToInt64(Console.ReadLine());
            for (int i = 0; i < tcount; i++)
                Result.AddEditors(Person.Scan());
            return Result;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Edition a = Edition.Scan();
            Edition b = Edition.Scan();
            Console.WriteLine("Проверка на равенство по значению");
            Console.WriteLine(a.Equals(b).ToString());
            Console.WriteLine("Проверка на равенство ссылок");
            Console.WriteLine(object.ReferenceEquals(a, b).ToString());
            Console.WriteLine("Хэш коды объектов");
            Console.WriteLine(a.GetHashCode().ToString() + " " + b.GetHashCode().ToString());

            try
            {
                a.count = -2;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

            Magazine m = Magazine.Scan();
            Console.WriteLine(m.ToString());

            Console.WriteLine("Данные свойства Edition");
            Console.WriteLine(m.Editions.ToString());

            Magazine mCopy = (Magazine)m.DeepCopy();
            Console.WriteLine("Введите название");
            m.NameS = Console.ReadLine();
            Console.WriteLine("Копия");
            Console.WriteLine(mCopy.NameS);
            Console.WriteLine("Исходныый объект");
            Console.WriteLine(m.NameS);
            m.NameS = mCopy.NameS;

            Console.WriteLine("Введите минимальный рейтинг");
            foreach (Article art in m.ArticlesByRate(Convert.ToDouble(Console.ReadLine())))
                Console.WriteLine(art.ToString());

            Console.WriteLine("Введите фразу для поиска");
            foreach (Article art in m.ArticlesByString(Console.ReadLine()))
                Console.WriteLine(art.ToString());

            Console.WriteLine("Статьи, авторы которых не являются редакторами");
            foreach (Article art in m)
            {
                Console.WriteLine(art.ToString());
            }

            Console.WriteLine("Статьи, авторы которых являются редакторами");
            foreach (Article x in m.ArticlesByEditors())
            {
                Console.WriteLine(x.ToString());
            }

            Console.WriteLine("Редакторы, у которых нет статей");
            foreach (Person x in m.OnlyEditors())
            {
                Console.WriteLine(x.ToString());
            }
            Console.ReadLine();
        }
    }
}