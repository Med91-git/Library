using System.Timers;

namespace BibliothequeCRUD
{
    internal class Program
    {
        // Fonctions liées au traitement des données (CRUD)  


        static void GestionnaireDeLivres(Dictionary<int, List<string>> bibliotheque, int numeroId, string optionQuitter)
        {
            int optionQuitterProgramme = 5;
            int numOption = 0; 

            while (numOption != optionQuitterProgramme)  
            {
                // Création d'un compteur pour garder en mémoire le nombre de livres dans la bibliothèque 

                int compteurNbLivres = bibliotheque.Count;

                AfficherMenuPrincipal();
                Console.WriteLine();
                numOption = DemanderOptionMenu("Faites votre choix (saisir un numéro) : ", 1, 5);

                if (numOption == 1)
                {
                    Console.Clear();

                    // Récupèrer l'id pour garder en mémoire le numéro de l'id suivant (variable incrémentée dans la fonction AjouterLivre) 

                    numeroId = AjouterLivre(bibliotheque, numeroId, optionQuitter); 
                }
                else if (numOption == 2) 
                {
                    Console.Clear();

                    AfficherLivres(bibliotheque);
                    RevenirAuMenuPrincipal(optionQuitter); 
                }
                else if (numOption == 3)
                {
                    Console.Clear();

                    ModifierLivre(bibliotheque, optionQuitter); 
                    
                }
                else if (numOption == 4)
                {
                    Console.Clear();

                    SupprimerLivre(bibliotheque, optionQuitter); 
                    
                }
                else if (numOption == optionQuitterProgramme)
                {
                    return;
                }
            }
            
        } 

