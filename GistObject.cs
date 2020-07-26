using System;

namespace MomentForGist.Model
{
    public class Gist
    {
        public string id {get;set;}
        public string html_url {get;set;}
        public string user {get;set;}
        public string description { get;set; }

        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}

    }

    public class File {
        public string filename {get;set;}
        public string type {get;set;}
        public string language {get;set;}
        public string content {get;set;}
    }
}