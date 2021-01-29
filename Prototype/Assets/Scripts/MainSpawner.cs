using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MainSpawner : MonoBehaviour {

    public float speed, tilt;
    public GameObject Meteor;
    public UnityEvent SpawnMeteorEvent;
        
    public Text score;
    public int cntScore = 0;
    public int GoldBoundsMultiplier = 10;

    public bool semaphoreForSpawn = true;
    public bool semaphoreForSpeed = true;
    
    private Vector3 spawnPosition;
    
    private int randomGold;

    void Start () {
        randomGold = randomSeed(1,3);
        Debug.Log(randomGold);
        AddMeteor();

    }
	
	void Update () {
        // Score
        score.text = cntScore.ToString();        

        if (PlayerPrefs.GetInt("Score") < cntScore)
            PlayerPrefs.SetInt("Score", cntScore);
        
        // Smile Work
        if (cntScore != 0 && cntScore % 10 == 0 && semaphoreForSpawn == true) {
            semaphoreForSpawn = false; 
            SpawnMeteorEvent.Invoke();
        }
        
        if (cntScore % 10 == 9)
        {
            semaphoreForSpawn = true;
        }
        //Lose
        if (!GameObject.FindWithTag("Meteor"))
        {
            PlayerLose();
        }
    }

    public void AddMeteor()
    {        
        spawnPosition = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-3.5f, 3.5f), 0);
        GameObject _objectMeteorButton = Instantiate(Meteor, spawnPosition, Quaternion.identity);
        MeteorButton meteorButton = _objectMeteorButton.GetComponent<MeteorButton>();
        meteorButton.mainSpawner = this;
        meteorButton.objectMeteor.BecameNormal();

        if (cntScore / 10 == randomGold)
        {
            meteorButton.objectMeteor.BecameGold();
            randomGold = randomSeed(1, 3) * GoldBoundsMultiplier;
            GoldBoundsMultiplier = GoldBoundsMultiplier + 10;
        }        
    }
        
    private void PlayerLose()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private int randomSeed(int first, int second)
    {
        return Random.Range(first, second);
    }
}
