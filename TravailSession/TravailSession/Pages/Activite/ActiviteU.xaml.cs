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
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages.Activite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActiviteU : Page
    {

        //L'index de l'objet � modifier
        ActiviteClasse activiteModif = new ActiviteClasse("", "", 0, 0);

        public ActiviteU()
        {
            this.InitializeComponent();
            //Initialiser le combobox avec les cat�gories de la BD
            cmb_type.Items.Clear();


            foreach (string item in SingletonActivite.getInstance().getCategories())
            {
                cmb_type.Items.Add(item);
            }

            cmb_type.SelectedIndex = 0;
        }

        //Re�oit l'activit� lorsque la page se load
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is not null)
            {
                //Va chercher l'activit� � modifier
                activiteModif = (ActiviteClasse)e.Parameter;

                //Assignation des attributs de l'activit� dans les champs appropri�
                tbx_nom.Text = activiteModif.Nom;
                cmb_type.SelectedValue = activiteModif.Type;
                tbx_cout.Text = activiteModif.CoutOrganisationClient.ToString();
                tbx_prix.Text = activiteModif.PrixVenteClient.ToString();


                Debug.WriteLine(activiteModif.ToString());
            }
        }


        private void btn_modif_Click(object sender, RoutedEventArgs e)
        {
            string nom = tbx_nom.Text;
            string type = cmb_type.SelectedItem.ToString();
            string cout = tbx_cout.Text;
            string prix = tbx_prix.Text;

            double nombre1;
            double nombre2;

            bool estValide = true;

            //Reset des erreurs � chaque envoie.
            resetChamps();


            //Validation avant l'ajout d'une activit�
            if (string.IsNullOrWhiteSpace(nom))
            {
                tbl_erreur_nom.Text = "Un nom d'activit� est requis.";
                estValide = false;
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                tbl_erreur_type.Text = "Un type d'activit� est requis.";
                estValide = false;
            }

            if (!Double.TryParse(cout, out nombre1))
            {
                tbl_erreur_cout.Text = "Veuillez entrer un co�t valide.";

                estValide = false;
            }
            else
            {
                if (nombre1 < 0)
                {
                    tbl_erreur_cout.Text = "Le co�t ne peut �tre inf�rieur � 0.";
                    estValide = false;
                }
            }

            if (!Double.TryParse(prix, out nombre2))
            {
                tbl_erreur_prix.Text = "Veuillez entrer un prix valide.";
                estValide = false;
            }
            else
            {
                if (nombre2 < 0)
                {
                    tbl_erreur_prix.Text = "Le prix ne peut �tre inf�rieur � 0.";
                    estValide = false;
                }
            }


            //La modification se fait uniquement SI les formulaires est valide
            if (estValide)
            {

                if (Double.Parse(cout) > Double.Parse(prix))
                {
                    tbl_rentable.Text = "Attention! L'activit� est a �t� modifi�e, mais celle-ci ne sera pas rentable.";
                }


                SingletonActivite.getInstance().modifierActivite(activiteModif, nom, type, Double.Parse(cout), Double.Parse(prix));

                //Redirige � la page pr�c�dente
                this.Frame.GoBack();
            }

        }


        private void resetChamps()
        {
            tbl_erreur_nom.Text = "";
            tbl_erreur_type.Text = "";
            tbl_erreur_cout.Text = "";
            tbl_erreur_prix.Text = "";
            tbl_rentable.Text = "";
        }
    }
}
