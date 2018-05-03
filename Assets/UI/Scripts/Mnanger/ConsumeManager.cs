using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumeManager : MonoBehaviour,IPointerDownHandler{

    private GameObject ObjectPool;
    private ObjectPool m_ObjectPool;
    private StoreItems m_StoreItems;

    private Transform m_Transform;

    private Text itemNum;
    private Text m_text;

	void Start () {
        ObjectPool = GameObject.Find("ObjectPool");
        m_ObjectPool = ObjectPool.GetComponent<ObjectPool>();
        m_StoreItems = ObjectPool.GetComponent<StoreItems>();

        m_Transform = gameObject.GetComponent<Transform>();

        //找到子物体身上的text组件
        itemNum = gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>();
        m_text = GameObject.Find("ItemInfo").GetComponent<Text>();
	}
	
    //当控件被点击时调用
    public void OnPointerDown(PointerEventData data)
    {   
        //按下鼠标右键时被消耗 
        if (Input.GetMouseButtonDown(1))
        {
            int num = int.Parse(itemNum.text);
            //数量大于1，则数量减1，否则直接销毁
            if (num > 1)
            {
                num--;
                itemNum.text = num + "";
            }
            else
            {
                //Debug.Log("消耗该物品");
                //GameObject.Destroy(gameObject);
                m_ObjectPool.Return(gameObject);
                //该物品被消耗完，从存储器中删除
                m_StoreItems.Delete(gameObject.transform.parent.name);
                m_text.text = "";       //清空显示信息，已无该物品

                m_Transform.SetParent(ObjectPool.transform);
            }           
        }
    }
}
