using UnityEngine;
using System.Collections;

//求救工具类，继承BaseItem类
public class HelpTool : BaseItem {

	    public int HpIndex          //灭火力（1--100）
    {
        get;
        private set;
    }
        public HelpTool(int id, string name, string ename, string description, int score, string icon,
        int index)
         : base(id, name, ename, description, score, icon)
    {
        this.HpIndex = index;
    }
}
