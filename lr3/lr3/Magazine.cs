﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lr3
{

    delegate TKey KeySelector<TKey>(Magazine mg);

    class MagazineCollection<TKey>
    {
        private Dictionary<TKey, Magazine> magazines;
        private KeySelector<TKey> keySelector;

        public MagazineCollection(KeySelector<TKey> keySelector)
        {
            this.keySelector = keySelector;
            magazines = new Dictionary<TKey, Magazine>();
        }
        public void AddDefaults() { }
        public void AddMagazines(params Magazine[] magazines)
        {
            foreach (Magazine entry in magazines)
            {
                this.magazines.Add(keySelector(entry), entry);
            }
        }

        public override string ToString()
        {
            string result = "";
            foreach (KeyValuePair<TKey, Magazine> entry in magazines)
            {
                result += entry.Value.ToString() + '\n';
            }
            return result;
        }
        public string ToShortString()
        {
            string result = "";
            foreach (KeyValuePair<TKey, Magazine> entry in magazines)
            {
                result += entry.Value.ToShortString() + '\n';
            }
            return result;
        }

        public double MaxScore
        {
            get
            {
                return magazines.Max(x => x.Value.AverageScore);
            }
        }

        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency period)
        {
            return magazines.Where(x => x.Value.Period == period);
        }

        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> Groups
        {
            get
            {
                return magazines.GroupBy(x => x.Value.Period);
            }
        }
    }
    class Magazine : Edition, IRateAndCopy, IEnumerable
    {
        private Frequency period;
        private List<Person> editors;
        private List<Article> articles;
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

        public override object DeepCopy()
        {
            Magazine result = new Magazine();
            result.name = (string)name.Clone();
            result.period = period;
            result.outDate = outDate;
            result.count = count;
            foreach (Article a in articles)
                result.articles.Add((Article)a.DeepCopy());
            foreach (Person a in editors)
                result.editors.Add((Person)a.DeepCopy());
            return result;
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
