using BibliothequeCRUD.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.business
{
    public class GestionnaireLivres
    {
        public List<Livre> bibliotheque { get; set; }

        public GestionnaireLivres() 
        {
            this.bibliotheque = new List<Livre>();
        }

        public void AjouterLivre(string titre, string auteur)
        {
            // Instancier un nouveau livre

            Livre livre = new Livre();

            // Récupérer les informations nécessaires

            livre.titre = titre;
            livre.auteur = auteur;

            // Ajouter le livre dans la bilbiothèque

            bibliotheque.Add(livre);
        }
        
        public Livre RechercherLivre(int idLivre) 
        {
            // Comparer l'id d'un livre existant de la bibliotheque avec l'id saisi par l'utilisateur
            
            foreach (Livre livre in bibliotheque)
            {
                if (livre.id == idLivre)
                {
                    return livre;
                }
            }
            return null;
             
        }

        public void ModifierLivre(string nouveauTitre, string nouvelAuteur, int idLivre)
        {
            // Vérifier si le livre a été trouvé

            Livre livreRecherche = RechercherLivre(idLivre);

            if (livreRecherche != null)
            {
                // Remplacer les valeurs du livre par les saisies utilisateurs

                livreRecherche.titre = nouveauTitre;
                livreRecherche.auteur = nouvelAuteur;
            }                       
            
        } 

        public void SupprimerLivre(int idLivre)
        {
            // Vérifier si le livre a été trouvé

            Livre livreRecherche = RechercherLivre(idLivre);

            if (livreRecherche != null)
            {
                bibliotheque.Remove(livreRecherche);
            }                         

        }        

    }
}
