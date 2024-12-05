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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages.Seance
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SeanceCRUD : Page
    {
        public SeanceCRUD()
        {
            this.InitializeComponent();

            //Initialise le singleton
            SingletonSeance.getInstance().getSeances();
            SingletonSeance.getInstance().getListe();


            lv_seance.ItemsSource = SingletonSeance.getInstance().Liste;
        }


        //Navigue vers la page d'ajout
        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SeanceC));
        }


        //Amène l'utilisateur au formulaire de modification de l'activité
        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SeanceClasse seanceModif = button.DataContext as SeanceClasse;

            //Envoie l'index de l'objet à modifier
            this.Frame.Navigate(typeof(SeanceU), seanceModif);
        }

        //Supprime un élément de la listview
        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SeanceClasse seance = button.DataContext as SeanceClasse;


            SingletonSeance.getInstance().supprimerSeance(seance);
        }
    }
}
