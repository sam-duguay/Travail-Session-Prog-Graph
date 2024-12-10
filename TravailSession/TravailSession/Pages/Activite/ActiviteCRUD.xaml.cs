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
using MySqlX.XDevAPI;
using System.Threading.Tasks;

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

        private async void btn_export_Click(object sender, RoutedEventArgs e)
        {

            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(Utilitaires.mainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "liste des activite";
            picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".csv" });

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            List<ActiviteClasse> liste = SingletonActivite.getInstance().Liste.ToList<ActiviteClasse>();


            // La fonction ToString de la classe Client retourne: nom + ";" + prenom
            if (monFichier != null)
            {
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x => x.RetourCsv), Windows.Storage.Streams.UnicodeEncoding.Utf8);
            }
            

        }
    }
}
