using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KnapsackManager : MonoBehaviour {

    public GameObject[] UCells;              //直接引用效率最高
   
    private Transform m_Transform;
    private GameObject Item;
    private GameObject ObjectPoolParent;
    private ObjectPool m_ObjectPool;
    private StoreItems m_StoreItems;
    public Image imageInfo;
    private Vector3 startpos;

    private Image itemImage;
    private Text itemNum;

    //存储少量数据，<>中的类型可以是任意类型
    public Dictionary<int, BaseItem> ItemList;  //Dictionary集合必须导引入System.Collections.Generic命名空间

    void Awake()
    {
        InitDictionary();    //一开始便唤醒并保留
    }
	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        ObjectPoolParent = GameObject.Find("ObjectPool");
        m_ObjectPool = ObjectPoolParent.GetComponent<ObjectPool>();
        m_StoreItems = ObjectPoolParent.GetComponent<StoreItems>();
        startpos = imageInfo.rectTransform.position;
        
	}
	
	void Update () {
        // 从摄像机开始，到屏幕触摸点，发出一条射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 撞击到了哪个3D物体
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            name = hit.transform.name;     //记录当前物体的名字
            if (hit.transform.tag == "pack")
            {
                Image image;
                Text text;
                image = imageInfo.transform.FindChild("Image").GetComponent<Image>();
                text = imageInfo.transform.FindChild("Text").GetComponent<Text>();
                int index;   
                for (index = 0; index < ItemList.Count; index++)
                {
                    if (name == ItemList[index].EgName)  //找到与当前物体匹配的类对象
                        break;
                }
                image.overrideSprite = Resources.Load(ItemList[index].Icon, typeof(Sprite)) as Sprite;
                text.text = ItemList[index].Name;
                imageInfo.rectTransform.position = Input.mousePosition +new Vector3(0,0.2f,0);
                /*pickInfo.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    Pickup(ItemList[index]);                  
                    Debug.Log("已拾取");
                }*/
            }
            else
            {
                imageInfo.rectTransform.position = startpos;
                //pickInfo.SetActive(false);
            }
        }
       
	}
    private void InitDictionary()   //初始化Dictionary集合，存储五种物品
    {
        ItemList = new Dictionary<int, BaseItem>();
        OutfireTool extinguisher = new OutfireTool(0, "灭火器","extinguisher","拔出保险销，对准火源根部，按下压把喷射灭火", 5, "Pictures/pic01", 100, "易燃、可燃液体、气体及带电设备的初起火灾");
        OutfireTool bucket = new OutfireTool(1, "水桶","bucket","装满水灭火", 3, "Pictures/pic02", 70, "不能用于电和油引发的火灾");
        OutfireTool glass = new OutfireTool(2, "水杯", "glass","倒水灭火", 1, "Pictures/pic03", 30, "不能用于电和油引发的火灾");
        OutfireTool pot_cover = new OutfireTool(3, "锅盖","pot_cover","快速盖于锅上，隔绝氧气", 3, "Pictures/pic04", 50, "锅中着火，且火势不大");

        DefendTool quilt = new DefendTool(4, "被子","quilt", "将被子打湿，裹在身上", 5, "Pictures/pic05", 100, "大型火灾");
        DefendTool towel = new DefendTool(5, "毛巾", "towel","用水打湿，捂住口鼻", 4, "Pictures/pic06", 100, "小型火灾");

        HelpTool phone = new HelpTool(6, "手机", "phone","拨打火警电话119求救", 5, "Pictures/pic07", 80);

        //将实例化的物品加入Dictiona集合中
        ItemList.Add(extinguisher.Id,extinguisher);
        ItemList.Add(bucket.Id, bucket);
        ItemList.Add(glass.Id, glass);
        ItemList.Add(pot_cover.Id, pot_cover);
        ItemList.Add(quilt.Id, quilt);
        ItemList.Add(towel.Id, towel);
        ItemList.Add(phone.Id, phone);
    }

    //拾取方法，动态加载物品,将传入的物品显示在合适的位置
    public void Pickup(BaseItem baseItem)
    {
        //Debug.Log(baseItem.Name);
        //Item = GameObject.Instantiate(Resources.Load("Prefabs/UItem") as GameObject, m_Transform.position, Quaternion.identity) as GameObject;    //实例化一个物品
        
        Item = m_ObjectPool.Get("UItem", m_Transform.position, Quaternion.identity);    //不再实例化，而是在对象池中取对象
        
        itemImage = Item.GetComponent<Image>();
        //将Item上的Image组件中的精灵图片通过动态加载,具体化baseItem的图片类型
        itemImage.overrideSprite = Resources.Load(baseItem.Icon, typeof(Sprite)) as Sprite;
        for (int i = 0; i < UCells.Length; i++)
        {
            if (UCells[i].transform.childCount > 0)      //如果子物体数量大于0且与baseItem的图片名称相同，则数量加1
            {               
                //直接num+++，并且销毁当前物品,没必要重复生成
                if (UCells[i].transform.GetChild(0).transform.GetComponent<Image>().overrideSprite.name
                    == itemImage.overrideSprite.name)
                {
                    //Debug.Log(UCells[i].transform.childCount);
                    itemNum = UCells[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
                    int num = int.Parse(itemNum.text) + 1;    //将string类型转化为int
                    itemNum.text = num + "";

                    //Destroy(Item); 
                    m_ObjectPool.Return(Item);         //不再销毁，而是放入对象池，隐藏
                    Item.transform.SetParent(ObjectPoolParent.transform);
                    
                    break;
                }
            }
            else     //没有子物体，则直接放入
            {
                //Debug.Log(UCells[i].name);
                Item.transform.position = UCells[i].transform.position;
                Item.transform.SetParent(UCells[i].transform);

                m_StoreItems.Store(UCells[i].name, baseItem);

                break;
            }
        }
    }
}
