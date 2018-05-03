using UnityEngine;
using System.Collections;

//背包物品的基类（普通的类）
public class BaseItem {
    public int Id
    {   //get，set，方法的简写
        get;
        private set;
    }
    public string Name
    {
        get;
        private set;
    }
    public string EgName
    {
        get;
        private set;
    }
    public string Description    //使用方法
    {
        get;
        private set;
    }
    public int ControlScore     //消防指数值（1--5颗星）
    {
        get;
        private set;
    }
    public string Icon         //物品的类型(实质上存储图片的路径，动态加载的路径)
    {
        get;
        private set;
    }

    //构造函数
    public BaseItem(int id,string name,string ename,string description,int score,string icon)
    {
        this.Id = id;
        this.Name = name;
        this.EgName = ename;
        this.Description = description;
        this.ControlScore = score;
        this.Icon = icon;
    }	
}
