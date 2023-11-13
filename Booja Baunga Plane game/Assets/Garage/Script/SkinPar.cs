using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPar :MonoBehaviour
{
    public Button But;
    public Image TumbImg;
    public TextMeshProUGUI SkinName;
    public Material Mat;
    private void Start()
    {
        
        But = GetComponent<Button>();
        But.onClick.AddListener(() =>GarageManager.Instance.ChangeSkin(Mat,SkinName.text));
    }

}
