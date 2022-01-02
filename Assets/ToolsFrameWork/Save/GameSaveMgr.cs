using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public class GameSaveMgr : Singleton<GameSaveMgr>
{
    public void SaveGame<T>(string fileName,T obj)
    {
        if (!Directory.Exists(Application.persistentDataPath + PathCfg.PATH_GAME_SAVE_DATA))
        {
            Directory.CreateDirectory(Application.persistentDataPath + PathCfg.PATH_GAME_SAVE_DATA);
        }

        //二进制转化
        BinaryFormatter formater = new BinaryFormatter();
        //创建文件
        FileStream file = File.Create(Application.persistentDataPath + PathCfg.PATH_GAME_SAVE_DATA + "/" + fileName); 
        //将数据变成json格式保存在json变量
        var json = JsonUtility.ToJson(obj);
        //序列化
        formater.Serialize(file, json);

        file.Close();
    }

    public T LoadGame<T>(string fileName) where T:new()
    {
        T t = new T();
        BinaryFormatter bf = new BinaryFormatter();

        //如果文件存在
        if (File.Exists(Application.persistentDataPath + PathCfg.PATH_GAME_SAVE_DATA + "/" + fileName))
        {
            //打开文件
            FileStream file = File.Open(Application.persistentDataPath + PathCfg.PATH_GAME_SAVE_DATA + "/" + fileName, FileMode.Open);

            //反序列化
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), t);

            file.Close();
        }
        return t;
    }


}
