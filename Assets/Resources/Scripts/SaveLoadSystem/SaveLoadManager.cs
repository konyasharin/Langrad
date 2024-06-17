using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Resources.Scripts.DialogSystem;
using Resources.Scripts.ServiceLocatorSystem;
using UnityEngine;

namespace Resources.Scripts.SaveLoadSystem
{
    public class SaveLoadManager : IService
    {
        private static readonly string FilePath = Application.persistentDataPath + "save.save";
        
        private readonly Dialog[] _dialogs = Array.Empty<Dialog>();
        private readonly DialogsManager _dialogsManager = ServiceLocator.Instance.Get<DialogsManager>();

        public void SaveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(FilePath, FileMode.Create);
            Save save = new Save();
            save.SaveDialogs(_dialogs);
            save.SavePlotInfluences(_dialogsManager.PlotInfluences);
            bf.Serialize(fs, save);
            fs.Close();
        }

        public Save LoadGame()
        {
            Save save;
            if (!File.Exists(FilePath))
            {
                save = new Save();
                InitializeSaveData(save);
                return save;
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(FilePath, FileMode.Open);
            save = (Save)bf.Deserialize(fs);
            fs.Close();
            return save;
        }

        private void InitializeSaveData(Save save)
        {
            save.SavePlotInfluences(new Dictionary<PlotInfluenceType, int>()
            {
                { PlotInfluenceType.Family, 0 },
                { PlotInfluenceType.Balance, 0 },
                { PlotInfluenceType.State, 0 },
            });
        }
    }
}
