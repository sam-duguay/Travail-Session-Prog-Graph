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
    public sealed partial class ActiviteU : Page
    {
        public ActiviteU()
        {
            this.InitializeComponent();
        }

        //Reçoit l'activité lorsque la page se load
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is not null)
            {
                ActiviteClasse activite = (ActiviteClasse)e.Parameter;

                //Assignation des attributs de l'activité dans les champs approprié.

                tbx_nom.Text = activite.Nom;
                tbx_type.Text = activite.Type;
                tbx_cout.Text = activite.CoutOrganisationClient.ToString();
                tbx_prix.Text = activite.PrixVenteClient.ToString();
            }
        }
    }
}
