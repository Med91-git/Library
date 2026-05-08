using BibliothequeCRUD.business;
using BibliothequeCRUD.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.presentation
{
    internal class MenuModifier : Menu
    {
        MenuAfficher menuAfficher;

        public MenuModifier(AssistanceUtilisateur assistanceUtilisateur, GestionnaireLivres gestionnaireLivres, MenuAfficher menuAfficher) : base(assistanceUtilisateur, gestionnaireLivres)
        {
            numero = 3;
            message = "Modifier un livre";
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

        public void ModifierLivre() 
        {
            string reponseConfirmationModifier = "";            
            string reponsePropositionModifier = "";            

            menuAfficher.AfficherLivres();
            
            // Vérifier que la bibliothèque possède au moins un livre avant de modifier

            bool livreExiste = gestionnaireLivres.LivreExiste();

            if (!livreExiste)
            {
                return;
            }
            else
            {
                while (livreExiste)
                {
                    int idLivre = assistanceUtilisateur.DemanderIdLivre("Saisir l'identifiant du livre à modifier : ");

                    // Vérifier l'existance du livre  

                    Livre livreARechercher = gestionnaireLivres.RechercherLivre(idLivre);

                    // Si le livre trouvé -> demander confirmation à l'utilisateur + modifier s'il accepte, sinon -> redemander à l'utilisateur id valide

                    if (livreARechercher != null)
                    {

                        if (livreARechercher.estEmprunte == true)
                        {
                            assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Impossible de modifier le livre n° " + livreARechercher.id + " car il a été emprunté...", ConsoleColor.Red);
                            Console.WriteLine(); 
                        }
                        else
                        {
                            menuAfficher.AfficherLivreTrouve(livreARechercher); 
                            while (true)
                            {
                                reponseConfirmationModifier = assistanceUtilisateur.DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir modifier le livre n° " + livreARechercher.id + " ? (o/n) : ");

                                if (reponseConfirmationModifier.ToLower() == "o")
                                {
                                    // Demander à l'utilisateur les nouvelles valeurs du livre à saisir (titre + auteur)

                                    Console.WriteLine();

                                    string nouveauTitre = assistanceUtilisateur.DemanderInformationLivre("Saisir nouveau titre : ");
                                    string nouvelAuteur = assistanceUtilisateur.DemanderInformationLivre("Saisir nouvel auteur : ");

                                    gestionnaireLivres.ModifierLivre(nouveauTitre, nouvelAuteur, livreARechercher);
                                    Console.WriteLine();

                                    // Afficher à l'utilisateur la confirmation de modification 

                                    assistanceUtilisateur.AfficherMessageConfirmationCRUD("Livre n° " + livreARechercher.id + " modifié avec succès !", ConsoleColor.Green);

                                    menuAfficher.MettreAJourBibliotheque();
                                    break;
                                }
                                else if (reponseConfirmationModifier.ToLower() == "n")
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

                        // Proposer de modifier un autre livre  

                        while (true)
                        {

                            reponsePropositionModifier = assistanceUtilisateur.DemanderChoixUtilisateurStr("Souhaitez-vous modifier un autre livre ? (o/n) : ");
                            reponsePropositionModifier = reponsePropositionModifier.ToLower();

                            if (reponsePropositionModifier == "o") // si utilisateur accepte -> permettre à l'utilisateur de choisir un livre à modifier
                            {
                                Console.WriteLine();
                                break;
                            }
                            else if (reponsePropositionModifier == "n") // si utilisateur refuse -> revenir au menu principal
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
