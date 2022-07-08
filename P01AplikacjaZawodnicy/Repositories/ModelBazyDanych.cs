namespace P01AplikacjaZawodnicy.Repositories
{
    partial class ModelBazyDanychDataContext
    {
    }

    partial class Zawodnik 
    {
        public string ImieNazwiskoKraj => Imie + " " + Nazwisko + " " + Kraj;
    }

}