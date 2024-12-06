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
using static System.Runtime.InteropServices.JavaScript.JSType;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages.Seance
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    //L'index de la seance à modifier


    public sealed partial class SeanceU : Page
    {
        SeanceClasse seanceModif;


        public SeanceU()
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
            timepicker.SelectedTime = new TimeSpan(8, 0, 0);
        }

        //Reçoit l'activité lorsque la page se load
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                //Va chercher l'activité à modifier
                seanceModif = (SeanceClasse)e.Parameter;

                //Assignation des attributs de l'activité dans les champs approprié
                cmb_activite.SelectedValue = seanceModif.NomActivite + "/" + seanceModif.TypeActivite;
                tbx_place.Text = seanceModif.NbPlaceDispo.ToString();

                calendardatepicker.MinDate = new DateTime(seanceModif.DateSeance.Year, seanceModif.DateSeance.Month, seanceModif.DateSeance.Day);
                calendardatepicker.Date = new DateTime(seanceModif.DateSeance.Year, seanceModif.DateSeance.Month, seanceModif.DateSeance.Day);
                timepicker.SelectedTime = new TimeSpan(seanceModif.HeureOrganisation.Hour, seanceModif.HeureOrganisation.Minute, seanceModif.HeureOrganisation.Second);
            }
        }


        private void btn_modif_Click(object sender, RoutedEventArgs e)
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


            //La modification se fait uniquement SI les formulaires est valide
            if (estValide)
            {

                string nomActivite;
                string nomCategorie;

                //Sépare les deux clés primaires du combobox pour faciliter l'ajout dans la BD.
                string[] tab = activite.Split('/');
                nomActivite = tab[0];
                nomCategorie = tab[1];


                //Ajout de la séance dans la BD avec l'aide d'un singleton
                SingletonSeance.getInstance().modifierSeance(seanceModif, date, heure, Int32.Parse(nbPlace), nomActivite, nomCategorie);


                //Redirige à la page précédente
                this.Frame.GoBack();
            }

        }


        private void resetChamps()
        {
            tbl_erreur_activite.Text = "";
            tbl_erreur_date.Text = "";
            tbl_erreur_heure.Text = "";
            tbl_erreur_place.Text = "";
        }




    }
}
