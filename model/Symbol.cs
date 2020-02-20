using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ConvertIPAddressToLocation.model
{
    public class Symbol
    {
        public ObjectId _id { get; set; }
        public string IP { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public DateTime dateEntry { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
    }
}