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
        ObservableCollection<SeanceClasse> listeSeance;
        ObservableCollection<ParticipationClasse> listeParticipation;

        static SingletonAccueil instance = null;

        string typeUtilisateur;
        string statutInscription;
        ActiviteClasse activiteChoissi;
        


        //Propriétés qui retourne les listes
        internal ObservableCollection<ActiviteClasse> ListeActivite { get { return listeActivite; } }
        internal ObservableCollection<SeanceClasse> ListeSeance { get { return listeSeance; } }
        internal ObservableCollection<ParticipationClasse> ListeParticipation { get { return listeParticipation; } }

        public SingletonAccueil()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq3;Uid=1477852;Pwd=1477852;");
            listeActivite = new ObservableCollection<ActiviteClasse>();
            listeSeance = new ObservableCollection<SeanceClasse>();
            listeParticipation = new ObservableCollection<ParticipationClasse>();

            //Sert à déterminer la catégorie de l'utilisateur pour déterminer s'il a accès ou non
            //aux séances sur la page d'accueil une fois qu'il clique sur une activitée.
            typeUtilisateur = string.Empty;
            statutInscription = string.Empty;
            activiteChoissi = new ActiviteClasse("", "", 0, 0);
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

        public ObservableCollection<SeanceClasse> getListeSeance()
        {
            return listeSeance;
        }

        public void assignerTypeUtilisateur(string type)
        {
            typeUtilisateur = type;
        }

        public string getTypeUtilisateur()
        {
            return typeUtilisateur;
        }

        public void assignerStatutInscription(string statut)
        {
            statutInscription = statut;
        }

        public string getStatutInscription()
        {
            return statutInscription;
        }

        public void assignerActiviteChoissi(ActiviteClasse activite)
        {
            activiteChoissi = activite;
        }

        public ActiviteClasse getactiviteChoissi()
        {
            return activiteChoissi;
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

        public ActiviteClasse getActivite(int index)
        {
            return listeActivite[index];
        }



        public void getSeances(ActiviteClasse activiteRecherche)
        {
            listeSeance.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from seance WHERE nomActivite='" + activiteRecherche.Nom + "' AND typeActivite='" + activiteRecherche.Type + "' AND nbPlaceDispo!=0";

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

                listeSeance.Add(seance);
            }

            r.Close();
            con.Close();
        }

        public void getSeancesInscrit(string idAdherent)
        {
            listeParticipation.Clear();

            try
            {
                //Procédure stockée
                MySqlCommand commande = new MySqlCommand("detailsParticipation");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("id", idAdherent);

                con.Open();
                commande.Prepare();
                //int i = commande.ExecuteNonQuery();

                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {

                    int id = r.GetInt32("idSeance");
                    string activite = r.GetString("nomActivite");
                    string typeActivite = r.GetString("typeActivite");
                    DateOnly date = DateOnly.FromDateTime(r.GetDateTime("dateSeance"));
                    DateTime heure = r.GetDateTime("heureOrganisation");
                    int nbPlace = r.GetInt32("nbPlaceDispo");
                    string adherent = r.GetString("idAdherent");
                    int note = r.GetInt32("note");
                    
                    ParticipationClasse participation = new ParticipationClasse(id, adherent, date, heure, nbPlace, activite, typeActivite, note);

                    listeParticipation.Add(participation);

                }

                r.Close();
                con.Close();


            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }


        public void ajouterParticipation(int note, string idAdherent, int idSeance)
        {

            //Réinitialise la liste des activités
            listeParticipation.Clear();

            try
            {
                //Procédure stockée
                MySqlCommand commande = new MySqlCommand("creation_Participation");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("noteAppreciation", note);
                commande.Parameters.AddWithValue("identificationClient", idAdherent);
                commande.Parameters.AddWithValue("identificationSeance", idSeance);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();


                //Mise à jour du statut de l'inscription pour confirmation dans le DialogueParticipation
                statutInscription = "Vous êtes maintenant inscrit à cette séance!";

            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();


                //Si l'utilisateur est déjà inscrit, le message du DialogueParticipation sera différent
                statutInscription = "Vous êtes déjà inscrit pour cette séance.";
            }


            getSeancesInscrit(idAdherent);


            
        }



    }
}
