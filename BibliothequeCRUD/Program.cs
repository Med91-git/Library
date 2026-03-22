namespace BibliothequeCRUD
{
    internal class Program
    {
        
        static string DemanderChoixUtilisateurStr(string message)
        {
            Console.Write(message);
            string choixStr = Console.ReadLine().ToString();

            return choixStr;

        }

        static int DemanderChoixUtilisateurInt(string message) 
        {
            Console.Write(message); 
            string choixStr = Console.ReadLine().ToString(); 
            
            int choixInt = int.Parse(choixStr);

            return choixInt;

        }

        static int AjouterLivre(Dictionary<int, List<string>> bibliotheque, int nbIdDisponibles)
        {
            bool ajouterLivre = true;

            while (ajouterLivre)
            {
                // Demander à l'utilisateur les informations nécessaires pour la création d'un livre

                Console.WriteLine();
                string titre = DemanderChoixUtilisateurStr("Saisir le titre : ");
                string auteur = DemanderChoixUtilisateurStr("Saisir l'auteur : "); 

                // Ajouter le livre dans la bibliothèque 

                bibliotheque.Add(nbIdDisponibles, new List<string> { titre, auteur });

                // Incrémenter le prochain ID (en cas d'ajout d'un nouveau livre)    

                nbIdDisponibles++; 
                Console.WriteLine();
                Console.WriteLine("Votre livre a été ajouté !");  
                Console.WriteLine();
                
                string reponse = DemanderChoixUtilisateurStr("Voulez-vous ajouter un autre livre ? (o/n) : ");  

                if (reponse == "n") 
                {
                    break;  
                }
            }

            return nbIdDisponibles; 
        } 

        static bool LivreExiste(Dictionary<int, List<string>> bibliotheque)
        {
            if (bibliotheque.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        static void AfficherLivres(Dictionary<int, List<string>> bibliotheque)
        {
            if (bibliotheque.Count == 0)
            {
                Console.WriteLine("Aucun livre existant dans la bibliothèque.");
                return;
            }

            // Afficher le(s) livre(s)

            foreach (KeyValuePair<int, List<string>> livre in bibliotheque)  
            {
                Console.WriteLine("Livre n° " + livre.Key);
                string titre = livre.Value[0];

                foreach (string info in livre.Value)
                {
                    if (info == titre)
                    {
                        Console.WriteLine("Titre : " + info); 
                    }
                    else
                    {
                        Console.WriteLine("Auteur : " + info);    
                    }
                }
                Console.WriteLine();  
            }
        }

        static void AfficherLivreParId(Dictionary<int, List<string>> bibliotheque, int idLivre)
        {
            List<string> livre = bibliotheque[idLivre];
            string titre = livre[0];

            foreach (string info in livre)
            {
                if (info == titre)
                {
                    Console.WriteLine("Titre : " + info);
                }
                else
                {
                    Console.WriteLine("Auteur : " + info);
                }
            }
        }

        static int RechercherLivre(Dictionary<int, List<string>> bibliotheque, int idLivre)
        {
            
            // Vérifier si la bibliothèque contient un livre qui correspond à l'id de la saisie utilisateur (idLivre)
            // Si saisie utilisateur valide -> Afficher le livre 

            if (bibliotheque.ContainsKey(idLivre))
            {
                Console.WriteLine();
                AfficherLivreParId(bibliotheque, idLivre);
            }
            else
            {
                Console.WriteLine("Erreur : aucun livre ne contient cet identifiant ");  
            }

            return idLivre;

        }
        
        static void SupprimerLivre(Dictionary<int, List<string>> bibliotheque)
        {

            AfficherLivres(bibliotheque); 

            // Vérifier que la bibliothèque possède au moins un livre avant de supprimer

            bool livreExiste = LivreExiste(bibliotheque);

            if (livreExiste)
            {

                int idLivre = DemanderChoixUtilisateurInt("Saisir l'identifiant du livre à supprimer : ");

                int idLivreASupprimer = RechercherLivre(bibliotheque, idLivre);

                Console.WriteLine();
                string reponse = DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir supprimer le livre n° " + idLivreASupprimer + " ? (o/n) : ");

                if (reponse.ToLower() == "o")
                {
                    bibliotheque.Remove(idLivreASupprimer);
                    Console.WriteLine();
                    Console.WriteLine("Ce livre a été supprimé."); 
                    MettreAJourBibliotheque(bibliotheque); 
                    return; 
                } 
            }            
        } 

        static void ModifierLivre(Dictionary<int, List<string>> bibliotheque)
        {
            
            AfficherLivres(bibliotheque);

            // Vérifier que la bibliothèque possède au moins un livre avant de modifier

            bool livreExiste = LivreExiste(bibliotheque);

            if (livreExiste)
            {
                int idLivre = DemanderChoixUtilisateurInt("Saisir l'identifiant du livre à modifier : ");

                int idLivreAModifier = RechercherLivre(bibliotheque, idLivre);

                Console.WriteLine();
                string reponse = DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir modifier le livre n° " + idLivreAModifier + " ? (o/n) : ");

                if (reponse.ToLower() == "o")
                {
                    // Demander à l'utilisateur les nouvelles valeurs du livre à saisir (titre + auteur) 

                    Console.WriteLine();
                    string nouveauTitre = DemanderChoixUtilisateurStr("Saisir nouveau titre : "); 
                    string nouvelAuteur = DemanderChoixUtilisateurStr("Saisir nouvel auteur : ");

                    // Remplacer les valeurs du dictionnaire par les saisies utilisateurs (à partir de la clé du dictionnaire) 

                    bibliotheque[idLivreAModifier][0] = nouveauTitre;
                    bibliotheque[idLivreAModifier][1] = nouvelAuteur;
                    Console.WriteLine();
                    Console.WriteLine("Ce livre a été modifié.");
                    MettreAJourBibliotheque(bibliotheque);  
                    return;
                }
            }
            
        }

        static void MettreAJourBibliotheque(Dictionary<int, List<string>> bibliotheque)
        {
            Console.WriteLine();
            Console.WriteLine("Mise à jour de la bibliothèque : ");
            Console.WriteLine();
            AfficherLivres(bibliotheque);
        }

        static void OptionMenu(int numero, string option)
        {
            // Afficher le numéro en couleur
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(numero);

            // Afficher l'option du menu principal avec la couleur de la console par défaut

            Console.ResetColor();
            Console.WriteLine(". " + option);

        }

        static void AfficherMenuPrincipal()
        {
            string titreMenu = "--------- Menu --------- ";
            string finMenu = "------------------------ ";

            Console.WriteLine(titreMenu);
            Console.WriteLine();
            OptionMenu(1, "Ajouter un livre");
            OptionMenu(2, "Afficher les livres");
            OptionMenu(3, "Modifier un livre");
            OptionMenu(4, "Supprimer un livre");
            OptionMenu(5, "Quitter");
            Console.WriteLine();
            Console.WriteLine(finMenu);
        }

        static void GestionnaireDeLivres(Dictionary<int,List<string>> bibliotheque, int numeroId)  
        {
            // Création d'un compteur pour garder en mémoire le nombre de livres dans la bibliothèque 

            int compteurNbLivres = bibliotheque.Count;

            AfficherMenuPrincipal();  
            Console.WriteLine();
            int numOption = DemanderChoixUtilisateurInt("Faites votre choix : ");
            string optionQuitter;  
            
            if (numOption == 1) 
            {
                Console.Clear();

                // on récupère l'id pour garder en mémoire le numéro de l'id suivant (variable incrémentée dans la fonction AjouterLivre)

                numeroId = AjouterLivre(bibliotheque, numeroId); 
                Console.WriteLine();
                optionQuitter = DemanderChoixUtilisateurStr("Tapez la touche Entrez pour revenir au menu principal : "); 

                if (optionQuitter == "")
                {
                    Console.Clear();
                    GestionnaireDeLivres(bibliotheque, numeroId);  
                }      

            }
            else if (numOption == 2) 
            {
                Console.Clear();
                AfficherLivres(bibliotheque); 
                Console.WriteLine(); 
                optionQuitter = DemanderChoixUtilisateurStr("Tapez la touche Entrez pour revenir au menu principal : ");

                if (optionQuitter == "") 
                {
                    Console.Clear();
                    GestionnaireDeLivres(bibliotheque, numeroId); 
                }

            }
            else if (numOption == 3)
            {
                Console.Clear();
                ModifierLivre(bibliotheque);
                Console.WriteLine();
                optionQuitter = DemanderChoixUtilisateurStr("Tapez la touche Entrez pour revenir au menu principal : "); 

                if (optionQuitter == "")
                {
                    Console.Clear();
                    GestionnaireDeLivres(bibliotheque, numeroId); 
                }

            }
            else if (numOption == 4)
            {
                Console.Clear();
                SupprimerLivre(bibliotheque);  
                Console.WriteLine();
                optionQuitter = DemanderChoixUtilisateurStr("Tapez la touche Entrez pour revenir au menu principal : ");

                if (optionQuitter == "")
                {
                    Console.Clear();
                    GestionnaireDeLivres(bibliotheque, numeroId); 
                }
            }
            else if (numOption == 5) 
            {
                return;  
            } 
        }                

        static void Main(string[] args) 
        {
            /* On crée la bibliothèque (une seule fois) afin qu'elle reste accessible partout dans le programme
            + permettre de pouvoir garder les informations en mémoire pour le CRUD ! */   
            
            Dictionary<int,List<string>> bibliotheque = new Dictionary<int,List<string>>();
            int numeroId = 1;  
                        
            GestionnaireDeLivres(bibliotheque, numeroId);

        } 
    }
}
