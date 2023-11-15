using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Grap 
{
    Low,
    Medium,
    Hard
}
public class SettingsManager : MonoBehaviour
{
    #region Vars 
    public static SettingsManager instance;
    public Slider Voise;
    public List<Button> Graphic = new List<Button>(3);
    public Grap grap;
    #endregion


    #region Unity Function
    public void Start()
    {
        instance = this;

        foreach (Button b in Graphic)
        {
            b.onClick.AddListener(() =>ValueChange(b.name));
            b.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Graphic"))
        {
            ValueChange(PlayerPrefs.GetString("Graphic"));
        }
        else
        {
            grap = Grap.Low;
            ValueChange(grap.ToString());
        }

    }
    public void Update()
    {
        
    }
    #endregion

    #region Engine Function
    public void ValueChange(string name)
    {
        foreach(Button b in Graphic)
        {
            b.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        Graphic.Find(x=>x.name == name).transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        switch (name)
        {
            case "Low": grap = Grap.Low; break;
            case "Medium": grap= Grap.Medium; break;
            case "Hard": grap = Grap.Hard; break;
            default: break;
        }
    }

    public void Restart()
    {
        grap = Grap.Low;
        PlayerPrefs.SetString("Graphic", grap.ToString());
        ValueChange(grap.ToString());
    }

    public void APPLY()
    {
        PlayerPrefs.SetString("Graphic",grap.ToString());
    }
    #endregion
}
