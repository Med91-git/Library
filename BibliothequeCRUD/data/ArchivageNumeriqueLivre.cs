using BibliothequeCRUD.business;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.data
{
    public class ArchivageNumeriqueLivre
    {
        string nomFichier;
        internal string cheminFichier;
        string dossierCourant;
        string cheminDossier;


        public ArchivageNumeriqueLivre()
        {
            this.nomFichier = "testEcriture.txt";
            this.dossierCourant = AppContext.BaseDirectory; // récupérer le dossier d'éxécution de l'application
            this.cheminDossier = Path.Combine(dossierCourant, "datafiles"); // construction du chemin vers le dossier de stockage (datafiles)
            this.cheminFichier = Path.Combine(cheminDossier, nomFichier);  // construction du chemin complet du fichier (dossier + nom du fichier)

            // Vérifier si le dossier existe

            if (Directory.Exists(cheminDossier)) 
            {
                // le dossier existe déjà -> aucune action nécessaire
            }
            else
            {
                Directory.CreateDirectory(cheminDossier);
            }
        }

        public void SauvegarderLivre(Livre livreASauvegarder)
        {
            // Récupérer les informations du livre           

            int idLivre = livreASauvegarder.id;
            string titre = livreASauvegarder.titre;
            string auteur = livreASauvegarder.auteur;


            // Définir le contenu et le format de l'enregistrement du livre

            char separateur = ';';
            string idLivreStr = idLivre.ToString(); // Convertir id livre en string pour le bon formatage de l'enregistrement
            string enregistrementLivre = idLivreStr + separateur + titre + separateur + auteur + "\n";


            // Sauvegarde de l'enregistrement du livre selon l'existence du fichier 

            if (File.Exists(cheminFichier))
            {
                File.AppendAllText(cheminFichier, enregistrementLivre);
            }
            else
            {
                File.WriteAllText(cheminFichier, enregistrementLivre);
            } 
            
        }

        public List<Livre> ChargerLivres(string nomFichier) 
        {
            char separateur = ';';
            
            List<Livre> livresExistants = new List<Livre>();

            // Lire les enregistrements du fichier

            string[] enregistrements = File.ReadAllLines(nomFichier);
            
            foreach (string enregistrement in enregistrements)
            {
                // Décomposer chaque enregistrement pour récupérer les infos séparément

                string[] livre = enregistrement.Split(separateur);  

                // Vérifier le bon nombre d'éléments dans la ligne

                if (livre.Count() == 3)
                {
                    string idLivreStr = livre[0];

                    int idLivre = int.Parse(idLivreStr);

                    // Convertir les données récupérées au format objet
                    
                    Livre livreExistant = new Livre();

                    livreExistant.id = idLivre;
                    livreExistant.titre = livre[1];
                    livreExistant.auteur = livre[2];

                    livresExistants.Add(livreExistant);  
                }
            }  
            return livresExistants;  
            
        }
        
        public void SauvegarderBibliotheque(List<Livre> bibliotheque) 
        {
            char separateur = ';';
            List<string> enregistrements = new List<string>();

            foreach (Livre livre in bibliotheque)
            {
                // Récupérer les informations du livre           

                int idLivre = livre.id;
                string titre = livre.titre;
                string auteur = livre.auteur;

                // Définir le contenu et le format de l'enregistrement du livre
                
                string idLivreStr = idLivre.ToString(); // Convertir id livre en string pour le bon formatage de l'enregistrement
                string enregistrementLivre = idLivreStr + separateur + titre + separateur + auteur + "\n"; 

                // Récupérer tous les enregistrements 

                enregistrements.Add(enregistrementLivre); 
            }

            // Mise à jour de la bibliothèque dans le fichier (réecriture du fichier)
            
            File.WriteAllLines(cheminFichier, enregistrements);             

        }

    }
}
