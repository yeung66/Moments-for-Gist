using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using MomentForGist.Model;
using System.Collections.Generic;

namespace MomentForGist
{
    public class GistClient
    {
        HttpClient client;
        string token;
        public string gistToken;
        readonly string baseApiUrl = @"https://api.github.com/gists/";

        public GistClient()
        {
            client = new HttpClient();
            GetConfig();
            client.DefaultRequestHeaders.Add("Accept","application/vnd.github.v3+json");
            client.DefaultRequestHeaders.Add("User-Agent","Mozilla/5.0 (Macintosh; Intel Mac OS X x.y; rv:42.0) Gecko/20100101 Firefox/42.0");
            client.DefaultRequestHeaders.Add("Authorization",$"token {token}");
        }

        public void GetConfig()
        {
            using(var file=System.IO.File.OpenText("config.json"))
            {
                var config = JsonSerializer.Deserialize<Dictionary<string,string>>(file.ReadToEnd());
                token = config["token"];
                gistToken=config["gistToken"];
            }
        }

        public async Task UpdateGist(string gistToken, File gistFile)
        {
            var dict = new Dictionary<string, object>();
            
            dict.Add("description","Latest Moments from My Blog.\nhttp://scottyeung.top/moments ");
            dict.Add("files", new Dictionary<string,object>(){
                {gistFile.filename,gistFile}
            });
            var postBody = JsonSerializer.SerializeToUtf8Bytes(dict);
            var postBodyTxt = JsonSerializer.Serialize(dict);
            
            var resp = await client.PatchAsync(baseApiUrl+gistToken, new ByteArrayContent(postBody));
            var content = await resp.Content.ReadAsStringAsync();
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateGistViaNewFile(string gistToken, File gistFile)
        {
            var oldGist = await GetGist(gistToken);
            var files = oldGist["files"];

            ((Dictionary<string,object>)files).Add(gistFile.filename,gistFile);

            var dict = new Dictionary<string,object>();
            dict.Add("description","Latest Moments from My Blog.\nhttp://scottyeung.top/moments ");
            dict.Add("files", files);

            var postBody = JsonSerializer.SerializeToUtf8Bytes(dict);

            var resp = await client.PatchAsync(baseApiUrl+gistToken, new ByteArrayContent(postBody));
            var content = await resp.Content.ReadAsStringAsync();
            resp.EnsureSuccessStatusCode();
        }

        public async Task<Dictionary<string,object>> GetGist(string gistToken)
        {
            var resp = await client.GetAsync(baseApiUrl+gistToken);
            resp.EnsureSuccessStatusCode();
            var gist = JsonSerializer.Deserialize<Dictionary<string,object>>(await resp.Content.ReadAsStringAsync());
            
            return gist;
            
        }
    }
}