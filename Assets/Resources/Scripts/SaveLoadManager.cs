using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string _filePath;
    [SerializeField]
    private Dialog[] dialogs;

    private void Start()
    {
        _filePath = Application.persistentDataPath + "save.save";
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Create);
        Save save = new Save();
        save.SaveDialogs(dialogs);
        bf.Serialize(fs, save);
        fs.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(_filePath)) return;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Open);
        Save save = (Save)bf.Deserialize(fs);
        fs.Close();
    }
}
