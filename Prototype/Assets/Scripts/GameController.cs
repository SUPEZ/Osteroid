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
    public int randomGold;

    public bool semaphoreForSpawn = true;
    public bool semaphoreForSpeed = true;
    
    private ArrayList smiles = new ArrayList();
    private Vector3 spawnPosition;
    
    void Start () {
        randomGold = Random.Range(20, 200);        
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
        if (cntScore == randomGold)
        {
            smile.GetComponent<CntrlSmile>().isGold = true;
            randomGold = Random.Range(randomGold, randomGold + 100);
        }
        smiles.Add(smile);        
    }
        
    private void PlayerLose()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
