using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RilevazioneAccessi.Models
{
    public class Utente
    {
        public int ID { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
