using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RilevazioneAccessi.Models
{
    public class TipiAccesso
    {
        [Key]
        public int Id { get; set; }

        public string Valore { get; set; }
    }
}
