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
            string reponseConfirmation = "";
            string reponseProposition = "";

            menuAfficher.AfficherLivres();

            // Vérifier que la bibliothèque possède au moins un livre avant de rendre un livre

            bool livreExiste = gestionnaireLivres.LivreExiste();

            if (!livreExiste)
            {
                return;
            }
            else
            {
                while (livreExiste) 
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

                            // Demander confirmation de la remise du livre

                            while (true)
                            {
                                reponseConfirmation = assistanceUtilisateur.DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir rendre le livre n° " + livreARechercher.id + " ? (o/n) : ");

                                if (reponseConfirmation.ToLower() == "o")
                                {
                                    // Rendre le livre

                                    gestionnaireLivres.RendreLivre(livreARechercher);
                                    Console.WriteLine();

                                    // Afficher à l'utilisateur la confirmation de la remise du livre 

                                    assistanceUtilisateur.ConfirmerEmpruntLivre("Livre n° " + livreARechercher.id + " emprunté le " + livreARechercher.dateDebutEmprunt.ToString("dd MMMM yyyy") + " a été rendu à la bibliothèque le " + livreARechercher.dateRetourLivre.ToString("dd MMMM yyyy"), ConsoleColor.Green);
                                    Console.WriteLine();
                                    menuAfficher.MettreAJourBibliotheque();
                                    Console.WriteLine();
                                    break;
                                }
                                else if (reponseConfirmation.ToLower() == "n")
                                {
                                    Console.WriteLine();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Vous devez répondre 'o' pour oui ou 'n' pour non.", ConsoleColor.Yellow);
                                }
                                Console.WriteLine();
                            }

                            // Proposer de rendre un autre livre  

                            while (true)
                            {

                                reponseProposition = assistanceUtilisateur.DemanderChoixUtilisateurStr("Souhaitez-vous rendre un autre livre ? (o/n) : ");
                                reponseProposition = reponseProposition.ToLower();

                                if (reponseProposition == "o") // si utilisateur accepte -> permettre à l'utilisateur de choisir un livre à rendre
                                {
                                    Console.WriteLine();
                                    break;
                                }
                                else if (reponseProposition == "n") // si utilisateur refuse -> revenir au menu principal
                                {
                                    Console.WriteLine();
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Vous devez répondre 'o' pour oui ou 'n' pour non.", ConsoleColor.Yellow);
                                }
                                Console.WriteLine();
                            } 

                        }
                        else
                        {
                            Console.WriteLine();
                            assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Impossible de rendre le livre n° " + livreARechercher.id + " car il n'a pas été emprunté...", ConsoleColor.Red);
                            Console.WriteLine();
                        }
                        
                    }
                    else 
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
