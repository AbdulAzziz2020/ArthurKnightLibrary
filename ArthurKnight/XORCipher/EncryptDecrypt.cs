using System.IO;
using UnityEngine;

namespace ArthurKnight.XORCipher
{
    public abstract class Data
    {
        public static string Encrypt<T>(T data, string key)
        {
            string json = JsonUtility.ToJson(data);
            string result = "";

            for (int i = 0; i < json.Length; i++) result += (char)(json[i] ^ key[i % key.Length]);

            Debug.Log($"<color=blue>Encrypt Success!</color>");
            return result;
        }
        
        public static T Decrypt<T>(string fullPath, string key) where T : class
        {
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                string result = "";

                for (int i = 0; i < json.Length; i++) result += (char)(json[i] ^ key[i % key.Length]);

                T data = JsonUtility.FromJson<T>(result);

                Debug.Log($"<color=blue>Decrypt Success!</color>");
                return data;
            }
            
            else
            {
                Debug.Log($"<color=red>File Doesn't Exist!</color>");
                return null;
            }
        }

        public static void SafetySave<T>(T data, string directory, string filename, string key)
        {
            string dir = Application.persistentDataPath + directory;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            string json = Encrypt(data, key);
            File.WriteAllText(dir + filename, json);
            
            Debug.Log($"<color=green>Save Successfull!</color>");
        }

        public static T SafetyLoad<T>(string directory, string filename, string key) where T : class
        {
            string fullpath = Application.persistentDataPath + directory + filename;

            T load = Decrypt<T>(fullpath, key);
            
            Debug.Log($"<color=yellow>File Loaded!</color>");
            return load;
        }
    }
}