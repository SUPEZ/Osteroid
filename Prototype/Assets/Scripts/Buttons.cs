using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public AudioSource sourceSound;
     
    private IEnumerator OnMouseUpAsButton()
    {
        SoundEffect(sourceSound);
        yield return new WaitForSeconds(0.5f);
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

    private void SoundEffect(AudioSource sourceSound)
    {
        sourceSound.pitch = Random.Range(0.75f, 1.25f);
        sourceSound.PlayOneShot(sourceSound.clip);
    }
       
}
