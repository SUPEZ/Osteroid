using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CntrlSmile : MonoBehaviour
{
    public GameController gameController;
    public Sprite goodSmile;
    public Sprite badSmile;
    public Sprite goldSmile;
    public bool isGold = false;

    private float speed, tilt;
    private Vector3 target;
    private bool isAnger = false;
    private int localScore = 0;
    private int randomAngerActivator;

    private void Start()
    {
        speed = gameController.speed;
        tilt = gameController.tilt;
        ChangeTarget();
        randomAngerActivator = Random.Range(5, 9);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.Rotate(Vector3.back * tilt);
        if (localScore % randomAngerActivator == 0 && localScore != 0)
        {
            isAnger = true;
            tilt = Random.Range(-10, 10);
            GetComponent<SpriteRenderer>().sprite = badSmile;
        }
        else if (isAnger == false)
        {
            if (isGold)
            {
                GetComponent<SpriteRenderer>().sprite = goldSmile;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = goodSmile;
            }
            
        }
        if (gameController.cntScore == 20)
        {
            speed = gameController.speed;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D nearObj)
    {
        
        if (nearObj.gameObject.tag == "Wall")
        {
            Debug.Log(nearObj.gameObject.name);
            if (isAnger == true)
            {
                localScore += 1;
                gameController.cntScore += 1;
                isAnger = false;
                randomAngerActivator = Random.Range(5, 9);
                ChangeTarget();
                
            }else if (isAnger == false)
            {
                Destroy(gameObject);
            }
            
        }
        if (nearObj.gameObject.tag == "Smile")
        {
            if (isAnger == true && nearObj.gameObject.GetComponent < CntrlSmile > ().isAnger == false)
            {
                localScore += 1;
                gameController.cntScore += 1;
                Destroy(nearObj.gameObject);

            }
            else if (isAnger == true && nearObj.gameObject.GetComponent<CntrlSmile>().isAnger == true)
            {
                ChangeTarget();
            }
            else if (isAnger == false && nearObj.gameObject.GetComponent<CntrlSmile>().isAnger == false)
            {
                ChangeTarget();
            }
        }
        
    }

    void OnMouseUpAsButton()
    {
        if (isAnger)
        {
            Destroy(gameObject);
            return;
        }else if (!isAnger)
        {
            if (isGold)
                PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + 1);
            gameController.cntScore += 1;
            localScore += 1;
        }
        if (gameController.semaphoreForSpawn == false)
        {
            gameController.semaphoreForSpawn = true;
        }
        if (gameController.semaphoreForSpeed == false)
        {
            gameController.semaphoreForSpeed = true;
        }
        ChangeTarget();
    }

    private void ChangeTarget()// смена конечной точки
    {
        if (gameObject.transform.position.x >= 0 && gameObject.transform.position.y >= 0)
        {
            target = new Vector3(Random.Range(-10, 1) * 100, Random.Range(-10, 1) * 100, 0);
        }
        else if (gameObject.transform.position.x < 0 && gameObject.transform.position.y >= 0)
        {
            target = new Vector3(Random.Range(1, 10) * 100, Random.Range(-10, 1) * 100, 0);
        }
        else if (gameObject.transform.position.x < 0 && gameObject.transform.position.y < 0)
        {
            target = new Vector3(Random.Range(1, 10) * 100, Random.Range(1, 10) * 100, 0);
        }
        else if (gameObject.transform.position.x >= 0 && gameObject.transform.position.y < 0)
        {
            target = new Vector3(Random.Range(-10, 1) * 100, Random.Range(1, 10) * 100, 0);
        }
    }
    
}
