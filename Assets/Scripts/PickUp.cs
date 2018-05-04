using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    private Transform transform;
    public GameObject m_knapsack;
    private KnapsackManager m_knapmanager;
    public GameObject pickupImg;

	// Use this for initialization
	void Start () {
        pickupImg.SetActive(false);
        transform = gameObject.GetComponent<Transform>();
        m_knapmanager = m_knapsack.GetComponent<KnapsackManager>();
	}

    void OnTriggerStay (Collider collider) {

        if (collider.gameObject.tag == "player")
        {
            pickupImg.SetActive(true);
            Debug.Log("请按F拾取");
            string name = transform.gameObject.name;
            int index;
            for (index = 0; index < m_knapmanager.ItemList.Count; index++)
            {
                if (name == m_knapmanager.ItemList[index].EgName)  //找到与当前物体匹配的类对象
                    break;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                m_knapmanager.Pickup(m_knapmanager.ItemList[index]);
                Destroy(transform.gameObject);
            }
        }
        else
        {
            pickupImg.SetActive(false);
        }

    }
}
