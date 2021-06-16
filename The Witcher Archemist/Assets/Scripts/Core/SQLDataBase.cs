using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using UnityEngine.Networking;
using Mono.Data.Sqlite;

public class SQLDataBase
{
    //public static 

    //public static void 

    public static string filepath = string.Empty;

    //streamingAsset에서 들고와서 DB를 생성합니다.
    public static void CreateDB()
    {
        filepath = Application.dataPath + "/WitchStoreDataBase.sqbpro";
        if (!File.Exists(filepath))
        {
            File.Copy(Application.streamingAssetsPath + "/WitchStoreDataBase.sqbpro", filepath);
            Debug.Log("생성완료");

        }
        else
        {
            Debug.Log("생성되어있음");

        }

    }

    public static void ConnDB()
    {
        CreateDB();
        try
        {
            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());//SqliteConnection(GetDBFilePath());
            dbConnection.Open();

            if(dbConnection.State == ConnectionState.Open)
            {
                Debug.Log("DB연결 성공");
            }
            else
            {
                Debug.Log("DB연결 실패");

            }
        }
        catch
        {

        }
    }

    public static void DataRead(string query)
    {
        //IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());//SqliteConnection(GetDBFilePath());
        //dbConnection.Open();
        //IDbCommand dbCommand = dbConnection.CreateCommand();
        //dbCommand.CommandText = "select * from ItemTable";
        //IDataReader dataReader = dbCommand.ExecuteReader();

        //while (dataReader.Read())
        //{
        //    Debug.Log(dataReader.GetInt32(0));
        //}
        

    }

    public static string GetDBFilePath()
    {
        string str = string.Empty;
        str = "URI=file:" + Application.dataPath + "/WitchStoreDataBase.sqbpro";
        return str;
    }
    
}
