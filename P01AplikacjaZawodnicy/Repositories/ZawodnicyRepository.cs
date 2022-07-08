
using P01AplikacjaZawodnicy.ViewModels;
using P04BibliotekaPolaczenieZBaza;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01AplikacjaZawodnicy.Repositories
{
    //http://github.com/tomasz-trener/14DDzien6 
    //przerwa 13:40
    internal class ZawodnicyRepository 
    {
        public ZawodnicyResultVM PobierzZawodnikow(int strona)
        {
            return Szukaj("", strona);
        }
        internal ZawodnicyResultVM Szukaj(string text, int strona)
        {
            int ile = Convert.ToInt32(ConfigurationManager.AppSettings["liczbaElementow"]) ;
            ModelBazyDanychDataContext db = new ModelBazyDanychDataContext();

            var zapytanieSzablon = db.Zawodnik.Where(x =>
                x.Imie.Contains(text) ||
                x.Nazwisko.Contains(text) ||
                x.Kraj.Contains(text) ||
                x.Data_ur.ToString().Contains(text) ||
                x.Wzrost.ToString().Contains(text) ||
                x.Waga.ToString().Contains(text));
                   
            var zawodnicy = zapytanieSzablon.Skip((strona - 1) * ile).Take(ile).ToArray();
            int ileStron = Convert.ToInt32(Math.Ceiling(zapytanieSzablon.Count() / Convert.ToDouble(ile)));

            return new ZawodnicyResultVM()
            {
                Zawodnicy = zawodnicy,
                LiczbaStron = ileStron
            };
        }

        internal void Edytuj(Zawodnik zaznaczony)
        {
            ModelBazyDanychDataContext db = new ModelBazyDanychDataContext();
            var z = db.Zawodnik.FirstOrDefault(x => x.Id_zawodnika == zaznaczony.Id_zawodnika);

            z.Imie = zaznaczony.Imie;
            z.Nazwisko = zaznaczony.Nazwisko;
            z.Kraj = zaznaczony.Kraj;
            z.Data_ur = zaznaczony.Data_ur;
            z.Waga = zaznaczony.Waga;
            z.Wzrost = zaznaczony.Wzrost;

            db.SubmitChanges();
        }

        internal void Usun(Zawodnik zaznaczony)
        {
            ModelBazyDanychDataContext db = new ModelBazyDanychDataContext();
            var z = db.Zawodnik.FirstOrDefault(x => x.Id_zawodnika == zaznaczony.Id_zawodnika);
            db.Zawodnik.DeleteOnSubmit(z);
            db.SubmitChanges();
        }

        internal void Dodaj(Zawodnik zaznaczony)
        {
            ModelBazyDanychDataContext db = new ModelBazyDanychDataContext();
            db.Zawodnik.InsertOnSubmit(zaznaczony);
            db.SubmitChanges();
        }
    }
}
