using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Model
{

    /**
     * Différentes parties d'une tâche, tel que le titre, le contenu, la deadline, l'importance ainsi que le status
     * 
     **/
    public class Tasks
    {
        public String title { get; set; }
        public String content { get; set; }
        public DateTime date { get; set; }
        public Boolean urgence { get; set; }
        public int statut { get; set; }
    }
}
