namespace BibliothequeCRUD
{
    internal class Program
    {
        
        static string DemanderChoixUtilisateurStr()
        {
            Console.Write("Tapez la touche Entrez pour revenir au menu principal : ");
            string choixStr = Console.ReadLine();

            return choixStr;

        }

        static int DemanderChoixUtilisateurInt()
        {
            Console.Write("Faites votre choix : ");
            string choixStr = Console.ReadLine(); 
            
            int reponse = int.Parse(choixStr);

            return reponse;

        }

        static int AjouterLivre(Dictionary<int, List<string>> bibliotheque, int nbIdDisponibles)
        {
            bool ajouterLivre = true;

            while (ajouterLivre)
            {
                // Demander à l'utilisateur les informations nécessaires pour la création d'un livre

                Console.WriteLine();
                Console.Write("Saisir le titre : "); 
                string titre = Console.ReadLine();

                Console.Write("Saisir l'auteur : ");
                string auteur = Console.ReadLine();
                
                // Ajouter le livre dans la bibliothèque 

                bibliotheque.Add(nbIdDisponibles, new List<string> { titre, auteur });

                // Incrémenter le prochain ID (en cas d'ajout d'un nouveau livre)    

                nbIdDisponibles++; 

                Console.WriteLine("Votre livre a été ajouté !"); 
                
                Console.WriteLine();
                Console.Write("Voulez-vous ajouter un autre livre ? (o/n) : ");
                string reponse = Console.ReadLine().ToString();  

                if (reponse == "n") 
                {
                    break; 
                }
            }

            return nbIdDisponibles; 
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

                foreach (string infoLivre in livre.Value)
                {
                    if (infoLivre == titre)
                    {
                        Console.WriteLine("Titre : " + infoLivre); 
                    }
                    else
                    {
                        Console.WriteLine("Auteur : " + infoLivre);    
                    }
                }
                Console.WriteLine();  
            }
        }

        static void SupprimerLivre(Dictionary<int, List<string>> bibliotheque)
        {
            // Vérifier que la bilbiothèque possède au moins un livre

            AfficherLivres(bibliotheque);

            // Demander à l'utilisateur l'identifiant du livre à supprimer
            
            Console.Write("Saisir l'identifiant du livre à supprimer : ");
            string idLivreStr = Console.ReadLine();

            // Convertir la réponse utilisateur en type int

            int idLivreInt = int.Parse(idLivreStr);

            // Vérifier si la bibliothèque contient un livre qui correspond à l'id de la saisie utilisateur (idLivre)
            // Si saisie valide -> Afficher le livre 

            if (bibliotheque.ContainsKey(idLivreInt))
            {
                Console.WriteLine();
                Console.WriteLine("Livre n° " + idLivreInt);

                foreach (string e in bibliotheque[idLivreInt])
                {
                    if (e == bibliotheque[idLivreInt][0])
                    {
                        Console.WriteLine("Titre : " + e);
                    }
                    else
                    {
                        Console.WriteLine("Auteur : " + e); 
                    }    
                }
                Console.WriteLine();  
                Console.Write("Etes-vous sûr de vouloir supprimer ce livre ? (o/n) : ");
                string reponse = Console.ReadLine().ToLower();

                if (reponse == "o")
                {
                    bibliotheque.Remove(idLivreInt);
                    Console.WriteLine();
                    Console.WriteLine("Ce livre a été supprimé.");
                    return;
                }
                return;  
            } 
        }

