using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {
    public enum ButtonType { Play, Tutorial, Market, Ads, Options, Main}
    public ButtonType buttonType;
    
    public AudioSource sourceSound;

    private IEnumerator OnMouseUpAsButton()
    {
        SoundEffect(sourceSound);
        yield return new WaitForSeconds(0.5f);
        switch (buttonType)
        {
            case ButtonType.Play:                
                SceneManager.LoadScene("Play");
                break;
            case ButtonType.Tutorial:
                SceneManager.LoadScene("Tutorial");
                break;
            case ButtonType.Market:
                SceneManager.LoadScene("Market");
                break;
            case ButtonType.Ads:
                SceneManager.LoadScene("Ads");
                break;
            case ButtonType.Options:
                SceneManager.LoadScene("Options");
                break;
            case ButtonType.Main:
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
