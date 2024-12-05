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
using static System.Runtime.InteropServices.JavaScript.JSType;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages.Seance
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SeanceC : Page
    {
        public SeanceC()
        {
            this.InitializeComponent();

            //Initialiser le combobox avec les activités de la BD
            cmb_activite.Items.Clear();


            foreach (string item in SingletonSeance.getInstance().getActivite())
            {
                cmb_activite.Items.Add(item);
            }

            cmb_activite.SelectedIndex = 0;


            //Initialiser le CalendarDatePicker et le TimePicker pour leur donner des valeurs par défauts
            calendardatepicker.Date = DateTime.Now;
            calendardatepicker.MinDate = DateTime.Now;
            timepicker.SelectedTime= new TimeSpan(8, 0, 0);
        }


        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            string activite = cmb_activite.SelectedItem as string;
            string nbPlace = tbx_place.Text;
            string date = calendardatepicker.Date.Value.ToString("yyyy-MM-dd");
            TimeSpan heure = (TimeSpan)timepicker.SelectedTime;

            int nombre;

            bool estValide = true;

            //Reset des erreurs à chaque envoie.
            resetChamps();



            //Validation avant l'ajout d'une séance
            if (string.IsNullOrWhiteSpace(activite))
            {
                tbl_erreur_activite.Text = "Une activité est requise.";
                estValide = false;
            }

            if (!Int32.TryParse(nbPlace, out nombre))
            {
                tbl_erreur_place.Text = "Veuillez entrer un nombre de places valide.";

                estValide = false;
            }
            else
            {
                if (nombre < 0)
                {
                    tbl_erreur_place.Text = "Le nombre de places ne peut être inférieur à 0.";
                    estValide = false;
                }
            }


            //L'ajout se fait uniquement SI les formulaires est valide
            if (estValide)
            {

                string nomActivite;
                string nomCategorie;

                //Sépare les deux clés primaires du combobox pour faciliter l'ajout dans la BD.
                string[] tab = activite.Split('/');
                nomActivite= tab[0];
                nomCategorie = tab[1];


                //Ajout de la séance dans la BD avec l'aide d'un singleton
                SingletonSeance.getInstance().ajouterSeance(date, heure, Int32.Parse(nbPlace), nomActivite, nomCategorie);
                tbl_succes.Text = "La séance a été ajoutée!";



                //Vide les champs pour préparer l'ajout d'une nouvelle activité
                cmb_activite.SelectedIndex = 0;
                tbx_place.Text = "";
                calendardatepicker.Date = DateTime.Now;
                timepicker.SelectedTime = new TimeSpan(0, 0, 0);
            }

        }


        private void resetChamps()
        {
            tbl_erreur_activite.Text = "";
            tbl_erreur_date.Text = "";
            tbl_erreur_heure.Text = "";
            tbl_erreur_place.Text = "";
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            //Redirige à la page précédente
            this.Frame.GoBack();
        }
    }
}
