namespace BibliothequeCRUD
{
    internal class Program
    {
        static bool ContientCaractereSpecial(string info, char[] caracterespeciaux)
        {
            
            foreach (char caractereSpecial in caracterespeciaux)
            {
                foreach (char caractere in info)
                {
                    if (info.Contains(caractereSpecial))
                    {
                        return true;
                    }
                }
            }
            return false; 

        }

        static string DemanderInformationLivre(string message)
        {
            char[] caracteresSpeciauxAutorises = {'\'','-','.',',',':','(',')'};  
            char[] caracteresSpeciauxInterdits = {'#','$','%','&','*','+','=','<','>','?','@','[',']','/','^','_','`','{','}','|','~'};
            string infoLivre = "";
            bool contientCaractereSpecial = false; 

            while (infoLivre == "" || contientCaractereSpecial == true) 
            {
                infoLivre = DemanderChoixUtilisateurStr(message); 

                // Gérer cas d'erreur saisie utilisateur vide 

                infoLivre = infoLivre.Trim();

                if (infoLivre == "")
                {
                    Console.WriteLine("Vous devez saisir une information.");
                }

                // Gérer cas d'erreur caractères spéciaux  

                contientCaractereSpecial = ContientCaractereSpecial(infoLivre, caracteresSpeciauxInterdits);
                
                if (contientCaractereSpecial)
                {
                    Console.WriteLine("Saisie invalide, vous pouvez uniquement inclure les caractères spéciaux suivants :");
                    Console.WriteLine();
                    foreach (char caractereSpecial in caracteresSpeciauxAutorises)
                    {
                        char premierCaractere = caracteresSpeciauxAutorises[0];
                        char dernierCaractere = caracteresSpeciauxAutorises[caracteresSpeciauxAutorises.Length - 1];

                        if (caractereSpecial == premierCaractere)
                        {
                            Console.Write("apostrophe (" + caractereSpecial + "), "); 
                        }
                        else if (caractereSpecial == dernierCaractere)
                        {
                            Console.Write("'" + caractereSpecial + "'"); 
                        }
                        else
                        {
                            Console.Write("'" + caractereSpecial + "', "); 
                        }
                    }
                    Console.WriteLine(); 
                }
                
                // Saisie utilisateur valide 

                if (infoLivre != "" && contientCaractereSpecial == false) 
                {
                    break;  
                }
                Console.WriteLine();
                
            }
            return infoLivre;
            
        }

        static string DemanderChoixUtilisateurStr(string message)
        {
            Console.Write(message);
            string choixStr = Console.ReadLine().ToString();

            return choixStr; 

        }

        static int DemanderOptionMenu(string message, int min , int max)
        {
            int choixInt = 0;

            while (true)
            {
                // Récupérer la saisie utilisateur
                
                string choixStr = DemanderChoixUtilisateurStr(message);

                // Vérifier la validité de la saisie utilisateur  

                try
                {
                    choixInt = int.Parse(choixStr);

                    if (choixInt >= min && choixInt <= max)
                    {
                        break;
                    }
                    else if (choixInt < 0)
                    {
                        Console.WriteLine("Choix invalide : le numéro ne peut pas être négatif");
                    }
                    else
                    {
                        Console.WriteLine("Choix invalide : vous devez saisir un numéro entre 1 et 5"); 
                    }

                }
                catch
                {
                    Console.WriteLine("Erreur : vous devez saisir un nombre");  
                }
                Console.WriteLine();
            }

            return choixInt;


        }

        static int DemanderChoixUtilisateurInt(string message)  
        {

            string choixStr = DemanderChoixUtilisateurStr(message);

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
                string titre = DemanderInformationLivre("Saisir le titre : ");
                string auteur = DemanderInformationLivre("Saisir l'auteur : ");  
                
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
                    string nouveauTitre = DemanderInformationLivre("Saisir nouveau titre : "); 
                    string nouvelAuteur = DemanderInformationLivre("Saisir nouvel auteur : ");

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
            int numOption = DemanderOptionMenu("Faites votre choix (saisir un numéro) : ", 1, 5); 
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
