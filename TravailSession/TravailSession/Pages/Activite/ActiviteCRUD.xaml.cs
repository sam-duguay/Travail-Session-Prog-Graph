using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
//Pour acc�der au dossier classes
using TravailSession.Classes;
using System.Collections.ObjectModel;
using TravailSession.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages.Activite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActiviteCRUD : Page
    {
        public ActiviteCRUD()
        {
            this.InitializeComponent();


            //***EXEMPLE DE LISTVIEW pour voir l'affichage***
            ObservableCollection<ActiviteClasse> liste = new ObservableCollection<ActiviteClasse>();

            liste.Add(new ActiviteClasse("test", "escalade", 50, 100));
            liste.Add(new ActiviteClasse("test1", "gym", 50, 100));
            liste.Add(new ActiviteClasse("test2", "yoga", 50, 100));
            liste.Add(new ActiviteClasse("test", "escalade", 50, 100));
            liste.Add(new ActiviteClasse("test1", "gym", 50, 100));
            liste.Add(new ActiviteClasse("test2", "yoga", 50, 100));
            liste.Add(new ActiviteClasse("test", "escalade", 50, 100));
            liste.Add(new ActiviteClasse("test1", "gym", 50, 100));
            liste.Add(new ActiviteClasse("test2", "yoga", 50, 100));

            lv_activite.ItemsSource = liste;
        }


        //Navigue vers la page d'ajout
        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ActiviteC));
        }


        //Fait une recherche parmis les �l�ments de la listview
        private void btn_recherche_Click(object sender, RoutedEventArgs e)
        {

        }


        //Am�ne l'utilisateur au formulaire de modification de l'activit�
        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ActiviteClasse activite = button.DataContext as ActiviteClasse;

            //Envoie l'objet de la liste � la page suivante
            this.Frame.Navigate(typeof(ActiviteU), activite);
        }

        //Supprime un �l�ment de la listview
        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ActiviteClasse activite = button.DataContext as ActiviteClasse;

            //TODO:Faire le reste pour supprimer
        }
    }
}
