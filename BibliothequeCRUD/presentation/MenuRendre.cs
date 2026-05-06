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
            string reponseRendreLivre = "";

            menuAfficher.AfficherLivres();

            // Vérifier que la bibliothèque possède au moins un livre avant de rendre un livre

            bool livreExiste = gestionnaireLivres.LivreExiste();

            if (!livreExiste)
            {
                return;
            }
            else
            {
                while (reponseRendreLivre != "n" && reponseRendreLivre != "o")
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

                            // Demander confirmation restitution

                            while (true)
                            {
                                reponseRendreLivre = assistanceUtilisateur.DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir rendre le livre n° " + livreARechercher.id + " ? (o/n) : ");

                                if (reponseRendreLivre.ToLower() == "o")
                                {
                                    // Rendre le livre

                                    
                                    Console.WriteLine();

                                    // Afficher à l'utilisateur la confirmation de la remise du livre 

                                    assistanceUtilisateur.ConfirmerEmpruntLivre("Livre n° " + livreARechercher.id + " emprunté le " + livreARechercher.dateDebutEmprunt.ToString("dd MMMM yyyy") + " a été rendu à la bibliothèque le " + livreARechercher.dateRetourLivre.ToString("dd MMMM yyyy"), ConsoleColor.Green);
                                    Console.WriteLine();
                                    menuAfficher.MettreAJourBibliotheque();
                                    Console.WriteLine();
                                    return;
                                }
                                else if (reponseRendreLivre.ToLower() == "n")
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
                            assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Impossible de rendre le livre n° " + livreARechercher.id + " car il n'a pas été emprunté...", ConsoleColor.Red);
                            Console.WriteLine();
                        }

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
