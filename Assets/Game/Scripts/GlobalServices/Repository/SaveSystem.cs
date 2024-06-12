namespace Game.Scripts.GlobalServices.Repository
{
    using System.IO;
    using UnityEngine;

    public static class SaveSystem
    {
        public static void Set(string key, string value) =>
            File.WriteAllText(GetPath(key), value);

        public static bool Get(string key, out string value)
        {
            value = !File.Exists(GetPath(key)) ? null : File.ReadAllText(GetPath(key));
            return value != null;
        }

        private static string GetPath(string key) => Path.Combine(Application.persistentDataPath, key);
    }
}