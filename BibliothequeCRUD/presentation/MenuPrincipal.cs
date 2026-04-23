using BibliothequeCRUD.business;
using BibliothequeCRUD.utils;
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
        MenuEmprunter optionEmprunter;

        public MenuPrincipal(AssistanceUtilisateur assistanceUtilisateur, GestionnaireLivres gestionnaireLivres) : base(assistanceUtilisateur, gestionnaireLivres)
        {
            message = "--------- Menu --------- ";
            finMenu = "------------------------ "; 
            numero = 6; 
            optionQuitter = "Quitter";
            optionAjouter = new MenuAjouter(assistanceUtilisateur, gestionnaireLivres);
            optionAfficher = new MenuAfficher(assistanceUtilisateur, gestionnaireLivres);
            optionModifier = new MenuModifier(assistanceUtilisateur, gestionnaireLivres, optionAfficher);
            optionSupprimer = new MenuSupprimer(assistanceUtilisateur, gestionnaireLivres, optionAfficher);
            optionEmprunter = new MenuEmprunter(assistanceUtilisateur, gestionnaireLivres, optionAfficher);
        }
        
        public override void Afficher()   
        {
            Console.WriteLine(message);
            Console.WriteLine();

            // Afficher les options du menu 

            optionAjouter.Afficher();
            optionAfficher.Afficher();
            optionModifier.Afficher();
            optionSupprimer.Afficher();
            optionEmprunter.Afficher();

            // Afficher le numéro du menu principal en couleur 

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(numero);

            // Afficher l'option du menu principal avec la couleur de la console par défaut

            Console.ResetColor();
            Console.WriteLine(". " + optionQuitter);
            Console.WriteLine();
            Console.WriteLine(finMenu); 
            Console.WriteLine();
            
        }

        public void Naviguer()
        {
            int numOption = 0;
            int optionQuitterProgramme = 6;

            while (numOption != optionQuitterProgramme)
            {
                Afficher();
                numOption = assistanceUtilisateur.DemanderOptionMenu("Faites votre choix : ", 1, optionQuitterProgramme);

                if (numOption == 1)
                {
                    Console.Clear();
                    optionAjouter.AjouterLivre();
                    assistanceUtilisateur.RevenirAuMenuPrincipal();
                }
                else if (numOption == 2)
                {
                    Console.Clear();
                    optionAfficher.AfficherLivres();
                    assistanceUtilisateur.RevenirAuMenuPrincipal();
                }
                else if (numOption == 3)
                {
                    Console.Clear();
                    optionModifier.ModifierLivre();
                    assistanceUtilisateur.RevenirAuMenuPrincipal();
                }
                else if (numOption == 4)
                {
                    Console.Clear();
                    optionSupprimer.SupprimerLivre();
                    assistanceUtilisateur.RevenirAuMenuPrincipal();
                }
                else if (numOption == 5)
                {
                    Console.Clear();
                    optionEmprunter.EmprunterLivre(); 
                    assistanceUtilisateur.RevenirAuMenuPrincipal();
                }
                else if (numOption == optionQuitterProgramme)
                {
                    return; 
                }
            }

        }

        
    }
}
