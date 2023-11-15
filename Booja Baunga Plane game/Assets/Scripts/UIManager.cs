using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Vars 
    [SerializeField] string MeanUI;
    [Header("Buttons")]
    [SerializeField] List<_ButtonFind> Buttons; 
    [Header("UIObject")]
    [SerializeField] List<UiObjects> UI;
    #endregion


    #region Unity Function
    private void Start()
    {
        for(int i =0;i < GameObject.Find("Canvas").transform.childCount; i++)
        {
            GameObject.Find("Canvas").transform.GetChild(i).gameObject.SetActive(true);
        }
        DontDestroyOnLoad(this);
        UI.Clear();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Menu"))
        {
            UI.Add(new UiObjects(obj,obj.name));
        }
        
        Buttons.Clear();
        foreach (var item in GameObject.FindGameObjectsWithTag("Button"))
        {
            Buttons.Add(new _ButtonFind(item.GetComponent<Button>(),item.name));
        }



        foreach (var But in Buttons)
        {
            But.But.onClick.AddListener(() => UIChange(But.ButName));
        }

        foreach (var i in UI)
        {
            i.GameObject.SetActive(false);
        }
        UI.Find(x => x.Name == MeanUI).GameObject.SetActive(true);
        /*PlayBut = GameObject.Find("PlayBTN").GetComponent<Button>();
        //if (PlayBut != null)
        //{
        //    PlayBut.onClick.AddListener(Play);
        //}
        //GarageBut = GameObject.Find("GarageBTN").GetComponent<Button>();
        //if (GarageBut != null)
        //{
        //    GarageBut.onClick.AddListener(() => UIChange("GarageBut"));
        //}
        //MainMenuBut = GameObject.Find("MainMenuBTN").GetComponent<Button>();
        //if (MainMenuBut != null)
        //{
        //    MainMenuBut.onClick.AddListener(() => UIChange("MainMenuBut"));
        //}*/
    }
    #endregion

    #region Engine Function
    public void Play()
    {
        SceneManager.LoadSceneAsync("Game");
    }
    private void UIChange(string name)
    {
        if (name.Contains("SET"))
        {
            string Nam = name.Replace("SET","");
            SETBTN(Nam);
        }
        else
        {
            foreach (var item in UI)
            {
                item.GameObject.SetActive(false);
            }
            if (name.Contains("ChS"))
            {
                string[] a = name.Split("ChS");
                SceneManager.LoadSceneAsync(a[1]);
            }
            else
            {
                if(name == "Menu")
                {
                    GarageManager.Instance.ChangePilot(PlayerPrefs.GetString("ChosenPilotName"));
                    GarageManager.Instance.ChangeSkin(PlayerPrefs.GetString("ChosenSkinName"));
                }
                UI.Find(x => x.Name == name).GameObject.SetActive(true);
            }
        }
    }


    public void SETBTN(string ButName)
    {
        switch (ButName)
        {
            //Save
            case "Save": 
                GarageManager.Instance.SaveSkin();
                break;
            //RestartSettings
            case "Reset":
                SettingsManager.instance.Restart();
                break;
            //SaveSettings
            case "Apply":
                SettingsManager.instance.APPLY();
                break;
            //PlainColor change graph
            case "Collor": 
                GarageManager.Instance.CollorGraph();
                break;
            //PlainPilot change graph
            case "Pilot":
                GarageManager.Instance.PilotGraph();
                break;
            default: 
                break;
        }
    }
    #endregion
}
[System.Serializable]
public class UiObjects
{
    public GameObject GameObject;
    public string Name;
    public UiObjects(GameObject Obj, string name)
    {
        GameObject = Obj;
        Name = name;
    }
}
[System.Serializable]
public class _ButtonFind
{
    public Button But;
    public string ButName;
    public _ButtonFind(Button BTN, string BTName)
    {
        But = BTN;
        ButName = BTName;
    }
}
