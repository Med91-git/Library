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
                while (reponseEmprunterLivre != "n" && reponseEmprunterLivre != "o")
                {
                    int idLivre = assistanceUtilisateur.DemanderIdLivre("Saisir l'identifiant du livre à emprunter : ");

                    // Vérifier l'existance du livre dans la bibliothèque 

                    Livre livreARechercher = gestionnaireLivres.RechercherLivre(idLivre); 

                    if (livreARechercher != null)
                    {
                        // Vérifier que le livre trouvé est disponible pour un emprunt

                        Console.WriteLine();
                        if (livreARechercher.estEmprunte == true)
                        {
                            assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Impossible d'emprunter le livre n° " + livreARechercher.id + " car il a déjà été emprunté...", ConsoleColor.Red);
                            Console.WriteLine(); 
                        }
                        else
                        {
                            assistanceUtilisateur.ConfirmerDisponibiliteEmpruntLivre("Le livre n° " + livreARechercher.id + " est disponible à l'emprunt : ", ConsoleColor.Green); 
                            AfficherLivreTrouve(livreARechercher);

                            // Demander confirmation emprunt

                            while (true)
                            {
                                reponseEmprunterLivre = assistanceUtilisateur.DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir emprunter le livre n° " + livreARechercher.id + " ? (o/n) : ");

                                if (reponseEmprunterLivre.ToLower() == "o")
                                {
                                    // Emprunter le livre

                                    gestionnaireLivres.EmprunterLivre(livreARechercher);
                                    Console.WriteLine();

                                    // Afficher à l'utilisateur la confirmation de l'emprunt 

                                    assistanceUtilisateur.ConfirmerEmpruntLivre("Livre n° " + livreARechercher.id + " emprunté le " + livreARechercher.dateDebutEmprunt.ToString("dd MMMM yyyy") + ".\n", ConsoleColor.Green);
                                    Console.WriteLine("Vous devez rendre ce livre au plus tard le " + livreARechercher.dateFinEmprunt.ToString("dd MMMM yyyy")+ ".");
                                    Console.WriteLine();
                                    menuAfficher.MettreAJourBibliotheque();
                                    Console.WriteLine(); 
                                    return;
                                }
                                else if (reponseEmprunterLivre.ToLower() == "n") 
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
