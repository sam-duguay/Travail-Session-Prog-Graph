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
        }


        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
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
                default:
                    break;
            }
        }
    }
}
