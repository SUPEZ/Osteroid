using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float speed, tilt;
    public GameObject goodSmile;
    public Text score;
    public int cntScore = 0;

    public bool semaphoreForSpawn = true;
    public bool semaphoreForSpeed = true;
    
    private ArrayList smiles = new ArrayList();
    private Vector3 spawnPosition;
    
    void Start () {
        
        spawnPosition = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-3.5f, 3.5f), 0);
        AddSmile();
    }
	
	void Update () {
        score.text = cntScore.ToString();
        if (cntScore != 0 && cntScore % 10 == 0 && semaphoreForSpawn == true) {
            AddSmile();
            semaphoreForSpawn = false;
        }
        if (cntScore == 20 && semaphoreForSpeed == true)
        {
            speed += 0.5f;
            semaphoreForSpeed = false;
        }
    }

    void AddSmile ()
    {
        GameObject smile = Instantiate(goodSmile, spawnPosition, Quaternion.identity);
        smile.GetComponent<CntrlSmile>().gameController = this;
        smiles.Add(smile);
    }
}
