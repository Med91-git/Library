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


        public Livre()
        {
            nombreLivre ++; 

            id = nombreLivre; 

            this.estEmprunte = false; // par défaut le livre est disponible 
        }

        public List<DateTime> Emprunter()
        {
            // Créer une liste vide pour récupérer les dates de l'emprunt 

            List<DateTime> datesEmprunt = new List<DateTime>();

            // Générer les dates de début et de fin de l'emprunt 

            dateDebutEmprunt = DateTime.Now;

            dateFinEmprunt = dateDebutEmprunt.AddDays(21); 

            // Ajouter les dates dans la liste

            datesEmprunt.Add(dateDebutEmprunt);
            datesEmprunt.Add(dateFinEmprunt);             

            return datesEmprunt;

        }



    }
}
