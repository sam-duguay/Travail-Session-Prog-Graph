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
using System.Collections.ObjectModel;
using TravailSession.Classes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages.Adherent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdherentCRUD : Page
    {
        public AdherentCRUD()
        {
            this.InitializeComponent();
            ObservableCollection<AdherentClasse> liste = new ObservableCollection<AdherentClasse>();

            SingletonAdherent.getInstance().getAdherent();
            SingletonAdherent.getInstance().getListe();
           

        lv_adherent.ItemsSource = SingletonAdherent.getInstance().Liste;

        }
            

        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            AdherentClasse adherentClasse = btn.DataContext as AdherentClasse;

            int index =SingletonAdherent.getInstance().getListe().IndexOf(adherentClasse);

            //SingletonAdherent.getInstance().supprimerAdherent(index);
        }

        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            AdherentClasse adherentClasse = btn.DataContext as AdherentClasse;

            int index = SingletonAdherent.getInstance().getListe().IndexOf(adherentClasse);

            this.Frame.Navigate(typeof(AdherentU),index);
        }

        // naviguer dans la page ajout 
        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdherentC));
        }
    }
}