        static int AjouterLivre(Dictionary<int, List<string>> bibliotheque, int nbIdDisponibles, string optionQuitter)
        {
            string reponseAjoutLivre = ""; 
            
            while (true) 
            {
                // Demander à l'utilisateur les informations nécessaires pour la création d'un livre  

                Console.WriteLine();
                string titre = DemanderInformationLivre("Saisir le titre : ");
                string auteur = DemanderInformationLivre("Saisir l'auteur : ");  
                                
                // Ajouter le livre dans la bibliothèque  

                bibliotheque.Add(nbIdDisponibles, new List<string> { titre, auteur });

                // Afficher la confirmation de modification en couleur 
                Console.WriteLine();
                AjouterCouleurMessageConfirmationCRUD("Livre ajouté.", ConsoleColor.Green);

                // Sauvegarder le livre dans un fichier 

                bool cheminFichierValide = SauvegarderLivre(bibliotheque, nbIdDisponibles);
                
                if (cheminFichierValide)
                {
                    Console.WriteLine();
                    AjouterCouleurMessageConfirmationCRUD("Livre sauvegardé dans un fichier.", ConsoleColor.Green); 
                }
                Console.WriteLine();  

                // Incrémenter le prochain ID (en cas d'ajout d'un nouveau livre)
                
                nbIdDisponibles++;
                Console.WriteLine();

                reponseAjoutLivre = DemanderChoixUtilisateurStr("Voulez-vous ajouter un autre livre ? (o/n) : ");
                Console.WriteLine(); 

                while (reponseAjoutLivre != "o" && reponseAjoutLivre != "n")
                {
                    AjouterCouleurErreursChoixUtilisateur("Vous devez répondre 'o' pour oui ou 'n' pour non.", ConsoleColor.Yellow);
                    Console.WriteLine();
                    reponseAjoutLivre = DemanderChoixUtilisateurStr("Voulez-vous ajouter un autre livre ? (o/n) : ");                    

                }

                if (reponseAjoutLivre == "o")
                {
                    Console.Clear();

                }
                else if (reponseAjoutLivre == "n")
                {
                    // Mettre un caractère vide pour rentrer dans la condition de la boucle while 

                    optionQuitter = " ";

                    // Boucler tant que l'utilisateur n'a pas entré la bonne touche (mettre l'appel de la fonction "revenir au menu principal"

                    while (optionQuitter != "")
                    {
                        Console.WriteLine();
                        optionQuitter = DemanderChoixUtilisateurStr("Tapez la touche Entrez pour revenir au menu principal : ");

                        if (optionQuitter == "")
                        {
                            Console.Clear();
                            return nbIdDisponibles;
                        }
                        else
                        {
                            Console.WriteLine();
                            AjouterCouleurErreursChoixUtilisateur("Vous devez appuyer sur la touche 'Entrez' pour quitter.", ConsoleColor.Yellow);
                        }
                        Console.WriteLine();

                    }

                }

            }
            
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
                Console.WriteLine();
            }
            else
            {
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

        static int RechercherLivre(Dictionary<int, List<string>> bibliotheque, int idLivre, string message)
        {
            
            // Vérifier si la bibliothèque contient un livre qui correspond à l'id de la saisie utilisateur (idLivre)
            // Si saisie utilisateur valide -> Afficher le livre 

            while (!bibliotheque.ContainsKey(idLivre))
            {
                if (bibliotheque.ContainsKey(idLivre))
                {
                    Console.WriteLine();
                    AfficherLivreParId(bibliotheque, idLivre);
                    break;
                }
                else
                {
                    Console.WriteLine();
                    AjouterCouleurErreursChoixUtilisateur("Erreur : livre introuvable ", ConsoleColor.Red); 
                    Console.WriteLine();
                    idLivre = DemanderIdLivre(message); 
                }
            }            
            return idLivre; 

        }
        
        static void SupprimerLivre(Dictionary<int, List<string>> bibliotheque, string optionQuitter)
        {
            string reponse = "";
            AfficherLivres(bibliotheque);

            // Vérifier que la bibliothèque possède au moins un livre avant de supprimer

            bool livreExiste = LivreExiste(bibliotheque);

            if (!livreExiste)
            {
                RevenirAuMenuPrincipal(optionQuitter);
            }

            if (livreExiste)
            {

                int idLivre = DemanderIdLivre("Saisir l'identifiant du livre à supprimer : ");

                int idLivreASupprimer = RechercherLivre(bibliotheque, idLivre, "Saisir l'identifiant du livre à supprimer : ");

                while (reponse != "o" && reponse != "n") 
                {
                    Console.WriteLine();
                    reponse = DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir supprimer le livre n° " + idLivreASupprimer + " ? (o/n) : ");

                    if (reponse.ToLower() == "o")
                    {
                        bibliotheque.Remove(idLivreASupprimer);
                        Console.WriteLine();

                        // Afficher la confirmation de suppression en couleur

                        AjouterCouleurMessageConfirmationCRUD("Livre n° " + idLivreASupprimer + " supprimé.", ConsoleColor.Red); 

                        // Redéfinir la couleur de la console par défaut

                        Console.ResetColor();
                        MettreAJourBibliotheque(bibliotheque);
                        RevenirAuMenuPrincipal(optionQuitter);
                        return;
                    }
                    else if (reponse.ToLower() == "n")
                    {
                        Console.WriteLine();
                        RevenirAuMenuPrincipal(optionQuitter);
                    }
                    else
                    {
                        Console.WriteLine();
                        AjouterCouleurErreursChoixUtilisateur("Vous devez répondre 'o' pour oui ou 'n' pour non.", ConsoleColor.Yellow);
                    }
                }

            }            
        } 

        static void ModifierLivre(Dictionary<int, List<string>> bibliotheque, string optionQuitter)
        {
            string reponseModifierLivre = "";

            AfficherLivres(bibliotheque);
            
            // Vérifier que la bibliothèque possède au moins un livre avant de modifier

            bool livreExiste = LivreExiste(bibliotheque);

            if (!livreExiste)
            {
                RevenirAuMenuPrincipal(optionQuitter);
            }

            if (livreExiste) 
            {
                
                int idLivre = DemanderIdLivre("Saisir l'identifiant du livre à modifier : ");

                int idLivreAModifier = RechercherLivre(bibliotheque, idLivre, "Saisir l'identifiant du livre à modifier : ");

                while (reponseModifierLivre != "o" && reponseModifierLivre != "n")
                {
                    Console.WriteLine(); 
                    reponseModifierLivre = DemanderChoixUtilisateurStr("Etes-vous sûr de vouloir modifier le livre n° " + idLivreAModifier + " ? (o/n) : ");

                    if (reponseModifierLivre.ToLower() == "o")
                    {
                        // Demander à l'utilisateur les nouvelles valeurs du livre à saisir (titre + auteur) 

                        Console.WriteLine();
                        string nouveauTitre = DemanderInformationLivre("Saisir nouveau titre : ");
                        string nouvelAuteur = DemanderInformationLivre("Saisir nouvel auteur : ");

                        // Remplacer les valeurs du dictionnaire par les saisies utilisateurs (à partir de la clé du dictionnaire) 

                        bibliotheque[idLivreAModifier][0] = nouveauTitre;
                        bibliotheque[idLivreAModifier][1] = nouvelAuteur;
                        Console.WriteLine();

                        // Afficher la confirmation de modification en couleur

                        AjouterCouleurMessageConfirmationCRUD("Livre n° " + idLivreAModifier + " modifié avec succès !", ConsoleColor.Green);
                        
                        // Redéfinir la couleur de la console par défaut 
                        
                        Console.ResetColor();
                        MettreAJourBibliotheque(bibliotheque);
                        RevenirAuMenuPrincipal(optionQuitter);  
                        return;
                    }
                    else if (reponseModifierLivre.ToLower() == "n")
                    {
                        Console.WriteLine();
                        RevenirAuMenuPrincipal(optionQuitter); 
                    }
                    else
                    {
                        Console.WriteLine();
                        AjouterCouleurErreursChoixUtilisateur("Vous devez répondre 'o' pour oui ou 'n' pour non.", ConsoleColor.Yellow);
                    }
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


        // Fonctions liées à la gestion des fichiers (persistance des données)


        static bool SauvegarderLivre(Dictionary<int, List<string>> bibliotheque, int nbIdDisponibles)
        {
            try
            {
                // Définir l'emplacement du fichier 

                //string cheminFichier = "C:\\Dev\\ProjetsFormations\\Udemy\\C#\\Test création de fichiers\\testEcritureV1.txt"; // version correcte 
                string cheminFichier = "testLecture.txt";  // version test 

                // Récupérer les informations du livre

                int idLivre = nbIdDisponibles;                 

                string titre = bibliotheque[idLivre][0];
                string auteur = bibliotheque[idLivre][1];

                // Définir le contenu et le format de l'enregistrement du livre 

                char separateur = ';';
                string idLivreStr = idLivre.ToString(); // Convertir id livre en string pour le bon formatage de l'enregistrement 
                string enregistrementLivre = idLivreStr + separateur + titre + separateur + auteur + "\n";  


                // Sauvegarde de l'enregistrement du livre selon l'existence du fichier 
                
                if (File.Exists(cheminFichier)) 
                {
                    File.AppendAllText(cheminFichier, enregistrementLivre); 
                    return true;   
                }
                else   
                {
                    File.WriteAllText(cheminFichier, enregistrementLivre);
                    return true;
                }
                
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine();  
                AjouterCouleurErreursChoixUtilisateur("Erreur : échec de la sauvegarde du livre, chemin du fichier incomplet ... !\nVérifier votre chemin : " + ex.Message, ConsoleColor.Red);
                return false;  
            } 
            catch (Exception ex)
            {
                Console.WriteLine();
                AjouterCouleurErreursChoixUtilisateur("Une erreur est survenue : " + ex.Message, ConsoleColor.Red);
                return false;  
            }

        }

        static Dictionary<int, List<string>> ChargerLivres(Dictionary<int, List<string>> bibliotheque, string nomFichier)
        {
            char separateur = ';'; 
            
            try
            {
                // Lire les enregistrements du fichier

                string[] enregistrements = File.ReadAllLines(nomFichier);

                 

                foreach (string enregistrement in enregistrements)
                {                    
                    // Décomposer chaque enregistrement pour récupérer les infos du dictionnaire séparément

                    string[] livre = enregistrement.Split(separateur);

                    // Vérifier le bon nombre d'élement dans la ligne avant de récupérer les infos

                    if (livre.Count() == 3)
                    {
                        string idLivreStr = livre[0];

                        int idLivre = int.Parse(idLivreStr);
                        
                        if (!bibliotheque.ContainsKey(idLivre))
                        {
                            bibliotheque.Add(idLivre, new List<string> { livre[1], livre[2] });
                        }
                    }

                }                
                 
            }
            catch (FileNotFoundException ex) 
            {
                Console.WriteLine();
                AjouterCouleurErreursChoixUtilisateur("Erreur : le fichier n'existe pas : " + ex.Message, ConsoleColor.Red);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                AjouterCouleurErreursChoixUtilisateur("Une erreur est survenue : " + ex.Message, ConsoleColor.Red);
                
            }
            return bibliotheque; 
        }


        // Fonctions liées à l'interface du menu (affichage + intéractions avec l'utilisateur)       

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
            char[] caracteresSpeciauxAutorises = { '\'', '-', '.', ',', ':', '(', ')' };
            char[] caracteresSpeciauxInterdits = { '#', '$', '%', '&', '*', '+', '=', '<', '>', '?', '@', '[', ']', '/', '^', '_', '`', '{', '}', '|', '~', ';' }; 
            string infoLivre = "";
            bool contientCaractereSpecial = false;

            while (infoLivre == "" || contientCaractereSpecial == true)
            {
                infoLivre = DemanderChoixUtilisateurStr(message);

                // Gérer cas d'erreur saisie utilisateur vide 

                infoLivre = infoLivre.Trim();

                if (infoLivre == "")
                {
                    Console.WriteLine();
                    Console.WriteLine("Vous devez saisir une information.");
                }

                // Gérer cas d'erreur caractères spéciaux  

                contientCaractereSpecial = ContientCaractereSpecial(infoLivre, caracteresSpeciauxInterdits);

                if (contientCaractereSpecial)
                {
                    Console.WriteLine(); 
                    //Console.WriteLine("Saisie invalide, vous pouvez uniquement inclure les caractères spéciaux suivants :");
                    AjouterCouleurErreursChoixUtilisateur("Saisie invalide, vous pouvez uniquement inclure les caractères spéciaux suivants :", ConsoleColor.DarkYellow);
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

            choixStr.ToLower().Trim();

            return choixStr;

        }

        static int DemanderOptionMenu(string message, int min, int max)
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
                        Console.WriteLine();
                        AjouterCouleurErreursChoixUtilisateur("Choix invalide : le numéro ne peut pas être négatif", ConsoleColor.Yellow); 
                    }
                    else
                    {
                        Console.WriteLine();
                        AjouterCouleurErreursChoixUtilisateur("Choix invalide : vous devez saisir un numéro entre 1 et 5", ConsoleColor.Yellow);
                    }

                }
                catch
                {
                    Console.WriteLine();
                    AjouterCouleurErreursChoixUtilisateur("Erreur : vous devez saisir un nombre", ConsoleColor.Red);
                }
                Console.WriteLine();
            }

            return choixInt;

        }

        static int DemanderIdLivre(string message) 
        {
            int choixInt = 0;

            while (true)
            {
                string choixStr = DemanderChoixUtilisateurStr(message);

                try
                {
                    choixInt = int.Parse(choixStr);

                    if (choixInt < 0)
                    {
                        Console.WriteLine();
                        AjouterCouleurErreursChoixUtilisateur("Choix invalide : le numéro ne peut pas être négatif", ConsoleColor.Yellow);
                    }
                    else if (choixInt == 0)
                    {
                        Console.WriteLine(); 
                        AjouterCouleurErreursChoixUtilisateur("Choix invalide : l'identifiant ne peut pas être égal à 0", ConsoleColor.Yellow);
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine();
                    AjouterCouleurErreursChoixUtilisateur("Erreur : vous devez saisir un nombre", ConsoleColor.Red); 
                }
                Console.WriteLine(); 
            }
            return choixInt; 







        }

        static void AjouterCouleurOptionMenu(int numero, string option)
        {
            // Afficher le numéro en couleur
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(numero);

            // Afficher l'option du menu principal avec la couleur de la console par défaut

            Console.ResetColor();
            Console.WriteLine(". " + option);

        }

        static void AjouterCouleurErreursChoixUtilisateur(string message, ConsoleColor couleur)
        {

            Console.ForegroundColor = couleur;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void AjouterCouleurMessageConfirmationCRUD(string message, ConsoleColor couleur)
        {
            Console.ForegroundColor = couleur;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void AfficherMenuPrincipal()
        {
            string titreMenu = "--------- Menu --------- ";
            string finMenu = "------------------------ ";

            Console.WriteLine(titreMenu);
            Console.WriteLine();
            AjouterCouleurOptionMenu(1, "Ajouter un livre");
            AjouterCouleurOptionMenu(2, "Afficher les livres");
            AjouterCouleurOptionMenu(3, "Modifier un livre");
            AjouterCouleurOptionMenu(4, "Supprimer un livre");
            AjouterCouleurOptionMenu(5, "Quitter");
            Console.WriteLine();
            Console.WriteLine(finMenu);
        } 
        
        static void RevenirAuMenuPrincipal(string optionQuitter)
        {
            // Mettre un caractère vide pour rentrer dans la condition de la boucle while 

            optionQuitter = " "; 

            while (optionQuitter != "")
            {
                optionQuitter = DemanderChoixUtilisateurStr("Tapez la touche Entrez pour revenir au menu principal : ");

                if (optionQuitter == "")
                {
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("Vous devez appuyer sur la touche 'Entrez' pour quitter.");
                }
                Console.WriteLine();

            }
        }

        static void Main(string[] args) 
        {
            /* On crée la bibliothèque (une seule fois) afin qu'elle reste accessible partout dans le programme
            + permettre de pouvoir garder les informations en mémoire pour le CRUD ! */

            Dictionary<int,List<string>> bibliotheque = new Dictionary<int,List<string>>();

            string optionQuitter = "";

            string fichierALire = "testLecture.txt";             

            // Persistance des données au lancement du programme : alimenter la biliothèque par le contenu du fichier

            Dictionary<int,List<string>> bibliothequeAjour = ChargerLivres(bibliotheque, fichierALire); 

            int numeroId = bibliothequeAjour.Count+1; // initialiser id par la prochaine clé disponible (pou un éventuel ajout)

            GestionnaireDeLivres(bibliothequeAjour, numeroId, optionQuitter);  
            
        }
    }
}
