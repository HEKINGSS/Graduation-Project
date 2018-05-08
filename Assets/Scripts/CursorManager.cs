using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CursorManager : MonoBehaviour {

    public static CursorManager _instance;
    public bool lockCursor = true;
    public bool IsShowCursor = false;

    private bool m_cursorIsLocked = true;

    public static CursorManager Instance {
        get {
            if (!_instance) {
                _instance = GameObject.FindObjectOfType(typeof(CursorManager)) as CursorManager;
                if (!_instance) {
                    GameObject obj = new GameObject();
                    obj.name = "CursorManager";
                    _instance = obj.AddComponent(typeof(CursorManager)) as CursorManager;
                }
            }
            return _instance;
        }
    }

    void Awake () {
        _instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCursorLock();
	}

    public void SetCursorLock (bool value) {
        lockCursor = value;
        if (!lockCursor) {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    public void UpdateCursorLock () {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate () {

        //         if (Input.GetKeyUp(KeyCode.Escape)) {
        //             m_cursorIsLocked = false;
        //         } else if (Input.GetMouseButtonUp(0)) {
        //             m_cursorIsLocked = true;
        //         }

        if (Input.GetKeyUp(KeyCode.B)) {
            if (!IsShowCursor) {
                m_cursorIsLocked = false;
                IsShowCursor = true;
            } else {
                m_cursorIsLocked = true;
                IsShowCursor = false;
            }
        }

        if (m_cursorIsLocked) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else if (!m_cursorIsLocked) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ReleaseCursor (bool flag) {

        m_cursorIsLocked = flag;
        if (m_cursorIsLocked) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else if (!m_cursorIsLocked) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
}
