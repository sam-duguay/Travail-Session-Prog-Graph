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
//Pour accèder au dossier classes
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


            //Initialise le singleton
            SingletonActivite.getInstance().getListe();

            lv_activite.ItemsSource = SingletonActivite.getInstance().Liste;
        }


        //Navigue vers la page d'ajout
        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ActiviteC));
        }


        //Fait une recherche parmis les éléments de la listview
        private void btn_recherche_Click(object sender, RoutedEventArgs e)
        {

        }


        //Amène l'utilisateur au formulaire de modification de l'activité
        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ActiviteClasse activiteModif = button.DataContext as ActiviteClasse;

            //Envoie l'index de l'objet à modifier
            this.Frame.Navigate(typeof(ActiviteU), activiteModif);
        }

        //Supprime un élément de la listview
        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ActiviteClasse activite = button.DataContext as ActiviteClasse;


            SingletonActivite.getInstance().supprimerActivite(activite);
        }
    }
}
