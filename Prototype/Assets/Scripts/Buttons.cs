using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    
    private void OnMouseDown()
    {
        
    }

    private void OnMouseUp()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "PlayButton":
                SceneManager.LoadScene("Play");
                break;
            case "TutorialButton":
                SceneManager.LoadScene("Tutorial");
                break;
            case "MarketButton":
                SceneManager.LoadScene("Market");
                break;
            case "AdsButton":
                SceneManager.LoadScene("Ads");
                break;
            case "OptionsButton":
                SceneManager.LoadScene("Options");
                break;
            case "MainMenuButton":
                SceneManager.LoadScene("Main menu");
                break;

        }
    }
}
