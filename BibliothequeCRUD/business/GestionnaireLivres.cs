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

        

        
        
    }
}
