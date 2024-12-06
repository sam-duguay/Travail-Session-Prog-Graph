using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravailSession.Classes;
using MySql.Data.MySqlClient;
using Windows.UI.Notifications;
using System.Data;

namespace TravailSession.Classes
{
    class SingletonAdherent
    {
        MySqlConnection _connection;
        ObservableCollection<AdherentClasse> liste;
        static SingletonAdherent instance = null;

        // produit pour retourner la liste 
        internal ObservableCollection<AdherentClasse> Liste { get { return liste; } }

        public SingletonAdherent()
        {
            _connection = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq3;Uid=1073274;Pwd=1073274;");
            liste = new ObservableCollection<AdherentClasse>();
        }

        public static SingletonAdherent getInstance() 
        { 
        
            if (instance == null) 
            {
                instance = new SingletonAdherent();
            }
            return instance; 
        }

        public ObservableCollection<AdherentClasse> getListe() { return liste; }

      public void getAdherent()
        {
            liste.Clear();

            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = "Select * from adherent";

            _connection.Open();
            MySqlDataReader r = command.ExecuteReader();
            while (r.Read())
            {
                string nom = r.GetString("nomAdherent");
                string prenom = r.GetString("prenomAdherent");
                string adresse = r.GetString("adresse");
                DateTime date = r.GetDateTime("dateNais");
                int age = r.GetInt32("age");



                AdherentClasse adherent = new AdherentClasse(nom,prenom,adresse,date,age);
                liste.Add(adherent);
            }
            r.Close();
            _connection.Close();


            
        }

        public void ajouterAdherent(string nom,string prenom, string adresse, DateTimeOffset date)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("creation_Adherent");
                command.Connection = _connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("nom", nom);
                command.Parameters.AddWithValue("prenom", prenom);
                command.Parameters.AddWithValue("adresseAdherent", adresse);
                command.Parameters.AddWithValue("dateNaissance",date);

                _connection.Open();
                command.Prepare();
                int i = command.ExecuteNonQuery();

                _connection.Close();
            }
            catch(Exception ex)
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
            }



            liste.Clear();
            getAdherent();
        }



        // aller chercher un adhérent dans la liste.

  
        public AdherentClasse getAdherent(int position) 
        {

            return liste[position];
        }

        public void modifierAdherent(int position, AdherentClasse adherent)
        {
            liste[position] = adherent;
        }
        public void supprimerAdherent(int position)
        {
            liste.RemoveAt(position);
        }
    }
    
}
