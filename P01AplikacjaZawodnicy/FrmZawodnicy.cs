
using P01AplikacjaZawodnicy.Repositories;
using P01AplikacjaZawodnicy.Tools;
using P01AplikacjaZawodnicy.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P01AplikacjaZawodnicy
{
    public partial class FrmZawodnicy : Form
    {
        public FrmZawodnicy()
        {
            InitializeComponent();
        }

        public void Odswiez()
        {
            ZawodnicyRepository zr = new ZawodnicyRepository();
            ZawodnicyResultVM resultVM = zr.PobierzZawodnikow(Convert.ToInt32(txtStrona.Text));
            ZbindujDaneZawodnikow(resultVM.Zawodnicy);
            lblLiczbaStron.Text = Convert.ToString(resultVM.LiczbaStron);
        }

        private void ZbindujDaneZawodnikow(Zawodnik[] zawodnicy)
        {
            // lbDane.DataSource = zawodnicy;
            // lbDane.DisplayMember = "ImieNazwiskoKraj";

            lvDane.Items.Clear();
            //foreach (var z in zawodnicy)
            //    lvDane.Items.Add(z.ImieNazwiskoKraj);

            ImageList imageList = new ImageList();

            
            zawodnicy.Select(x => x.Kraj).Distinct().ToList().ForEach(x =>
            {
                string sciezka = $"flagi\\{x.ToLower()}.jpg";
                if (File.Exists(sciezka))
                    imageList.Images.Add(x.ToLower(), Image.FromFile(sciezka));
            });

            lvDane.LargeImageList = imageList;
            zawodnicy.ToList().ForEach(x =>
            {
                lvDane.Items.Add(new ListViewItem()
                {
                    Text = x.ImieNazwiskoKraj,
                    Tag = x,
                    ImageKey = x.Kraj.ToLower()
                });                
            });
            

        }

        private void btnWczytaj_Click(object sender, EventArgs e)
        {
            Odswiez();
        }

        private void btnSzczegoly_Click(object sender, EventArgs e)
        {
            if (lvDane.SelectedItems.Count == 0)
                return;

            Zawodnik zaznaczony = (Zawodnik)lvDane.SelectedItems[0].Tag;


            FrmSzczegoly fs = new FrmSzczegoly(zaznaczony, this);
            fs.Show();


        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            FrmSzczegoly fs = new FrmSzczegoly(this);
            fs.Show();
        }


        private void Szukaj()
        {
            if (txtSzukaj.Text.Length > 2)
            {
                
                ZawodnicyRepository zr = new ZawodnicyRepository();
                ZawodnicyResultVM resultVM = zr.Szukaj(txtSzukaj.Text, Convert.ToInt32(txtStrona.Text));
                ZbindujDaneZawodnikow(resultVM.Zawodnicy);
                lblLiczbaStron.Text = Convert.ToString(resultVM.LiczbaStron);
            }
            else
                Odswiez();
        }

        private void txtSzukaj_TextChanged(object sender, EventArgs e)
        {
            txtStrona.Text = "1";
            Szukaj();
        }

        private void pbStronaLewo_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtStrona.Text)>1)
            {
                txtStrona.Text = Convert.ToString(Convert.ToInt32(txtStrona.Text) - 1);
                Szukaj();
            }
            
        }

        private void pbStronaPrawo_Click(object sender, EventArgs e)
        {
            txtStrona.Text = Convert.ToString(Convert.ToInt32(txtStrona.Text) + 1);
            Szukaj();
        }

        private void btnGenerujPDF_Click(object sender, EventArgs e)
        {
            PDFManager pdfm = new PDFManager();

            //List<Zawodnik> zawodnicy = new List<Zawodnik>();
            //foreach (var item in lvDane.Items)
            //    zawodnicy.Add((Zawodnik)((ListViewItem)item).Tag);

            // można proście używajac linq 
            var zawodnicy = lvDane.Items.Cast<ListViewItem>().Select(x => (Zawodnik)x.Tag).ToArray();
       

            string nazwaPliku =  pdfm.StworzPdf(zawodnicy);

            //string sciezkaDoFolderu = @"C:\Users\kurs\source\repos\Dzien7\P01AplikacjaZawodnicy\bin\Debug";

            string sciezkaDoExe = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string sciezkaDoFolderu = System.IO.Path.GetDirectoryName(sciezkaDoExe);

            wbRaport.Navigate(sciezkaDoFolderu + $"\\{nazwaPliku}");
        }
    }
}
