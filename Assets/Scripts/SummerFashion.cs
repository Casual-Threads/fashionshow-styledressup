using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SummerFashionElements
{
    [Header("Panels")]
    public GameObject vsPanel;
    public GameObject vsAnimPanel, gamePanel, submitPanel, screenShotPanel, judgementalPanel, levelCompletePanel, adPanel, loadingPanel;
    [Header("PopUp")]
    public GameObject videoUnlockPopUp;
    public GameObject coinUnlockPopUp, getCoinPopUp, videoAdNotAvailablePopUp, warningPopUp;
    [Header("Scrollers")]
    public GameObject allScroller;
    public GameObject CategoryScroller, blushScroller, hatScroller, upperScroller, dressScroller, earingScroller, eyeshadeScroller, glassesScroller, necklaceScroller,
                      hairScroller, bagScroller, lipsScroller, shoesScroller;
    [Header("UI")]
    public GameObject nextBtn;
    public GameObject coinSlot, previewBtn, submitPanelBar, vsImage;
    [Header("SummerFashion Image")]
    public Image bgImage;
    public Image screenShotImage, fillbar;
}

[System.Serializable]
public class SummerFashionPlayerElenemts
{
    [Header("Player Character")]
    public GameObject character;
    [Header("Player Images")]
    public Image blushImage;
    public Image hatImage, upperImage, dressImage, earingImage, eyeshadeImage, glassesImage, necklaceImage, hairImage, bagImage, lipsImage, shoesImage;
    [Header("Player Score Text")]
    public Text voteScore;
    public Text totalScore;
    [Header("Player Winner")]
    public GameObject winner;

}

[System.Serializable]
public class SummerFashionOpponentElenemts
{
    [Header("Opponent Character")]
    public GameObject character;
    [Header("Opponent Images")]
    public Image blushImage;
    public Image hatImage, upperImage, dressImage, earingImage, eyeshadeImage, glassesImage, necklaceImage, hairImage, bagImage, lipsImage, shoesImage;
    [Header("Opponent Bot Images")]
    public Image botInVsPanel;
    public Image botInVsAnimPanel, botInJudgementalPanel;
    [Header("Opponent Text")]
    public Text voteScore;
    public Text totalScore;
    [Header("Opponent Winner")]
    public GameObject winner;
}

[System.Serializable]
public enum SummerFashionSelectedItem
{
    dress, upper, hair, lips, blush, eyeshade, earing, necklace, hat, glasses, bag, shoes
}

public class SummerFashion : MonoBehaviour
{
    public SummerFashionSelectedItem selectedItem;
    [FoldoutGroup("UI Elements")]
    [HideLabel]
    public SummerFashionElements uIElements;
    [FoldoutGroup("Player Elements")]
    [HideLabel]
    public SummerFashionPlayerElenemts playerElements;
    [FoldoutGroup("Opponent Elements")]
    [HideLabel]
    public SummerFashionOpponentElenemts oppoElements;
    [Header("Mover Item")]
    public MRS_Manager playerCharacterMover;
    public MRS_Manager oppoCharacterMover;
    public MRS_Manager backBtnInGamePanel;
    public CoinsAdder coinsAdder;
    [Header("Sprites Array")]
    public Sprite[] botSprites;
    public Sprite[] blushSprites;
    public Sprite[] hatSprites;
    public Sprite[] upperSprites;
    public Sprite[] dressSprites;
    public Sprite[] earingSprites;
    public Sprite[] eyeshadeSprites;
    public Sprite[] glassesSprites;
    public Sprite[] necklaceSprites;
    public Sprite[] hairSprites;
    public Sprite[] bagSprites;
    public Sprite[] lipsSprites;
    public Sprite[] shoesSprites;
    public Sprite[] defaultBtnSprites;
    public Sprite[] selectedBtnSprites;
    [Header("Scroller Btn Image Array")]
    public Image[] categoryBtn;
    [Header("Item List")]
    private List<ItemInfo> blushList = new List<ItemInfo>();
    private List<ItemInfo> hatList = new List<ItemInfo>();
    private List<ItemInfo> upperList = new List<ItemInfo>();
    private List<ItemInfo> dressList = new List<ItemInfo>();
    private List<ItemInfo> earingList = new List<ItemInfo>();
    private List<ItemInfo> eyeshadeList = new List<ItemInfo>();
    private List<ItemInfo> glassesList = new List<ItemInfo>();
    private List<ItemInfo> necklaceList = new List<ItemInfo>();
    private List<ItemInfo> hairList = new List<ItemInfo>();
    private List<ItemInfo> bagList = new List<ItemInfo>();
    private List<ItemInfo> lipsList = new List<ItemInfo>();
    private List<ItemInfo> shoesList = new List<ItemInfo>();

    private int[] dressScore = { 1564, 3245, 2845, 1515, 4705, 1635, 2585, 4357, 3065, 4546, 1457, 2415, 5162, 3568, 4523 };
    private int[] hairScore = { 2156, 4121, 5451, 4923, 1842, 1564, 8921, 4215, 4121, 5648, 2165, 2118, 8940, 1321, 5684 };
    private int[] lipsScore = { 1164, 5421, 2174, 3214, 5489, 7252, 1643, 2181, 2974, 2125, 4754, 2381, 1651, 3726, 3564 };
    private int[] blushScore = { 5481, 2894, 9545, 4784, 8415, 4892, 1654, 8756, 5645, 6156, 3121, 5726, 1161, 1644, 7845 };
    private int[] eyeshadeScore = { 2112, 1421, 4821, 5212, 1613, 8264, 1262, 3162, 1142, 3146, 3121, 2984, 1344, 9443, 5464 };
    private int[] earingScore = { 1964, 3175, 2595, 3815, 5805, 3735, 2475, 5487, 3578, 4886, 2455, 3425, 3282, 1546, 2354 };
    private int[] necklaceScore = { 2874, 2165, 1485, 2415, 4705, 2734, 2185, 2467, 3578, 3526, 1456, 2585, 1272, 2945, 2464 };
    private int[] hatScore = { 1763, 1296, 4584, 3715, 2401, 1744, 2568, 4589, 2054, 5726, 3548, 5128, 2381, 2585, 1472 };
    private int[] glassesScore = { 2864, 2185, 4593, 6915, 3505, 3965, 3476, 6489, 3676, 5876, 3425, 4523, 1242, 1656, 3564 };
    private int[] upperScore = { 1965, 3186, 6593, 5914, 1500, 2963, 1479, 7598, 1665, 6985, 2008, 6521, 1240, 1654, 4452 };
    private int[] shoesScore = { 2132, 4842, 1482, 2164, 4512, 1641, 2316, 6213, 2156, 4821, 1568, 4895, 1231, 6123, 2354, 2484 };
    private int[] bagScore = { 2154, 8421, 2184, 3214, 8489, 7212, 1848, 4231, 2484, 2156, 4844, 2391, 9681, 0824, 5326 };

