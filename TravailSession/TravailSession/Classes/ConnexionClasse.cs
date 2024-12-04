using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession.Classes
{
    internal class ConnexionClasse
    {
        AdherentClasse Adherent;
        bool connecter;
        string nomAdmin;
        string mdp;

        public ConnexionClasse(bool connecter)
        {
            this.connecter = connecter;
        }

        public ConnexionClasse(AdherentClasse adherent, bool connecter)
        {
            Adherent = adherent;
            this.connecter = connecter;
        }

        public ConnexionClasse(bool connecter, string nomAdmin, string mdp)
        {
            this.connecter = connecter;
            this.nomAdmin = nomAdmin;
            this.mdp = mdp;
        }

        public bool Connecter { get => connecter; set => connecter = value; }
        public string NomAdmin { get => nomAdmin; set => nomAdmin = value; }
        public string Mdp { get => mdp; set => mdp = value; }

        internal AdherentClasse Adherent1 { get => Adherent; set => Adherent = value; }
    }
}
