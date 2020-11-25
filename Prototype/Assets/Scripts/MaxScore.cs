using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxScore : MonoBehaviour {

	void Start () {
        GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString(); 
	}
	
}