    [Header("Sprites")]
    public Sprite bgSprite;
    public Sprite submitPanelBgSprite, judgementalPanelBgSprite, levelCompletePanelBgSprite, selectionDefaultSprite, selectionSelectedSprite;
    [Header("Text")]
    public Text totalCoins;
    public Text itemUnlockCoin, categoriesText;
    [Header("ItemInfo Variable")]
    private ItemInfo tempItem;
    [Header("Different Index")]
    private int selectedIndex;
    [Header("Bool Variable")]
    private bool canShowInterstitial;
    [Header("Animator")]
    public Animator categoryNameAndScoreAnim;
    [Header("Particles")]
    public GameObject finalPartical;
    [Header("AudioSources")]
    public AudioSource categorySFX;
    public AudioSource purchaseSFX, winSFX, loseSFX, voteCatSFX;
    public AudioSource[] voiceSounds;

    private int playerScore, oppoScore = 0;
    int playerTotalScore, oppoTotalScore = 0;

    private bool IsDressing, IsAccessioress, IsMakeup;
    private int playerdressScore, playershoesScore, playerupperScore, playerearingScore, playernecklaceScore, playerhatScore, playerglassesScore, playerlipsScore, playerhairScore,
                playereyeshadeScore, playerbagScore, playerblushScore = 0;
    private int oppodressScore, opposhoesScore, oppoupperScore, oppoearingScore, opponecklaceScore, oppohatScore, oppoglassesScore, oppolipsScore, oppohairScore, oppoeyeshadeScore,
                oppobagScore, oppoblushScore = 0;
    private enum RewardType
    {
        none, coins, multipulOfTwo, selectionItem
    }
    private RewardType rewardType;
    private enum SelectionName
    {
        Sita, Aashvi, Mirza, Saia, Surya, Zoya, Alani, Divya, Jaya, Khushi, Nirvi, Sana, Uma, Ziya, Zunaira, Sonia, Shivani, Prisha, Kiyana, Jasmine, Misha,
        Lakshmi, Kareena
    }
    private SelectionName Name;

    #region Start
    // Start is called before the first frame update
    void Start()
    {
        Usman_SaveLoad.LoadProgress();
        uIElements.vsPanel.SetActive(true);
        StartCoroutine(findOpponent());
        selectedItem = SummerFashionSelectedItem.dress;
        uIElements.dressScroller.SetActive(true);
        SetInitialValues();
        GetItemsInfo();
        StartCoroutine(AdDelay(45));
        totalCoins.text = SaveData.Instance.Coins.ToString();
        dressUpOpponent();
        //dressRank = hairRank =  1;
        //lipsRank = blushRank = hatRank = upperRank = earingRank = necklaceRank = eyeshadeRank = glassesRank = bagRank = shoesRank = -1;
    }
    public void ShowInterstitial()
    {
        if (MyAdsManager.instance)
        {
            MyAdsManager.instance.ShowInterstitialAds();
        }
    }
    void OnEnable()
    {
        if (MyAdsManager.Instance != null)
        {
            MyAdsManager.Instance.onRewardedVideoAdCompletedEvent += OnRewardedVideoComplete;
        }
    }

    void OnDisable()
    {
        if (MyAdsManager.Instance != null)
        {
            MyAdsManager.Instance.onRewardedVideoAdCompletedEvent -= OnRewardedVideoComplete;
        }
    }
    #endregion

