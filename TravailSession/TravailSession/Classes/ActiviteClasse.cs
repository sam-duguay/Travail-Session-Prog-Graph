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

        //Les propriétés
        public string Nom { get => nom; set => nom = value; }
        public string Type { get => type; set => type = value; }
        public double CoutOrganisationClient { get => coutOrganisationClient; set => coutOrganisationClient = value; }
        public double PrixVenteClient { get => prixVenteClient; set => prixVenteClient = value; }


        public double Moyenne { get => moyenne; set => moyenne = value; }


        //Le constructeur
        public ActiviteClasse(string nom, string type, double coutOrganisationClient, double prixVenteClient)
        {
            this.nom = nom;
            this.type = type;
            this.coutOrganisationClient = coutOrganisationClient;
            this.prixVenteClient = prixVenteClient;
        }
    }
}
