using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession.Classes
{
    class SeanceClasse
    {

        //Les variables
        int id;
        DateOnly dateSeance;
        DateTime heureOrganisation;
        int nbPlaceDispo;
        string nomActivite;
        string typeActivite;

        //Les propriétés
        public int Id { get => id; set => id = value; }
        public DateOnly DateSeance { get => dateSeance; set => dateSeance = value; }
        public DateTime HeureOrganisation { get => heureOrganisation; set => heureOrganisation = value; }
        public int NbPlaceDispo { get => nbPlaceDispo; set => nbPlaceDispo = value; }
        public string NomActivite { get => nomActivite; set => nomActivite = value; }
        public string TypeActivite { get => typeActivite; set => typeActivite = value; }

        //Propriété qui retourne l'heure formattée
        public string HeureFormatte
        {
            get => HeureOrganisation.ToString("HH:mm");
        }


        //Le constructeur
        public SeanceClasse(int id, DateOnly dateSeance, DateTime heureOrganisation, int nbPlaceDispo, string nomActivite, string typeActivite)
        {
            this.Id = id;
            this.DateSeance = dateSeance;
            this.HeureOrganisation = heureOrganisation;
            this.NbPlaceDispo = nbPlaceDispo;
            this.NomActivite = nomActivite;
            this.TypeActivite = typeActivite;
        }

        public SeanceClasse(DateOnly dateSeance, string nomActivite)
        {
            this.dateSeance = dateSeance;
            this.nomActivite = nomActivite;
        }


    }
}
