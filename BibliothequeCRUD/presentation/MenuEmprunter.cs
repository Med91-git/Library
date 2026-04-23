using BibliothequeCRUD.business;
using BibliothequeCRUD.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.presentation
{
    internal class MenuEmprunter : Menu
    {
        public MenuEmprunter(AssistanceUtilisateur assistanceUtilisateur, GestionnaireLivres gestionnaireLivres) : base(assistanceUtilisateur, gestionnaireLivres)
        {
            numero = 5;
            message = "Emprunter un livre"; 
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

    }
}
