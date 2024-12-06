using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession.Classes
{
    internal class SingletonAccueil
    {
        MySqlConnection con;
        ObservableCollection<ActiviteClasse> listeActivite;
        static SingletonAccueil instance = null;


        //Propriété qui retourne la liste
        internal ObservableCollection<ActiviteClasse> ListeActivite { get { return listeActivite; } }

        public SingletonAccueil()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq3;Uid=1477852;Pwd=1477852;");
            listeActivite = new ObservableCollection<ActiviteClasse>();
        }

        public static SingletonAccueil getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonAccueil();
            }

            return instance;
        }


        public ObservableCollection<ActiviteClasse> getListe()
        {
            return listeActivite;
        }

        public void getActivites()
        {
            listeActivite.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from moyenneNotesParActiviteComplete";

            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                string nom = r.GetString("nomActivite");
                string categorie = r.GetString("nomCategorie");
                double moyenne = r.GetDouble("moyenneNotes");
                double prix = r.GetDouble("prixVenteClient");

                ActiviteClasse activite = new ActiviteClasse(nom, categorie, 0.0, prix);

                activite.Moyenne = moyenne;

                listeActivite.Add(activite);
            }

            r.Close();
            con.Close();
        }
    }
}
