using System;
using System.Threading.Tasks;
using MomentForGist.Model;

namespace MomentForGist
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new GistClient(); 

            var parse = new DiffParse();
            var diff = parse.GetNewContent();
            var now = DateTime.Now;

            if(string.IsNullOrEmpty(diff)) return;

            await client.UpdateGist(client.gistToken, new File(){
                filename=$"Moment-{now.Year}{now.Month:00}{now.Day:00}.md",
                type="text/plain",
                language="Markdown",
                content=diff
            });

            Console.Write(diff);


        }


    }
}
