 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ZhaoChaManager : MonoBehaviour {
    public GameObject parentImage;
    public List<Sprite> allTex = new List<Sprite>();  //保存所有的图片
    public int TotalTime=30;//总时间
    public Text TimeText;//在UI里显示时间
    public GameObject[] Target;
    public string[] targetName = { "灭火器", "消防栓", "消防人员", "插座", "电器" };
    public int score = 0;   //记录总成绩
    public GameObject m_zhaocha;
    public GameObject OverPanel;
    public GameObject music;
    public GameObject nomusic;
    public AudioClip over;
    private UIControl m_uicontrol;
    public GameObject canvas;

    int getNum = 0;
    int rangeNum = 0;
    
	void Start () { 
        OverPanel.SetActive(false);
        nomusic.SetActive(false);     
        m_uicontrol = canvas.GetComponent<UIControl>();
	}	
	void Update () {
        
	}
    public void StartGame()
    {
        StartCoroutine(StartTime());
        InitAllPic();
        InitTargetText();
        Camera.main.GetComponent<AudioSource>().Play();
    }
    public int GetRangeNum(int num)   //每次得到与上次不同的随机数
    {
        do
        {
            rangeNum = Random.Range(0, num);
        } while (getNum == rangeNum);
        getNum = rangeNum;
        return getNum;
    }
    //倒计时器
    public IEnumerator StartTime()
    {
        while (TotalTime > 0)
        {
            Debug.Log(TotalTime);
            yield return new WaitForSeconds(1);   //没经过一秒减1
            TotalTime--;
            TimeText.text = "00:"+TotalTime;
            if (TotalTime == 5)
            {
                parentImage.transform.GetComponent<AudioSource>().Play();
            }	
            if (TotalTime <= 0)
            {
                parentImage.transform.GetComponent<AudioSource>().Stop();
                GameOver();
            }
        }
    }
    //初始化所有图片
    void InitAllPic()
    {
        Object[] textures = Resources.LoadAll("ZhaoChaPic", typeof(Sprite));
        for (int i = 0; i < textures.Length; i++)
        {
            Sprite sprite = textures[i] as Sprite;           
            allTex.Add(sprite);
        }
        Vector3 pos = new Vector3(50, 450, 0);
        Vector3 offset = new Vector3(115, 0, 0);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 37; j++)
            {
                GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/Item") as GameObject, pos, Quaternion.identity) as GameObject;
                int index = Random.Range(0, allTex.Count);
                go.transform.GetComponent<Image>().overrideSprite = allTex[index];
                //Debug.Log(go.transform.GetComponent<Image>().overrideSprite.name);
                go.transform.SetParent(parentImage.transform);
                pos += offset;
            }
            pos.x = 50;
            pos.y -= 115;
        }
    }
    //初始化要寻找的目标物品
    void InitTargetText()
    {
        for (int i = 0; i < 3; i++)
        {
            Text tarText = Target[i].transform.FindChild("Text1").GetComponent<Text>();
            tarText.text = targetName[GetRangeNum(4)];
            Text numText = Target[i].transform.FindChild("Text2").GetComponent<Text>();
            numText.text = "x " + Random.Range(1, 3);
        }
    }
    //找对一个物品
    public void FindTure()
    {
        score++;
        TotalTime += 2;
        Vector3 pos = new Vector3(760, 530, 0); ;
        GameObject add = GameObject.Instantiate(Resources.Load("Prefabs/Texts/AddTime") as GameObject, pos, Quaternion.identity) as GameObject;
        add.transform.SetParent(m_zhaocha.transform);
        add.transform.GetComponent<AudioSource>().Play();
        Destroy(add, 1);
        Debug.Log("找对了");
    }
    //找错一个物品
    public void FindFalse()
    {
        TotalTime -= 2;
        Vector3 pos = new Vector3(760, 530, 0); ;
        GameObject sub = GameObject.Instantiate(Resources.Load("Prefabs/Texts/SubTime") as GameObject, pos, Quaternion.identity) as GameObject;
        sub.transform.SetParent(m_zhaocha.transform);
        sub.transform.GetComponent<AudioSource>().Play();
        Destroy(sub, 1);
        Debug.Log("找错了");
    }
    //游戏结束！
    public void GameOver()
    {
        TotalTime = 0;
        int count = parentImage.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(parentImage.transform.GetChild(i).gameObject);
        }           
        OverPanel.SetActive(true);
        OverPanel.transform.FindChild("OverImage").FindChild("Text2").GetComponent<Text>().text = "一共找对" + score + "个！";
        Camera.main.GetComponent<AudioSource>().Stop();
    }
    public void MusicBtOnClick()
    {
        music.SetActive(false);
        nomusic.SetActive(true);
    }
    public void NomusicBtOnClick()
    {
        music.SetActive(true);
        nomusic.SetActive(false);
    }
    //重新开始
    public void AgainGame()
    {
        score = 0;
        TotalTime = 30;
        StartCoroutine(StartTime());
        InitAllPic();
        InitTargetText();
        OverPanel.SetActive(false);
        nomusic.SetActive(false);
        Camera.main.GetComponent<AudioSource>().Play();
    }
    //退出游戏,回到主菜单
    public void ExitGame()
    {
        m_uicontrol.zhaocha.SetActive(false);
        m_uicontrol.ZCImage.SetActive(false);
        m_uicontrol.main_menu.SetActive(true);
        m_uicontrol.menuImage.SetActive(true);
    }
}
