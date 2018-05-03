using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour {

    public GameObject main_menu;
    public GameObject prac_menu;
    public GameObject set_menu;
    public Button confirm;
    public GameObject handbook;
	public GameObject exit_panel;

    public GameObject family;
    public GameObject school;
    public GameObject hotel;
    public GameObject tool;

    public GameObject zhaocha;
    public GameObject ZCImage;
    public GameObject menuImage;
    private ZhaoChaManager m_manager;

	void Start () {
        handbook.SetActive(false);
        prac_menu.SetActive(false);
        set_menu.SetActive(false);
        main_menu.SetActive(true);
        prac_menu.transform.GetChild(0).gameObject.SetActive(false);
        confirm.gameObject.SetActive(false);
		exit_panel.SetActive (false);
        zhaocha.SetActive(false);
        ZCImage.SetActive(false);

        m_manager = Camera.main.GetComponent<ZhaoChaManager>();
	}

    public void MainMenuControl()
    {
        handbook.SetActive(false);
        prac_menu.SetActive(false);
        set_menu.SetActive(false);
        main_menu.SetActive(true);
    }
    public void SetMenuControl()
    {
        handbook.SetActive(false);
        prac_menu.SetActive(false);
        main_menu.SetActive(false);
        set_menu.SetActive(true);       
    }
    public void PracMenuControl()
    {
        prac_menu.SetActive(true );
        main_menu.SetActive(false);
    }
    public void ChangetoFamily()
    {
        prac_menu.transform.GetChild(0).gameObject.SetActive(true);
        prac_menu.transform.GetChild(4).gameObject.SetActive(true);
        confirm.enabled = true;
        prac_menu.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        prac_menu.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        prac_menu.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
    public void ChangetoSchool()
    {
        prac_menu.transform.GetChild(0).gameObject.SetActive(true);
        confirm.gameObject.SetActive(false);
        confirm.enabled = false;
        prac_menu.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        prac_menu.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        prac_menu.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
    }

    public void ChangetoHotel()
    {
        prac_menu.transform.GetChild(0).gameObject.SetActive(true);
        confirm.gameObject.SetActive(false);
        confirm.enabled = false;
        prac_menu.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        prac_menu.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        prac_menu.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
	//消防手册
    public void ChangtoHandbook()
    {
        backBTonClick();
        set_menu.SetActive(false);
        main_menu.SetActive(false);
        handbook.SetActive(true);
		//卷轴滚动出现，目录由下往上滑出
    }

    public void ChangetoUserBook()
    {
        Application.LoadLevel("Quesion_04");
    }

    public void backBTonClick()
    {
        family.SetActive(false);
        school.SetActive(false);
        hotel.SetActive(false);
        tool.SetActive(false);
        handbook.transform.GetChild(2).gameObject.SetActive(true);
        handbook.transform.GetChild(3).gameObject.SetActive(false);
    }
    public void familyBTonClick()
    {
        handbook.transform.GetChild(2).gameObject.SetActive(false);
        handbook.transform.GetChild(3).gameObject.SetActive(true);
        family.SetActive(true);
    }
    public void schoolBTonClick()
    {
        handbook.transform.GetChild(3).gameObject.SetActive(true);
        handbook.transform.GetChild(2).gameObject.SetActive(false);
        school.SetActive(true);
    }
    public void hotelBTonClick()
    {
        handbook.transform.GetChild(3).gameObject.SetActive(true);
        handbook.transform.GetChild(2).gameObject.SetActive(false);
        hotel.SetActive(true);
    }
    public void toolBTonClick()
    {
        handbook.transform.GetChild(3).gameObject.SetActive(true);
        handbook.transform.GetChild(2).gameObject.SetActive(false);
        tool.SetActive(true);
    }
	public void ChangetoExit()
	{
		exit_panel.SetActive (true);
	}

	public void CancelExit()
	{
		exit_panel.SetActive (false);
	}
    public void EixtGame()
    {

        Application.Quit();
		Debug.Log ("退出...");
    }

	//场景跳转
	public void ChangeScene()
	{
		//保存需要加载的目标场景  
        Globe.nextSceneName = "main_03";
        SceneManager.LoadScene("LoadScene_02"); 
		//Application.LoadLevelAsync("OutFireScene");
		Debug.Log ("跳转中...");
	}
    public void FindBtOnClick()
    {
        ZCImage.SetActive(true);
        main_menu.SetActive(false);
        menuImage.SetActive(false);
        zhaocha.SetActive(true);
        m_manager.StartGame();
    }
}
