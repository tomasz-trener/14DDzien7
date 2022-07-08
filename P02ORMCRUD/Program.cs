using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02ORMCRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ModekBazyDanychDataContext db = new ModekBazyDanychDataContext();


            // dodawanie 
            ////Zawodnik z = new Zawodnik()
            ////{
            ////    imie = "jan",
            ////    nazwisko = "kowalski",
            ////    kraj = "pol",
            ////    data_ur = DateTime.Now,
            ////    waga= 40,
            ////    wzrost=50
            ////};

            ////db.Zawodnik.InsertOnSubmit(z);
            ////db.SubmitChanges();

            // edycja
            // najpierw treba pobrac zawodnika, ktorego chcemy edytowac, potem go edytujemy i zapisuemy zmiany
            // 

            //var doEdycji = db.Zawodnik.Where(x => x.id_zawodnika == 19).ToArray()[0]

            //var doEdycji = db.Zawodnik.Where(x => x.id_zawodnika == 19).FirstOrDefault();

            ////var doEdycji = db.Zawodnik.FirstOrDefault(x => x.id_zawodnika == 19);

            ////doEdycji.nazwisko += "xx";
            ////db.SubmitChanges();

            // usuwanie 

            // napierw pobieramy tego do usuniecia , potem usuwamy i zapis zmian 

            var doUsuniecia = db.Zawodnik.FirstOrDefault(x => x.id_zawodnika == 19);
            db.Zawodnik.DeleteOnSubmit(doUsuniecia);
            db.SubmitChanges();


        }
    }
}
