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
            AssistanceUtilisateur assistanceUtilisateur = new AssistanceUtilisateur();

            GestionnaireLivres gestionnaireLivres = new GestionnaireLivres();             

            MenuPrincipal menuPrincipal = new MenuPrincipal(assistanceUtilisateur, gestionnaireLivres);

            menuPrincipal.Naviguer(); 
             
        }
    }
}
