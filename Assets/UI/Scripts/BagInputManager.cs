using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class BagInputManager : MonoBehaviour {

    public GameObject m_Knapsack;
    public GameObject m_FrontSight;
    public GameObject m_FpsController;

    private RectTransform m_KnapsackRT;
    private RigidbodyFirstPersonController m_RigidbodyFirstPersonController;
    private bool isShow = false;
    
	// Use this for initialization
	void Start () {
        m_KnapsackRT = m_Knapsack.GetComponent<RectTransform>();
        m_RigidbodyFirstPersonController = m_FpsController.GetComponent<RigidbodyFirstPersonController>();
	}

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.B)) {
            ShowBag();
        }
	}

    private void ShowBag () {
        if (isShow) {
            m_KnapsackRT.anchoredPosition = new Vector2(m_KnapsackRT.anchoredPosition.x - 800, m_KnapsackRT.anchoredPosition.y);
            m_RigidbodyFirstPersonController.enabled = true;
            isShow = false;
        } else {
            m_KnapsackRT.anchoredPosition = new Vector2(m_KnapsackRT.anchoredPosition.x + 800, m_KnapsackRT.anchoredPosition.y);
            m_RigidbodyFirstPersonController.enabled = false;
            isShow = true;
        }
    }
}
