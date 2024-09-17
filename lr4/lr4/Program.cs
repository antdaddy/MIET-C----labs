
 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace lr4
{
    class Program
    {
        static void Main(string[] args)
        {
            MagazineCollection<string> collection1 = new MagazineCollection<string>(x => x.Name, "Collection 1");
            MagazineCollection<string> collection2 = new MagazineCollection<string>(x => x.Name, "Collection 2");

            Listener<string> listener = new Listener<string>();
            collection1.Notify += listener.OnChanged;
            collection2.Notify += listener.OnChanged;



            Magazine m1 = new Magazine("Magazine 1", Frequency.Monthly, DateTime.Now, 150);
            Magazine m2 = new Magazine("Magazine 2", Frequency.Weekly, DateTime.Now, 156);
            Magazine m3 = new Magazine("Magazine 3", Frequency.Yearly, DateTime.Now, 154);
            Magazine m4 = new Magazine("Magazine 4", Frequency.Monthly, DateTime.Now, 789);

            collection1.AddMagazines(m1, m2);
            collection2.AddMagazines(m3, m4);


            Magazine m5 = new Magazine("Magazine 5", Frequency.Monthly, DateTime.Now, 789);
            m3.Name = "New Name";
            m2.Outdate = DateTime.Now;
            m1.Count = 15055;

            collection2.Replace(m4, m5);

            m4.Name = "test";

            Console.WriteLine(listener.ToString());
            Console.ReadKey();

        }
    }
}
