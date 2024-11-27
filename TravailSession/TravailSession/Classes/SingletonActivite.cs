using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravailSession.Classes;

namespace TravailSession.Classes
{
    internal class SingletonActivite
    {
        ObservableCollection<ActiviteClasse> liste;
        static SingletonActivite instance = null;


        //Propriété qui retourne la liste
        internal ObservableCollection<ActiviteClasse> Liste { get { return liste; } }

        public SingletonActivite()
        {
            liste = new ObservableCollection<ActiviteClasse>();
        }

        public static SingletonActivite getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonActivite();
            }

            return instance;
        }


        public ObservableCollection<ActiviteClasse> getListe()
        {
            return liste;
        }

        //TODO: va aller chercher les activités dans la BD
        public void getActivites()
        {
            liste.Clear();
        }

        

        //Va chercher une activité à une position X
        public ActiviteClasse getActivite(int position) 
        {
            return liste[position];
        }



        public void ajouterActivite(ActiviteClasse activite)
        {
            liste.Add(activite);
        }


        public void modifierActivite(int position, ActiviteClasse activite)
        {
            liste[position] = activite;
        }


        public void supprimerActivite(int position)
        {
            liste.RemoveAt(position);
        }

    }
}
