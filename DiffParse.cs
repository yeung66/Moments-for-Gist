using System.IO;

namespace MomentForGist
{
    public class DiffParse
    {
        string diffFile = "diff";

        public string GetNewContent()
        {
            if(!File.Exists(diffFile)) return "";
            using(var file=File.OpenText(diffFile))
            {
                string line, newContent="";
                while((line=file.ReadLine())!=null && !line.StartsWith("@@"));

                while((line=file.ReadLine())!=null)
                {
                    if(line.StartsWith("+")) newContent+=line.Substring(1)+"\n";
                }

                return newContent;
                
            }
            
            
        }
    }
}