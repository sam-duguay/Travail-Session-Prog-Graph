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


            //***EXEMPLE DE LISTVIEW***
            ObservableCollection<ActiviteClasse> liste = new ObservableCollection<ActiviteClasse>();

            liste.Add(new ActiviteClasse("test", "escalade", 50, 100));
            liste.Add(new ActiviteClasse("test1", "gym", 50, 100));
            liste.Add(new ActiviteClasse("test2", "yoga", 50, 100));
            liste.Add(new ActiviteClasse("test", "escalade", 50, 100));
            liste.Add(new ActiviteClasse("test1", "gym", 50, 100));
            liste.Add(new ActiviteClasse("test2", "yoga", 50, 100));
            liste.Add(new ActiviteClasse("test", "escalade", 50, 100));
            liste.Add(new ActiviteClasse("test1", "gym", 50, 100));
            liste.Add(new ActiviteClasse("test2", "yoga", 50, 100));

            lv_activite.ItemsSource = liste;


        }
    }
}
