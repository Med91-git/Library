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

        public void RendreLivre()
        {
            menuAfficher.AfficherLivres();

            // Vérifier que la bibliothèque possède au moins un livre avant de rendre un livre

            bool livreExiste = gestionnaireLivres.LivreExiste();

            if (!livreExiste)
            {
                return;
            }
            else
            {
                int idLivre = assistanceUtilisateur.DemanderIdLivre("Saisir l'identifiant du livre à rendre : ");

                // Vérifier l'existance du livre dans la bibliothèque 

                Livre livreARechercher = gestionnaireLivres.RechercherLivre(idLivre);

                if (livreARechercher != null)
                {
                    // Vérifier que le livre trouvé est déjà emprunté pour pouvoir le rendre

                    if (livreARechercher.estEmprunte == true)
                    {
                        menuAfficher.AfficherLivreTrouve(livreARechercher); 
                    }
                    else
                    {
                        assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Impossible de rendre le livre n° " + livreARechercher.id + " car il n'a pas été emprunté...", ConsoleColor.Red);
                        Console.WriteLine(); 
                    }

                }

            }

        }

    }
}
