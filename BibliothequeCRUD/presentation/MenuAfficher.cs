using BibliothequeCRUD.business;
using BibliothequeCRUD.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.presentation
{
    internal class MenuAfficher : Menu
    {
        
        public MenuAfficher(AssistanceUtilisateur assistanceUtilisateur, GestionnaireLivres gestionnaireLivres) : base(assistanceUtilisateur, gestionnaireLivres)
        {
            numero = 2;
            message = "Afficher les livres"; 
            
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

        public void AfficherLivres()
        {
            if (gestionnaireLivres.bibliotheque.Count > 0)
            {
                foreach (Livre livre in gestionnaireLivres.bibliotheque) 
                {
                    Console.WriteLine("Livre n° " + livre.id);
                    Console.WriteLine("Titre : " + livre.titre);
                    Console.WriteLine("Auteur : " + livre.auteur);
                    Console.WriteLine(); 
                } 
            }
            else
            {
                Console.WriteLine("Aucun livre existant dans la bibliothèque.");
                Console.WriteLine(); 
            }
        }

        public void MettreAJourBibliotheque()
        {
            Console.WriteLine();
            Console.WriteLine("Mise à jour de la bibliothèque : ");
            Console.WriteLine();
            AfficherLivres(); 
        }


    }
}
