using BibliothequeCRUD.business;
using BibliothequeCRUD.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.presentation
{
    public abstract class Menu
    {
        protected string message;
        protected int numero;
        protected AssistanceUtilisateur assistanceUtilisateur; 
        protected GestionnaireLivres gestionnaireLivres; 
        

        public Menu(AssistanceUtilisateur assistanceUtilisateur, GestionnaireLivres gestionnaireLivres)
        {
            this.assistanceUtilisateur = assistanceUtilisateur;
            this.gestionnaireLivres = gestionnaireLivres;  
        }

        public abstract void Afficher(); 
        
        
    }
}
