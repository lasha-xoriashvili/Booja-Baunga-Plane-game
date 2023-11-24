using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    #region Vars 
    [SerializeField] string MeanUI;
    [Header("Buttons")]
    [SerializeField] List<_ButtonFind> Buttons; 
    [Header("UIObject")]
    public List<UiObjects> UI;
    [Header("Levels")]
    [SerializeField] List<LevelIsOpen> levelAre;
    #endregion


    #region Unity Function
    private void Start()
    {
        instance = this;
        for (int i = 0; i < GameObject.Find("Canvas").transform.childCount; i++)
        {
            GameObject.Find("Canvas").transform.GetChild(i).gameObject.SetActive(true);
        }
        UI.Clear();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Menu"))
        {
            UI.Add(new UiObjects(obj, obj.name));
        }

        Buttons.Clear();
        foreach (var item in GameObject.FindGameObjectsWithTag("Button"))
        {
            Buttons.Add(new _ButtonFind(item.GetComponent<Button>(), item.name));
        }
        foreach (var But in Buttons)
        {
            But.But.onClick.AddListener(() => UIChange(But.ButName, But.But.gameObject));
            if (But.ButName.Contains("ChS"))
            {
                levelAre.Add(new LevelIsOpen(But.ButName, false));
            }
        }
        foreach (var it in levelAre)
        {

            if (int.Parse(it.LevelName[it.LevelName.Length - 1].ToString()) <= PlayerPrefs.GetInt("LevelIsOpened",1))
            {
                it.IsActive = true;
                Buttons.Find(x => x.ButName == it.LevelName).But.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                it.IsActive = false;
                Buttons.Find(x => x.ButName == it.LevelName).But.transform.GetChild(1).gameObject.SetActive(true);
            }
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
    public void UIChange(string name,GameObject Btn)
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
                if (Btn.transform.childCount > 1)
                {
                    if (!Btn.transform.GetChild(1).gameObject.activeSelf)
                    {
                        string[] a = name.Split("ChS");
                        LoadGameScene(a[1]);
                    }
                    else
                    {
                        UI.Find(x => x.Name == "Levels").GameObject.SetActive(true);
                    }
                }

                
            }
            else
            {
                UI.Find(x => x.Name == name).GameObject.SetActive(true);
                if (name == "Menu")
                {
                    GarageManager.Instance.ChangePilot(PlayerPrefs.GetString("ChosenPilotName"));
                    GarageManager.Instance.ChangeSkin(PlayerPrefs.GetString("ChosenSkinName"));
                }
                else if (name == "Settings")
                {
                    UI.Find(x => x.Name == name).GameObject.GetComponent<SettingsManager>().VoiseSet();
                }
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
                UI.Find(x => x.Name == "Garage").GameObject.SetActive(false);
                UI.Find(x => x.Name == "Menu").GameObject.SetActive(true);
                break;
            //RestartSettings
            case "Reset":
                SettingsManager.instance.Restart();
                break;
            //SaveSettings
            case "Apply":
                
                SettingsManager.instance.APPLY();
                UI.Find(x => x.Name == "Settings").GameObject.SetActive(false);
                UI.Find(x => x.Name == "Menu").GameObject.SetActive(true);
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


    public void LoadGameScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
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

[System.Serializable]
public class LevelIsOpen
{
    public string LevelName;
    public bool IsActive;
    public LevelIsOpen(string levelName, bool isactivescene)
    {
        this.LevelName = levelName;
        this.IsActive = isactivescene;
    }
}