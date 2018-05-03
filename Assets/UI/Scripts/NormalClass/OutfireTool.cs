using UnityEngine;
using System.Collections;

//灭火工具类，继承BaseItem类
public class OutfireTool : BaseItem
{
     public int OFIndex          //灭火力（1--100）
    {
        get;
        private set;
    }
    public string OFdes             //灭火功能描述
    {
        get;
        private set;
    }

    public OutfireTool(int id, string name, string ename, string description, int score, string icon,
        int index, string des)
         : base(id, name, ename, description, score, icon)
    {
        this.OFIndex = index;
        this.OFdes = des;
    }
	
}
