using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Application = UnityEngine.Device.Application;

namespace Resources.Scripts.SaveLoadSystem
{
    public class JsonSaveService
    {
        public void Save(string saveFileName, object data)
        {
            string json = JsonConvert.SerializeObject(data);

            using StreamWriter sw = new StreamWriter(BuildPath(saveFileName));
            sw.Write(json);
        }

        public T Load<T>(string saveFileName)
        {
            using StreamReader sr = new StreamReader(BuildPath(saveFileName));
            return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
        }

        private string BuildPath(string saveFileName)
        {
            return Path.Combine(Application.persistentDataPath, saveFileName);
        }
    }
}