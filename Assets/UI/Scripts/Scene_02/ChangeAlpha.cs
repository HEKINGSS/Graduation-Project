using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeAlpha : MonoBehaviour
{
    private float time = 0;
    private Image m_Image;

	void Start ()
	{
	    m_Image = gameObject.GetComponent<Image>();
	}

	void Update ()
	{
	    time  += Time.deltaTime;
	    if (time <= 1)
	        m_Image.color = new Color(1,1,1,1f);
        else if (time > 1 && time < 2)
            m_Image.color = new Color(1, 1, 0.9f, 0.9f);
        else 
        {
            m_Image.color = new Color(1, 1, 1, 0.8f);
            time = 0;
        }
            

	}
}
