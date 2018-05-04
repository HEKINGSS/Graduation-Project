using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour {

    private bool lockCursor = true;
    private bool m_cursorIsLocked = true;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}

    // Update is called once per frame
    void Update () {
        print("( " + Input.mousePosition.x + " , " + Input.mousePosition.y + " )");
	}
}
