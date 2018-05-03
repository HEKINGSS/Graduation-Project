using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//挂载在装备上，实现拖拽交换，拖拽事件
public class DragMove : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

    private Transform m_Transform;
    private CanvasGroup m_CanvasGroup;
    private StoreItems m_StoreItems;

    private GameObject ObjectPool;
    private Transform TempTransform;
    private Transform TempParent;

    private string parentName;

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        
        m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();
        m_StoreItems = GameObject.Find("ObjectPool").GetComponent<StoreItems>();
        ObjectPool = GameObject.Find("ObjectPool");      
	}
	
	void Update () {
	
	}

    public void OnBeginDrag(PointerEventData data)
    {
        parentName = m_Transform.parent.name;          //更新父亲的名称
        TempTransform = m_Transform;
        TempParent = m_Transform.parent;
        m_Transform.SetParent(ObjectPool.GetComponent<Transform>());
        //Debug.Log("拖拽开始"+ObjectPool.transform.childCount);
        
    }
    public void OnDrag(PointerEventData data)
    {        
        m_Transform.position = data.position;
        m_CanvasGroup.blocksRaycasts = false;
        m_Transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }
    public void OnEndDrag(PointerEventData data)
    {
        Transform newPos = data.pointerCurrentRaycast.gameObject.transform;
        if (newPos.tag == "Cell")
        {
            //更新父亲节点的信息
            BaseItem temp1 = m_StoreItems.GetItem(parentName);
            m_StoreItems.Delete(parentName);
            m_StoreItems.Store(newPos.name, temp1);

            m_Transform.position = newPos.position;
            m_Transform.SetParent(newPos);
        }
        if (newPos.tag == "Item")
        {
            //更新显示信息，先把两个的父亲信息删除,互相交换并存入
            BaseItem temp1 = m_StoreItems.GetItem(parentName);
            BaseItem temp2 = m_StoreItems.GetItem(newPos.parent.name);
            m_StoreItems.Delete(parentName);
            m_StoreItems.Delete(newPos.parent.name);
            //交换父亲，存
            m_StoreItems.Store(newPos.parent.name, temp1);
            m_StoreItems.Store(parentName, temp2);       

            m_Transform.SetParent(newPos.parent);
            m_Transform.position = newPos.parent.position;
            //Debug.Log(m_Transform.parent.name);
            newPos.SetParent(TempParent);
            newPos.position = TempParent.position;
            //Debug.Log(newPos.parent.name);

            TempTransform = m_Transform;
            TempParent = m_Transform.parent;    //重置物品的父物体
        }
        else
        {
            m_Transform.position = TempTransform.position;
        }
        m_Transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        m_CanvasGroup.blocksRaycasts = true;
    }
}
