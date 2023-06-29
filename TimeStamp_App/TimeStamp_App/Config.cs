using System.IO;

namespace TimeStamp_App
{
    public static class Paths
    {
        public static string sqlite_path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "data.sqlite");
    }
    public static class Config
    {
        public static string enteredUser="";
        public static string enteredPassword="";
        public static string ConnectionCode = "";
    }
}