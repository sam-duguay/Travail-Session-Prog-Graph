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
using TravailSession.Classes;
using TravailSession.Pages.Activite;
using TravailSession.Pages.Accueil;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccueilActivite : Page
    {
        public AccueilActivite()
        {
            this.InitializeComponent();

            //Initialise le singleton pour les activités
            SingletonAccueil.getInstance().getActivites();
            SingletonAccueil.getInstance().getListe();

            lv_activite.ItemsSource = SingletonAccueil.getInstance().ListeActivite;


            //Affiche le lien vers les inscriptions si un adhérent est connecté
            if (SingletonAccueil.getInstance().getTypeUtilisateur().Equals("Adhérent"))
            {
                stkpnl_inscription.Visibility = Visibility.Visible;
            }

        }


        //***Exécuter cette fonction seulement si c'est un adhérent qui est connecté
        private void lv_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //Regarde si l'utilisateur est connecté
            if (SingletonAccueil.getInstance().getTypeUtilisateur().Equals("Adhérent"))
            {
                int index = lv_activite.SelectedIndex;

                //Envoie l'index de l'objet à modifier
                this.Frame.Navigate(typeof(AccueilZoomSeance), index);
            }

        }

        private void btn_inscription_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AccueilZoomParticipation));
        }
    }
}
