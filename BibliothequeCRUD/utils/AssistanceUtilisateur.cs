using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.utils
{
    public class AssistanceUtilisateur
    {

        public string DemanderChoixUtilisateurStr(string message)
        {
            Console.Write(message);
            string choixStr = Console.ReadLine().ToString();
            
            choixStr = choixStr.Trim(); 

            return choixStr;

        }

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

        public void AfficherMessageErreurChoixUtilisateur(string message, ConsoleColor couleur)
        {

            Console.ForegroundColor = couleur;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void AfficherMessageConfirmationCRUD(string message, ConsoleColor couleur)
        {
            Console.ForegroundColor = couleur;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public string DemanderInformationLivre(string message) 
        {
            char[] caracteresSpeciauxAutorises = { '\'', '-', '.', ',', ':', '(', ')' };
            char[] caracteresSpeciauxInterdits = { '#', '$', '%', '&', '*', '+', '=', '<', '>', '?', '@', '[', ']', '/', '^', '_', '`', '{', '}', '|', '~', ';' };
            string infoLivre = "";
            bool contientCaractereSpecial = false;

            while (infoLivre == "" || contientCaractereSpecial == true)
            {
                infoLivre = DemanderChoixUtilisateurStr(message);

                // Gérer cas d'erreur saisie utilisateur vide  

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
                    AfficherMessageErreurChoixUtilisateur("Saisie invalide, vous pouvez uniquement inclure les caractères spéciaux suivants :", ConsoleColor.DarkYellow);
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

        public int DemanderIdLivre(string message)
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
                        AfficherMessageErreurChoixUtilisateur("Choix invalide : le numéro ne peut pas être négatif", ConsoleColor.Yellow);
                    }
                    else if (choixInt == 0)
                    {
                        Console.WriteLine();
                        AfficherMessageErreurChoixUtilisateur("Choix invalide : l'identifiant ne peut pas être égal à 0", ConsoleColor.Yellow);
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine();
                    AfficherMessageErreurChoixUtilisateur("Erreur : vous devez saisir un nombre", ConsoleColor.Red);
                }
                Console.WriteLine();
            }
            return choixInt;

        }

        public int DemanderOptionMenu(string message, int min, int max)
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
                        AfficherMessageErreurChoixUtilisateur("Choix invalide : le numéro ne peut pas être négatif", ConsoleColor.Yellow);
                    }
                    else
                    {
                        Console.WriteLine();
                        AfficherMessageErreurChoixUtilisateur("Choix invalide : vous devez saisir un numéro entre 1 et 5", ConsoleColor.Yellow);
                    }

                }
                catch
                {
                    Console.WriteLine();
                    AfficherMessageErreurChoixUtilisateur("Erreur : vous devez saisir un nombre", ConsoleColor.Red);
                }
                Console.WriteLine();
            }

            return choixInt;

        }

        public void RevenirAuMenuPrincipal()
        {
            // Mettre un caractère vide pour rentrer dans la condition de la boucle while 

            string optionQuitter = " "; 

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


    }
}
