using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession
{
    public sealed partial class ConnexionAdmin : ContentDialog
    {
        Boolean admin;

        public ConnexionAdmin(string statut)
        {
            if (admin is true)
            {
                statut = "admin";
            }

            this.InitializeComponent();
            if (statut == "admin")
            {
                admin = true;
            }
            else
            {
                pwd_user.Visibility = Visibility.Collapsed;
                admin = false;
            }
        }

     
        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            // avant la fermeture:
            if (args.Result == ContentDialogResult.Primary)
            {

                //verifier si le mot de passe est vide ou non 
                if (admin == true) 
                { 
                    if (string.IsNullOrWhiteSpace(pwd_user.Password))
                    {
                        args.Cancel = true;
                        tbl_validation_pwd.Text = "Le mot de passe est vide";

                    }
                    if (string.IsNullOrEmpty(pwd_user.Password))
                    {
                        args.Cancel = true;
                        tbl_validation_pwd.Text = "Le mot de passe est vide";
                    }

                }

                


                //prend la valeur du texte user
                string texte = tbx_user.Text;
                string expression = "^[a-zA-Z]{2}-[0-9]{4}-[0-9]{3}";
                
                //verifie sil fonction selon l'expression
                if (Regex.IsMatch(texte, expression))
                {
                    tbl_validation_user.Text = "le format est respecter";
                }
                else
                {
                    args.Cancel = true;
                    tbl_validation_user.Text = "le format n'est pas respecter";
                }
                //verifier sil est vide ou non.
                if (string.IsNullOrWhiteSpace(tbx_user.Text))
                {
                    args.Cancel = true;
                    tbl_validation_user.Text = "le format n'est pas respecter";

                }






            }
            else
            {
                args.Cancel = false;
            }

        }
    }
}
