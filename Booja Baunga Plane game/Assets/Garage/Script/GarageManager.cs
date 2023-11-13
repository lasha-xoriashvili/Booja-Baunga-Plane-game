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
    [SerializeField] GameObject SkinObj;
    [SerializeField] List<ParForSkin> AirPlanMaterial;
    [SerializeField] Transform Content;
    #endregion


    #region Unity Function
    private void Start()
    {
        Instance = this;
        
        //AirPlan Skin Changer
        SpownSkinsObj();
        if (PlayerPrefs.HasKey("ChosenSkinName"))
        {
            AirPlanObj.GetComponent<Renderer>().material = AirPlanMaterial.Find(x => x.skinname == PlayerPrefs.GetString("ChosenSkinName")).skinMat;
            PlaneParametrs.PleanMat = AirPlanObj.GetComponent<Renderer>().material;
        }
    }
    private void Update()
    {
        
    }

    #endregion

    #region Engine Function
    public void ChangeSkin(Material NewMat,string name)
    {
        AirPlanObj.GetComponent<Renderer>().material = NewMat;
        PlaneParametrs.PleanMat = NewMat;
        PlayerPrefs.SetString("ChosenSkinName", name);
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
    

    #endregion
}
[System.Serializable]
public class ParForSkin
{
    public Material skinMat;
    public string skinname;
    public Sprite SkinImage;
}
