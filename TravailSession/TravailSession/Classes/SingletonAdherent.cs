using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravailSession.Classes;

namespace TravailSession.Classes
{
    class SingletonAdherent
    {
        ObservableCollection<AdherentClasse> liste;
        static SingletonAdherent instance = null;

        // produit pour retourner la liste 
        internal ObservableCollection<AdherentClasse> Liste { get { return liste; } }

        public SingletonAdherent()
        {
            liste = new ObservableCollection<AdherentClasse>();
        }

        public static SingletonAdherent getInstance() 
        { 
        
            if (instance == null) 
            {
                instance = new SingletonAdherent();
            }
            return instance; 
        }

        public ObservableCollection<AdherentClasse> getListe() { return liste; }

        public void getAdherent() { liste.Clear(); }


        public void ajouterAdherent(AdherentClasse adherent)
        {
            liste.Add(adherent);
        }



        // aller chercher un adhérent dans la liste.

        public AdherentClasse getAdherent(int position) 
        {
            return liste[position];
        }

        public void modifierAdherent(int position, AdherentClasse adherent)
        {
            liste[position] = adherent;
        }
        public void supprimerAdherent(int position)
        {
            liste.RemoveAt(position);
        }
    }
    
}
