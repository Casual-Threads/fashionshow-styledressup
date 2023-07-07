using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MainMenuElements
{
    [Header("GameObject")]
    public GameObject LoadingPanel;
    public Image fillbar;
}
public class MyMainMenu : MonoBehaviour
{
    public MainMenuElements uIElements;
    [Header("Text")]
    public Text totalCoins;
    public CoinsAdder coinsAdder;

    void Start()
    {
        Usman_SaveLoad.LoadProgress();
        print(SaveData.Instance.IsFirstTime);
        if(SaveData.Instance.IsFirstTime == false)
        {
            StartCoroutine(AddCoins(0.1f, 2000));
            SaveData.Instance.IsFirstTime = true;
            //print(SaveData.Instance.IsFirstTime);
        }
        Usman_SaveLoad.SaveProgress();
        totalCoins.text = SaveData.Instance.Coins.ToString();
    }
    public void Play(string str)
    {
        //ShowInterstitial();
        //if (GAManager.Instance != null)
        //{
        //    GAManager.Instance.LogDesignEvent("MainMenu:PlayClick");
        //}
        uIElements.LoadingPanel.SetActive(true);
        StartCoroutine(LoadingScene(str));
    }
    IEnumerator LoadingScene(string str)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(str);
        asyncLoad.allowSceneActivation = false;

        while (uIElements.fillbar.fillAmount < 1)
        {
            uIElements.fillbar.fillAmount += Time.deltaTime / 3;
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }
    IEnumerator AddCoins(float delay, int coins)
    {
        yield return new WaitForSeconds(delay);
        if (coinsAdder)
        {
            coinsAdder.addCoins = coins;
            coinsAdder.addNow = true;
        }
    }
}