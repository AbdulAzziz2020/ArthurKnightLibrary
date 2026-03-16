using UnityEditor;
using UnityEngine;
using static System.IO.Directory;
using static System.IO.Path;
using Application = UnityEngine.Device.Application;

namespace ArthurKnight
{
#if UNITY_EDITOR
    public static class _Setup
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Folders.CreateDefault("",
                "Animation", "Prefabs", "Scriptables",
                "Scripts", "Scenes", "Settings", "Inputs", "Sprites"
            );
        }

        static class Folders
        {
            public static void CreateDefault(string root, params string[] folders)
            {
                var fullpath = Combine(Application.dataPath, root);
                for (int i = 0; i < folders.Length; i++)
                {
                    var folder = folders[i];
                    var path = Combine(fullpath, folder);
                    if (!Exists(path))
                        CreateDirectory(path);
                }
            }
        }
        
        [MenuItem("Tools/Find Missing Scripts In Scene")]
        public static void Find()
        {
            GameObject[] objects = GameObject.FindObjectsOfType<GameObject>(true);

            foreach (var go in objects)
            {
                var components = go.GetComponents<Component>();

                for (int i = 0; i < components.Length; i++)
                {
                    if (components[i] == null)
                    {
                        Debug.LogError($"Missing Script found in: {GetFullPath(go)}", go);
                    }
                }
            }
        }

        static string GetFullPath(GameObject obj)
        {
            string path = obj.name;
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = obj.name + "/" + path;
            }
            return path;
        }
    }
#endif
}