using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;

public class ModeSelection : MonoBehaviour
{
    public static ModeSelection instance;
    public static ModeSelection Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ModeSelection();
            }
            return instance;
        }
    }
    [Header("GameObject")]
    public GameObject LoadingPanel;
    //public GameObject enoughCoinPopUp, videoAdNotAvailablePopUp;
    [Header("Arrays")]
    public ItemInfo[] itemInfo;
    //[Header("Text")]
    //public Text TotalCoins;
    [Header("UI Images")]
    public Image fillBar;
    [Header("Audio Source")]
    //public AudioSource purchaseSFX;
    //public AudioSource itemSelectSFX;
    //public CoinsAdder coinsAdder;
    private int selectedIndex;
    private enum RewardType
    {
        none, coins, SelectionItem
    }
    private RewardType rewardType;
    public enum LoadLevel
    {
        FashionStar, SummerFashion, FashionQueen
    }
    private LoadLevel loadLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //public void ShowInterstitial()
    //{
    //    if (MyAdsManager.instance)
    //    {
    //        MyAdsManager.instance.ShowInterstitialAds();
    //    }
    //}
    //void OnEnable()
    //{
    //    if (MyAdsManager.Instance != null)
    //    {
    //        MyAdsManager.Instance.onRewardedVideoAdCompletedEvent += OnRewardedVideoComplete;
    //    }
    //}

    //void OnDisable()
    //{
    //    if (MyAdsManager.Instance != null)
    //    {
    //        MyAdsManager.Instance.onRewardedVideoAdCompletedEvent -= OnRewardedVideoComplete;
    //    }
    //}
    private void Start()
    {
        //if (!GameManager.Instance.Initialized)
        //{
        //    GameManager.Instance.Initialized = true;
        //}
        Usman_SaveLoad.LoadProgress();
        SetInitialProps();
        GetItemsInfo();
        //TotalCoins.text = SaveData.Instance.Coins.ToString();
    }


    #region SetInitialProps
    private void SetInitialProps()
    {

        #region Initialing 
        if (SaveData.Instance.ModeProps.Count < itemInfo.Length)
        {
            for (int i = 0; i < itemInfo.Length; i++)
            {
                if (SaveData.Instance.ModeProps.Count <= i)
                {
                    // Add new data to SaveData file in case the file is empty or new data is available
                    Modesprops modeProps = new Modesprops();
                    modeProps.isLocked = itemInfo[i].isLocked;
                    SaveData.Instance.ModeProps.Add(modeProps);
                }
            }
        }
        // Setting up Dresses Properties to actual Properties from SaveData file  
        for (int i = 0; i < itemInfo.Length; i++)
        {
            itemInfo[i].isLocked = SaveData.Instance.ModeProps[i].isLocked;
        }
        //Adding Click listeners to btns 
        for (int i = 0; i < itemInfo.Length; i++)
        {
            int Index = i;
            if (itemInfo[i].itemBtn)
            {
                itemInfo[i].itemBtn.onClick.AddListener(() =>
                {
                    selectedIndex = Index;
                    SelectItem(Index);
                });
            }
        }
        #endregion

        Usman_SaveLoad.SaveProgress();
    }
    #endregion

    #region GetItemsInfo
    private void GetItemsInfo()
    {
        #region Get Info
        for (int i = 0; i < itemInfo.Length; i++)
        {
            if (itemInfo[i].isLocked)
            {
                if (itemInfo[i].LockIcon) itemInfo[i].LockIcon.SetActive(true);
                if (itemInfo[i].coinsUnlock)
                {

                    if (itemInfo[i].coinLock)
                    {
                        if (itemInfo[i].unlockCoins)
                        {
                            itemInfo[i].unlockCoins.text = itemInfo[i].requiredCoins.ToString();
                        }
                    }
                }
            }
            else
            {
                if (itemInfo[i].LockIcon) itemInfo[i].LockIcon.SetActive(false);
                if (itemInfo[i].coinLock)
                {
                    //itemInfo[i].letsPlay.SetActive(true);
                    itemInfo[i].coinLock.SetActive(false);
                }
            }
        }
        #endregion
    }
    #endregion

    #region SelectItem
    private void SelectItem(int selectedIndex)
    {
        //if (itemSelectSFX) itemSelectSFX.Play();
        rewardType = RewardType.SelectionItem;
        if (itemInfo[selectedIndex].isLocked)
        {
            if (itemInfo[selectedIndex].coinsUnlock)
            {
                if (SaveData.Instance.Coins >= itemInfo[selectedIndex].requiredCoins)
                {
                    itemInfo[selectedIndex].isLocked = false;
                    SaveData.Instance.ModeProps[selectedIndex].isLocked = false;
                    SaveData.Instance.Coins -= itemInfo[selectedIndex].requiredCoins;
                    Usman_SaveLoad.SaveProgress();
                    //if (purchaseSFX) purchaseSFX.Play();
                }
                //else
                //{
                //    if (enoughCoinPopUp)
                //        enoughCoinPopUp.SetActive(true);
                //}
                //TotalCoins.text = SaveData.Instance.Coins.ToString();
            }
        }
        else
        {
            //ShowInterstitial();
            //if (itemSelectSFX) itemSelectSFX.Play();
            LoadingPanel.SetActive(true);
            loadLevel = (LoadLevel)selectedIndex;
            SaveData.Instance.levelIndex = selectedIndex;
            StartCoroutine(Loading(loadLevel.ToString()));
            Usman_SaveLoad.SaveProgress();
        }
        //TotalCoins.text = SaveData.Instance.Coins.ToString();
        GetItemsInfo();
    }
    #endregion

    //#region RewardedVideoCompleted
    //public void OnRewardedVideoComplete()
    //{
    //    if (rewardType == RewardType.coins)
    //    {
    //        StartCoroutine(AddCoins(2000));
    //        //SaveData.Instance.Coins += 2000;
    //        TotalCoins.text = SaveData.Instance.Coins.ToString();
    //        Usman_SaveLoad.SaveProgress();
    //    }
    //    rewardType = RewardType.none;
    //    //if (purchaseSFX) purchaseSFX.Play();
    //}
    //#endregion

    //#region GetRewardedCoins
    //public void GetRewardedCoins()
    //{
    //    rewardType = RewardType.coins;
    //    CheckVideoStatus();
    //}
    //#endregion

    //#region CheckVideoStatus
    //public void CheckVideoStatus()
    //{
    //    if (MyAdsManager.Instance != null)
    //    {
    //        if (MyAdsManager.Instance.IsRewardedAvailable())
    //        {
    //            enoughCoinPopUp.SetActive(false);
    //            MyAdsManager.Instance.ShowRewardedVideos();
    //        }
    //        else
    //        {
    //            videoAdNotAvailablePopUp.SetActive(true);
    //            StartCoroutine(VideoPanelOff());
    //        }
    //    }
    //    else
    //    {
    //        videoAdNotAvailablePopUp.SetActive(true);
    //        StartCoroutine(VideoPanelOff());
    //    }
    //}
    //#endregion

    #region IEnumerator
    IEnumerator Loading(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (fillBar.fillAmount < 1)
        {
            fillBar.fillAmount += Time.deltaTime / 3;
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }

    //IEnumerator AddCoins( int coins)
    //{
    //    yield return null;
    //    if (coinsAdder)
    //    {
    //        coinsAdder.addCoins = coins;
    //        coinsAdder.addNow = true;
    //    }
    //}

    //IEnumerator VideoPanelOff()
    //{
    //    yield return new WaitForSeconds(2.2f);
    //    videoAdNotAvailablePopUp.SetActive(false);
    //}
    #endregion
}
