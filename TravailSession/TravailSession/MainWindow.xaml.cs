using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
//Liens vers les dossier pour les pages
using TravailSession.Pages;
using TravailSession.Pages.Activite;
using TravailSession.Pages.Adherent;
using TravailSession.Pages.Seance;
using TravailSession.Classes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            mainFrame.Navigate(typeof(AccueilActivite));


            //Initialisations des singlestons

            iActiviteCRUD.Visibility = Visibility.Collapsed;
            iAdherentCRUD.Visibility = Visibility.Collapsed;
            iSeanceCRUD.Visibility = Visibility.Collapsed;
            iStatistique.Visibility = Visibility.Collapsed;
            imenu.Visibility = Visibility.Collapsed;
            iDeconnexion.Visibility = Visibility.Collapsed;



            //Initialisation des singleton
            SingletonActivite.getInstance().getListe();
            SingletonSeance.getInstance().getListe();


            //Ajoute les activités au singleton
            SingletonActivite.getInstance().getActivites();
            SingletonSeance.getInstance().getSeances();

        }

   
        


        private async void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;

            switch (item.Name)
            {
                //Page que l'utilisateur voit par défaut
                case "iActivite":
                    mainFrame.Navigate(typeof(AccueilActivite));
                    break;

                //Page pour l'administrateur
                case "iActiviteCRUD":
                    mainFrame.Navigate(typeof(ActiviteCRUD));
                    break;
                case "iAdherentCRUD":
                    mainFrame.Navigate(typeof(AdherentCRUD));
                    break;
                case "iSeanceCRUD":
                    mainFrame.Navigate(typeof(SeanceCRUD));
                    break;
                case "iStatistique":
                    mainFrame.Navigate(typeof(Statistique));
                    break;

                case "iLoginAdmin":
                    connexionAdmin();
                    mainFrame.Navigate(typeof(AccueilActivite));
                    navView.SelectedItem = iActivite; 
                    break;
                case "iLoginAdherent":
                    connexionAdherent();
                    mainFrame.Navigate(typeof(AccueilActivite));
                    navView.SelectedItem = iActivite;
                    break;

                default:


                    break;
            }
        }

        public async void connexionAdmin()
        {
            ConnexionAdmin dialog = new ConnexionAdmin("admin");
            dialog.XamlRoot = mainFrame.XamlRoot;
            dialog.Title = "Authentification Admin";
            dialog.PrimaryButtonText = "Se connecter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialog.ShowAsync();
            //gestion visibilité
            if (resultat.ToString() == "Primary")
            {
                iActiviteCRUD.Visibility = Visibility.Visible;
                iAdherentCRUD.Visibility = Visibility.Visible;
                iSeanceCRUD.Visibility = Visibility.Visible;
                iStatistique.Visibility = Visibility.Visible;
                imenu.Visibility = Visibility.Visible;
                iDeconnexion.Visibility = Visibility.Visible;
                iLoginAdmin.Visibility = Visibility.Collapsed;
                iLoginAdherent.Visibility = Visibility.Collapsed;
                tbl_etat.Text = "Admin connecté";
            }
            
               
            

        }

        public async void connexionAdherent()
        {
            ConnexionAdmin dialog = new ConnexionAdmin("adherent");
            dialog.XamlRoot = mainFrame.XamlRoot;
            dialog.Title = "Authentification Adhérent";
            dialog.PrimaryButtonText = "Se connecter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;



            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat.ToString() == "Primary")
            {
                iActiviteCRUD.Visibility = Visibility.Collapsed;
                iAdherentCRUD.Visibility = Visibility.Collapsed;
                iSeanceCRUD.Visibility = Visibility.Collapsed;
                iStatistique.Visibility = Visibility.Collapsed;
                imenu.Visibility = Visibility.Collapsed;
                iDeconnexion.Visibility = Visibility.Visible;
                iLoginAdmin.Visibility = Visibility.Collapsed;
                iLoginAdherent.Visibility = Visibility.Collapsed;
            }
        }




    }
}
