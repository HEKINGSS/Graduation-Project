using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour {

    public GameObject talkPanel;
    public GameObject npcPanel;
    public GameObject mingPanel;
    public GameObject FpsController;
    public Text fireManText;
    public Text mingText;

    private RectTransform m_TalkPanelTransform;
    private RigidbodyFirstPersonController m_RigidbodyFirstPersonController;
    private string missionContent = "寻找物品";
    private string[] fireManTalkContent = { "消防员：小明，你好。", 
                                            "消防员：小明，你知道家中发生火灾了都可以用什么东西灭火呀？",
                                            "消防员：对的，快去家中找出这些东西。" };
    private string[] mingTalkContent = { "小明：你好，消防员叔叔。", 
                                         "小明：有灭火器，水等",
                                         "小明：好的！"};
    private int index1 = 0;
    private int index2 = 0;

    void Awake () {

    }

	// Use this for initialization
	void Start () {
        m_TalkPanelTransform = talkPanel.GetComponent<RectTransform>();
        m_RigidbodyFirstPersonController = FpsController.GetComponent<RigidbodyFirstPersonController>();
        fireManText.text = fireManTalkContent[index1];
        npcPanel.SetActive(true);
        mingPanel.SetActive(false);
        index1++;

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            if (npcPanel.activeSelf) {
                mingText.text = mingTalkContent[index2];
                npcPanel.SetActive(false);
                mingPanel.SetActive(true);
                index2++;
                if (index2 == mingTalkContent.Length) {
//                     index2 = 0;
                    m_TalkPanelTransform.anchoredPosition = new Vector2(m_TalkPanelTransform.anchoredPosition.x, m_TalkPanelTransform.anchoredPosition.y - 200);
                    CursorManager._instance.ReleaseCursor(true);
                    m_RigidbodyFirstPersonController.enabled = true;
                    Destroy(gameObject);
                }
            } else {
                fireManText.text = fireManTalkContent[index1];
                mingPanel.SetActive(false);
                npcPanel.SetActive(true);
                index1++;
                if (index1 == fireManTalkContent.Length) {
                    index1 = 0;
                }
            }
        }

	}

    void OnTriggerEnter (Collider collider) {
        if (collider.gameObject.tag == "Player") {
            m_TalkPanelTransform.anchoredPosition = new Vector2(m_TalkPanelTransform.anchoredPosition.x, m_TalkPanelTransform.anchoredPosition.y + 200);
//             CursorManager._instance.IsShowCursor = true;
//             Cursor.lockState = CursorLockMode.None;
//             Cursor.visible = true;
            CursorManager._instance.ReleaseCursor(false);

            m_RigidbodyFirstPersonController.enabled = false;
        }
    }

}
