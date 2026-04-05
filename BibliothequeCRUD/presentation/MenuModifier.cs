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

        public void AfficherLivreTrouve(Livre livreTrouve)
        {
            Console.WriteLine();
            //Console.WriteLine("Livre n° : " + livreTrouve.id);
            Console.WriteLine("Titre : " + livreTrouve.titre);
            Console.WriteLine("Auteur : " + livreTrouve.auteur);
            Console.WriteLine();
        }

        public void ModifierLivre() 
        {
            string reponseModifierLivre = "";            

            menuAfficher.AfficherLivres();
            
            // Vérifier que la bibliothèque possède au moins un livre avant de modifier

            bool livreExiste = gestionnaireLivres.LivreExiste();

            if (!livreExiste)
            {
                return;
            }
            else
            {
                while (true)
                {
                    int idLivre = assistanceUtilisateur.DemanderIdLivre("Saisir l'identifiant du livre à modifier : ");

                    // Vérifier l'existance du livre 

                    Livre livreARechercher = gestionnaireLivres.RechercherLivre(idLivre);

                    // Si le livre trouvé -> demander confirmation à l'utilisateur + modifier s'il accepte, sinon -> redemander à l'utilisateur id valide

                    if (livreARechercher != null)
                    {
                        AfficherLivreTrouve(livreARechercher); 

                        while (true)
                        {
                            reponseModifierLivre = assistanceUtilisateur.DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir modifier le livre n° " + livreARechercher.id + " ? (o/n) : ");

                            if (reponseModifierLivre.ToLower() == "o")
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
                                return;
                            }
                            else if (reponseModifierLivre.ToLower() == "n")
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
