using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemBtManager : MonoBehaviour {
    private Transform m_transform;
    private Button m_bt;
    private Image m_img;
    private ZhaoChaManager m_manager;
	void Start () {
        m_manager = Camera.main.GetComponent<ZhaoChaManager>();
        m_transform = gameObject.GetComponent<Transform>();
        m_bt = m_transform.GetComponent<Button>();
        m_img = m_transform.GetComponent<Image>();
        m_bt.onClick.AddListener(
            delegate()
            {
                ItemBtOnClick();
            });
    }
	void Update () {
	
	}
    public void ItemBtOnClick()
    {
        string classAllName = m_img.overrideSprite.name;
        string crName = classAllName.Split('_')[0];
        //Debug.Log(crName);
        int i = 0;
        for (i = 0; i < 3; i++)
        {
            Text tarText = m_manager.Target[i].transform.FindChild("Text1").GetComponent<Text>();
            Text numText = m_manager.Target[i].transform.FindChild("Text2").GetComponent<Text>();
            string name = tarText.text;
            if (crName == name)      //找对物品
            {
                int num = int.Parse(numText.text.Split(' ')[1]);
                num--;               
                //更换图片
                int index = Random.Range(0, m_manager.allTex.Count);
                m_img.overrideSprite = m_manager.allTex[index];
                Vector3 pos = new Vector3(550, 320, 0);
                GameObject info = GameObject.Instantiate(Resources.Load("Prefabs/Texts/InfoImage") as GameObject, pos, Quaternion.identity) as GameObject;
                if (num == 0)
                {
                    tarText.text = m_manager.targetName[m_manager.GetRangeNum(4)];
                    numText.text = "x " + Random.Range(1, 3);
                    info.transform.FindChild("Text").GetComponent<Text>().text = tarText.text + numText.text;
                }
                else
                {
                    numText.text = "x " + num;
                    info.transform.FindChild("Text").GetComponent<Text>().text = crName + "x1";
                }
                
                info.transform.SetParent(m_manager.m_zhaocha.transform);
                Destroy(info, 1);
                break;
            }
        }
        if (i == 3)
            m_manager.FindFalse();
        else
            m_manager.FindTure();       
    }
}
