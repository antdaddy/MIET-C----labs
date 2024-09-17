using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace lr5
{
    class Program
    {
        static void Main(string[] args)
        {
            Magazine magazine = new Magazine("Magazine", Frequency.Monthly, new DateTime(2023, 3, 12), 10);

            Person p1 = new Person("Usman", "Alisherov", new DateTime(1968, 1, 3));
            Person p2 = new Person("Nikolaev", "Patrush", new DateTime(1984, 6, 11));
            Person p3 = new Person("Medved", "Dmitriev", new DateTime(1963, 5, 3));

            Article a1 = new Article(p1, "How to not pay taxes", 6.64);
            Article a2 = new Article(p1, "My way to the yacht", 4.41);
            Article a3 = new Article(p3, "The most beautiful vineyards", 8.54);
            Article a4 = new Article(p2, "The americans have not been to the moon", 4.59);
            Article a5 = new Article(p2, "The truth about the golden billion", 10);
            Article a6 = new Article(p1, "How much is a million", 9.41);

            magazine.AddArticles(a1, a2, a3, a4, a5, a6);

            Magazine copy = (Magazine)magazine.DeepCopy();

            Console.WriteLine(magazine);
            Console.WriteLine(copy);

            Magazine wm = new Magazine();

            Console.WriteLine("Введите имя файла");
            string filename = Console.ReadLine();
            if (!File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.CreateNew)) { }
                Console.WriteLine("Файла не существует, мы создали файл за вас");
            }
            else
            {
                wm.Load(filename);
            }
            Console.WriteLine(wm);
            wm.AddFromConsole();
            wm.Save(filename);
            Console.WriteLine(wm);

            Magazine.Load(filename, wm);
            wm.AddFromConsole();
            Magazine.Save(filename, wm);
            Console.WriteLine(wm);
        }
    }
}
