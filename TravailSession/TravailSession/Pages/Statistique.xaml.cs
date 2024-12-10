using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using TravailSession.Classes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Statistique : Page
    {
        MySqlConnection con;


        ObservableCollection<ActiviteClasse> liste_nb_adherent;
        ObservableCollection<SeanceClasse> listeSeanceFinal;


        public Statistique()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq3;Uid=1073274;Pwd=1073274;");

           

            this.InitializeComponent();
            tb_nb_total_adherent.Text = total_adherent();
            tb_nb_total_activite.Text = total_activiter();
            tb_moyenne_note_appreciation.Text = moy_note_activite();
            tb_cours_plus_de_sceance.Text = plus_sceance();
            tb_adherent_plus_cours.Text = adherent_plus_cour();
            lv_nb_adherent_x_activite.ItemsSource = nbAdherentActivite();
            lv_sceance_final_cours.ItemsSource = sceanceFinal();



        }

        public string total_adherent()
        {

            string total = "Aucune Valeur";
            try
            {
                MySqlCommand tot_adh = new MySqlCommand();
                tot_adh.Connection = con;
                tot_adh.CommandText = "SELECT count(*) FROM adherent";
                con.Open();
                tot_adh.ExecuteScalar().ToString();

                total = tot_adh.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex) { }


            return total ;
        }

        public string total_activiter()
        {

            string total = "Aucune Valeur";
            try
            {
                MySqlCommand tot_adh = new MySqlCommand();
                tot_adh.Connection = con;
                tot_adh.CommandText = "SELECT count(*) FROM activite";
                con.Open();
                tot_adh.ExecuteScalar().ToString();

                total = tot_adh.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex) { }


            return total;
        }

        public string plus_sceance()
        {

            string total = "Aucune Valeur";
            try
            {
                MySqlCommand tot_adh = new MySqlCommand();
                tot_adh.Connection = con;
                tot_adh.CommandText = "SELECT nomActivite FROM seance GROUP BY nomActivite ORDER BY Count(idSeance) DESC LIMIT 1;";
                con.Open();
                tot_adh.ExecuteScalar().ToString();

                total = tot_adh.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex) { }


            return total;
        }
        public string adherent_plus_cour()
        {

            string total = "Aucune Valeur";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select `Nom du participant`   from  afficher_participant_plus_seances;";
                con.Open();
                cmd.ExecuteScalar().ToString();

                total = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex) { }


            return total;
        }


        private ObservableCollection<SeanceClasse> sceanceFinal()
        {
            listeSeanceFinal = new ObservableCollection<SeanceClasse>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT *FROM seance GROUP BY nomActivite ORDER BY MAX(dateSeance);";
            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DateOnly date = DateOnly.FromDateTime(reader.GetDateTime("dateSeance"));
                string activite = reader.GetString("nomActivite");

                SeanceClasse ligne = new SeanceClasse(date,activite);
                listeSeanceFinal.Add(ligne);

            }

            reader.Close();
            con.Close();

            return listeSeanceFinal;



        }


        private ObservableCollection<ActiviteClasse> nbAdherentActivite()
        {
            liste_nb_adherent = new ObservableCollection<ActiviteClasse>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM nbparticpantparactivite";
            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                string nom= reader.GetString("nomActivite");
                string nbr = reader.GetInt64("nbParticipant").ToString();

                ActiviteClasse ligne = new ActiviteClasse(nom, nbr);
                liste_nb_adherent.Add(ligne);

            }

            reader.Close();
            con.Close();

            return liste_nb_adherent;



        }

        public string  moy_note_activite()
        {
            string total = "Aucune Valeur";
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select avg(moyennenotesparactivite.moyenneNotes) FROM moyennenotesparactivite;";
                con.Open();
                cmd.ExecuteScalar().ToString();

                total = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex) { }


            return total;

        }

    }
}
