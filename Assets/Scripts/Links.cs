using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Links : MonoBehaviour
{
    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/search?q=pub%3A%20S%20%26%20U%20Games&c=apps&hl=en&gl=PK");
    }
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.sugames.fashiongirl.makeup.game");
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://docs.google.com/document/d/1FifyUdyGCnEXalDgDaGcYahm7rHYyQSZfq43Uw1XqVw/edit");
    }
}