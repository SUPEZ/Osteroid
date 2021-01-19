using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public float speed, tilt;
    public GameObject goodSmile;
        
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
        AddSmile();
    }
	
	void Update () {
        // Score
        score.text = cntScore.ToString();        

        if (PlayerPrefs.GetInt("Score") < cntScore)
            PlayerPrefs.SetInt("Score", cntScore);
        
        // Smile Work
        if (cntScore != 0 && cntScore % 10 == 0 && semaphoreForSpawn == true) {
            AddSmile();
            semaphoreForSpawn = false;
        }
        if (cntScore == 20 && semaphoreForSpeed == true)
        {
            speed += 0.5f;
            semaphoreForSpeed = false;
        }

        //Lose
        if (!GameObject.FindWithTag("Smile"))
        {
            PlayerLose();
        }
    }

    void AddSmile ()
    {        
        spawnPosition = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-3.5f, 3.5f), 0);
        GameObject smile = Instantiate(goodSmile, spawnPosition, Quaternion.identity);
        smile.GetComponent<CntrlSmile>().gameController = this;
        if (cntScore / 10 == randomGold)
        {
            smile.GetComponent<CntrlSmile>().isGold = true;
            randomGold = randomSeed(1, 3) * GoldBoundsMultiplier;
            GoldBoundsMultiplier = GoldBoundsMultiplier + 10;
            Debug.Log(randomGold);

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
