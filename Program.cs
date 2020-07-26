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

            await client.UpdateGist(client.gistToken, new File(){
                filename="Moment.md",
                type="text/plain",
                language="Markdown",
                content=diff
            });


        }


    }
}
