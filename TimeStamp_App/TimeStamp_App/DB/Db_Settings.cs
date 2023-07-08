﻿using System;
using System.Collections.Generic;
using SQLite;
using TimeStamp_App.Helper;

namespace TimeStamp_App.DB;

public class Db_Settings
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Ressource { get; set; }
    public string Comment { get; set; }

    public static Error CreateTable(string dataSource)
    {
        SQLiteConnection conn = null;

        try
        {
            conn = new SQLiteConnection(dataSource);
            conn.CreateTable<Db_Settings>();

            return null;
        }
        catch (Exception ex)
        {
            return new Error(ex.ToString());
        }
        finally
        {
            conn?.Close();
        }
    }
}