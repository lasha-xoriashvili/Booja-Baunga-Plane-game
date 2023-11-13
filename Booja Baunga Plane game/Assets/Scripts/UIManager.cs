using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Vars 
    [SerializeField] string MeanUI;
    [Header("Buttons")]
    [SerializeField] List<_ButtonFind> Buttons; 
    [SerializeField] Button PlayBut;
    [SerializeField] Button GarageBut;
    [SerializeField] Button MainMenuBut;
    [Header("UIObject")]
    [SerializeField] List<UiObjects> UI;
    #endregion


    #region Unity Function
    private void Start()
    {
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
        foreach(var But in Buttons)
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
            UI.Find(x => x.Name == name).GameObject.SetActive(true);
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