        static void ModifierLivre(Dictionary<int, List<string>> bibliotheque)
        {
            // Vérifier que la bilbiothèque possède au moins un livre

            AfficherLivres(bibliotheque);

            // Demander à l'utilisateur l'identifiant du livre à modifier

            Console.Write("Saisir l'identifiant du livre à modifier : ");
            string idLivreStr = Console.ReadLine();

            // Convertir la réponse utilisateur en type int

            int idLivreInt = int.Parse(idLivreStr); 

            // Vérifier si la bibliothèque contient un livre qui correspond à l'id de la saisie utilisateur (idLivre)
            // Si saisie utilisateur valide -> Afficher le livre 

            if (bibliotheque.ContainsKey(idLivreInt))
            {
                Console.WriteLine();
                Console.WriteLine("Livre n° " + idLivreInt);

                foreach (string e in bibliotheque[idLivreInt])
                {
                    if (e == bibliotheque[idLivreInt][0])
                    {
                        Console.WriteLine("Titre : " + e);
                    }
                    else
                    {
                        Console.WriteLine("Auteur : " + e);
                    }
                }
                Console.WriteLine();
                Console.Write("Etes-vous sûr de vouloir modifier ce livre ? (o/n) : ");
                string reponse = Console.ReadLine().ToLower();

                if (reponse == "o")
                {
                    // Demander à l'utilisateur les nouvelles valeurs du livre à saisir (titre + auteur) 

                    Console.WriteLine();
                    Console.Write("Saisir nouveau titre : ");
                    string nouveauTitre = Console.ReadLine();

                    Console.Write("Saisir nouvel auteur : ");
                    string nouvelAuteur = Console.ReadLine();

                    // Remplacer les valeurs du dictionnaire par les saisies utilisateurs (à partir de la clé du dictionnaire) 
                     
                    bibliotheque[idLivreInt][0] = nouveauTitre;
                    bibliotheque[idLivreInt][1] = nouvelAuteur;
                    Console.WriteLine();
                    Console.WriteLine("Ce livre a été modifié.");
                    return;
                }
                return; 
            }
        }

        static void OptionsMenu(int numero, string option)
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
            OptionsMenu(1, "Ajouter un livre");
            OptionsMenu(2, "Afficher les livres");
            OptionsMenu(3, "Modifier un livre");
            OptionsMenu(4, "Supprimer un livre");
            OptionsMenu(5, "Quitter");
            Console.WriteLine();
            Console.WriteLine(finMenu);
        }

        static void GestionnaireDeLivres(Dictionary<int,List<string>> bibliotheque, int numeroId)  
        {
            // Création d'un compteur pour garder en mémoire le nombre de livres dans la bibliothèque 

            int compteurNbLivres = bibliotheque.Count;

            AfficherMenuPrincipal();  
            Console.WriteLine();
            int reponseUtilisateur = DemanderChoixUtilisateurInt();
            string optionQuitter;  
            
            if (reponseUtilisateur == 1) 
            {
                Console.Clear();

                // on récupère l'id pour garder en mémoire le numéro de l'id suivant (variable incrémentée dans la fonction AjouterLivre)

                numeroId = AjouterLivre(bibliotheque, numeroId); 
                Console.WriteLine();
                optionQuitter = DemanderChoixUtilisateurStr(); 

                if (optionQuitter == "")
                {
                    Console.Clear();
                    GestionnaireDeLivres(bibliotheque, numeroId);  
                }      

            }
            else if (reponseUtilisateur == 2) 
            {
                Console.Clear();
                AfficherLivres(bibliotheque);
                Console.WriteLine(); 
                optionQuitter = DemanderChoixUtilisateurStr();

                if (optionQuitter == "") 
                {
                    Console.Clear();
                    GestionnaireDeLivres(bibliotheque, numeroId); 
                }

            }
            else if (reponseUtilisateur == 3)
            {
                Console.Clear();
                ModifierLivre(bibliotheque);
                Console.WriteLine();
                optionQuitter = DemanderChoixUtilisateurStr(); 

                if (optionQuitter == "")
                {
                    Console.Clear();
                    GestionnaireDeLivres(bibliotheque, numeroId); 
                }

            }
            else if (reponseUtilisateur == 4)
            {
                Console.Clear();
                SupprimerLivre(bibliotheque);  
                Console.WriteLine();
                optionQuitter = DemanderChoixUtilisateurStr();

                if (optionQuitter == "")
                {
                    Console.Clear();
                    GestionnaireDeLivres(bibliotheque, numeroId); 
                }
            }
            else if (reponseUtilisateur == 5) 
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
