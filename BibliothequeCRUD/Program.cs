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

            menuPrincipal.Naviguer();  
             
        }
    }
}
