using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01ORMWstep
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ModelBazyDanychDataContext db = new ModelBazyDanychDataContext();

            ////Zawodnik[] zawodnicy= db.Zawodnik.ToArray();


            ////Zawodnik[] wynik1 = db.Zawodnik.Where(wiersz => wiersz.kraj == "pol" || wiersz.kraj == "ger").ToArray();

            ////Zawodnik[] wynik2= db.Zawodnik.Where(wiersz => wiersz.kraj == "pol" || wiersz.kraj == "ger").OrderBy(x => x.wzrost).ToArray();

            ////Zawodnik[] wynik3 = db.Zawodnik.OrderByDescending(x => x.kraj).ThenBy(x => x.wzrost).ToArray();


            ////string[] wynik4 = db.Zawodnik.Select(x => x.imie).ToArray();

            ////string[] wynik5 = db.Zawodnik.OrderBy(x => x.kraj).Select(x => x.kraj).ToArray();

            ////string[] wynik6= db.Zawodnik.Select(x => x.kraj).OrderBy(x => x).ToArray();

            ////ZawodnikMini[] wynik7= db.Zawodnik.Select(x => new ZawodnikMini() { Imie = x.imie, Nazwisko = x.nazwisko }).ToArray();

            ////var wynik8 = db.Zawodnik.Select(x => new { MojeImie = x.imie, MojeNazwisko = x.nazwisko }).ToArray();

            ////var a = 5; // a jest typu int
            ////var b = "ala ma kota"; //b jest typu string 
            //////b = 5;

            ////dynamic c = "ala ma kota";
            ////c = 5;

            ////var d = new List<string[]>();

            ////List<string[]> e = new List<string[]>();


            ////var wynik9= db.Zawodnik.OrderByDescending(x => x.wzrost).Take(5).ToArray();


            ////// zad 1 : wypisz imie, nazwisko i BMI zawodnikow (waga[kg]/wzrost[m]^2)

            ////var wynik10=  db.Zawodnik.Select(x => new
            ////{
            ////    Imie = x.imie,
            ////    Nazwisko = x.nazwisko,
            ////    BMI = x.wzrost == null || x.waga == null ? 0 : x.waga / Math.Pow((int)x.wzrost / 100.0, 2)
            ////});

            //var wynik11 = db.Zawodnik.ToArray().Select(x => new
            //{
            //    Imie = x.imie,
            //    Nazwisko = x.nazwisko,
            //    BMI = x.wzrost == null || x.waga == null ? "brak" : String.Format("{0:0.00}", x.waga / Math.Pow((int)x.wzrost / 100.0, 2))
            //});

            var wynik14 = db.Zawodnik.ToArray().Select(x => new
            {
                Imie = x.imie,
                Nazwisko = x.nazwisko,
                BMI = FunkcjaBMI(x.waga,x.wzrost)
            });


            // Oznacza to, że z języka LINQ mozemy korzystac loklanie 

            string[] tablica = { "ala", "ma", "kota" };
            // wypisz tylko wyrazy dluzsze niz 2 
          //  string[] wynik13 = tablica.Where(x => x.Length > 2).OrderBy(x=>x).ToArray();


            //var wynik12 = db.Zawodnik.Select(x => WlasnaFunkcja(x.wzrost, x.waga)).ToArray();


            //var wynik13 = db.Zawodnik.Select(x => string.Format("{0:0.00}",x.waga)).ToArray();

            
            //// zad 2 : wypisz zawodnikow, ktorzy maja imie i nazwisko zaczynajace sie albo konczonce sie na te sama litere 


       //     var wynik16 =db.Zawodnik.Where(x => x.imie.ToLower()[0] == x.imie.ToLower()[x.imie.Length - 1] || x.nazwisko[0] == x.nazwisko[x.nazwisko.Length - 1]).ToArray();

         
            // zad 3 : wypisz zawodnika najstraszego 



            // Zawodnik wynik17= db.Zawodnik.OrderBy(x => x.data_ur).Take(1).ToArray()[0];

            //  Zawodnik wynik18 = db.Zawodnik.OrderBy(x => x.data_ur).Take(1).FirstOrDefault(); // first : execption , first or default : null


            // zad 4 : wypisz wszystkich zawodnikow z tego samogo kraju co zawodnik najlzejszy 

            //db.Zawodnik.OrderBy(x=>x.waga).Take(2) // tak nie zadziala bo nie wiemy ilu 

           // int minWaga = (int)db.Zawodnik.Min(x => x.waga);
           // string[] kraje = db.Zawodnik.Where(x => x.waga == minWaga).Select(x=>x.kraj).ToArray();
           // Zawodnik[] wynik19= db.Zawodnik.Where(x => kraje.Contains(x.kraj)).ToArray();


            Zawodnik[] wynik20 = db.Zawodnik
                .Where(x => 
                    db.Zawodnik.Where(y => y.waga == (int)db.Zawodnik.Min(z => z.waga)
                    ).Select(y => y.kraj).ToArray()
                        .Contains(x.kraj)
                    ).ToArray();


            foreach (var z in wynik20)
                Console.WriteLine(z.imie + " " + z.nazwisko);


            // zad 5 : wypisz imiona i nazwiska i daty urdzenia zawodnikow wraz z podaniem liczby dni za ile beda mieli urodziny
            // posotruj wyniki tak aby najpierw byli Ci co maja urodzny najblizej 

            // to jest delegat czyli wskaznik na funkcje 
            // w tym przypadku ta funkcja jest anonimowa 
            Func<DateTime?, DateTime?> NajblUro = (dataUr) =>
            {
                if (dataUr == null)
                    return null;

                var dataUrWAktualnymRoku = new DateTime(DateTime.Now.Year, ((DateTime)dataUr).Month, ((DateTime)dataUr).Day);
                if (dataUrWAktualnymRoku < DateTime.Now)
                    return dataUrWAktualnymRoku.AddYears(1);
                return dataUrWAktualnymRoku;
            };

            // to jest delegat czyli wskaznik na funkcje juz nie anonimowa tylko konkretna , ktora ma swoja nazwe i zostala gdzies wczensij utoworzona
            Func<DateTime?, DateTime?> NajblUro2 = NajblizszeUrodziny;

            var wynik21 = db.Zawodnik.ToArray().Select(x => new
            {
                Imie = x.imie,
                Nazwisko = x.nazwisko,
                DataUr = x.data_ur,
                ZaIleDniUrodziny = NajblUro(x.data_ur)- DateTime.Now
            }).ToArray();

            wynik21 = wynik21.OrderBy(x => x.ZaIleDniUrodziny.Value.TotalDays).ToArray();

            foreach (var z in wynik21)
                Console.WriteLine(
                    z.Imie.Substring(0, 3) +
                    "\t" + z.Nazwisko.Substring(0,3) + 
                    "\t" + z.DataUr.Value.ToString("ddMMyyy") + 
                    "\t" + Math.Floor(z.ZaIleDniUrodziny.Value.TotalDays)
                    );


            var wynik22 = db.Zawodnik.Where(x => x.kraj == "pol").ToArray();

            Func<Zawodnik, bool> czyPolak = (naszZawodnik) =>
            {
                string s = "pol";
                if (naszZawodnik.kraj == s)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };

            var wynik23 = db.Zawodnik.ToArray().Where(x => czyPolak(x)).ToArray();

            // deletegat typu void to Action 
            Action funkcjaTypuVoid = () =>
            {
                string s = "heej";
                Console.WriteLine("ala ma kota");
            };




            // grupowanie 

            var wynik24= db.Zawodnik
                .GroupBy(x => x.kraj)
                .Select(x => new { MojKraj = x.Key, SrWZrost = x.Average(y => y.wzrost) })
                .ToArray();

            foreach (var grupa in wynik24)
            {
                Console.WriteLine(grupa.MojKraj + " " + grupa.SrWZrost);
            }


            // dla kazdego kraju wypisz ilu jest zawodnikow wyzszych niz 180 cm 

            var wynik25 = db.Zawodnik
                .Where(x => x.wzrost > 180)
                .GroupBy(x => x.kraj)
                .Select(x => new { Kraj = x.Key, Ile = x.Count() })
                .ToArray();




            Console.ReadKey();
        }

        public static int WlasnaFunkcja(int? a, int? b)
        {
            if (a == null || b == null)
                return 0;

            return (int)a + (int)b;
        }

        public static string FunkcjaBMI(int? waga, int? wzrost)
        {
            if (waga == null || wzrost == null)
                return "brak";

            return String.Format("{0:0.00}", waga / Math.Pow((int)wzrost / 100.0, 2));
        }

        public static DateTime? NajblizszeUrodziny(DateTime? dataUr)
        {
            if (dataUr == null)
                return null;

            var dataUrWAktualnymRoku = new DateTime(DateTime.Now.Year, ((DateTime)dataUr).Month, ((DateTime)dataUr).Day);
            if (dataUrWAktualnymRoku < DateTime.Now)
                return dataUrWAktualnymRoku.AddYears(1);
            return dataUrWAktualnymRoku;
        }
    }
}
