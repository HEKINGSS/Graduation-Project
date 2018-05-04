using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameCanvas : MonoBehaviour {
    public Image Kanpsack;
    private Vector3 stPos;
    public GameObject panel;
    public GameObject player;
    private RigidbodyFirstPersonController rfc;
    
	void Start () {
        stPos = Kanpsack.rectTransform.position;
        panel.SetActive(false);
        rfc = player.GetComponent<RigidbodyFirstPersonController>();
	}
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangePOs();
        }
	}
    public void ChangePOs()
    {
        Vector3 kanPos = Kanpsack.rectTransform.position;
        if (kanPos == stPos)
        {
            panel.SetActive(true);
            Kanpsack.rectTransform.position += new Vector3(800, 0, 0);
            rfc.mouseLook.SetCursorLock(false);
            rfc.enabled = false;
        }
        else
        {
            Kanpsack.rectTransform.position = stPos;
            panel.SetActive(false);
            rfc.mouseLook.SetCursorLock(true);
            rfc.enabled = true;
        }
    }
}
