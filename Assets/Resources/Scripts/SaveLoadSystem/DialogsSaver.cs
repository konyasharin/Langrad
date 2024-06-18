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
        private readonly JsonSaveService _jsonSaveService = new();
        private static readonly string FilePath = Path.Combine(Application.persistentDataPath, "dialogs.json");

        public DialogsSaver()
        {
            _currentSave = Load();
        }

        private Dictionary<string, DialogStatus> Load()
        {
            if (!File.Exists(FilePath))
            {
                return new();
            }

            return _jsonSaveService.Load<Dictionary<string, DialogStatus>>(FilePath);
        }

        private void Save()
        {
            _jsonSaveService.Save(FilePath, _currentSave);
        }
        
        public void SaveDialog(DialogModel dialog)
        {
            if (!_currentSave.TryAdd(dialog.DialogScriptableObject.name, dialog.status))
            {
                _currentSave[dialog.DialogScriptableObject.name] = dialog.status;
            }
            
            Save();
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