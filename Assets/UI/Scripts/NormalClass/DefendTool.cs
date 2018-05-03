using UnityEngine;
using System.Collections;

//保护工具类，继承BaseItem类
public class DefendTool : BaseItem
{
     public int DefendIndex          //保护力（1--100）
    {
        get;
        private set;
    }
     public string DFdes             //保护功能描述
     {
         get;
         private set;
     }

     public DefendTool(int id, string name, string ename, string description, int score, string icon,
        int index, string des)
         : base(id, name, ename, description, score, icon)
    {
        this.DefendIndex = index;
        this.DFdes = des; ;
    }

}
