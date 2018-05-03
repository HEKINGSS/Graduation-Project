using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestionManager : MonoBehaviour
{
    [SerializeField]
    private List<Toggle> toggleList;     //四个答题开关
    [SerializeField]
    private Text TM_Text;                //题目
    [SerializeField]
    private List<Text> DA_TextList;      //四个选项
    [SerializeField]
    private Text indexText;              //题号
    [SerializeField]
    private Text tipsText;               //提示文本
    [SerializeField]
    private GameObject tipsbtn;          //提示按钮
    [SerializeField]
    private InputField jumpInput;        //跳转框
    [SerializeField] 
    private Text crText;                 //正确率文本
    [SerializeField]
    private GameObject Over;                
    private int topicIndex = 0;//第几题
    private int topicMax = 0;//最大题数+1
    private int isRightNum = 0;//正确题数
    private int anserint = 0;//已经答过几题
    private List<bool> isAnserList = new List<bool>();
    /*****************读取txt数据******************/
    int x = 0;
    private static string url = "";
    string[][] ArrayX;       //所有题目以二维存放
    string[] lineArray;     //存放每一道题
    /*****************读取txt数据******************/
    /// <summary>
    /// #27FF02FF 绿色
    /// #FF0020FF 红色
    /// #FFAB08FF 橙色
    /// </summary>
    // Use this for initialization
    void Start () {
        TextCsv();
        toggleList[0].onValueChanged.AddListener(OnValChanged0);
        toggleList[1].onValueChanged.AddListener(OnValChanged1);
        toggleList[2].onValueChanged.AddListener(OnValChanged2);
        toggleList[3].onValueChanged.AddListener(OnValChanged3);
        Over.SetActive(false);
    }	
	// Update is called once per frame
	void Update () {
		
	}    
    //绑定到提示、前一题、后一题按钮
    public void Select_Answer(int index)
    {
        switch(index)
        {
            case 0://提示
                int idx = ArrayX[topicIndex].Length - 1;
                int n = int.Parse(ArrayX[topicIndex][idx]);
                string nM = "";
                switch(n)
                {
                    case 1:
                        nM = "A";
                        break;
                    case 2:
                        nM = "B";
                        break;
                    case 3:
                        nM = "C";
                        break;
                    case 4:
                        nM = "D";
                        break;
                }
                tipsText.text = "<color=#FFAB08FF>" + nM + "</color>";
                break;
            case 1://上一题
                if (topicIndex > 0)
                {
                    topicIndex--; 
                    TopicAnswer();
                } 
                else
                {
                    tipsText.text = "<color=#27FF02FF>" + "前面已经没有题目了！" + "</color>";
                    topicIndex = topicMax;
                }
                break;
            case 2://下一题
                if (topicIndex < topicMax - 1)
                { 
                    topicIndex++;
                    TopicAnswer();
                }
                else
                {
                    tipsText.text = "<color=#27FF02FF>" + "哎呀！已经是最后一题了。" + "</color>";
                    topicIndex = -1;
                }
                break;
        }
    }
    public void Toggle_Select(int index)
    {
        //Debug.Log(index);
        bool isRight = false;
        for (int x = 0;x < 4;x++)
        {
            if (x == index)
            {
                toggleList[x].isOn = true;
                int idx = ArrayX[topicIndex].Length - 1;
                int n = int.Parse(ArrayX[topicIndex][idx]) - 1;
                if (n == x)
                {
                    tipsText.text = "<color=#27FF02FF>" + "恭喜你，答对了！" + "</color>";
                    isRight = true;
                }
                else
                {
                    tipsText.text = "<color=#FF0020FF>" + "对不起，答错了！" + "</color>";
                    tipsbtn.SetActive(true);
                }
            }
            else toggleList[x].isOn = false;
        }
        if (isAnserList[topicIndex])//这道题已经答过一次
        {
        }
        else
        {
            anserint++;
            if (isRight)
            {
                isRightNum++;
            }
            else
            {

            }
            isAnserList[topicIndex] = true;
            crText.text = "正确率：" + ((float)isRightNum / anserint * 100 ) + "%";
            //Debug.Log("isRightNum + anserint" + isRightNum + " " + anserint);
        }     
    }
    void OnValChanged0(bool check)
    {
        //Debug.Log(check);
        if (check)
        {
            Toggle_Select(0);
        }
    }
    void OnValChanged1(bool check)
    {
        //Debug.Log(check);
        if (check)
        {
            Toggle_Select(1);
        }
    }
    void OnValChanged2(bool check)
    {
        //Debug.Log(check);
        if (check)
        {
            Toggle_Select(2);
        }
    }
    void OnValChanged3(bool check)
    {
        //Debug.Log(check);
        if (check)
        {
            Toggle_Select(3);
        }
    }
    //显示题目
    void TopicAnswer()
    {
        tipsbtn.SetActive(false);
        tipsText.text = "";
        for (int x = 0; x < 4; x++)
        {
            toggleList[x].isOn = false;
        }

        indexText.text = "第" + (topicIndex + 1) + "题(共18题)：";//第几题
        TM_Text.text = ArrayX[topicIndex][1];//题目

        int idx = ArrayX[topicIndex].Length - 3;//有几个选项
        for (int x = 0; x < idx;x++)
        {
            DA_TextList[x].text = ArrayX[topicIndex][x + 2];//选项
        }
    }   
    //跳转题目
    public void jumpVoid()
    {
        int x = int.Parse(jumpInput.text) - 1;
        if ( x >= 0 && x <= topicMax + 1)
        {
            topicIndex = x;
            TopicAnswer();
            jumpInput.text = "";
        } 
    }
    /*****************读取txt数据******************/
    void TextCsv()
    {
        //读取csv二进制文件  
        TextAsset binAsset = Resources.Load("q_content", typeof(TextAsset)) as TextAsset;
        //显示在GUITexture中  
        // GetComponent<GUIText>().text = binAsset.text;  

        //读取每一行的内容  
        lineArray = binAsset.text.Split("\r"[0]);

        //创建二维数组  
        ArrayX = new string[lineArray.Length][];

        //把csv中的数据储存在二维数组中  
        for (int i = 0; i < lineArray.Length; i++)
        {
            ArrayX[i] = lineArray[i].Split(":"[0]);
        }

        //通过索引即可得到数据内容  
        // Debug.Log(Array[0][1]);  
        //         Debug.Log(lineArray[1]);  
        //         Debug.Log(Array[2][1]);  
        string str = ArrayX[1][2];
        url = str;
        //Debug.Log(lineArray.Length);
        topicMax = lineArray.Length;
        for (int x = 0;x < topicMax + 1;x++)
        {
            isAnserList.Add(false);
        }

        TopicAnswer();//加载题目
    }
    /*****************读取txt数据******************/

    //按钮事件
    public void SubmitOnClick()
    {
        Over.SetActive(true);
        if (isRightNum != (topicMax - 1))
        {
            Over.transform.GetChild(0).gameObject.GetComponent<Text>().text = "本次共答对" + isRightNum + "道题，再接再厉！！！";
        }
        else
        {
            Over.transform.GetChild(0).gameObject.GetComponent<Text>().text = "哇！你真是太棒了，快去实战练习吧！";
        }
    }
    public void AgainOnClick()
    {
        isRightNum = 0;
        topicIndex = 0;
        TopicAnswer();
        Over.SetActive(false);
    }
    public void ExitOnClick()
    {
        Application.LoadLevel("UIScene_01");
    }
}
