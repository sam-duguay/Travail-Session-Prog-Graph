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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages.Adherent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdherentC : Page
    {
        public AdherentC()
        {
            this.InitializeComponent();
            dp_naissance.MaxYear = DateTimeOffset.Now.AddYears(-18);

        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            //Redirige à la page précédente
            this.Frame.GoBack();
        }

        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            string nom = tbx_nom.Text;
            string prenom = tbx_prenom.Text;
            string adresse = tbx_adresse.Text;
            string date_naiss = dp_naissance.Date.ToString("yyyy-MM-dd");

            double nombre1;
            double nombre2;

            bool estValide = true;

            //Reset des erreurs à chaque envoie.
            resetChamps();


            //Validation avant l'ajout d'une activité
            if (string.IsNullOrWhiteSpace(nom))
            {
                tbl_erreur_nom.Text = "Un nom d'activité est requis.";
                estValide = false;
            }

            if (string.IsNullOrWhiteSpace(prenom))
            {
                tbl_erreur_prenom.Text = "Un type d'activité est requis.";
                estValide = false;
            }

            if (string.IsNullOrWhiteSpace(adresse))
            {
                tbl_erreur_adresse.Text = "Veuillez entrer un adresse valide.";

                estValide = false;
            }

   




            //L'ajout se fait uniquement SI les formulaires est valide
            if (estValide)
            {
                

                //Ajout de l'activité dans la BD avec l'aide d'un singleton
                SingletonAdherent.getInstance().ajouterAdherent(nom, prenom, adresse, date_naiss);

                //Vide les champs pour préparer l'ajout d'une nouvelle activité
                tbx_nom.Text = "";
                tbx_prenom.Text = "";
                tbx_adresse.Text = "";
                dp_naissance = null;


            }

        }


        private void resetChamps()
        {
            tbx_nom.Text = "";
            tbx_prenom.Text = "";
            tbx_adresse.Text = "";
            dp_naissance = null;
        }
    }
}
