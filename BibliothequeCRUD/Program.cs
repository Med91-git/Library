namespace BibliothequeCRUD
{
    internal class Program
    {
        static void RevenirAuMenuPrincipal(string messageMenuPrincipal)
        {
            Console.Clear();
            AfficherMenu(messageMenuPrincipal);
        }
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

        static void AfficherMenu(string message)
        {
            Console.WriteLine(message); 
            Console.WriteLine();

            int reponseUtilisateur = DemanderChoixUtilisateurInt();
            int optionSousMenu;
            string optionQuitterSousMenu; 
            
            if (reponseUtilisateur == 1) 
            {
                Console.Clear();
                Console.WriteLine("Menu ajouter un livre"); 
                Console.WriteLine();
                optionQuitterSousMenu = DemanderChoixUtilisateurStr(); 

                if (optionQuitterSousMenu == "")
                {
                    RevenirAuMenuPrincipal(message);  
                }                            

            }
            else if (reponseUtilisateur == 2)
            {
                Console.Clear();
                Console.WriteLine("Menu afficher les livres"); 
                Console.WriteLine();
                optionQuitterSousMenu = DemanderChoixUtilisateurStr();

                if (optionQuitterSousMenu == "")
                {
                    RevenirAuMenuPrincipal(message); 
                }

            }
            else if (reponseUtilisateur == 3)
            {
                Console.Clear();
                Console.WriteLine("Menu modifier un livre");
                Console.WriteLine();
                optionQuitterSousMenu = DemanderChoixUtilisateurStr(); 

                if (optionQuitterSousMenu == "")
                {
                    RevenirAuMenuPrincipal(message);
                }

            }
            else if (reponseUtilisateur == 4)
            {
                Console.Clear();
                Console.WriteLine("Menu supprimer un livre");
                Console.WriteLine();
                optionQuitterSousMenu = DemanderChoixUtilisateurStr();

                if (optionQuitterSousMenu == "")
                {
                    RevenirAuMenuPrincipal(message);
                }

            }
            else if (reponseUtilisateur == 5) 
            {
                return; 
            } 
        }        

        static void Main(string[] args)
        {
            string reponseUtilisateur = "";

            AfficherMenu("-------- Menu -------- \n" +
                "\n" +
                "1. Ajouter un livre \n" +
                "2. Afficher les livres\n" +
                "3. Modifier un livre\n" +
                "4. Supprimer un livre\n" +
                "5. Quitter\n" +
                "\n" +
                "----------------------");       
            
        }
    }
}
