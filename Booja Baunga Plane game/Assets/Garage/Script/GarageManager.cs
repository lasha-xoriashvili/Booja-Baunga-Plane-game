using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GarageManager : MonoBehaviour
{
    #region Vars 
    public static GarageManager Instance;
    [Header("Complection")]
    [SerializeField] GameObject AirPlanObj;

    [Header("Skins")]
    [SerializeField] Image ColorGraphBut;
    [SerializeField] GameObject PlaneSkinsGraph;
    [SerializeField] GameObject SkinObj;
    [SerializeField] List<ParForSkin> AirPlanMaterial;
    [SerializeField] Transform Content;

    [Header("pilot")]
    [SerializeField] GameObject PilotSkinsGraph;
    [SerializeField] Image PilotGraphBut;
    [SerializeField] Transform ContentForPilot;
    [SerializeField] Transform PilotPos;
    [SerializeField] GameObject pilot;
    [SerializeField] List<ParForPilot> PilotObjs;

    private string SkinName;
    private string PilotName;
    #endregion


    #region Unity Function
    private void Start()
    {
        Instance = this;
        SpownPilotObj();
        SpownSkinsObj();
        CollorGraph();
        if (PlayerPrefs.HasKey("ChosenSkinName"))
        {
            AirPlanObj.GetComponent<Renderer>().material = AirPlanMaterial.Find(x => x.skinname == PlayerPrefs.GetString("ChosenSkinName")).skinMat;
            PlaneParametrs.PleanMat = AirPlanObj.GetComponent<Renderer>().material;
        }
        else
        {
            AirPlanObj.GetComponent<Renderer>().material = AirPlanMaterial[0].skinMat;
            PlaneParametrs.PleanMat = AirPlanObj.GetComponent<Renderer>().material;
        }
        if (PlayerPrefs.HasKey("ChosenPilotName"))
        {
            ChangePilot(PlayerPrefs.GetString("ChosenPilotName"));
        }
        else
        {
            pilot = Instantiate(PilotObjs[0].skinObj, PilotPos.transform.position,Quaternion.identity);
        }
    }
    private void Update()
    {
        
    }

    #endregion

    #region Engine Function
    public void ChangeSkin(string name)
    {
        if (name != null)
        {
            AirPlanObj.GetComponent<Renderer>().material = AirPlanMaterial.Find(x => x.skinname == name).skinMat;
            SkinName = name;
        }
    }
    public void ChangePilot( string name)
    {
        if (name != null)
        {
            if (pilot != null)
            {
                Destroy(pilot);
            }
            pilot = Instantiate(PilotObjs.Find(x => x.skinname == name).skinObj, PilotPos.transform.position, Quaternion.identity);
            PilotName = name;
            PlaneParametrs.Pilot = PilotObjs.Find(x => x.skinname == name).SkinImage;
        }
    }
    public void SaveSkin()
    {
        if (SkinName != null)
        {
            PlaneParametrs.PleanMat = AirPlanMaterial.Find(x => x.skinname == SkinName).skinMat;
            PlayerPrefs.SetString("ChosenSkinName", SkinName);
        }
        if(PilotName != null)
        {
            PlayerPrefs.SetString("ChosenPilotName", PilotName);
        }
    }
    public void SpownSkinsObj()
    {
        for (int i = 0; i < AirPlanMaterial.Count; i++)
        {
            GameObject skin = Instantiate(SkinObj, Content);
            skin.GetComponent<SkinPar>().Mat = AirPlanMaterial[i].skinMat;
            skin.GetComponent<SkinPar>().TumbImg.sprite = AirPlanMaterial[i].SkinImage;
            skin.GetComponent<SkinPar>().SkinName.text = AirPlanMaterial[i].skinname;
        }
    }
    public void SpownPilotObj()
    {
        for (int i = 0; i < PilotObjs.Count; i++)
        {
            GameObject skin = Instantiate(SkinObj, ContentForPilot);
            skin.GetComponent<SkinPar>().Obj = PilotObjs[i].skinObj;
            skin.GetComponent<SkinPar>().TumbImg.sprite = PilotObjs[i].SkinImage;
            skin.GetComponent<SkinPar>().SkinName.text = PilotObjs[i].skinname;
        }
    }

    public void CollorGraph()
    {
        ColorGraphBut.color = Color.clear;
        PilotGraphBut.color = Color.white;
        PilotSkinsGraph.SetActive(false);
        PlaneSkinsGraph.SetActive(true);
    }
    public void PilotGraph()
    {
        ColorGraphBut.color = Color.white;
        PilotGraphBut.color = Color.clear;
        PilotSkinsGraph.SetActive(true);
        PlaneSkinsGraph.SetActive(false);
    }
    

    #endregion
}
[System.Serializable]
public class ParForSkin
{
    public Material skinMat;
    public string skinname;
    public Sprite SkinImage;
}
[System.Serializable]
public class ParForPilot
{
    public GameObject skinObj;
    public string skinname;
    public Sprite SkinImage;
}
