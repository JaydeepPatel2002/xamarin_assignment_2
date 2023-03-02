using System.IO;
using Xamarin.Forms;
[assembly: Dependency(typeof(assignment.Droid.FileHelper))]
namespace assignment.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
                
            return Path.Combine(path, filename);
        }
    }
}