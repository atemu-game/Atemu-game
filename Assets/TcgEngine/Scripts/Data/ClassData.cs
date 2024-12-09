using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TcgEngine
{
    /// <summary>
    /// Defines all Class data 
    /// </summary>

    [CreateAssetMenu(fileName = "ClassData", menuName = "TcgEngine/ClassData", order = 1)]
    public class ClassData : ScriptableObject
    {
        public string id;
        public string title;
        public Sprite icon;

        public static List<ClassData> class_list = new List<ClassData>();

        public static void Load(string folder = "")
        {
            if (class_list.Count == 0)
                class_list.AddRange(Resources.LoadAll<ClassData>(folder));
        }

        public static ClassData Get(string id)
        {
            foreach (ClassData classData in GetAll())
            {
                if (classData.id == id)
                    return classData;
            }
            return null;
        }

        public static List<ClassData> GetAll()
        {
            return class_list;
        }
    }
}