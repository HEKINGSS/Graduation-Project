using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//存储物品的信息类
public class StoreItems : MonoBehaviour {

    public static Dictionary <string ,BaseItem> ItemStore = new Dictionary<string,BaseItem>();

    //存储一个物品
    public void Store(string key,BaseItem baseItem)
    {
        //如果含有此名称的类，不添加
        if (ItemStore.ContainsKey(key))
        {
            return;
        }
        else
        {
            ItemStore.Add(key, baseItem);
            //Debug.Log("加入："+key + baseItem.Name);
        }
    }

    //删除字典中的名为“key”的物品
    public void Delete(string key)
    {
        if (ItemStore.ContainsKey(key))
        {
            ItemStore.Remove(key);
            //Debug.Log("已无物品" + key);
        }
        else
            return;
    }

    public BaseItem GetItem(string key)
    {
        if (ItemStore.ContainsKey(key))
        {
            return ItemStore[key];
        }
        else
            return null;
    }
}
