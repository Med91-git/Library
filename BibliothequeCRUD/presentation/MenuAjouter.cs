using BibliothequeCRUD.business;
using BibliothequeCRUD.utils;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.presentation
{

    internal class MenuAjouter : Menu 
    {
        
        public MenuAjouter(AssistanceUtilisateur assistanceUtilisateur, GestionnaireLivres gestionnaireLivres) : base(assistanceUtilisateur, gestionnaireLivres) 
        {
            numero = 1;
            message = "Ajouter un livre";  
             
        }

        public override void Afficher()
        {
            // Afficher le numéro du menu en couleur 

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(numero);

            // Afficher l'option du menu avec la couleur de la console par défaut

            Console.ResetColor();
            Console.WriteLine(". " + message);
            
        }

        public void AjouterLivre()
        {
            string reponseAjoutLivre = ""; 

            while (true)
            {
                Console.WriteLine();
                string titre = assistanceUtilisateur.DemanderInformationLivre("Saisir le titre : ");

                bool doublonTitre = gestionnaireLivres.VerifierDoublonTitreLivre(titre);

                if (doublonTitre == true)
                {
                    Console.WriteLine();
                    assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur(titre + " existe déjà dans la bibliothèque", ConsoleColor.Red);
                }
                else
                {
                    string auteur = assistanceUtilisateur.DemanderInformationLivre("Saisir l'auteur : ");

                    // Ajouter le livre dans la bibliothèque et sauvegarde dans un fichier 

                    try
                    {
                        gestionnaireLivres.AjouterLivre(titre, auteur);

                        // Confirmer ajout du livre à l'utilisateur 

                        Console.WriteLine();
                        assistanceUtilisateur.AfficherMessageConfirmationCRUD("Livre ajouté.", ConsoleColor.Green);
                        Console.WriteLine();

                        // Confirmer sauvegarde du livre à l'utilisateur

                        assistanceUtilisateur.AfficherMessageConfirmationCRUD("Sauvegarde du fichier effectuée", ConsoleColor.Green);
                    }
                    catch (DirectoryNotFoundException exception)
                    {
                        Console.WriteLine();
                        assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Erreur, le chemin du fichier est incomplet !\nVérifier votre chemin : " + exception.Message, ConsoleColor.Red);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine();
                        assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Erreur, le livre n'a pas pu être sauvegardé ... : \n" + exception.Message, ConsoleColor.Red);
                    }
                    
                }
                Console.WriteLine();

                // Proposer d'ajouter un autre livre  

                reponseAjoutLivre = assistanceUtilisateur.DemanderChoixUtilisateurStr("Voulez-vous ajouter un autre livre ? (o/n) : ");
                Console.WriteLine();

                reponseAjoutLivre = reponseAjoutLivre.ToLower();

                while (reponseAjoutLivre != "o" && reponseAjoutLivre != "n")
                {
                    assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Vous devez répondre 'o' pour oui ou 'n' pour non.", ConsoleColor.Yellow);
                    Console.WriteLine();
                    reponseAjoutLivre = assistanceUtilisateur.DemanderChoixUtilisateurStr("Voulez-vous ajouter un autre livre ? (o/n) : ");
                }

                if (reponseAjoutLivre == "o")
                {
                    Console.Clear(); 
                }
                else if (reponseAjoutLivre == "n")
                {
                    Console.WriteLine();
                    return; 
                }
            }

        }

    }
}
