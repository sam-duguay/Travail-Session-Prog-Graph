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


            SingletonAdherent.getInstance().supprimerAdherent(adherentClasse);

        }

        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            AdherentClasse adherentClasse = btn.DataContext as AdherentClasse;

            this.Frame.Navigate(typeof(AdherentU),adherentClasse);
        }

        // naviguer dans la page ajout 
        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdherentC));
        }


        private async void btn_export_Click(object sender, RoutedEventArgs e)
        {

            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(Utilitaires.mainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "liste des adherent";
            picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".csv" });

            //cr�e le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            List<AdherentClasse> liste = SingletonAdherent.getInstance().Liste.ToList<AdherentClasse>();


            // La fonction ToString de la classe Client retourne: nom + ";" + prenom
            if (monFichier != null)
            {
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x => x.RetourCsv), Windows.Storage.Streams.UnicodeEncoding.Utf8);
            }


        }


    }
}
