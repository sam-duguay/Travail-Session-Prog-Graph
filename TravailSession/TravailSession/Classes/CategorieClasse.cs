using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailSession.Classes
{
    internal class CategorieClasse
    {
        //Variable
        string nom;

        //Propriété
        public string Nom { get => nom; set => nom = value; }


        //Constructeur
        public CategorieClasse(string nom)
        {
            this.Nom = nom;
        }


    }
}
