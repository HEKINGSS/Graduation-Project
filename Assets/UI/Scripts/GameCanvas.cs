using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour {
    public Image Kanpsack;
    private Vector3 stPos;
	void Start () {
        stPos = Kanpsack.rectTransform.position;
	}
	void Update () {	
	}
    public void ChangePOs()
    {
        Vector3 kanPos = Kanpsack.rectTransform.position;
        if (kanPos == stPos)
            Kanpsack.rectTransform.position += new Vector3(800, 0, 0);
        else
            Kanpsack.rectTransform.position = stPos;
    }
}
