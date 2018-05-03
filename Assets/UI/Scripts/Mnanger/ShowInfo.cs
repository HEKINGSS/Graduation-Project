using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

    private Text m_text;
    private StoreItems m_StoreItems; 

	void Start () {
        m_text = GameObject.Find("ItemInfo").GetComponent<Text>();
        m_StoreItems = GameObject.Find("ObjectPool").GetComponent<StoreItems>();
	}

	void Update () {
	
	}

    public void OnPointerEnter(PointerEventData data)
    {
        //Debug.Log(gameObject.transform.parent.name);
        BaseItem baseItem = m_StoreItems.GetItem(gameObject.transform.parent.name);
        m_text.text = baseItem.Name + "\n\n";
        m_text.text += baseItem.Description;
    }

    public void OnPointerExit(PointerEventData data)
    {
        m_text.text = "";
    }
}
