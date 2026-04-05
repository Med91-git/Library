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
                // Demander à l'utilisateur les informations nécessaires pour la création d'un livre 

                Console.WriteLine();
                string titre = assistanceUtilisateur.DemanderInformationLivre("Saisir le titre : ");
                string auteur = assistanceUtilisateur.DemanderInformationLivre("Saisir l'auteur : ");

                // Ajouter le livre dans la bibliothèque 

                gestionnaireLivres.AjouterLivre(titre, auteur);

                // Confirmer ajout du livre à l'utilisateur 

                Console.WriteLine();
                assistanceUtilisateur.AfficherMessageConfirmationCRUD("Livre ajouté.", ConsoleColor.Green);
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
                    return; 
                }

            }

            





        }


    }
}
