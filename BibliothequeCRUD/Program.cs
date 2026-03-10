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

        static void AjouterLivre(Dictionary<int, List<string>> bibliotheque, int compteurNbLivres)
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

                // Incrémenter le "compteur" à chaque livre crée       

                compteurNbLivres++;

                // Créer + ajouter le livre dans la bibliothèque 

                bibliotheque.Add(compteurNbLivres, new List<string> { titre, auteur });

                Console.WriteLine("Votre livre a été ajouté !"); 
                Console.WriteLine();
                Console.Write("Voulez-vous ajouter un autre livre ? (o/n) : ");
                string reponse = Console.ReadLine();  

                if (reponse == "n")  
                {
                    return; 
                }
            }
        } 

        static void AfficherLivres(Dictionary<int, List<string>> bibliotheque)
        {
            if (bibliotheque.Count == 0)
            {
                Console.WriteLine("Aucun livre existant dans la bibliothèque.");
                return;
            }

            foreach (KeyValuePair<int, List<string>> d in bibliotheque) 
            {
                Console.WriteLine("Livre n° " + d.Key);

                string titre = d.Value[0];

                foreach (string infoLivre in d.Value)
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
       
        static void GestionnaireDeLivres(string message, Dictionary<int,List<string>> bibliotheque)  
        {
            // Création d'un compteur pour garder en mémoire le nombre de livres dans la bibliothèque  
            int compteurNbLivres = bibliotheque.Count;

            Console.WriteLine(message); 
            Console.WriteLine();
            int reponseUtilisateur = DemanderChoixUtilisateurInt();
            string optionQuitter; 
            
            if (reponseUtilisateur == 1) 
            {
                Console.Clear();
                AjouterLivre(bibliotheque, compteurNbLivres); 
                Console.WriteLine();
                optionQuitter = DemanderChoixUtilisateurStr(); 

                if (optionQuitter == "")
                {
                    Console.Clear();
                    GestionnaireDeLivres(message, bibliotheque);  
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
                    GestionnaireDeLivres(message, bibliotheque); 
                }

            }
            else if (reponseUtilisateur == 3)
            {
                Console.Clear();
                Console.WriteLine("Menu modifier un livre");
                Console.WriteLine();
                optionQuitter = DemanderChoixUtilisateurStr(); 

                if (optionQuitter == "")
                {
                    Console.Clear();
                    GestionnaireDeLivres(message, bibliotheque); 
                }

            }
            else if (reponseUtilisateur == 4)
            {
                Console.Clear();
                Console.WriteLine("Menu supprimer un livre");
                Console.WriteLine();
                optionQuitter = DemanderChoixUtilisateurStr();

                if (optionQuitter == "")
                {
                    Console.Clear();
                    GestionnaireDeLivres(message, bibliotheque); 
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

            string menuPrincipal = "-------- Menu -------- \n" +
                "\n" +
                "1. Ajouter un livre \n" +
                "2. Afficher les livres\n" +
                "3. Modifier un livre\n" +
                "4. Supprimer un livre\n" +
                "5. Quitter\n" +
                "\n" +
                "----------------------";
              
            GestionnaireDeLivres(menuPrincipal, bibliotheque); 
            
        }
    }
}
