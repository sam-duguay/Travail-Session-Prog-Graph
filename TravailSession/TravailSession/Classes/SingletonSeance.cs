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

        //TODO: va aller chercher les activités dans la BD
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

            // À CONSTRUIRE


            Collection<string> categories = new Collection<string>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select nomCategorie from categorie";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                string categorie = r.GetString("nomCategorie");

                categories.Add(categorie);
            }

            r.Close();
            con.Close();

            return categories;
        }



        //Va chercher une activité à une position X
        //public ActiviteClasse getActivite(int position)
        //{
        //    //return liste[position];
        //}



        public void ajouterActivite(string nom, string categorie, double coutOrganiser, double prixVente)
        {
            try
            {
                //Procédure stockée
                MySqlCommand commande = new MySqlCommand("creation_Activite");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("nom", nom);
                commande.Parameters.AddWithValue("categorie", categorie);
                commande.Parameters.AddWithValue("coutOrganiser", coutOrganiser);
                commande.Parameters.AddWithValue("prixVente", prixVente);

                con.Open();
                commande.Prepare();
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


        public void supprimerActivite(ActiviteClasse activite)
        {

            try
            {
                string nom = activite.Nom;
                string categorie = activite.Type;

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "DELETE FROM activite WHERE nomActivite = '" + nom + "' AND nomCategorie= '" + categorie + "'";
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


        public void modifierActivite(ActiviteClasse activiteModif, string nom, string categorie, double coutOrganiser, double prixVente)
        {
            //Les clés primaires de l'ancienne activite
            string oldNom = activiteModif.Nom;
            string oldCategorie = activiteModif.Type;

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "UPDATE activite SET nomActivite=@nom, nomCategorie=@categorie, coutOrganiserClient=@cout, prixVenteClient=@prix WHERE nomActivite=@oldNom AND nomCategorie=@oldCategorie ";
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@categorie", categorie);
                commande.Parameters.AddWithValue("@cout", coutOrganiser);
                commande.Parameters.AddWithValue("@prix", prixVente);
                commande.Parameters.AddWithValue("@oldNom", oldNom);
                commande.Parameters.AddWithValue("@oldCategorie", oldCategorie);

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
