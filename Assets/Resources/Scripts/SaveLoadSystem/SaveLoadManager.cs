using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager
{
    private static readonly string _filePath = Application.persistentDataPath + "save.save";
    private static Dialog[] dialogs = Array.Empty<Dialog>();

    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Create);
        Save save = new Save();
        save.SaveDialogs(dialogs);
        save.SavePlotInfluences(DialogsManager.Instance.PlotInfluences);
        bf.Serialize(fs, save);
        fs.Close();
    }

    public static Save LoadGame()
    {
        Save save;
        if (!File.Exists(_filePath))
        {
            save = new Save();
            InitializeSaveData(save);
            return save;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Open);
        save = (Save)bf.Deserialize(fs);
        fs.Close();
        return save;
    }

    private static void InitializeSaveData(Save save)
    {
        save.SavePlotInfluences(new Dictionary<PlotInfluence, int>()
        {
            { PlotInfluence.Family, 0 },
            { PlotInfluence.Balance, 0 },
            { PlotInfluence.State, 0 },
        });
    }
}
