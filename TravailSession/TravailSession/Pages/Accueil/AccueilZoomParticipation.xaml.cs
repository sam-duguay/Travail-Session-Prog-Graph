using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravailSession.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession.Pages.Accueil
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccueilZoomParticipation : Page
    {
        public AccueilZoomParticipation()
        {
            this.InitializeComponent();  
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //Remplissage de la listview

            SingletonAccueil.getInstance().getSeancesInscrit(sessionAdherent._Adherent.IdAdherent.ToString());

            lv_participation.ItemsSource = SingletonAccueil.getInstance().ListeParticipation;
        }

            private void btn_vote_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
