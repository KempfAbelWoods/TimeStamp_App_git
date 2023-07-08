using System.Collections.Generic;
using TimeStamp_App.DB;
using Newtonsoft.Json;


namespace TimeStamp_App.Helper;

public class Lists
{
    public List<Db_Tasks> Tasks { get; set; }
    public List<Db_Users> UsersList { get; set; }
}


public class ConnectionHelper
{

    public static Lists DeSerializeList(string response)
    {

        Lists lists = JsonConvert.DeserializeObject<Lists>(response);
        return lists;
        
    }
    
    public static string DeSerializeString(string response)
    {

        string stringResponse = JsonConvert.DeserializeObject<string>(response);
        return stringResponse;
        
    }

    public static void RW_Users_Tasks(Lists lists)
    {
        Rw_Tasks.Write(lists.Tasks, Paths.sqlite_path);
        Rw_Users.Write(lists.UsersList, Paths.sqlite_path);
    }
}        