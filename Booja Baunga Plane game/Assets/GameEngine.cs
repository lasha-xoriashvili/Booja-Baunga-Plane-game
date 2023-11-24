using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    public static GameEngine Instance;

    [SerializeField] TextMeshProUGUI CoinCountText;
    [SerializeField] TextMeshProUGUI WarningScreen;
    [SerializeField] Image Pilot;
    [SerializeField] GameObject EndObj;
    [SerializeField] GameObject Enemy;
    public GameObject BackObj;
    public int CoinAmount;

    public bool EndGame = false;

    public bool Warning = false;
    public bool Destroed = false;
    bool Win = true;
    bool Once = true;


    float timer = 0;
    private void Start()
    {
        Instance = this;
        EndObj.SetActive(false);
        CoinAmount = 10;
        Pilot.sprite = PlaneParametrs.Pilot;
    }
 
    private void Update()
    {
        CoinCountText.text = CoinAmount.ToString();
        if(CoinAmount <= 0 && EndGame)
        {
            if (Win)
            {
                EndObj.SetActive(true);
                WinGame();
            }

        }
        else if (CoinAmount > 0 && EndGame)
        {
            if (Win)
            {
                EndObj.SetActive(true);
            }
        }
        if (Warning && Once)
        {
            StartCoroutine(_Warning());
            PlainEngine2.Instance.interactcolider = 1;
        }
        if(!Warning && !Destroed && !EndGame)
        {
            PlainEngine2.Instance.interactcolider = 0;
            Once = true;
            StopCoroutine(_Warning());
            WarningScreen.gameObject.SetActive(false);
        }
        if (Destroed)
        {

                UIManager.instance.UI.Find(x => x.Name == "EndMenu").GameObject.SetActive(true);
                StopCoroutine(_Warning());

        }

        if(timer < Time.time)
        {
            timer += 20;
            for (int i = 0; i < 2; i++)
            {
                Instantiate(Enemy, new Vector3(PlainEngine2.Instance.transform.position.x + 200, 100f, PlainEngine2.Instance.transform.position.z - i * 10), Quaternion.identity);
            }
        }
    }

    IEnumerator _Warning()
    {
        WarningScreen.text = "Cerfully explode forward";
        Once = false;
        while (Warning)
        {
            WarningScreen.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            WarningScreen.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);

        }
    }

    void WinGame()
    {
        Win = false;
        Scene currentScene = SceneManager.GetActiveScene();
        if (PlayerPrefs.GetInt("LevelIsOpened") <= int.Parse(currentScene.name[currentScene.name.Length - 1].ToString())) 
        {
            PlayerPrefs.SetInt("LevelIsOpened", (int.Parse(currentScene.name[currentScene.name.Length - 1].ToString()) + 1));
        }
    }

    public void ReloadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Garage");
    }
}
