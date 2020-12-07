using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxGems : MonoBehaviour {

    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("Gems").ToString();
    }

}
