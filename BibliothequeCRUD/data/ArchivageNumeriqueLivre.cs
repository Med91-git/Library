using BibliothequeCRUD.business;
using Newtonsoft.Json;
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
            this.nomFichier = "testEcriture.json"; 
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

        public void SauvegarderBibliotheque(List<Livre> bibliotheque)
        {
            // Convertir la bibliothèque au format Json

            string json = JsonConvert.SerializeObject(bibliotheque);

            // Mise à jour de la bibliothèque dans le fichier (réecriture du fichier)

            File.WriteAllText(cheminFichier, json);

        }


        public List<Livre> ChargerLivres(string nomFichier) 
        {
            if (!File.Exists(nomFichier))
            {
                return new List<Livre>();
            }
            else 
            {
                // Lire les données json dans le fichier

                string json = File.ReadAllText(nomFichier);

                if ((string.IsNullOrEmpty(json)))
                {
                    return new List<Livre>();
                }
                else
                {
                    // Convertir les données json au format "Liste d'objets" -> List <Livre> (Désérialiser)

                    List<Livre> bibliotheque;

                    bibliotheque = JsonConvert.DeserializeObject<List<Livre>>(json);
                    
                    if (bibliotheque == null)
                    {
                        return new List<Livre>();
                    }
                    else
                    {
                        return bibliotheque;
                    }
                     
                }                
            }                          
            
        }
        

    }
}
