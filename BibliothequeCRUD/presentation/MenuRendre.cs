using BibliothequeCRUD.business;
using BibliothequeCRUD.utils;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BibliothequeCRUD.presentation
{
    internal class MenuRendre : Menu
    {
        MenuAfficher menuAfficher;

        public MenuRendre(AssistanceUtilisateur assistanceUtilisateur, GestionnaireLivres gestionnaireLivres, MenuAfficher menuAfficher) : base(assistanceUtilisateur, gestionnaireLivres)
        {
            numero = 6;
            message = "Rendre un livre";
            this.menuAfficher = menuAfficher;
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
