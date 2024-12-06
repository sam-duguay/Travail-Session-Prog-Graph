using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession.Classes
{
    class SingletonSeance
    {
        MySqlConnection con;
        ObservableCollection<SeanceClasse> liste;
        static SingletonSeance instance = null;


        //Propriété qui retourne la liste
        internal ObservableCollection<SeanceClasse> Liste { get { return liste; } }

        public SingletonSeance()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq3;Uid=1477852;Pwd=1477852;");
            liste = new ObservableCollection<SeanceClasse>();
        }

        public static SingletonSeance getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonSeance();
            }

            return instance;
        }


        public ObservableCollection<SeanceClasse> getListe()
        {
            return liste;
        }



        public void getSeances()
        {
            liste.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from seance";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                int id = r.GetInt32("idSeance");
                DateOnly date = DateOnly.FromDateTime(r.GetDateTime("dateSeance"));
                DateTime heure = r.GetDateTime("heureOrganisation");
                int nbPlace = r.GetInt32("nbPlaceDispo");
                string activite = r.GetString("nomActivite");
                string type = r.GetString("typeActivite");


                SeanceClasse seance = new SeanceClasse(id, date, heure, nbPlace, activite, type);

                liste.Add(seance);
            }

            r.Close();
            con.Close();
        }

        public Collection<string> getActivite()
        {

            Collection<string> activites = new Collection<string>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select nomActivite, nomCategorie from activite";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                string activite = r.GetString("nomActivite") + "/" + r.GetString("nomCategorie");

                activites.Add(activite);
            }

            r.Close();
            con.Close();

            return activites;
        }



        //Va chercher une activité à une position X
        //public ActiviteClasse getActivite(int position)
        //{
        //    //return liste[position];
        //}



        public void ajouterSeance(string date, TimeSpan heure, int nbPlace, string nomActivite, string nomCategorie)
        {
            try
            {
                //Procédure stockée
                MySqlCommand commande = new MySqlCommand("creation_Seance");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("date", date);
                commande.Parameters.AddWithValue("heure", heure);
                commande.Parameters.AddWithValue("nbplaceDisponible", nbPlace);
                commande.Parameters.AddWithValue("nom", nomActivite);
                commande.Parameters.AddWithValue("type", nomCategorie);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            //Réinitialise la liste des activités
            liste.Clear();
            getSeances();
        }


        public void supprimerSeance(SeanceClasse seance)
        {

            try
            {
                int id = seance.Id;

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "DELETE FROM seance WHERE idSeance = '" + id + "'";
                con.Open();
                int i = commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }


            //Réinitialise la liste des activités
            liste.Clear();
            getSeances();
        }


        public void modifierSeance(SeanceClasse seanceModif, string date, TimeSpan heure, int nbPlace, string nomActivite, string nomCategorie)
        {
            //Les clés primaires de l'ancienne activite
            int id = seanceModif.Id;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "UPDATE seance SET dateSeance=@date, heureOrganisation=@heure, nbPlaceDispo=@place, nomActivite=@activite, typeActivite=@categorie WHERE idSeance=@id";
                commande.Parameters.AddWithValue("@date", date);
                commande.Parameters.AddWithValue("@heure", heure);
                commande.Parameters.AddWithValue("@place", nbPlace);
                commande.Parameters.AddWithValue("@activite", nomActivite);
                commande.Parameters.AddWithValue("@categorie", nomCategorie);
                commande.Parameters.AddWithValue("@id", id);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();


            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            //Réinitialise la liste des activités
            liste.Clear();
            getSeances();
        }



    }
}
