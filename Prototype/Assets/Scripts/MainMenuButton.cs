using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {

    public Sprite SpriteOnMouseUpButton, SpriteOnMouseDownButton;

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
        }
    }
}
