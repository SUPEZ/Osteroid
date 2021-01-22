using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CntrlSmile : MonoBehaviour
{
    public GameController gameController;
    public GameObject objectGoodSmile;
    public bool isGold = false;
    public bool isAnger = false;
    public AudioSource sourceSound;

    private float speed, tilt;
    private Vector3 target;
    private Vector3 rotateOrientation;
    private int localScore = 0;
    private int randomAngerActivator;

    private void Start()
    {
        if (gameController)
        {
            speed = gameController.speed;
            tilt = gameController.tilt;
        }
        ChangeTarget();
        randomAngerActivator = Random.Range(5, 9);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        objectGoodSmile.transform.Rotate(rotateOrientation * tilt);
        if (localScore % randomAngerActivator == 0 && localScore != 0)
        {
            isAnger = true;
            tilt = Random.Range(-10, 10);
            objectGoodSmile.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red + Color.black);            
        }
        else if (isAnger == false)
        {
            if (isGold)
            {
                objectGoodSmile.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow );                
            }
            else
            {
                objectGoodSmile.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.gray + Color.black);
            }
            
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
        SoundEffect(sourceSound);

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
        //Координаты 
        if (gameObject.transform.position.x >= 0 && gameObject.transform.position.y >= 0)
        {
            target = new Vector3(Random.Range(-10f, 1f) * 100f, Random.Range(-10f,-1f) * 100f, 0);
        }
        else if (gameObject.transform.position.x < 0 && gameObject.transform.position.y >= 0)
        {
            target = new Vector3(Random.Range(1f, 10f) * 100f, Random.Range(-10f, -1f) * 100f, 0);
        }
        else if (gameObject.transform.position.x < 0 && gameObject.transform.position.y < 0)
        {
            target = new Vector3(Random.Range(1f, 10f) * 100f, Random.Range(1f, 10f) * 100f, 0f);
        }
        else if (gameObject.transform.position.x >= 0 && gameObject.transform.position.y < 0)
        {
            target = new Vector3(Random.Range(-10f, -1f) * 100f, Random.Range(1f, 10f) * 100f, 0);
        }
        //Поворот
        rotateOrientation = RandomRotate();
    }

    private Vector3 RandomRotate()
    {
        var randomCnt = Random.Range(1, 6);
        switch (randomCnt)
        {
            case 1:
                return Vector3.back;
            case 2:
                return Vector3.forward;
            case 3:
                return Vector3.down;
            case 4:
                return Vector3.up;
            case 5:
                return Vector3.left;
            case 6:
                return Vector3.right;
        } 
        return Vector3.zero;
    }

    private void SoundEffect(AudioSource sourceSound)
    {
        sourceSound.pitch = Random.Range(0.75f, 1.25f);
        sourceSound.PlayOneShot(sourceSound.clip);
    }
}
