using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession.Classes
{
    internal class ParticipationClasse
    {
        //Les variables
        int idSeance;
        string idAdherent;
        DateOnly dateSeance;
        DateTime heureOrganisation;
        int nbPlaceDispo;
        string nomActivite;
        string typeActivite;
        int note;

        public int IdSeance { get => idSeance; set => idSeance = value; }
        public string IdAdherent { get => idAdherent; set => idAdherent = value; }
        public DateOnly DateSeance { get => dateSeance; set => dateSeance = value; }
        public DateTime HeureOrganisation { get => heureOrganisation; set => heureOrganisation = value; }
        public int NbPlaceDispo { get => nbPlaceDispo; set => nbPlaceDispo = value; }
        public string NomActivite { get => nomActivite; set => nomActivite = value; }
        public string TypeActivite { get => typeActivite; set => typeActivite = value; }
        public int Note { get => note; set => note = value; }


        //Propriété qui retourne l'heure formattée
        public string HeureFormatte
        {
            get => HeureOrganisation.ToString("HH:mm");
        }



        public ParticipationClasse(int idSeance, string idAdherent, DateOnly dateSeance, DateTime heureOrganisation, int nbPlaceDispo, string nomActivite, string typeActivite, int note)
        {
            this.IdSeance = idSeance;
            this.IdAdherent = idAdherent;
            this.DateSeance = dateSeance;
            this.HeureOrganisation = heureOrganisation;
            this.NbPlaceDispo = nbPlaceDispo;
            this.NomActivite = nomActivite;
            this.TypeActivite = typeActivite;
            this.Note = note;
        }


    }
}
