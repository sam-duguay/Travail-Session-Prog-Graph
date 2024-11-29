using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravailSession.Classes;
using MySql.Data.MySqlClient;

namespace TravailSession.Classes
{
    internal class SingletonActivite
    {
        MySqlConnection con;
        ObservableCollection<ActiviteClasse> liste;
        static SingletonActivite instance = null;


        //Propriété qui retourne la liste
        internal ObservableCollection<ActiviteClasse> Liste { get { return liste; } }

        public SingletonActivite()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq3;Uid=1477852;Pwd=1477852;");
            liste = new ObservableCollection<ActiviteClasse>();
        }

        public static SingletonActivite getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonActivite();
            }

            return instance;
        }


        public ObservableCollection<ActiviteClasse> getListe()
        {
            return liste;
        }

        //TODO: va aller chercher les activités dans la BD
        public void getActivites()
        {
            liste.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from activite";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                string nom = r.GetString("nomActivite");
                string categorie = r.GetString("nomCategorie");
                double cout = r.GetDouble("coutOrganiserClient");
                double prix = r.GetDouble("prixVenteClient");

                ActiviteClasse activite = new ActiviteClasse(nom, categorie, cout, prix);

                liste.Add(activite);
            }

            r.Close();
            con.Close();
        }

        public Collection<string> getCategories()
        {
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
        public ActiviteClasse getActivite(int position) 
        {
            return liste[position];
        }



        public void ajouterActivite(string nom, string categorie, double coutOrganiser, double prixVente)
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


            //Réinitialise la liste des activités
            liste.Clear();
            getActivites();
        }


        public void modifierActivite(ActiviteClasse activiteVielle, ActiviteClasse activiteNouvelle)
        {

            supprimerActivite(activiteVielle);
            ajouterActivite(activiteNouvelle.Nom, activiteNouvelle.Type, activiteNouvelle.CoutOrganisationClient, activiteNouvelle.PrixVenteClient);

            //Réinitialise la liste des activités
            liste.Clear();
            getActivites();
        }


        public void supprimerActivite(ActiviteClasse activite)
        {

            try
            {
                string nom = activite.Nom;

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "DELETE FROM activite WHERE nomActivite = '" + nom + "'";
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
            getActivites();
        }

    }
}
