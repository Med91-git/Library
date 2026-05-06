using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.business
{
    public class Livre
    {
        public static int nombreLivre = 0;
        public int id { get; set; } 
        public string titre { get; set; }
        public string auteur { get; set; } 
        public bool estEmprunte { get; set; }
        public DateTime dateDebutEmprunt { get; set; }
        public DateTime dateFinEmprunt { get; set; } 
        public DateTime dateRetourLivre { get; set; }

        public Livre()
        {
            nombreLivre ++; 

            id = nombreLivre; 

            this.estEmprunte = false; // par défaut le livre est disponible 
        }

        public void Emprunter(Livre livreAEmprunter)
        {
            // Récupérer les dates de début et de fin de l'emprunt du livre à emprunter 

            livreAEmprunter.dateDebutEmprunt = DateTime.Now;

            livreAEmprunter.dateFinEmprunt = dateDebutEmprunt.AddDays(21);

            // Mettre à jour la disponibilité du livre

            livreAEmprunter.estEmprunte = true; 
            
        }

        public void Rendre(Livre livreARendre)
        {

            // Récupérer la date de remise du livre (date d'aujourd'hui)

            livreARendre.dateRetourLivre = DateTime.Now;

            // Mettre à jour la disponibilité du livre (le livre n'est plus emprunté et de nouveau disponible à la bibliothèque)

            livreARendre.estEmprunte = false; 
        }



    }
}
