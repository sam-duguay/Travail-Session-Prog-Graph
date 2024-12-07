using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using TravailSession.Classes;
using TravailSession.Pages;
using TravailSession.Pages.Activite;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages.Accueil
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccueilZoomSeance : Page
    {
        public AccueilZoomSeance()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                //Rechercher l'Activité dans la BD

                //Va chercher l'activité sélectionnée dans le SingletonActivite
                ActiviteClasse activite = SingletonAccueil.getInstance().getActivite((int)e.Parameter);

                SingletonAccueil.getInstance().assignerActiviteChoissi(activite);

                //Remplissage de la listview

                tbl_activite.Text = activite.Nom;

                SingletonAccueil.getInstance().getSeances(activite);

                lv_seance.ItemsSource = SingletonAccueil.getInstance().ListeSeance;
            }
        }

        private void btn_inscription_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SeanceClasse seance = button.DataContext as SeanceClasse;


            SingletonAccueil.getInstance().ajouterParticipation(1, sessionAdherent._Adherent.IdAdherent.ToString(), seance.Id);

            SingletonAccueil.getInstance().getSeances(SingletonAccueil.getInstance().getactiviteChoissi());
            SingletonAccueil.getInstance().getListeSeance();
            
        }
    }
}
