using BibliothequeCRUD.business;
using BibliothequeCRUD.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.presentation
{
    internal class MenuSupprimer : Menu
    {
        MenuAfficher menuAfficher;

        public MenuSupprimer(AssistanceUtilisateur assistanceUtilisateur, GestionnaireLivres gestionnaireLivres, MenuAfficher menuAfficher) : base(assistanceUtilisateur, gestionnaireLivres)
        {
            numero = 4;
            message = "Supprimer un livre"; 
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

        public void SupprimerLivre() 
        {
            string reponseConfirmationSupprimer = "";
            string reponsePropositionSupprimer = "";

            menuAfficher.AfficherLivres();

            // Vérifier que la bibliothèque possède au moins un livre avant de supprimer

            bool livreExiste = gestionnaireLivres.LivreExiste();

            if (!livreExiste)
            {
                return;
            }
            else
            {
                while (livreExiste)
                {
                    int idLivre = assistanceUtilisateur.DemanderIdLivre("Saisir l'identifiant du livre à supprimer : ");

                    // Vérifier l'existance du livre 

                    Livre livreARechercher = gestionnaireLivres.RechercherLivre(idLivre);

                    // Si le livre trouvé -> demander confirmation à l'utilisateur + supprimer s'il accepte, sinon -> redemander à l'utilisateur id valide

                    if (livreARechercher != null)
                    {
                        if (livreARechercher.estEmprunte == true)
                        {
                            assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Impossible de supprimer le livre n° " + livreARechercher.id + " car il a été emprunté...", ConsoleColor.Red);
                            Console.WriteLine();
                        }
                        else
                        {
                            menuAfficher.AfficherLivreTrouve(livreARechercher);  
                            while (true)
                            {
                                reponseConfirmationSupprimer = assistanceUtilisateur.DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir supprimer le livre n° " + livreARechercher.id + " ? (o/n) : ");

                                if (reponseConfirmationSupprimer.ToLower() == "o")
                                {
                                    gestionnaireLivres.SupprimerLivre(livreARechercher);
                                    Console.WriteLine();

                                    // Afficher à l'utilisateur la confirmation de suppression 

                                    assistanceUtilisateur.AfficherMessageConfirmationCRUD("Livre n° " + livreARechercher.id + " supprimé.", ConsoleColor.Red);
                                    menuAfficher.MettreAJourBibliotheque();
                                    break; 
                                }
                                else if (reponseConfirmationSupprimer.ToLower() == "n")
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
                        }

                        // Proposer de supprimer un autre livre 

                        while (true)
                        {

                            reponsePropositionSupprimer = assistanceUtilisateur.DemanderChoixUtilisateurStr("Souhaitez-vous supprimer un autre livre ? (o/n) : ");
                            reponsePropositionSupprimer = reponsePropositionSupprimer.ToLower();

                            if (reponsePropositionSupprimer == "o") // si utilisateur accepte -> permettre à l'utilisateur de choisir un livre à supprimer
                            {
                                Console.WriteLine();
                                break;
                            }
                            else if (reponsePropositionSupprimer == "n") // si utilisateur refuse -> revenir au menu principal
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
                        assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Erreur : livre introuvable ", ConsoleColor.Red);
                        Console.WriteLine();
                    }

                }
            }

        }

    }
}
