using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthurKnight.Pooler
{
    public abstract class ObjectPool
    {
        #region Initialization

        #region Generic

        //Object need parent
        public static void Init<T>(T prefab, int size, List<T> list, Transform parent) where T : Component
        {
            for (int i = 0; i < size; i++)
            {
                T obj = GameObject.Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                list.Add(obj);
            }
        }

        //Object no need parent
        public static void Init<T>(T prefab, int size, List<T> list) where T : Component
        {
            for (int i = 0; i < size; i++)
            {
                T obj = GameObject.Instantiate(prefab);
                obj.gameObject.SetActive(false);
                list.Add(obj);
            }
        }

        #endregion

        #region GameObject

        //Object need parent
        public static void Init(GameObject prefab, int size, List<GameObject> list, Transform parent)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                list.Add(obj);
            }
        }

        //Object no need parent
        public static void Init(GameObject prefab, int size, List<GameObject> list)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab);
                obj.gameObject.SetActive(false);
                list.Add(obj);
            }
        }

        #endregion

        #endregion

        #region GetObjectInPool

        #region Generic

        //Object need Parent
        public static T GetObjectInPool<T>(T prefab, bool expandable, List<T> list, Transform parent)
            where T : Component
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].gameObject.activeInHierarchy) return list[i];
            }

            if (expandable)
            {
                T obj = GameObject.Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                list.Add(obj);
            }

            return null;
        }

        //Object no need Parent
        public static T GetObjectInPool<T>(T prefab, bool expandable, List<T> list) where T : Component
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].gameObject.activeInHierarchy) return list[i];
            }

            if (expandable)
            {
                T obj = GameObject.Instantiate(prefab);
                obj.gameObject.SetActive(false);
                list.Add(obj);
            }

            return null;
        }

        #endregion

        #region GameObject

        //Object need Parent
        public static GameObject GetObjectInPool(GameObject prefab, bool expandable, List<GameObject> list,
            Transform parent)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].gameObject.activeInHierarchy) return list[i];
            }

            if (expandable)
            {
                GameObject obj = GameObject.Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                list.Add(obj);
            }

            return null;
        }

        //Object no need Parent
        public static GameObject GetObjectInPool(GameObject prefab, bool expandable, List<GameObject> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].gameObject.activeInHierarchy) return list[i];
            }

            if (expandable)
            {
                GameObject obj = GameObject.Instantiate(prefab);
                obj.gameObject.SetActive(false);
                list.Add(obj);
            }

            return null;
        }

        #endregion

        #endregion

        #region SetActiveObject

        #region Generic

        //Object need Parent
        public static void Activated<T>(T prefab, bool expandable, List<T> list, Transform parent, Transform origin)
            where T : Component
        {
            T obj = GetObjectInPool<T>(prefab, expandable, list, parent);
            if (obj != null)
            {
                obj.transform.position = origin.transform.position;
                obj.gameObject.SetActive(true);
            }
        }

        //Object no need Parent
        public static void Activated<T>(T prefab, bool expandable, List<T> list, Transform origin) where T : Component
        {
            T obj = GetObjectInPool<T>(prefab, expandable, list);
            if (obj != null)
            {
                obj.transform.position = origin.transform.position;
                obj.gameObject.SetActive(true);
            }
        }

        #endregion

        #region GameObject

        //Object need Parent
        public static void Activated(GameObject prefab, bool expandable, List<GameObject> list, Transform parent,
            Transform origin)
        {
            GameObject obj = GetObjectInPool(prefab, expandable, list, parent);
            if (obj != null)
            {
                obj.transform.position = origin.transform.position;
                obj.gameObject.SetActive(true);
            }
        }

        //Object no need Parent
        public static void Activated(GameObject prefab, bool expandable, List<GameObject> list, Transform origin)
        {
            GameObject obj = GetObjectInPool(prefab, expandable, list);
            if (obj != null)
            {
                obj.transform.position = origin.transform.position;
                obj.gameObject.SetActive(true);
            }
        }

        #endregion

        #endregion
    }
}
