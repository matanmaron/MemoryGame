using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class DataStorage
{
    #region persisted
    //persisted save n load
    //string
    public static void SavePersisted(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
        PlayerPrefs.Save();
    }
    public static string LoadPersisted(string key, string defVal)
    {
        return PlayerPrefs.GetString(key, defVal);
    }
    //bool
    public static void SavePersisted(string key, bool val)
    {
        if (val == false)
        {
            SavePersisted(key, 0);
        }
        SavePersisted(key, 1);
    }
    public static bool LoadPersisted(string key, bool defVal)
    {
        if (LoadPersisted(key, defVal == false ? 0 : 1) == 0)
        {
            return false;
        }
        return true;
    }
    //int
    public static void SavePersisted(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
        PlayerPrefs.Save();
    }
    public static int LoadPersisted(string key, int defVal)
    {
        return PlayerPrefs.GetInt(key, defVal);
    }
    //float
    public static void SavePersisted(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
        PlayerPrefs.Save();
    }
    public static float LoadPersisted(string key, float defVal)
    {
        return PlayerPrefs.GetFloat(key, defVal);
    }
    #endregion persisted

    #region nonpersisted
    //regular save n load
    public static void SaveNonPersisted<T> (T obj, int index)
    {
        string json = JsonUtility.ToJson(obj);
        StringBuilder path = new StringBuilder(Application.dataPath + "/save");
        if (!Directory.Exists(path.ToString()))
        {
            Directory.CreateDirectory(path.ToString());
        }
        path.Append("/ game" + index + ".sav");
        File.WriteAllText(path.ToString(), json);
    }
    public static T LoadNonPersisted<T>(int index)
    {
        StringBuilder path = new StringBuilder(Application.dataPath + "/save");
        if (!Directory.Exists(path.ToString()))
        {
            Directory.CreateDirectory(path.ToString());
        }
        path.Append("/ game" + index + ".sav");
        if (File.Exists(path.ToString()))
        {
            string json = File.ReadAllText(path.ToString());
            T obj = JsonUtility.FromJson<T>(json);
            return obj;
        }
        return default;
    }
    #endregion nonpersisted
}
