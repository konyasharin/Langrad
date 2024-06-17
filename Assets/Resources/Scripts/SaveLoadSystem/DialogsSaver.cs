using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Resources.Scripts.DialogSystem;
using Resources.Scripts.MVC;
using UnityEngine;

namespace Resources.Scripts.SaveLoadSystem
{
    public class DialogsSaver
    {
        private Dictionary<string, DialogStatus> _currentSave;
        private JsonSaveService _jsonSaveService = new();
        private static readonly string FilePath = Path.Combine(Application.persistentDataPath, "dialogs.save");

        public DialogsSaver()
        {
            Load();
        }

        public void Load()
        {
            if (!File.Exists(FilePath))
            {
                //_currentSave = _jsonSaveService.Load<>()
                //callback.Invoke(new Dictionary<string, DialogStatus>());
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            using FileStream fs = new FileStream(FilePath, FileMode.Open);
            //callback.Invoke((Dictionary<string, DialogStatus>)bf.Deserialize(fs));
        }

        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using FileStream fs = new FileStream(FilePath, FileMode.Create);
            
            bf.Serialize(fs, _currentSave);
        }

        /// <summary>
        /// Сохраняет диалог, на время (до закрытия игры), если не запустить метод Save.
        /// Нужен так как метод Save затратно постоянно вызывать.
        /// </summary>
        public void SaveDialog(DialogModel dialog)
        {
            if (!_currentSave.TryAdd(dialog.DialogScriptableObject.name, dialog.status))
            {
                _currentSave[dialog.DialogScriptableObject.name] = dialog.status;
            }
        }
        
        public void LoadDialog(DialogModel dialog)
        {
            if (_currentSave.TryGetValue(dialog.DialogScriptableObject.name, out var status))
            {
                dialog.status = status;
            }
        }
    }
}