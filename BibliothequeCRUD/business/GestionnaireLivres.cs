using BibliothequeCRUD.data;
using BibliothequeCRUD.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.business
{
    public class GestionnaireLivres
    {
        public List<Livre> bibliotheque { get; set; }
        public ArchivageNumeriqueLivre archivageNumeriqueLivre;
        int prochainId;
        

        public GestionnaireLivres(ArchivageNumeriqueLivre archivageNumeriqueLivre) 
        {
            this.bibliotheque = new List<Livre>();
            this.archivageNumeriqueLivre = archivageNumeriqueLivre;            
        }

        public void AjouterLivre(string titre, string auteur)
        {
            // Instancier un nouveau livre

            Livre livre = new Livre();

            // Récupérer les informations nécessaires

            livre.id = prochainId; 
            livre.titre = titre;
            livre.auteur = auteur;

            // Ajouter le livre dans la bilbiothèque

            bibliotheque.Add(livre);

            // Incrémenter l'id

            prochainId++;

            // Stocker le livre dans un fichier (sauvegarde)

            archivageNumeriqueLivre.SauvegarderLivre(livre);            

        }

        public bool LivreExiste()
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

        public void ModifierLivre(string nouveauTitre, string nouvelAuteur, Livre livreAModifier)
        {
            // Remplacer les valeurs du livre trouvé par les saisies utilisateurs

            livreAModifier.titre = nouveauTitre;
            livreAModifier.auteur = nouvelAuteur;

            archivageNumeriqueLivre.SauvegarderBibliotheque(bibliotheque);
        }

        public void SupprimerLivre(Livre livreASupprimer)
        {
            bibliotheque.Remove(livreASupprimer);

            archivageNumeriqueLivre.SauvegarderBibliotheque(bibliotheque);
        }

        public void ChargerLivresDepuisFichier()
        {
            // Récupérer le chemin du fichier

            string cheminFichier = archivageNumeriqueLivre.cheminFichier;

            // Récupérer les livres existants (livres stockés dans le fichier après l'ajout)

            List<Livre> livresExistants = archivageNumeriqueLivre.ChargerLivres(cheminFichier);

            // Mettre à jour la bibliothèque (alimenter la biliothèque par le contenu du fichier)

            bibliotheque = livresExistants;            
            
            if (bibliotheque.Count == 0)
            {
                prochainId = 1;
            }
            else if (bibliotheque.Count > 0)
            {
                // Calculer le prochain id pour un eventuel ajout

                Livre premierElement = bibliotheque[0];
                int idMax = premierElement.id;

                foreach (Livre livre in bibliotheque)
                {
                    if (livre.id > idMax)
                    {
                        idMax = livre.id;
                    }
                }
                prochainId = idMax + 1;
            }            

        }
        
    }
}
