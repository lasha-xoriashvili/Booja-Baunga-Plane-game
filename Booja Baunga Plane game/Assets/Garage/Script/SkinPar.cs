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
    public GameObject Obj;
    private void Start()
    {
        if (Mat != null)
        {
            But = GetComponent<Button>();
            But.onClick.AddListener(() => GarageManager.Instance.ChangeSkin( SkinName.text));
        }
        else
        {
            But = GetComponent<Button>();
            But.onClick.AddListener(() => GarageManager.Instance.ChangePilot( SkinName.text));
        }
    }

}
