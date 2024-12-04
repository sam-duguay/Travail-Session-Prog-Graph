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

namespace TravailSession.Pages.Activite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActiviteC : Page
    {
        public ActiviteC()
        {
            this.InitializeComponent();

            //Initialiser le combobox avec les catégories de la BD
            cmb_type.Items.Clear();


            foreach(string item in SingletonActivite.getInstance().getCategories())
            {
                cmb_type.Items.Add(item);
            }

            cmb_type.SelectedIndex = 0;
        }

        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            string nom = tbx_nom.Text;
            string type = cmb_type.SelectedItem as string;
            string cout = tbx_cout.Text;
            string prix = tbx_prix.Text;

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

            if (string.IsNullOrWhiteSpace(type))
            {
                tbl_erreur_type.Text = "Un type d'activité est requis.";
                estValide = false;
            }

            if (!Double.TryParse(cout, out nombre1))
            {
                tbl_erreur_cout.Text = "Veuillez entrer un coût valide.";

                estValide = false;
            }
            else
            {
                if (nombre1 < 0)
                {
                    tbl_erreur_cout.Text = "Le coût ne peut être inférieur à 0.";
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
                    tbl_erreur_prix.Text = "Le prix ne peut être inférieur à 0.";
                    estValide = false;
                }
            }


            //L'ajout se fait uniquement SI les formulaires est valide
            if (estValide)
            {

                tbl_rentable.Text = "L'activité a été ajoutée!";

                if (Double.Parse(cout) > Double.Parse(prix))
                {
                    tbl_rentable.Text = "Attention! L'activité est a été ajoutée, mais celle-ci ne sera pas rentable.";
                }

                //Ajout de l'activité dans la BD avec l'aide d'un singleton
                SingletonActivite.getInstance().ajouterActivite(nom, type, Double.Parse(cout), Double.Parse(prix));

                //Vide les champs pour préparer l'ajout d'une nouvelle activité
                tbx_nom.Text = "";
                cmb_type.SelectedIndex = 0;
                tbx_cout.Text = "";
                tbx_prix.Text = "";


            }

        }


        private void resetChamps()
        {
            tbl_erreur_nom.Text = "";
            tbl_erreur_type.Text = "";
            tbl_erreur_cout.Text = "";
            tbl_erreur_prix.Text = "";
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            //Redirige à la page précédente
            this.Frame.GoBack();
        }
    }
}
