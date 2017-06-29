using Dapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RilevazioneAccessi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RilevazioneAccessi.Data
{
    public class DataAccess : IDataAccess
    {
        public string connectionString { get; private set; }

        private readonly string uri = "https://pw2017.mvlabs.it/";
        private readonly string checkSuspect = "check/";
        public DataAccess(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public List<Utente> GetUtenti()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);

                return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<Utente>>(response.Result)).Result;

                //la riga sotto è stata commentata perchè definita obsoleta da visual studio
                //return JsonConvert.DeserializeObjectAsync<List<Utente>>(response.Result).Result;               
            }
        }
        
        public Sospettato GetUtenteById(int id)
        {
            string suspectUrl = uri + checkSuspect + id;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(suspectUrl);

                return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Sospettato>(response.Result)).Result;           
            }
        }

        public IEnumerable<Accessi> ListAccessi()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                return connection.Query<Accessi>(@"
SELECT A.[Id]
      ,[Id_utente]
      ,[DataOra]
      ,[Gate]
      ,[Sospettato]
      ,[Id_tipoAccesso]
      ,TA.Valore
  FROM [PrismaCode].[dbo].[Accessi] A
  join [PrismaCode].[dbo].[TipiAccesso] TA
  on (A.Id_tipoAccesso = TA.Id)");
            }

        }

        public IEnumerable<Utente> ListAccessi(DateTime dataDa, DateTime dataA, string cognome, string nome) //DA MIGLIORARE
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                return connection.Query<Utente> ($@"
  SELECT U.ID,
      U.[Surname]
      ,U.[Name]
      ,U.[BirthDate]
	  , A.DataOra
  FROM [PrismaCode].[dbo].[Utenti] U
  INNER JOIN Accessi A
  ON A.Id_utente = U.ID
  WHERE A.DataOra BETWEEN '{dataDa}' AND {dataA}
OR U.Surname = '{cognome}' 
OR U.Name = '{nome}' ");              

            }
        }

        public void InsertUtenti()
        {
            var list = new List<Utente>();

            list = GetUtenti();

            foreach (var item in list)
            {
                using (var connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    connection.Query(@"
INSERT INTO Utenti(ID, Surname, Name, BirthDate)
VALUES (@ID, @Surname, @Name, @BirthDate)", item);
                }
            }
        }

        public string GetValore(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<string>($@"
SELECT Valore
  FROM [PrismaCode].[dbo].[TipiAccesso]
  WHERE Id = {id}");
            }

        }

        public void SaveEntrance(int id, string gate)
        {
            var accesso = new Accessi()
            {
                DataOra = new DateTime(),
                Gate = gate,
                Id_tipoaccesso = 1, // entrata
                Id_utente = id, 
                Sospettato = false                
            };


        }

        public void SaveExit(int id, string gate)
        {
            var uscita = new Accessi()
            {
                DataOra = new DateTime(),
                Gate = gate,
                Id_tipoaccesso = 2, // uscita
                Id_utente = id,
                Sospettato = false
            };
        }


        public bool isAuthenticated(string user, string passwd)
        {
            string rightUser = "admin";
            string rightPassword = "Password1";

            return (user == rightUser && passwd == rightPassword);
        }
    }
}
