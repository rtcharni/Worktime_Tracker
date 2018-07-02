﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kirjautuminen___DataBase.Models;

namespace Kirjautuminen___DataBase
{
    public partial class Form2 : Form
    {
        //Form2 luominen - konstruktori
        public Form2(Työntekijät työntekijä)
        {
            InitializeComponent();
            kirjautunut_käyttäjä.Text = työntekijä.Etunimi;
            if (työntekijä.Admin == false)
            {
                adminToiminnot.Hide();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
            pvm.Text = DateTime.Now.ToShortDateString();
            klo.Text = DateTime.Now.ToLongTimeString();
        }

        //Kellon juoksemis Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            klo.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        //Etsi käyttäjiä tietokannasta
        private void nappiEtsi_Click(object sender, EventArgs e) //Validointia pitää lisätä!
        {
            YdinvoimalaDBEntities dBEntities = new YdinvoimalaDBEntities();

            //Etsitään ID:n mukaan
            var käyttäjä = dBEntities.Työntekijät.Where(x => x.Käyttäjä_id == kenttäId.Value).FirstOrDefault();
            if (käyttäjä != null)
            {
                kenttäEtunimi.Text = käyttäjä.Etunimi;
                kenttäSukunimi.Text = käyttäjä.Sukunimi;
                kenttäKäyttäjätunnus.Text = käyttäjä.Käyttäjätunnus;
                kenttäSalasana.Text = käyttäjä.Salasana;
                if (käyttäjä.Admin)
                    nappiAdminTrue.Checked = true;
                else
                    nappiAdminFalse.Checked = true;
                käyttäjä = null;
                return;
            }

            //Etsitään käyttäjätunnuksen mukaan
            var käyttäjä2 = dBEntities.Työntekijät.Where(x => x.Käyttäjätunnus == kenttäKäyttäjätunnus.Text).FirstOrDefault();
            if (käyttäjä2 != null)
            {
                kenttäId.Value = käyttäjä2.Käyttäjä_id;
                kenttäEtunimi.Text = käyttäjä2.Etunimi;
                kenttäSukunimi.Text = käyttäjä2.Sukunimi;
                kenttäKäyttäjätunnus.Text = käyttäjä2.Käyttäjätunnus;
                kenttäSalasana.Text = käyttäjä2.Salasana;
                if (käyttäjä2.Admin)
                    nappiAdminTrue.Checked = true;
                else
                    nappiAdminFalse.Checked = true;
                käyttäjä2 = null;
                return;
            }

            //Etsitään etunimen ja sukunimen mukaan
            var käyttäjä1 = dBEntities.Työntekijät.Where(x => x.Etunimi == kenttäEtunimi.Text && x.Sukunimi == kenttäSukunimi.Text).ToList();
            if (käyttäjä1.Count == 1)
            {
                kenttäId.Value = käyttäjä1.FirstOrDefault().Käyttäjä_id;
                kenttäEtunimi.Text = käyttäjä1.FirstOrDefault().Etunimi;
                kenttäSukunimi.Text = käyttäjä1.FirstOrDefault().Sukunimi;
                kenttäKäyttäjätunnus.Text = käyttäjä1.FirstOrDefault().Käyttäjätunnus;
                kenttäSalasana.Text = käyttäjä1.FirstOrDefault().Salasana;
                if (käyttäjä1.FirstOrDefault().Admin)
                    nappiAdminTrue.Checked = true;
                else
                    nappiAdminFalse.Checked = true;
                käyttäjä1 = null;
                return;
            }
            else if (käyttäjä1.Count > 1)
            {
                kenttäUseampiLöytyi.Show();
                kenttäUseampiLöytyi.Items.AddRange(käyttäjä1.Select(x => x.Käyttäjätunnus.ToString()).ToArray());
            }
        }

        //Valitaan oikea henkilö useammasta löydetystä käyttäjästä
        private void kenttäUseampiLöytyi_SelectionChangeCommitted(object sender, EventArgs e)
        {
            YdinvoimalaDBEntities dBEntities = new YdinvoimalaDBEntities();
            var a = kenttäUseampiLöytyi.SelectedItem.ToString();
            var b = dBEntities.Työntekijät.Where(x => x.Käyttäjätunnus == a).FirstOrDefault();

            kenttäId.Value = b.Käyttäjä_id;
            kenttäEtunimi.Text = b.Etunimi;
            kenttäSukunimi.Text = b.Sukunimi;
            kenttäKäyttäjätunnus.Text = b.Käyttäjätunnus;
            kenttäSalasana.Text = b.Salasana;
            if (b.Admin)
                nappiAdminTrue.Checked = true;
            else
                nappiAdminFalse.Checked = true;
            b = null;
        }

        //Useampikenttä katoaa kun valittu oikea henkilö
        private void kenttäUseampiLöytyi_Leave(object sender, EventArgs e)
        {
            kenttäUseampiLöytyi.Items.Clear();
            kenttäUseampiLöytyi.Visible = false;
        }

        //Uuden käyttäjän luominen
        private void nappiLuo_Click(object sender, EventArgs e)
        {
            YdinvoimalaDBEntities dBEntities = new YdinvoimalaDBEntities();
            var onkoKäyttäjätunnustaOlemassa = dBEntities.Työntekijät.Where(x => x.Käyttäjätunnus == kenttäKäyttäjätunnus.Text).FirstOrDefault();

            if (kenttäEtunimi.TextLength == 0 || kenttäSukunimi.TextLength == 0 || kenttäKäyttäjätunnus.TextLength == 0 ||
                kenttäSalasana.TextLength == 0 || nappiAdminTrue.Checked == false && nappiAdminFalse.Checked == false)
            {
                MessageBox.Show("Täytä kaikki kentät");
                return;
            } else if (onkoKäyttäjätunnustaOlemassa != null)
            {
                MessageBox.Show("Käyttäjätunnus on olemassa");
                return;
            }
            else
            {
                bool onkoAdmin;
                if (nappiAdminTrue.Checked)
                    onkoAdmin = true;
                else
                    onkoAdmin = false;

                dBEntities.Työntekijät.Add(new Työntekijät()
                {
                    Etunimi = kenttäEtunimi.Text,
                    Sukunimi = kenttäSukunimi.Text,
                    Käyttäjätunnus = kenttäKäyttäjätunnus.Text,
                    Salasana = kenttäSalasana.Text,
                    Luomispäivä = DateTime.Now.Date,
                    Admin = onkoAdmin
                });
                dBEntities.SaveChanges();
                TyhjennäKaikkiKentät();
            }
        }


        

        //Apumetodi - kenttien tyhjennyt
        public void TyhjennäKaikkiKentät()
        {
            kenttäId.Value = 0;
            kenttäEtunimi.ResetText();
            kenttäSukunimi.ResetText();
            kenttäKäyttäjätunnus.ResetText();
            kenttäSalasana.ResetText();
            nappiAdminTrue.Checked = false;
            nappiAdminFalse.Checked = false;
            kenttäLisätiedot.ResetText();
        }

        //Tietojen päivitys nappi
        private void nappiPäivitä_Click(object sender, EventArgs e)
        {
            YdinvoimalaDBEntities dBEntities = new YdinvoimalaDBEntities();
            var käyttäjä = dBEntities.Työntekijät.Where(x => x.Käyttäjätunnus == kenttäKäyttäjätunnus.Text).FirstOrDefault();

            if (kenttäId.Value == 0 || kenttäEtunimi.TextLength == 0 || kenttäSukunimi.TextLength == 0 || kenttäKäyttäjätunnus.TextLength == 0 ||
                kenttäSalasana.TextLength == 0 || nappiAdminTrue.Checked == false && nappiAdminFalse.Checked == false)
            {
                MessageBox.Show("Puuttuu tietoja, täytä kaikki kentät");
                return;
            }
            else if (käyttäjä == null)
            {
                MessageBox.Show("Käyttäjää ei löytynyt käyttäjätunnuksella");
                return;
            }
            else if (kenttäId.Value != käyttäjä.Käyttäjä_id || kenttäKäyttäjätunnus.Text != käyttäjä.Käyttäjätunnus)
            {
                MessageBox.Show("ID ja Käyttäjätunnus eivät täsmää, päivitys ei onnistu");
                return;
            }
            else if (kenttäId.Value == käyttäjä.Käyttäjä_id && kenttäKäyttäjätunnus.Text == käyttäjä.Käyttäjätunnus)
            {
                käyttäjä.Etunimi = kenttäEtunimi.Text;
                käyttäjä.Sukunimi = kenttäSukunimi.Text;
                käyttäjä.Salasana = kenttäSalasana.Text;
                käyttäjä.Admin = nappiAdminTrue.Checked;
                MessageBox.Show("Käyttäjätiedot päivitetty");
                dBEntities.SaveChanges();
                TyhjennäKaikkiKentät();
            }
        }

        //Kirjaudu ulos nappi
        private void nappi_ulosKirjautuminen_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
