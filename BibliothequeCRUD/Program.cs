using BibliothequeCRUD.business;
using BibliothequeCRUD.data;
using BibliothequeCRUD.presentation;
using BibliothequeCRUD.utils;
using System.Timers;

namespace BibliothequeCRUD
{
    internal class Program
    {

        static void Main(string[] args)  
        {
            ArchivageNumeriqueLivre archivageNumeriqueLivre = new ArchivageNumeriqueLivre();
                       
            GestionnaireLivres gestionnaireLivres = new GestionnaireLivres(archivageNumeriqueLivre);            
            
            AssistanceUtilisateur assistanceUtilisateur = new AssistanceUtilisateur();             

            MenuPrincipal menuPrincipal = new MenuPrincipal(assistanceUtilisateur, gestionnaireLivres);

            // Persistance des données au lancement du programme 

            try
            {
                gestionnaireLivres.ChargerLivresDepuisFichier(); 
                
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine();
                assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Erreur : le fichier n'existe pas : " + ex.Message, ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                assistanceUtilisateur.AfficherMessageErreurChoixUtilisateur("Une erreur est survenue : " + ex.Message, ConsoleColor.Red);
            }

            menuPrincipal.Naviguer();  

            
        }
    }
}