    #region SetInitialValues
    private void SetInitialValues()
    {

        #region Initialing blush
        if (uIElements.blushScroller)
        {
            var blushInfo = uIElements.blushScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < blushInfo.Length; i++)
            {
                blushList.Add(blushInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.blush, blushList);
        SetItemIcon(blushList, blushSprites);
        #endregion
        
        #region Initialing hat
        if (uIElements.hatScroller)
        {
            var hatInfo = uIElements.hatScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < hatInfo.Length; i++)
            {
                hatList.Add(hatInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.hat, hatList);
        SetItemIcon(hatList, hatSprites);
        #endregion
        
        #region Initialing upper
        if (uIElements.upperScroller)
        {
            var upperInfo = uIElements.upperScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < upperInfo.Length; i++)
            {
                upperList.Add(upperInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.upper, upperList);
        SetItemIcon(upperList, upperSprites);
        #endregion

        #region Initialing dress
        if (uIElements.dressScroller)
        {
            var dressInfo = uIElements.dressScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < dressInfo.Length; i++)
            {
                dressList.Add(dressInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.dress, dressList);
        SetItemIcon(dressList, dressSprites);
        #endregion

        #region Initialing earing
        if (uIElements.earingScroller)
        {
            var earingInfo = uIElements.earingScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < earingInfo.Length; i++)
            {
                earingList.Add(earingInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.earing, earingList);
        SetItemIcon(earingList, earingSprites);
        #endregion

        #region Initialing eyeshade
        if (uIElements.eyeshadeScroller)
        {
            var eyeshadeInfo = uIElements.eyeshadeScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < eyeshadeInfo.Length; i++)
            {
                eyeshadeList.Add(eyeshadeInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.eyeshade, eyeshadeList);
        SetItemIcon(eyeshadeList, eyeshadeSprites);
        #endregion

        #region Initialing glasses
        if (uIElements.glassesScroller)
        {
            var glassesInfo = uIElements.glassesScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < glassesInfo.Length; i++)
            {
                glassesList.Add(glassesInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.glasses, glassesList);
        SetItemIcon(glassesList, glassesSprites);
        #endregion

        #region Initialing hair
        if (uIElements.hairScroller)
        {
            var hairInfo = uIElements.hairScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < hairInfo.Length; i++)
            {
                hairList.Add(hairInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.hair, hairList);
        SetItemIcon(hairList, hairSprites);
        #endregion

        #region Initialing bag
        if (uIElements.bagScroller)
        {
            var bagInfo = uIElements.bagScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < bagInfo.Length; i++)
            {
                bagList.Add(bagInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.bag, bagList);
        SetItemIcon(bagList, bagSprites);
        #endregion

        #region Initialing lips
        if (uIElements.lipsScroller)
        {
            var lipsInfo = uIElements.lipsScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < lipsInfo.Length; i++)
            {
                lipsList.Add(lipsInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.lips, lipsList);
        SetItemIcon(lipsList, lipsSprites);
        #endregion

        #region Initialing shoes
        if (uIElements.shoesScroller)
        {
            var shoesInfo = uIElements.shoesScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < shoesInfo.Length; i++)
            {
                shoesList.Add(shoesInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.shoes, shoesList);
        SetItemIcon(shoesList, shoesSprites);
        #endregion
        
        #region Initialing necklace
        if (uIElements.necklaceScroller)
        {
            var necklaceInfo = uIElements.necklaceScroller.GetComponentsInChildren<ItemInfo>();
            for (int i = 0; i < necklaceInfo.Length; i++)
            {
                necklaceList.Add(necklaceInfo[i]);
            }
        }
        SetupItemData(SaveData.Instance.SummerFashionModeElements.necklace, necklaceList);
        SetItemIcon(necklaceList, necklaceSprites);
        #endregion

        Usman_SaveLoad.SaveProgress();
    }
    #endregion

    #region SetupItemData
    private void SetupItemData(List<bool> unlockItems, List<ItemInfo> _ItemsInfo)
    {
        if (_ItemsInfo.Count > 0)
        {
            if (unlockItems.Count < _ItemsInfo.Count)
            {
                for (int i = 0; i < _ItemsInfo.Count; i++)
                {
                    if (unlockItems.Count <= i)
                    {
                        // Add new data to SaveData file in case the file is empty or new data is available
                        unlockItems.Add(_ItemsInfo[i].isLocked);
                    }
                }
            }
            // Setting up Hairs Properties to actual Properties from SaveData file  
            for (int i = 0; i < _ItemsInfo.Count; i++)
            {
                _ItemsInfo[i].isLocked = unlockItems[i];
            }
            //Adding Click listeners to btns 
            for (int i = 0; i < _ItemsInfo.Count; i++)
            {
                int Index = i;
                if (_ItemsInfo[i].itemBtn)
                {
                    _ItemsInfo[i].itemBtn.onClick.AddListener(() =>
                    {
                        selectedIndex = Index;
                        SelectItem(Index);
                    });
                }
            }
        }
    }
    #endregion

    #region SetItemIcon
    private void SetItemIcon(List<ItemInfo> refList, Sprite[] btnIcons)
    {
        if (refList != null)
        {
            for (int i = 0; i < refList.Count; i++)
            {
                if (btnIcons.Length > i)
                {
                    if (btnIcons[i] && refList[i].itemIcon)
                    {
                        refList[i].itemIcon.sprite = btnIcons[i];
                    }
                }
            }
        }
    }
    #endregion

    #region SelectItem
    public void SelectItem(int index)
    {
        if (selectedItem == SummerFashionSelectedItem.blush)
        {
            CheckSelectedItem(blushList, blushSprites, playerElements.blushImage);
        }
        else if (selectedItem == SummerFashionSelectedItem.hat)
        {
            CheckSelectedItem(hatList, hatSprites, playerElements.hatImage);
        }
        else if (selectedItem == SummerFashionSelectedItem.upper)
        {
            CheckSelectedItem(upperList, upperSprites, playerElements.upperImage);
        }
        else if (selectedItem == SummerFashionSelectedItem.dress)
        {
            CheckSelectedItem(dressList, dressSprites, playerElements.dressImage);
        }
        else if (selectedItem == SummerFashionSelectedItem.earing)
        {
            CheckSelectedItem(earingList, earingSprites, playerElements.earingImage);
        } 
        else if (selectedItem == SummerFashionSelectedItem.eyeshade)
        {
            CheckSelectedItem(eyeshadeList, eyeshadeSprites, playerElements.eyeshadeImage);
        }
        else if (selectedItem == SummerFashionSelectedItem.glasses)
        {
            CheckSelectedItem(glassesList, glassesSprites, playerElements.glassesImage);
        }
        else if (selectedItem == SummerFashionSelectedItem.hair)
        {
            CheckSelectedItem(hairList, hairSprites, playerElements.hairImage);
        }
        else if (selectedItem == SummerFashionSelectedItem.bag)
        {
            CheckSelectedItem(bagList, bagSprites, playerElements.bagImage);
        }
        else if (selectedItem == SummerFashionSelectedItem.lips)
        {
            CheckSelectedItem(lipsList, lipsSprites, playerElements.lipsImage);
        }
        else if (selectedItem == SummerFashionSelectedItem.shoes)
        {
            CheckSelectedItem(shoesList, shoesSprites, playerElements.shoesImage);
        }
        else if (selectedItem == SummerFashionSelectedItem.necklace)
        {
            CheckSelectedItem(necklaceList, necklaceSprites, playerElements.necklaceImage);
        }
        GetItemsInfo();
        totalCoins.text = SaveData.Instance.Coins.ToString();
    }
    #endregion

    #region CheckSelectedItem
    private void CheckSelectedItem(List<ItemInfo> itemInfoList, Sprite[] itemSprites, Image itemImage)
    {
        rewardType = RewardType.selectionItem;
        if (itemInfoList.Count > selectedIndex)
        {
            tempItem = itemInfoList[selectedIndex];
            if (itemInfoList[selectedIndex].isLocked)
            {
                if (itemInfoList[selectedIndex].videoUnlock)
                {
                    //CheckVideoStatus();
                    uIElements.videoUnlockPopUp.SetActive(true);
                }
                else if (itemInfoList[selectedIndex].coinsUnlock)
                {
                    uIElements.coinUnlockPopUp.SetActive(true);
                    itemUnlockCoin.text = itemInfoList[selectedIndex].requiredCoins.ToString();
                }
            }
            else
            {
                if (itemSprites.Length > selectedIndex)
                {
                    if (itemSprites[selectedIndex])
                    {
                        if (itemImage)
                        {
                           
                            if (selectedItem == SummerFashionSelectedItem.blush)
                            {
                                IsMakeup = true;
                                if (playerblushScore == blushScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playerblushScore == 0)
                                {
                                    playerScore = playerScore + blushScore[selectedIndex];
                                    playerblushScore = blushScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playerblushScore;
                                    playerScore = playerScore + blushScore[selectedIndex];
                                    playerblushScore = blushScore[selectedIndex];
                                }
                            }
                            else if (selectedItem == SummerFashionSelectedItem.hat)
                            {
                                IsAccessioress = true;
                                if (playerhatScore == hatScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playerhatScore == 0)
                                {
                                    playerScore = playerScore + hatScore[selectedIndex];
                                    playerhatScore = hatScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playerhatScore;
                                    playerScore = playerScore + hatScore[selectedIndex];
                                    playerhatScore = hatScore[selectedIndex];
                                }
                            }
                            else if (selectedItem == SummerFashionSelectedItem.upper)
                            {
                                IsAccessioress = true;
                                if (playerupperScore == upperScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playerupperScore == 0)
                                {
                                    playerScore = playerScore + upperScore[selectedIndex];
                                    playerupperScore = upperScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playerupperScore;
                                    playerScore = playerScore + upperScore[selectedIndex];
                                    playerupperScore = upperScore[selectedIndex];
                                }
                            }
                            else if (selectedItem == SummerFashionSelectedItem.dress)
                            {
                                IsDressing = true;
                                if (playerdressScore == dressScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playerdressScore == 0)
                                {
                                    playerScore = playerScore + dressScore[selectedIndex];
                                    playerdressScore = dressScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playerdressScore;
                                    playerScore = playerScore + dressScore[selectedIndex];
                                    playerdressScore = dressScore[selectedIndex];
                                }
                            }
                            else if (selectedItem == SummerFashionSelectedItem.earing)
                            {
                                IsAccessioress = true;
                                if (playerearingScore == earingScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playerearingScore == 0)
                                {
                                    playerScore = playerScore + earingScore[selectedIndex];
                                    playerearingScore = earingScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playerearingScore;
                                    playerScore = playerScore + earingScore[selectedIndex];
                                    playerearingScore = earingScore[selectedIndex];
                                }
                            } 
                            else if (selectedItem == SummerFashionSelectedItem.eyeshade)
                            {
                                IsMakeup = true;
                                if (playereyeshadeScore == eyeshadeScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playereyeshadeScore == 0)
                                {
                                    playerScore = playerScore + eyeshadeScore[selectedIndex];
                                    playereyeshadeScore = eyeshadeScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playereyeshadeScore;
                                    playerScore = playerScore + eyeshadeScore[selectedIndex];
                                    playereyeshadeScore = eyeshadeScore[selectedIndex];
                                }
                            }
                            else if (selectedItem == SummerFashionSelectedItem.glasses)
                            {
                                IsAccessioress = true;
                                if (playerglassesScore == glassesScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playerglassesScore == 0)
                                {
                                    playerScore = playerScore + glassesScore[selectedIndex];
                                    playerglassesScore = glassesScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playerglassesScore;
                                    playerScore = playerScore + glassesScore[selectedIndex];
                                    playerglassesScore = glassesScore[selectedIndex];
                                }
                            }
                            else if (selectedItem == SummerFashionSelectedItem.necklace)
                            {
                                IsAccessioress = true;
                                if (playernecklaceScore == necklaceScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playernecklaceScore == 0)
                                {
                                    playerScore = playerScore + necklaceScore[selectedIndex];
                                    playernecklaceScore = necklaceScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playernecklaceScore;
                                    playerScore = playerScore + necklaceScore[selectedIndex];
                                    playernecklaceScore = necklaceScore[selectedIndex];
                                }
                            }
                            else if (selectedItem == SummerFashionSelectedItem.hair)
                            {
                                IsMakeup = true;
                                if (playerhairScore == hairScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playerhairScore == 0)
                                {
                                    playerScore = playerScore + hairScore[selectedIndex];
                                    playerhairScore = hairScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playerhairScore;
                                    playerScore = playerScore + hairScore[selectedIndex];
                                    playerhairScore = hairScore[selectedIndex];
                                }
                            }
                            else if (selectedItem == SummerFashionSelectedItem.bag)
                            {
                                IsAccessioress = true;
                                if (playerbagScore == bagScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playerbagScore == 0)
                                {
                                    playerScore = playerScore + bagScore[selectedIndex];
                                    playerbagScore = bagScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playerbagScore;
                                    playerScore = playerScore + bagScore[selectedIndex];
                                    playerbagScore = bagScore[selectedIndex];
                                }
                            }
                            else if (selectedItem == SummerFashionSelectedItem.lips)
                            {
                                IsMakeup = true;
                                if (playerlipsScore == lipsScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playerlipsScore == 0)
                                {
                                    playerScore = playerScore + lipsScore[selectedIndex];
                                    playerlipsScore = lipsScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playerlipsScore;
                                    playerScore = playerScore + lipsScore[selectedIndex];
                                    playerlipsScore = lipsScore[selectedIndex];
                                }
                            }
                            else if (selectedItem == SummerFashionSelectedItem.shoes)
                            {
                                if (playershoesScore == shoesScore[selectedIndex])
                                {
                                    return;
                                }

                                if (playershoesScore == 0)
                                {
                                    playerScore = playerScore + shoesScore[selectedIndex];
                                    playershoesScore = shoesScore[selectedIndex];
                                }
                                else
                                {
                                    playerScore = playerScore - playershoesScore;
                                    playerScore = playerScore + shoesScore[selectedIndex];
                                    playershoesScore = shoesScore[selectedIndex];
                                }
                            }

                            voiceSounds[Random.Range(0, voiceSounds.Length)].Play();
                            itemImage.gameObject.SetActive(false);
                            itemImage.gameObject.SetActive(true);
                            itemImage.sprite = itemSprites[selectedIndex];
                        }
                    }
                }
                CheckInterstitialAD();
            }
        }
    }
    #endregion

    #region UnlockCoinItems
    public void UnlockCoinItems()
    {
        if (selectedItem == SummerFashionSelectedItem.blush)
        {
            coinUnlockItem(blushList);
        }
        else if (selectedItem == SummerFashionSelectedItem.hat)
        {
            coinUnlockItem(hatList);
        }
        else if (selectedItem == SummerFashionSelectedItem.upper)
        {
            coinUnlockItem(upperList);
        }
        else if (selectedItem == SummerFashionSelectedItem.dress)
        {
            coinUnlockItem(dressList);
        }
        else if (selectedItem == SummerFashionSelectedItem.earing)
        {
            coinUnlockItem(earingList);
        }     
        else if (selectedItem == SummerFashionSelectedItem.eyeshade)
        {
            coinUnlockItem(eyeshadeList);
        }
        else if (selectedItem == SummerFashionSelectedItem.glasses)
        {
            coinUnlockItem(glassesList);
        }
        else if (selectedItem == SummerFashionSelectedItem.necklace)
        {
            coinUnlockItem(necklaceList);
        }
        else if (selectedItem == SummerFashionSelectedItem.hair)
        {
            coinUnlockItem(hairList);
        }
        else if (selectedItem == SummerFashionSelectedItem.bag)
        {
            coinUnlockItem(bagList);
        }
        else if (selectedItem == SummerFashionSelectedItem.lips)
        {
            coinUnlockItem(lipsList);
        }
        else if (selectedItem == SummerFashionSelectedItem.shoes)
        {
            coinUnlockItem(shoesList);
        }
        GetItemsInfo();
        totalCoins.text = SaveData.Instance.Coins.ToString();
    }
    #endregion

    #region CoinUnlockFunction
    public void coinUnlockItem(List<ItemInfo> itemInfoList)
    {
        if (SaveData.Instance.Coins >= itemInfoList[selectedIndex].requiredCoins)
        {
            uIElements.coinUnlockPopUp.SetActive(false);
            itemInfoList[selectedIndex].isLocked = false;
            SaveData.Instance.Coins -= itemInfoList[selectedIndex].requiredCoins;
            UnlockSingleItem();
            if (purchaseSFX) purchaseSFX.Play();
            SelectItem(selectedIndex);
        }
        else
        {
            if (uIElements.getCoinPopUp)
                uIElements.getCoinPopUp.SetActive(true);
        }
    }
    #endregion

    #region GetItemsInfo
    private void GetItemsInfo()
    {
        if (selectedItem == SummerFashionSelectedItem.blush)
        {
            SetItemsInfo(blushList);
        }
        else if (selectedItem == SummerFashionSelectedItem.hat)
        {
            SetItemsInfo(hatList);
        }
        else if (selectedItem == SummerFashionSelectedItem.upper)
        {
            SetItemsInfo(upperList);
        }
        if (selectedItem == SummerFashionSelectedItem.dress)
        {
            SetItemsInfo(dressList);
        }
        else if (selectedItem == SummerFashionSelectedItem.earing)
        {
            SetItemsInfo(earingList);
        }
        else if (selectedItem == SummerFashionSelectedItem.eyeshade)
        {
            SetItemsInfo(eyeshadeList);
        }
        else if (selectedItem == SummerFashionSelectedItem.glasses)
        {
            SetItemsInfo(glassesList);
        }
        else if (selectedItem == SummerFashionSelectedItem.necklace)
        {
            SetItemsInfo(necklaceList);
        }
        else if (selectedItem == SummerFashionSelectedItem.hair)
        {
            SetItemsInfo(hairList);
        }
        else if (selectedItem == SummerFashionSelectedItem.bag)
        {
            SetItemsInfo(bagList);
        }
        else if (selectedItem == SummerFashionSelectedItem.lips)
        {
            SetItemsInfo(lipsList);
        }
        else if (selectedItem == SummerFashionSelectedItem.shoes)
        {
            SetItemsInfo(shoesList);
        }
    }
    #endregion

    #region SetItemsInfo
    private void SetItemsInfo(List<ItemInfo> _ItemInfo)
    {
        if (_ItemInfo == null) return;
        for (int i = 0; i < _ItemInfo.Count; i++)
        {
            if (_ItemInfo[i].isLocked)
            {
                if (_ItemInfo[i].videoUnlock)
                {
                    if (_ItemInfo[i].videoLock)
                    {
                        _ItemInfo[i].videoLock.SetActive(true);
                    }
                    if (_ItemInfo[i].coinLock)
                    {
                        _ItemInfo[i].coinLock.SetActive(false);
                    }
                }
                else if (_ItemInfo[i].coinsUnlock)
                {
                    if (_ItemInfo[i].videoLock)
                    {
                        _ItemInfo[i].videoLock.SetActive(false);
                    }
                    if (_ItemInfo[i].coinLock)
                    {
                        _ItemInfo[i].coinLock.SetActive(true);
                        if (_ItemInfo[i].unlockCoins)
                        {
                            _ItemInfo[i].unlockCoins.text = _ItemInfo[i].requiredCoins.ToString();
                        }
                    }
                }
            }
            else
            {
                if (_ItemInfo[i].videoLock) _ItemInfo[i].videoLock.SetActive(false);
                if (_ItemInfo[i].coinLock) _ItemInfo[i].coinLock.SetActive(false);
            }
        }
    }
    #endregion

    #region RankingFormula
    private int GetRank(int selectedCard, int totalItems)
    {
        int rankDivider = 0;
        rankDivider = totalItems / 10;
        if (rankDivider == 0)
        {
            rankDivider += 1;
        }
        if (selectedCard / rankDivider < 10)
        {
            return (selectedCard / rankDivider) + 1;
        }
        else
        {
            return 10;
        }
    }
    private int GetRankValue(int _Rank)
    {
        if (_Rank > -1)
            return _Rank;
        else
            return 0;
    }
    #endregion

    #region SelectedCatagory
    private void DisableScrollers()
    {
        for (int i = 0; i < categoryBtn.Length; i++)
        {
            categoryBtn[i].GetComponent<Image>().sprite = defaultBtnSprites[i];
        }
        
        uIElements.blushScroller.SetActive(false);
        uIElements.hatScroller.SetActive(false);
        uIElements.upperScroller.SetActive(false);
        uIElements.dressScroller.SetActive(false);
        uIElements.earingScroller.SetActive(false);
        uIElements.eyeshadeScroller.SetActive(false);
        uIElements.glassesScroller.SetActive(false);
        uIElements.hairScroller.SetActive(false);
        uIElements.bagScroller.SetActive(false);
        uIElements.lipsScroller.SetActive(false);
        uIElements.shoesScroller.SetActive(false);
        uIElements.necklaceScroller.SetActive(false);
    }
    public void SelectedCatagory(int index)
    {
        DisableScrollers();
        if (categorySFX) categorySFX.Play();
        categoryBtn[index].GetComponent<Image>().sprite = selectedBtnSprites[index];

        if (index == (int)SummerFashionSelectedItem.blush)
        {
            selectedItem = SummerFashionSelectedItem.blush;
            uIElements.blushScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(18, -550, -1000), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.hat)
        {
            selectedItem = SummerFashionSelectedItem.hat;
            uIElements.hatScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(18, -550, -1000), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.upper)
        {
            selectedItem = SummerFashionSelectedItem.upper;
            uIElements.upperScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(-38, -173, 0), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.dress)
        {
            selectedItem = SummerFashionSelectedItem.dress;
            uIElements.dressScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(-38, -173, 0), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.earing)
        {
            selectedItem = SummerFashionSelectedItem.earing;
            uIElements.earingScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(18, -550, -1000), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.eyeshade)
        {
            selectedItem = SummerFashionSelectedItem.eyeshade;
            uIElements.eyeshadeScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(18, -550, -1000), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.glasses)
        {
            selectedItem = SummerFashionSelectedItem.glasses;
            uIElements.glassesScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(18, -550, -1000), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.necklace)
        {
            selectedItem = SummerFashionSelectedItem.necklace;
            uIElements.necklaceScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(18, -550, -1000), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.hair)
        {
            selectedItem = SummerFashionSelectedItem.hair;
            uIElements.hairScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(18, -550, -1000), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.bag)
        {
            selectedItem = SummerFashionSelectedItem.bag;
            uIElements.bagScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(-38, -173, 0), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.lips)
        {
            selectedItem = SummerFashionSelectedItem.lips;
            uIElements.lipsScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(18, -550, -1000), 0.5f, true, false);
        }
        else if (index == (int)SummerFashionSelectedItem.shoes)
        {
            selectedItem = SummerFashionSelectedItem.shoes;
            uIElements.shoesScroller.SetActive(true);
            playerCharacterMover.Move(new Vector3(-38, 8, 0), 0.5f, true, false);
        }
        GetItemsInfo();
    }
    #endregion

    #region UnlockSingleItem
    public void UnlockSingleItem()
    {
        if (selectedItem == SummerFashionSelectedItem.blush)
        {
            SaveData.Instance.SummerFashionModeElements.blush[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.hat)
        {
            SaveData.Instance.SummerFashionModeElements.hat[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.upper)
        {
            SaveData.Instance.SummerFashionModeElements.upper[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.dress)
        {
            SaveData.Instance.SummerFashionModeElements.dress[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.earing)
        {
            SaveData.Instance.SummerFashionModeElements.earing[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.eyeshade)
        {
            SaveData.Instance.SummerFashionModeElements.eyeshade[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.glasses)
        {
            SaveData.Instance.SummerFashionModeElements.glasses[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.hair)
        {
            SaveData.Instance.SummerFashionModeElements.hair[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.bag)
        {
            SaveData.Instance.SummerFashionModeElements.bag[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.lips)
        {
            SaveData.Instance.SummerFashionModeElements.lips[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.shoes)
        {
            SaveData.Instance.SummerFashionModeElements.shoes[selectedIndex] = false;
        }
        else if (selectedItem == SummerFashionSelectedItem.necklace)
        {
            SaveData.Instance.SummerFashionModeElements.necklace[selectedIndex] = false;
        }
        totalCoins.text = SaveData.Instance.Coins.ToString();
        Usman_SaveLoad.SaveProgress();
    }
    #endregion

    #region BtnsTask
    public void GameStart()
    {
        StartCoroutine(startGame());
    }

    public void Preview()
    {
        StartCoroutine(preview());
    }

    public void SubmitLook()
    {
        if(IsDressing == true && IsMakeup == true && IsAccessioress == true)
        {
            StartCoroutine(submitlook());
        }
        else
        {
            uIElements.warningPopUp.SetActive(true);
        }
        
    }

    public void Back(int i)
    {
        if(i == 0)
        {
            backBtnInGamePanel.Move(new Vector3(-800, 745, 0), 0.5f, true, false);
            uIElements.loadingPanel.SetActive(true);
            StartCoroutine(LoadingScene("ModeSelection"));
        }
        else if(i == 1)
        {
            DisableScrollers();
            backBtnInGamePanel.Move(new Vector3(-530, 745, 0), 0.5f, true, false);
            uIElements.submitPanel.SetActive(false);
            uIElements.submitPanelBar.SetActive(false);
            uIElements.allScroller.SetActive(true);
            uIElements.coinSlot.SetActive(true);
            uIElements.dressScroller.SetActive(true);
            selectedItem = SummerFashionSelectedItem.dress;
            playerCharacterMover.Move(new Vector3(0, -100, 0), 0.5f, true, false);
        } 
    }

    public void Play(string str)
    {
        finalPartical.gameObject.SetActive(false);
        uIElements.levelCompletePanel.SetActive(false);
        uIElements.loadingPanel.SetActive(true);
        StartCoroutine(LoadingScene(str));
    }
    #endregion

    #region IEnumerator
    IEnumerator startGame()
    {
        uIElements.vsPanel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        uIElements.vsAnimPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        uIElements.vsAnimPanel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        uIElements.bgImage.sprite = bgSprite;
        uIElements.gamePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        uIElements.coinSlot.SetActive(true);
    }
    IEnumerator findOpponent()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < Random.Range(10, 25); i++)
        {
            oppoElements.botInVsPanel.gameObject.SetActive(false);
            oppoElements.botInVsPanel.gameObject.SetActive(true);
            oppoElements.botInVsPanel.GetComponent<AudioSource>().Play();
            oppoElements.botInVsPanel.sprite = botSprites[Random.Range(0, botSprites.Length)];
            oppoElements.botInJudgementalPanel.sprite = oppoElements.botInVsPanel.sprite;
            oppoElements.botInVsAnimPanel.sprite = oppoElements.botInVsPanel.sprite;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        uIElements.nextBtn.SetActive(true);
    }

    IEnumerator preview()
    {
        uIElements.bgImage.sprite = submitPanelBgSprite;
        uIElements.submitPanel.SetActive(true);
        uIElements.submitPanelBar.SetActive(true);
        uIElements.allScroller.SetActive(false);
        uIElements.coinSlot.SetActive(false);
        playerCharacterMover.Move(new Vector3(0, -300, -800), 0.7f, true, false);
        yield return new WaitForSeconds(0.68f);
        playerCharacterMover.Move(new Vector3(0, 200, -800), 0.7f, true, false);
        yield return new WaitForSeconds(0.68f);
        playerCharacterMover.Move(new Vector3(0, -100, -100), 0.7f, true, false);
    }
    IEnumerator submitlook()
    {
        uIElements.bgImage.sprite = judgementalPanelBgSprite;
        uIElements.judgementalPanel.SetActive(true);
        uIElements.submitPanel.SetActive(false);
        
        yield return new WaitForSeconds(1f);
        oppoElements.character.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerCharacterMover.Move(new Vector3(-300, -100, 350), 0.7f, true, false);
        yield return new WaitForSeconds(0.3f);
        oppoCharacterMover.Move(new Vector3(350, -100, 350), 0.7f, true, false);
        uIElements.vsImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(startComparing());
    }

    IEnumerator objectActivation(GameObject obj, float time, bool istrue)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(istrue);
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

    IEnumerator AddCoinAnim(int coins)
    {
        yield return null;
        if (coinsAdder)
        {
            coinsAdder.addCoins = coins;
            coinsAdder.addNow = true;
        }
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
    #endregion

    #region CheckVideoStatus
    public void CheckVideoStatus()
    {
        if (MyAdsManager.Instance != null)
        {
            if (MyAdsManager.Instance.IsRewardedAvailable())
            {
                uIElements.getCoinPopUp.SetActive(false);
                MyAdsManager.Instance.ShowRewardedVideos();
            }
            else
            {
                uIElements.videoAdNotAvailablePopUp.SetActive(true);
                StartCoroutine(objectActivation(uIElements.videoAdNotAvailablePopUp, 2.2f, false));
            }
        }
        else
        {
            uIElements.videoAdNotAvailablePopUp.SetActive(true);
            StartCoroutine(objectActivation(uIElements.videoAdNotAvailablePopUp, 2.2f, false));
        }
    }

    public void showRewardedVideo()
    {
        if (MyAdsManager.Instance != null)
        {
            if (MyAdsManager.Instance.IsRewardedAvailable())
            {
                uIElements.videoUnlockPopUp.SetActive(false);
                uIElements.coinUnlockPopUp.SetActive(false);
                MyAdsManager.Instance.ShowRewardedVideos();
            }
            else
            {
                uIElements.videoAdNotAvailablePopUp.SetActive(true);
                StartCoroutine(objectActivation(uIElements.videoAdNotAvailablePopUp, 2.2f, false));
            }
        }
        else
        {
            uIElements.videoAdNotAvailablePopUp.SetActive(true);
            StartCoroutine(objectActivation(uIElements.videoAdNotAvailablePopUp, 2.2f, false));
        }
    }

    #endregion

    #region GetRewardedCoins
    public void GetRewardedCoins()
    {
        rewardType = RewardType.coins;
        CheckVideoStatus();
    }
    #endregion

    #region RewardedVideoCompleted
    public void OnRewardedVideoComplete()
    {
        if (rewardType == RewardType.selectionItem)
        {
            if (tempItem != null) tempItem.isLocked = false;
            UnlockSingleItem();
            canShowInterstitial = false;
            //StartCoroutine(AdDelay(45));
            SelectItem(selectedIndex);
        }
        else if (rewardType == RewardType.coins)
        {
            StartCoroutine(AddCoinAnim(200));
            //SaveData.Instance.Coins += 2 * totalReward;
            totalCoins.text = SaveData.Instance.Coins.ToString();
            Usman_SaveLoad.SaveProgress();
        }
        else if (rewardType == RewardType.multipulOfTwo)
        {
            if (playerTotalScore >= oppoTotalScore)
            {
                StartCoroutine(AddCoinAnim(4000));
            }
            else
            {
                StartCoroutine(AddCoinAnim(1000));
            }
            //SaveData.Instance.Coins += 2 * totalReward;
            totalCoins.text = SaveData.Instance.Coins.ToString();
            Usman_SaveLoad.SaveProgress();
        }
        GetItemsInfo();
        rewardType = RewardType.none;
        //if (purchaseSFX) purchaseSFX.Play();
    }
    public void changeRewardedCategory()
    {
        rewardType = RewardType.multipulOfTwo;
        CheckVideoStatus();
    }
    #endregion

    #region ShowInterstitialAD
    private void CheckInterstitialAD()
    {
        //if (MyAdsManager.Instance != null)
        //{

        //    if (MyAdsManager.Instance.IsInterstitialAvailable() && canShowInterstitial)
        //    {
        //        canShowInterstitial = !canShowInterstitial;
        //        StartCoroutine(AdDelay(45));
        //        StartCoroutine(ShowInterstitialAD());
        //    }
        //}
    }

    IEnumerator ShowInterstitialAD()
    {
        if (uIElements.adPanel)
        {
            uIElements.adPanel.SetActive(true);
            yield return new WaitForSeconds(1f);
            uIElements.adPanel.SetActive(false);
        }
        //ShowInterstitial();
    }
    IEnumerator AdDelay(float _Delay)
    {
        yield return new WaitForSeconds(_Delay);
        canShowInterstitial = !canShowInterstitial;
    }
    #endregion

    #region EnableOrDisable
    IEnumerator EnableOrDisable(float _Delay, GameObject activateObject, bool isTrue)
    {
        yield return new WaitForSecondsRealtime(_Delay);
        activateObject.SetActive(isTrue);
    }
    IEnumerator EnableAnim(float _Delay, Animator activateObject)
    {
        yield return new WaitForSecondsRealtime(_Delay);
        activateObject.enabled = true;
    }


    #endregion

    #region dressUpOpponent
    private void dressUpOpponent()
    {
        int randomIndex = 0;

        #region dress
        randomIndex = Random.Range(0, dressList.Count);
        if (dressList[randomIndex] && oppoElements.dressImage)
        {
            oppoElements.dressImage.gameObject.SetActive(true);
            oppoElements.dressImage.sprite = dressSprites[randomIndex];
        }

        oppoScore = oppoScore + dressScore[randomIndex];
        oppodressScore = dressScore[randomIndex];
        #endregion

        #region shoes
        randomIndex = Random.Range(0, shoesList.Count);
        if (shoesList[randomIndex] && oppoElements.shoesImage)
        {
            oppoElements.shoesImage.gameObject.SetActive(true);
            oppoElements.shoesImage.sprite = shoesSprites[randomIndex];
        }
        oppoScore = oppoScore + shoesScore[randomIndex];
        opposhoesScore = shoesScore[randomIndex];
        #endregion

        #region eyeshade
        randomIndex = Random.Range(0, eyeshadeList.Count);
        if (eyeshadeList[randomIndex] && oppoElements.eyeshadeImage)
        {
            oppoElements.eyeshadeImage.gameObject.SetActive(true);
            oppoElements.eyeshadeImage.sprite = eyeshadeSprites[randomIndex];
        }
        oppoScore = oppoScore + eyeshadeScore[randomIndex];
        oppoeyeshadeScore = eyeshadeScore[randomIndex];
        #endregion

        #region hair
        randomIndex = Random.Range(0, hairList.Count);
        if (hairList[randomIndex] && oppoElements.hairImage)
        {
            oppoElements.hairImage.gameObject.SetActive(true);
            oppoElements.hairImage.sprite = hairSprites[randomIndex];
        }
        oppoScore = oppoScore + hairScore[randomIndex];
        oppohairScore = hairScore[randomIndex];
        #endregion

        #region lips
        randomIndex = Random.Range(0, lipsList.Count);
        if (lipsList[randomIndex] && oppoElements.lipsImage)
        {
            oppoElements.lipsImage.gameObject.SetActive(true);
            oppoElements.lipsImage.sprite = lipsSprites[randomIndex];
        }
        oppoScore = oppoScore + lipsScore[randomIndex];
        oppolipsScore = lipsScore[randomIndex];
        #endregion

        #region blush
        randomIndex = Random.Range(0, blushList.Count);
        if (blushList[randomIndex] && oppoElements.blushImage)
        {
            oppoElements.blushImage.gameObject.SetActive(true);
            oppoElements.blushImage.sprite = blushSprites[randomIndex];
        }
        oppoScore = oppoScore + blushScore[randomIndex];
        oppoblushScore = blushScore[randomIndex];
        #endregion

        #region bag
        randomIndex = Random.Range(0, bagList.Count);
        if (bagList[randomIndex] && oppoElements.bagImage)
        {
            oppoElements.bagImage.gameObject.SetActive(true);
            oppoElements.bagImage.sprite = bagSprites[randomIndex];
        }
        oppoScore = oppoScore + bagScore[randomIndex];
        oppobagScore = bagScore[randomIndex];
        #endregion

        #region glasses
        randomIndex = Random.Range(0, glassesList.Count);
        if (glassesList[randomIndex] && oppoElements.glassesImage)
        {
            oppoElements.glassesImage.gameObject.SetActive(true);
            oppoElements.glassesImage.sprite = glassesSprites[randomIndex];
        }
        oppoScore = oppoScore + glassesScore[randomIndex];
        oppoglassesScore = glassesScore[randomIndex];
        #endregion

        #region earing
        randomIndex = Random.Range(0, earingList.Count);
        if (earingList[randomIndex] && oppoElements.earingImage)
        {
            oppoElements.earingImage.gameObject.SetActive(true);
            oppoElements.earingImage.sprite = earingSprites[randomIndex];
        }
        oppoScore = oppoScore + earingScore[randomIndex];
        oppoearingScore = earingScore[randomIndex];
        #endregion

        #region hat
        randomIndex = Random.Range(0, hatList.Count);
        if (hatList[randomIndex] && oppoElements.hatImage)
        {
            oppoElements.hatImage.gameObject.SetActive(true);
            oppoElements.hatImage.sprite = hatSprites[randomIndex];
        }
        oppoScore = oppoScore + hatScore[randomIndex];
        oppohatScore = hatScore[randomIndex];
        #endregion

        #region upper
        randomIndex = Random.Range(0, upperList.Count);
        if (upperList[randomIndex] && oppoElements.upperImage)
        {
            oppoElements.upperImage.gameObject.SetActive(true);
            oppoElements.upperImage.sprite = upperSprites[randomIndex];
        }
        oppoScore = oppoScore + upperScore[randomIndex];
        oppoupperScore = upperScore[randomIndex];
        #endregion

        #region necklace
        randomIndex = Random.Range(0, necklaceList.Count);
        if (necklaceList[randomIndex] && oppoElements.necklaceImage)
        {
            oppoElements.necklaceImage.gameObject.SetActive(true);
            oppoElements.necklaceImage.sprite = necklaceSprites[randomIndex];
        }
        oppoScore = oppoScore + necklaceScore[randomIndex];
        opponecklaceScore = necklaceScore[randomIndex];
        #endregion
    }
    #endregion

    #region Comparing
    IEnumerator startComparing()
    {

        int playerdressupTotal, playermakeupTotal, playeraccessioresTotal;
        int oppodressupTotal, oppomakeupTotal, oppoaccessioresTotal;
        int playerTotal = 0, oppoTotal = 0;
        yield return new WaitForSeconds(2);

        categoryNameAndScoreAnim.gameObject.SetActive(true);
        categoriesText.text = "DressUp";
        if (voteCatSFX) voteCatSFX.Play();
        categoryNameAndScoreAnim.Play(0);
        //player
        yield return new WaitForSeconds(2f);
        playerdressupTotal = playerdressScore + playershoesScore;
        playerTotal += playerdressupTotal;
        playerElements.voteScore.text = playerdressupTotal.ToString();
        //oppo
        oppodressupTotal = oppodressScore + opposhoesScore;
        oppoTotal += oppodressupTotal;
        oppoElements.voteScore.text = oppodressupTotal.ToString();
        TotalScoring();

        yield return new WaitForSeconds(2f);
        categoriesText.text = "MakeUp";
        if (voteCatSFX) voteCatSFX.Play();
        categoryNameAndScoreAnim.Play(0);
        //player
        yield return new WaitForSeconds(1f);
        playermakeupTotal = playereyeshadeScore + playerlipsScore + playerhairScore + playerblushScore;
        playerTotal += playermakeupTotal;
        playerElements.voteScore.text = playermakeupTotal.ToString();
        //oppo
        oppomakeupTotal = oppoeyeshadeScore + oppolipsScore + oppohairScore + oppoblushScore;
        oppoTotal += oppomakeupTotal;
        oppoElements.voteScore.text = oppomakeupTotal.ToString();
        TotalScoring();

        yield return new WaitForSeconds(2f);
        categoriesText.text = "Accessiores";
        if (voteCatSFX) voteCatSFX.Play();
        categoryNameAndScoreAnim.Play(0);
        //player
        yield return new WaitForSeconds(1f);
        playeraccessioresTotal = playerearingScore + playerglassesScore + playerhatScore + playernecklaceScore + playerupperScore + playerbagScore;
        playerTotal += playeraccessioresTotal;
        playerElements.voteScore.text = playeraccessioresTotal.ToString();
        //oppo
        oppoaccessioresTotal = oppoearingScore + oppoglassesScore + oppohatScore + opponecklaceScore + oppoupperScore + oppobagScore;
        oppoTotal += oppoaccessioresTotal;
        oppoElements.voteScore.text = oppoaccessioresTotal.ToString();
        TotalScoring();

        yield return new WaitForSeconds(2);
        categoryNameAndScoreAnim.gameObject.SetActive(false);
        if (playerTotal >= oppoTotal)
        {
            //player win
            if (winSFX) winSFX.Play();
            yield return new WaitForSeconds(1);
            playerElements.winner.SetActive(true);
            yield return new WaitForSeconds(3f);
            playerElements.winner.SetActive(false);
            playerElements.character.transform.SetSiblingIndex(-1);
            yield return new WaitForSeconds(1f);
            uIElements.vsImage.SetActive(false);
            yield return new WaitForSeconds(1f);
            oppoCharacterMover.Move(new Vector3(1500, -1500, 0), 0.5f, true, false);
            yield return new WaitForSeconds(0.3f);
            playerCharacterMover.Move(new Vector3(0, -150, 0), 0.5f, true, false);
            yield return new WaitForSeconds(1f);
            uIElements.judgementalPanel.SetActive(false);
            playerCharacterMover.Move(new Vector3(0, 79, 1098), 0.5f, true, false);
            uIElements.levelCompletePanel.SetActive(true);
            yield return new WaitForSeconds(1f);
            finalPartical.SetActive(true);
            uIElements.coinSlot.SetActive(true);

        }
        else
        {
            if (loseSFX) loseSFX.Play();
            yield return new WaitForSeconds(1);
            oppoElements.winner.SetActive(true);
            yield return new WaitForSeconds(3f);
            oppoElements.winner.SetActive(false);
            oppoElements.character.transform.SetSiblingIndex(-1);
            yield return new WaitForSeconds(1f);
            uIElements.vsImage.SetActive(false);
            yield return new WaitForSeconds(1f);
            playerCharacterMover.Move(new Vector3(-1500, -150, 0), 0.5f, true, false);
            yield return new WaitForSeconds(0.3f);
            oppoCharacterMover.Move(new Vector3(0, -150, 0), 0.5f, true, false);
            yield return new WaitForSeconds(1f);
            uIElements.judgementalPanel.SetActive(false);
            oppoElements.character.SetActive(false);
            playerCharacterMover.Move(new Vector3(0, 79, 1098), 0.5f, true, false);
            uIElements.levelCompletePanel.SetActive(true);
            yield return new WaitForSeconds(1f);
            finalPartical.SetActive(true);
            uIElements.coinSlot.SetActive(true);

        }
    }
    private void TotalScoring()
    {
        playerTotalScore += int.Parse(playerElements.voteScore.text);
        playerElements.totalScore.text = playerTotalScore.ToString();
        oppoTotalScore += int.Parse(oppoElements.voteScore.text);
        oppoElements.totalScore.text = oppoTotalScore.ToString();
    }
    #endregion

    public void PlayerReward(int index)
    {
        if(index == 0)
        {
            StartCoroutine(playerReward());
        }
        else if(index == 1)
        {
            StartCoroutine(playerMultipleofTwoReward());
        }
        
    }

    IEnumerator playerReward()
    {
        yield return new WaitForSeconds(0.5f);

        if(playerTotalScore >= oppoTotalScore)
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(AddCoins(0.1f, 2000));
        }
        else
        {

            yield return new WaitForSeconds(0.1f);
            StartCoroutine(AddCoins(0.1f, 500));

        }
        yield return new WaitForSeconds(4f);
        finalPartical.gameObject.SetActive(false);
        uIElements.levelCompletePanel.SetActive(false);
        uIElements.loadingPanel.SetActive(true);
        StartCoroutine(LoadingScene("ModeSelection"));
    }

    IEnumerator playerMultipleofTwoReward()
    {
        yield return new WaitForSeconds(0.1f);
        rewardType = RewardType.multipulOfTwo;
        CheckVideoStatus();
        yield return new WaitForSeconds(4f);
        finalPartical.gameObject.SetActive(false);
        uIElements.levelCompletePanel.SetActive(false);
        uIElements.loadingPanel.SetActive(true);
        StartCoroutine(LoadingScene("ModeSelection"));
    }

    #region ShareScreenShot
    public void ShareScreenShot()
    {
        uIElements.submitPanelBar.SetActive(false);
        StartCoroutine(shareScreenShot());
    }

    IEnumerator shareScreenShot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tx.Apply();
        string path = Path.Combine(Application.temporaryCachePath, "sharedImage.png");//image name
        File.WriteAllBytes(path, tx.EncodeToPNG());

        Destroy(tx); //to avoid memory leaks

        new NativeShare()
            .AddFile(path)
            //.SetSubject("This is my score")
            //.SetText("share your score with your friends")
            .Share();
        //uIElements.SubmitPanel.SetActive(false); //hide the panel
        uIElements.submitPanelBar.SetActive(true);
    }
    #endregion

    #region TakeScreenShot
    Texture2D _Taxture;
    public void TakeScreenShot()
    {
        uIElements.screenShotImage.transform.parent.localScale = Vector3.one;
        //uIElements.previewPanel.SetActive(false);
        StartCoroutine(takeScreenShot());
    }
    IEnumerator takeScreenShot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D _Texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        _Texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _Texture.Apply();
        _Texture.LoadImage(_Texture.EncodeToPNG());
        Sprite sprite = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height), new Vector2(_Texture.width / 2, _Texture.height / 2));
        if (uIElements.screenShotImage)
        {
            uIElements.screenShotImage.sprite = sprite;
            uIElements.screenShotPanel.SetActive(true);
            //DownloadImage();
        }
        _Taxture = _Texture;
        Invoke("DownloadImage", 0.8f);
    }
    public void DownloadImage()
    {
        string picturName = "ScreenShot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        NativeGallery.SaveImageToGallery(_Taxture, "My Pictures", picturName);
        Invoke("PictureSaved", 0.8f);

    }
    private void PictureSaved()
    {
        uIElements.screenShotPanel.SetActive(false);
        uIElements.submitPanel.SetActive(true);
        Destroy(_Taxture);
    }
    #endregion
}
