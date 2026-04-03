using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.presentation
{
    public class MenuPrincipal : Menu
    {
        string finMenu;
        string optionQuitter;
        MenuAjouter optionAjouter;
        MenuAfficher optionAfficher;
        MenuModifier optionModifier;
        MenuSupprimer optionSupprimer;

        public MenuPrincipal()
        {
            message = "--------- Menu --------- ";
            finMenu = "------------------------ "; 
            numero = 5;
            optionQuitter = "Quitter";
            optionAjouter = new MenuAjouter();
            optionAfficher = new MenuAfficher();
            optionModifier = new MenuModifier();
            optionSupprimer = new MenuSupprimer();
        }
        
    }
}
