using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    public TextMeshProUGUI voiceProcent;
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
            PlaneParametrs.grap = Grap.Low;
            ValueChange(PlaneParametrs.grap.ToString());
        }
    }
    public void Update()
    {
        voiceProcent.text = Voise.value.ToString("00%");
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
            case "Low": PlaneParametrs.grap = Grap.Low; break;
            case "Medium": PlaneParametrs.grap = Grap.Medium; break;
            case "Hard": PlaneParametrs.grap = Grap.Hard; break;
            default: break;
        }
    }

    public void VoiseSet()
    {
        
        if (PlayerPrefs.HasKey("Voise"))
        {
            Voise.value = PlayerPrefs.GetFloat("Voise");
        }
    }

    public void Restart()
    {
        PlaneParametrs.grap = Grap.Low;
        PlayerPrefs.SetString("Graphic", PlaneParametrs.grap.ToString());
        PlayerPrefs.SetFloat("Voise", 1);
        Voise.value = 1;
        ValueChange(PlaneParametrs.grap.ToString());
    }

    public void APPLY()
    {
        PlayerPrefs.SetString("Graphic", PlaneParametrs.grap.ToString());
        PlayerPrefs.SetFloat("Voise", Voise.value);
    }
    #endregion
}
