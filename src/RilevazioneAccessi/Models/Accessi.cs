using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RilevazioneAccessi.Models
{
    public class Accessi
    {
        [Key]
        public int Id { get; set; }

        public int Id_utente { get; set; }

        public DateTime DataOra { get; set; }

        public string Gate { get; set; }

        public bool Sospettato { get; set; }

        [ForeignKey("Id")]
        public int Id_tipoaccesso { get; set; }
    }
}
