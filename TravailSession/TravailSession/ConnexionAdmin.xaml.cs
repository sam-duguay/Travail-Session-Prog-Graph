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
using TravailSession.Pages.Activite;
using TravailSession.Pages.Adherent;
using TravailSession.Pages.Seance;
using TravailSession.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using TravailSession.Classes;
using Windows.Media.Casting;
using MySql.Data.MySqlClient;
using WinRT;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailSession
{
    public sealed partial class ConnexionAdmin : ContentDialog
    {
        MySqlConnection _connection = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq3; Uid=1073274;Pwd=1073274;");
        Boolean admin;
        ConnexionClasse connexion = new ConnexionClasse(false);

 
        
        

        public ConnexionAdmin(string statut)
        {
            this.InitializeComponent();

          

           
           
            if (admin is true)
            {
                statut = "admin";
            }

            
            if (statut == "admin")
            {
                tbx_user.Header = "Nom Admin";
                admin = true;
            }
            else
            {
                tbx_user.Header = "ID Adhérent";
                pwd_user.Visibility = Visibility.Collapsed;
                admin = false;
            }
        }

     
        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)

        {

            tbl_validation_pwd.Text = "";
            tbl_validation_user.Text = "";
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


                    

                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = _connection;
                        cmd.CommandText = $"SELECT * FROM administrateur WHERE nomAdmin like '{tbx_user.Text}' and mdp like '{pwd_user.Password}';";
                        _connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            Console.WriteLine("passer ici pour go");
                            connexion = new ConnexionClasse(true, tbx_user.Text, pwd_user.Password);
                            args.Cancel = false;
                        }
                        else
                        {
                            reader.Close();
                            _connection.Close();

                            cmd.CommandText = $"SELECT * FROM administrateur WHERE nomAdmin like '{tbx_user.Text}';";
                            _connection.Open();

                             reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            reader.Close();
                            _connection.Close();

                            cmd.CommandText = $"SELECT * FROM administrateur WHERE nomAdmin like '{tbx_user.Text}' and mdp like '{pwd_user.Password}';";
                            _connection.Open();

                            reader = cmd.ExecuteReader();


                            if (reader.Read())
                            {
                                

                            }
                            else
                            {

                                if (string.IsNullOrEmpty(pwd_user.Password))
                                {
                                    args.Cancel = true;
                                    tbl_validation_pwd.Text = "Le mot de passe est vide";
                                }
                                else
                                {
                                    args.Cancel = true;
                                    tbl_validation_pwd.Text = "Le mots de passe n'est pas valide";
                                }

                                
                            }




                        }
                        else
                        {
                        args.Cancel = true;
                            tbl_validation_user.Text = "L'identifiant n'est pas valide";
                        }



                        
                        }
                        reader.Close();
                        _connection.Close();



                }

                   else if (admin != true)
                    {
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

                        MySqlCommand cmd2 = new MySqlCommand();
                        cmd2.Connection = _connection;
                        cmd2.CommandText = $"SELECT * FROM adherent WHERE idAdherent like '{tbx_user.Text}';";
                        _connection.Open();
                        MySqlDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.Read())
                    {
                      
                        sessionAdherent.cree(new AdherentClasse(reader2["nomAdherent"].ToString(),
                                                                    reader2["prenomAdherent"].ToString(),
                                                                    reader2["adresse"].ToString(),
                                                                    DateOnly.FromDateTime(reader2.GetDateTime("dateNais")),
                                                                    (int)reader2["age"]
                                                                    ));
                        sessionAdherent._Adherent.IdAdherent = reader2["idAdherent"].ToString();
                            args.Cancel = false;
                        }
                        else
                        {
                            args.Cancel = true;
                            tbl_validation_user.Text = "L'Id n'existe pas. ";
                        }
                        reader2.Close();
                        _connection.Close();


                    






                }
                else 
                {
                    args.Cancel = false;
                }











            }


                

        }
    }
}
