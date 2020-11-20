using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CntrlSmile : MonoBehaviour
{
    public GameController gameController;
    public Sprite goodSmile;
    public Sprite badSmile;
    private float speed, tilt;
    private Vector3 target;
    private bool anger = false;
    private int localScore = 0;

    private void Start()
    {
        speed = gameController.speed;
        tilt = gameController.tilt;
        ChangeTarget();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.Rotate(Vector3.back * tilt);
        if (localScore % 5 == 0 && localScore != 0)
        {
            anger = true;
            tilt = Random.Range(-10, 10);
            GetComponent<SpriteRenderer>().sprite = badSmile;  
        }else if (localScore % 5 != 0 && anger == true)
        {
            anger = false;
            GetComponent<SpriteRenderer>().sprite = goodSmile;
        }
        if (gameController.cntScore == 20)
        {
            speed = gameController.speed;
        }
    }
    
    private void OnCollisionEnter(Collision nearObj)
    {
        
        if (nearObj.gameObject.tag == "Wall")
        {
            Debug.Log(nearObj.gameObject.name);
            if (anger == true)
            {
                localScore += 1;
                gameController.cntScore += 1;
                ChangeTarget();
                
            }else if (anger == false)
            {
                Destroy(gameObject);
            }
            
        }
        if (nearObj.gameObject.tag == "Smile")
        {
            if (anger == true && nearObj.gameObject.GetComponent < CntrlSmile > ().anger == false)
            {
                localScore += 1;
                gameController.cntScore += 1;
                Destroy(nearObj.gameObject);

            }
            else if (anger == true && nearObj.gameObject.GetComponent<CntrlSmile>().anger == true)
            {
                ChangeTarget();
            }
            else if (anger == false && nearObj.gameObject.GetComponent<CntrlSmile>().anger == false)
            {
                ChangeTarget();
            }
        }
        
    }

    void OnMouseUpAsButton()
    {
        if (anger)
        {
            Destroy(gameObject);
            return;
        }else if (!anger)
        {
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
            target = new Vector3(Random.Range(-10, 0) * 100, Random.Range(-10, 0) * 100, 0);
        }
        else if (gameObject.transform.position.x < 0 && gameObject.transform.position.y >= 0)
        {
            target = new Vector3(Random.Range(0, 10) * 100, Random.Range(-10, 0) * 100, 0);
        }
        else if (gameObject.transform.position.x < 0 && gameObject.transform.position.y < 0)
        {
            target = new Vector3(Random.Range(0, 10) * 100, Random.Range(0, 10) * 100, 0);
        }
        else if (gameObject.transform.position.x >= 0 && gameObject.transform.position.y < 0)
        {
            target = new Vector3(Random.Range(-10, 0) * 100, Random.Range(0, 10) * 100, 0);
        }
    }
}
