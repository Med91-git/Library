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
        
        public Livre()
        {
            nombreLivre ++;

            id = nombreLivre;
        }
        
        

    }
}
