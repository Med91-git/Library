using BibliothequeCRUD.business;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliothequeCRUD.data
{
    public class ArchivageNumeriqueLivre
    {
        string nomFichier;
        string cheminFichier;
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

    }
}
