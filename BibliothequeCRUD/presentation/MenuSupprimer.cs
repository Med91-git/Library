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

        public void AfficherLivreTrouve(Livre livreTrouve) 
        {
            Console.WriteLine();
            //Console.WriteLine("Livre n° : " + livreTrouve.id);
            Console.WriteLine("Titre : " + livreTrouve.titre);
            Console.WriteLine("Auteur : " + livreTrouve.auteur);
            Console.WriteLine();
        }

        public void SupprimerLivre() 
        {
            string reponseSupprimerLivre = "";

            menuAfficher.AfficherLivres();

            // Vérifier que la bibliothèque possède au moins un livre avant de supprimer

            bool livreExiste = gestionnaireLivres.LivreExiste();

            if (!livreExiste)
            {
                return;
            }
            else
            {
                while (true)
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
                            SupprimerLivre();
                        }
                        else
                        {
                            AfficherLivreTrouve(livreARechercher);
                            while (true)
                            {
                                reponseSupprimerLivre = assistanceUtilisateur.DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir supprimer le livre n° " + livreARechercher.id + " ? (o/n) : ");

                                if (reponseSupprimerLivre.ToLower() == "o")
                                {
                                    gestionnaireLivres.SupprimerLivre(livreARechercher);
                                    Console.WriteLine();

                                    // Afficher à l'utilisateur la confirmation de suppression 

                                    assistanceUtilisateur.AfficherMessageConfirmationCRUD("Livre n° " + livreARechercher.id + " supprimé.", ConsoleColor.Red);
                                    menuAfficher.MettreAJourBibliotheque();
                                    return;
                                }
                                else if (reponseSupprimerLivre.ToLower() == "n")
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
