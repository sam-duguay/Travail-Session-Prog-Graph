using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession.Classes
{
    class ActiviteClasse
    {
        //Variables
        string nom;
        string type;
        double coutOrganisationClient;
        double prixVenteClient;

        double moyenne;

        string nbr_adherent;

        //Les propriétés
        public string Nom { get => nom; set => nom = value; }
        public string Type { get => type; set => type = value; }
        public double CoutOrganisationClient { get => coutOrganisationClient; set => coutOrganisationClient = value; }
        public double PrixVenteClient { get => prixVenteClient; set => prixVenteClient = value; }


        public double Moyenne { get => moyenne; set => moyenne = value; }
        public string Nbr_adherent { get => nbr_adherent; set => nbr_adherent = value; }


        //Le constructeur
        public ActiviteClasse(string nom, string type, double coutOrganisationClient, double prixVenteClient)
        {
            this.nom = nom;
            this.type = type;
            this.coutOrganisationClient = coutOrganisationClient;
            this.prixVenteClient = prixVenteClient;
        }


        public ActiviteClasse(string nom,string nbr_adherent) 
        {
            this.nom = nom;
            this.Nbr_adherent = nbr_adherent;
        }

        public string RetourCsv { get { return $"{Nom};{Type};{CoutOrganisationClient};{prixVenteClient}"; } }


    }
}
