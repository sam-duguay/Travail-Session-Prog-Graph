using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession.Classes
{
    internal class sessionAdherent
    {

       static AdherentClasse  adherent;

        internal static AdherentClasse _Adherent { get => adherent; set => adherent = value; }

        public static void cree(AdherentClasse _adherent)
        {
            sessionAdherent.adherent = _adherent;
        }

        public static void deconnexion()
        {
            sessionAdherent.adherent = null;
        }
    }
}
