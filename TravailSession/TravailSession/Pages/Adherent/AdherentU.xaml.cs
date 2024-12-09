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
    public sealed partial class AdherentU : Page
    {
        AdherentClasse adherentModif;

        public AdherentU()
        {
            this.InitializeComponent();
            dp_naissance.Date = DateTime.Now.AddYears(-18);
            dp_naissance.MaxDate = DateTime.Now.AddYears(-18);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is not null) 
            { 
                adherentModif = (AdherentClasse)e.Parameter;
                tbx_nom.Text = adherentModif.NomAdherent;
                tbx_prenom.Text =adherentModif.PrenomAdherent;
                tbx_adresse.Text= adherentModif.Adresse;
                dp_naissance.Date = adherentModif.DateNais.Date;
           
            
            }

            base.OnNavigatedTo(e);
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            //Redirige à la page précédente
            this.Frame.GoBack();
        }

        private void btn_mod_Click(object sender, RoutedEventArgs e)
        {


            string nom = tbx_nom.Text;
            string prenom = tbx_prenom.Text;
            string adresse = tbx_adresse.Text;
            string date_naiss = dp_naissance.Date.Value.ToString("yyyy-MM-dd");




            bool estValide = true;


            resetChamps();


            //Validation avant l'ajout d'une activité
            if (string.IsNullOrWhiteSpace(nom))
            {
                tbl_erreur_nom.Text = "Un nom  est requis.";
                estValide = false;
            }

            if (string.IsNullOrWhiteSpace(prenom))
            {
                tbl_erreur_prenom.Text = "Un prenom est requis.";
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
                
                SingletonAdherent.getInstance().modifierAdherent(adherentModif,nom,prenom,adresse,date_naiss);

                //Vide les champs pour préparer l'ajout d'une nouvelle activité
                tbx_nom.Text = "";
                tbx_prenom.Text = "";
                tbx_adresse.Text = "";
                dp_naissance.Date = DateTime.Now.AddYears(-18);

                this.Frame.GoBack();
            }



        }


        private void resetChamps()
        {
            tbx_nom.Text = "";
            tbx_prenom.Text = "";
            tbx_adresse.Text = "";
            dp_naissance.Date = DateTime.Now.AddYears(-18);

            tbl_erreur_nom.Text = "";
            tbl_erreur_prenom.Text = "";
            tbl_erreur_adresse.Text = "";



        }
    }
}
