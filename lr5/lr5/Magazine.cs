using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace lr5
{
    [Serializable]
    class Magazine : Edition, IRateAndCopy, IEnumerable
    {
        private Frequency period;
        private List<Person> editors;
        private List<Article> articles;
        public event PropertyChangedEventHandler PropertyChanged;

        public double Score
        {
            get;
            set;
        }

        public Magazine(string a, Frequency b, DateTime c, int d)
        {
            name = (string)a.Clone();
            period = b;
            outDate = c;
            count = d;
            editors = new List<Person>();
            articles = new List<Article>();
        }

        public Magazine()
        {
            name = "";
            period = Frequency.Monthly;
            outDate = DateTime.Today;
            count = 0;
            editors = new List<Person>();
            articles = new List<Article>();
        }

        public new String Name
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

        public Frequency Period
        {
            get
            {
                return period;
            }
            set
            {
                period = value;
            }
        }

        public DateTime Outdate
        {
            get
            {
                return outDate;
            }
            set
            {
                outDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Outdate"));
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
                count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
            }
        }

        public List<Article> Articles
        {
            get
            {
                List<Article> result = new List<Article>();

                articles.ForEach((art) => result.Add((Article)art.DeepCopy()));

                return result;
            }
            set
            {
                List<Article> result = new List<Article>();

                value.ForEach((art) => result.Add((Article)art.DeepCopy()));

                articles = result;
            }
        }

        public List<Person> Editors
        {
            get
            {
                List<Person> result = new List<Person>();

                editors.ForEach((persone) => result.Add((Person)persone.DeepCopy()));

                return result;
            }
            set
            {

                List<Person> result = new List<Person>();

                value.ForEach((persone) => result.Add((Person)persone.DeepCopy()));

                editors = result;
            }
        }

        public bool this[Frequency a]
        {
            get
            {
                return period.Equals(a);
            }
        }

        public void AddArticles(params Article[] a)
        {
            foreach (Article x in a)
                articles.Add((Article)x.DeepCopy());
        }

        public void AddEditors(params Person[] a)
        {
            foreach (Person x in a)
                editors.Add((Person)x.DeepCopy());
        }

        public override string ToString()
        {
            string result;
            result = "Название: " + name + "\n" + "Периодичность: " + period.ToString() + "\n" + "Дата издания: " + outDate.ToShortDateString() + "\n" + "Тираж: " + count + "\n";
            result += "Информация о статьях: \n";
            foreach (Article a in articles)
                result = result + a.ToString() + "\n";
            foreach (Person a in editors)
                result = result + a.ToString() + "\n";
            return result;
        }

        public double AverageScore
        {
            get
            {
                double sum = 0;
                foreach (Article a in articles)
                    sum += a.Score;
                return sum / articles.Count;
            }
        }

        public virtual string ToShortString()
        {
            return name + " " + period.ToString() + " " + outDate.ToShortDateString() + " " + count + "\n" + "Средний рейтинг: " + this.AverageScore;
        }

        public bool AddFromConsole()
        {
            Console.WriteLine("Введите данные о статье в следующем формате");
            Console.WriteLine("ArticleName@FirstName@LastName@BirthDate@Score");
            string result = Console.ReadLine();

            try
            {
                string[] param = result.Split('@');

                Person p = new Person(param[1], param[2], DateTime.Parse(param[3]));
                Article article = new Article(p, param[0], Convert.ToDouble(param[4]));
                articles.Add(article);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
        public override object DeepCopy()
        {

            BinaryFormatter formatter = new BinaryFormatter();
            Magazine result = new Magazine();

            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                result = (Magazine)formatter.Deserialize(ms);
            }
            return result;
        }

        public bool Save(string filename)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, this);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public bool Load(string filename)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    Magazine result = (Magazine)formatter.Deserialize(fs);
                    Articles = result.Articles;
                    Count = result.Count;
                    Editions = result.Editions;
                    Editors = result.Editors;
                    Name = result.Name;
                    OutDate = result.OutDate;
                    Outdate = result.Outdate;
                    Period = result.Period;
                    Score = result.Score;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static bool Save(string filename, Magazine magazine)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, magazine);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static bool Load(string filename, Magazine magazine)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    Magazine result = (Magazine)formatter.Deserialize(fs);
                    magazine.Articles = result.Articles;
                    magazine.Count = result.Count;
                    magazine.Editions = result.Editions;
                    magazine.Editors = result.Editors;
                    magazine.Name = result.Name;
                    magazine.OutDate = result.OutDate;
                    magazine.Outdate = result.Outdate;
                    magazine.Period = result.Period;
                    magazine.Score = result.Score;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        public Edition Editions
        {
            get
            {
                return new Edition((string)name.Clone(), outDate, count);
            }
            set
            {
                name = (string)value.Name.Clone();
                outDate = value.OutDate;
                count = value.Count;
            }
        }

        public IEnumerable<Article> ArticlesByRate(double x)
        {
            foreach (Article a in articles)
            {
                if (a.Score > x)
                    yield return (Article)a.DeepCopy();
            }
        }

        public IEnumerable<Article> ArticlesByString(string x)
        {
            foreach (Article a in articles)
            {
                if (a.Name.Contains(x))
                    yield return (Article)a.DeepCopy();
            }
        }

        public IEnumerable<Article> ArticlesByEditors()
        {
            foreach (Article a in articles)
            {
                if (editors.LastIndexOf(a.Author) != -1)
                    yield return (Article)a.DeepCopy();
            }
        }

        public IEnumerable<Person> OnlyEditors()
        {

            foreach (Person a in editors)
            {
                bool ans = true;
                foreach (Article art in articles)
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
            Result.period = (Frequency)Frequency.Parse(typeof(Frequency), Console.ReadLine(), true);
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

        public void OrderByTitle()
        {
            articles = articles.OrderBy(x => x.Name).ToList();
        }
        public void OrderByEditorName()
        {
            articles = articles.OrderBy(x => x.Author.LName).ToList();
        }
        public void OrderByScore()
        {
            articles = articles.OrderBy(x => x.Score).ToList();
        }
    }
}
