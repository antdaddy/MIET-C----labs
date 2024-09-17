using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr5
{
    class ArticleComparer : IComparer<Article>
    {
        public int Compare(Article x, Article y)
        {
            if (x == null || y == null)
                throw new ArgumentException("One or both arguments is not type of Article");

            if (x.Score <= y.Score)
                return x.Score == y.Score ? 0 : -1;

            return 1;
        }
    }

    [Serializable]
    class Article : IRateAndCopy, IComparable, IComparer<Article>
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

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Article article = obj as Article;

            if (article != null)
                return Name.CompareTo(obj);

            throw new ArgumentException("Argument is not type of Article");
        }

        public int Compare(Article x, Article y)
        {
            if (x == null || y == null)
                throw new ArgumentException("One or both arguments is not type of Article");

            return string.Compare(x.Author.LName, y.Author.LName);
        }
    }
}
