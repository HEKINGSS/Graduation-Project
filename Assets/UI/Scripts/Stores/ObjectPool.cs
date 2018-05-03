using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

    //对象池
    public static Dictionary<string, ArrayList> pool = new Dictionary<string, ArrayList>();

    //从对象池中取走一个对象
    public GameObject Get(string key, Vector3 position, Quaternion rotation)
    {
        GameObject go = null;
        string GameObjectName = key + "(Clone)";
        if (pool.ContainsKey(GameObjectName) && pool[GameObjectName].Count > 0)
        {           
            ArrayList list = pool[GameObjectName];
            go = list[0] as GameObject;
            list.RemoveAt(0);            //从列表中去除此对象，因为已被取走
            go.SetActive(true);
            go.transform.position = position;
            go.transform.rotation = rotation;
            //Debug.Log("取走一个" + pool[GameObjectName].Count);
        }
        else
        {
            //Debug.Log("无对象，创建");
            go = GameObject.Instantiate(Resources.Load("Prefabs/" + key), position, rotation) as GameObject;
        }
        return go;
    }

    //在对象池中放入一个对象
    public GameObject Return(GameObject go)
    {
        string key = go.name;
        if (pool.ContainsKey(key))
        {
            ArrayList list = pool[key];
            list.Add(go);
            //Debug.Log("对象池中+1:" + pool[key].Count);
        }
        else
        {
            pool[key] = new ArrayList { go };
        }
        go.SetActive(false);
        return go;
    }
}
