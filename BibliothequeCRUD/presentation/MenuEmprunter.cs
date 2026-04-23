using BibliothequeCRUD.business;
using BibliothequeCRUD.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.presentation
{
    internal class MenuEmprunter : Menu
    {
        MenuAfficher menuAfficher;

        public MenuEmprunter(AssistanceUtilisateur assistanceUtilisateur, GestionnaireLivres gestionnaireLivres, MenuAfficher menuAfficher) : base(assistanceUtilisateur, gestionnaireLivres)
        {
            numero = 5;
            message = "Emprunter un livre"; 
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

        public void AfficherLivreTrouve(Livre livreTrouve)
        {
            Console.WriteLine();
            //Console.WriteLine("Livre n° : " + livreTrouve.id);
            Console.WriteLine("Titre : " + livreTrouve.titre);
            Console.WriteLine("Auteur : " + livreTrouve.auteur);
            Console.WriteLine();
        }

        public void EmprunterLivre()
        {
            string reponseEmprunterLivre = ""; 

            menuAfficher.AfficherLivres();

            // Vérifier que la bibliothèque possède au moins un livre avant d'emprunter un livre

            bool livreExiste = gestionnaireLivres.LivreExiste();

            if (!livreExiste)
            {
                return;
            }
            else
            {
                while (true)
                {
                    int idLivre = assistanceUtilisateur.DemanderIdLivre("Saisir l'identifiant du livre à emprunter : ");

                    // Vérifier l'existance du livre 

                    Livre livreARechercher = gestionnaireLivres.RechercherLivre(idLivre);

                    // Si le livre trouvé -> afficher le livre, sinon -> afficher message erreur livre introuvable

                    if (livreARechercher != null)
                    {
                        AfficherLivreTrouve(livreARechercher);
                    }
                    else // cas où on a pas trouvé le livre 
                    {
                        Console.WriteLine();
                        assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Erreur : livre introuvable ", ConsoleColor.Red);
                        Console.WriteLine(); 
                    }
                }



            }
        }

    }
}
