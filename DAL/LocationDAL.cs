using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Core.Servers;
using Newtonsoft.Json;
using System.Collections;
using MongoDB.Driver.Core;
using ConvertIPAddressToLocation.model;


namespace ConvertIPAddressToLocation.Helpers
{
    public class LocationDAL


    {
        public Symbol saveSymbolToDB(Symbol symbol)
        {
          
      /// MONGO CODE
                MongoServer mongoServer = getServer();
                MongoDatabase mongoDatabase = getDatabase(mongoServer);
                MongoCollection symbolCollection = getCollection(mongoDatabase);

                Console.WriteLine("Inserting document to collection............");
                try
                {
                    symbolCollection.Insert(symbol);
                    Console.WriteLine(symbolCollection.Count().ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to insert into collection of Database " + mongoDatabase.Name);
                    Console.WriteLine("Error :" + ex.Message);
                }

               
                return symbol;
        }

        private MongoServer getServer()
        {
            Console.WriteLine("Mongo DB Test Application");
            string connectionString = "mongodb://localhost:27017";


            Console.WriteLine("Creating Client..........");
            MongoClient client2 = null;
            try
            {
                client2 = new MongoClient(connectionString);
                Console.WriteLine("Client Created Successfuly........");
                Console.WriteLine("Client: " + client2.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Filed to Create Client.......");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Initianting Mongo Db Server.......");
            MongoServer server = null;
            try
            {
                Console.WriteLine("Getting Servicer object......");
                server = client2.GetServer();

                Console.WriteLine("Server object created Successfully....");
                Console.WriteLine("Server :" + server.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Filed to getting Server Details");
                Console.WriteLine(ex.Message);
            }

            return server;

        }

        private MongoDatabase getDatabase(MongoServer server)
        {

            Console.WriteLine("Initianting Mongo Databaser.........");
            MongoDatabase database = null;
            try
            {
                Console.WriteLine("Getting reference of database.......");
                database = server.GetDatabase("Kailash");
                Console.WriteLine("Database Name : " + database.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to Get reference of Database");
                Console.WriteLine("Error :" + ex.Message);
            }

            return database;
        }

        private MongoCollection getCollection(MongoDatabase database)
        {
            MongoCollection symbolcollection = null;
            try
            {
                symbolcollection = database.GetCollection<Symbol>("Symbols");
                Console.WriteLine(symbolcollection.Count().ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to Get collection from Database");
                Console.WriteLine("Error :" + ex.Message);
            }
            return symbolcollection;

        }

        public IEnumerable<Symbol> getLocations(string date, string country)
        {
            /// MONGO CODE
            MongoServer mongoServer = getServer();
            MongoDatabase mongoDatabase = getDatabase(mongoServer);
            MongoCollection symbolCollection = getCollection(mongoDatabase);
            IEnumerable<Symbol> list = null;
            if (date != null&&country!=null)
            {
                DateTime newDate = DateTime.Parse(date);
                var queryBuilder = new MongoDB.Driver.Builders.QueryBuilder<Symbol>();
                var query = queryBuilder.Where(s => s.dateEntry>= newDate && s.Country == country);
                list = symbolCollection.FindAs<Symbol>(query);
            }
            else if (country != null)
            {
                var queryBuilder = new MongoDB.Driver.Builders.QueryBuilder<Symbol>();
                var query = queryBuilder.Where(s=>s.Country==country);
                list = symbolCollection.FindAs<Symbol>(query);

            }

            else if (date != null)
            {
                DateTime newDate = DateTime.Parse(date);
                var queryBuilder = new MongoDB.Driver.Builders.QueryBuilder<Symbol>();
                var query = queryBuilder.Where(s => s.dateEntry>= newDate);
                list = symbolCollection.FindAs<Symbol>(query);

            }
            else
            {
                list = symbolCollection.FindAllAs<Symbol>();
            }
            //IEnumerable<Symbol> list = symbolCollection.FindAs<Symbol>(s => s.entryDate >= newDate);
            return list;      
        }
    }
}
