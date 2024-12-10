using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession.Classes
{
    internal class AdherentClasse
    {
        string idAdherent;
        string nomAdherent;
        string prenomAdherent;
        string adresse;
        DateTime dateNais;
        int age;

     

        public AdherentClasse(string nomAdherent, string prenomAdherent, string adresse, DateTime dateNais)
        {
            this.nomAdherent = nomAdherent;
            this.prenomAdherent = prenomAdherent;
            this.adresse = adresse;
            this.dateNais = dateNais;
        }

        public AdherentClasse(string nomAdherent, string prenomAdherent, string adresse, DateTime dateNais, int age)
        {
            this.nomAdherent = nomAdherent;
            this.prenomAdherent = prenomAdherent;
            this.adresse = adresse;
            this.dateNais = dateNais;
            this.age = age;
        }

        public string IdAdherent { get => idAdherent; set => idAdherent = value; }
        public string NomAdherent { get => nomAdherent; set => nomAdherent = value; }
        public string PrenomAdherent { get => prenomAdherent; set => prenomAdherent = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public DateTime DateNais { get => dateNais; set => dateNais = value; }
        public int Age { get => age; set => age = value; }
    }
}
