using RilevazioneAccessi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RilevazioneAccessi.Data
{
    public interface IDataAccess
    {
        // Ritorna la lista di tutti gli accessi del db
        IEnumerable<Accessi> ListAccessi();

        void SaveEntrance(int id, string gate);
        void SaveExit(int id, string gate);

        // Partendo dall'id di un tipoAccesso ne ricava il valore dal DB
        string GetValore(int id);
    }
}
