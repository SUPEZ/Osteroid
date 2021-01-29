using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorButton : MonoBehaviour
{
    public MainSpawner mainSpawner;
    public Meteor objectMeteor;

    [SerializeField] private AudioSource _sourceSound;
    [SerializeField] private float _speed, _tilt;
    [SerializeField] private Vector3 _target;
    [SerializeField] private Vector3 _rotateOrientation;
    [SerializeField] private int _localScore = 0;
    [SerializeField] private int _randomAngerActivator;

    private Transform _transformMeteor = null;
    private Transform _transformButton = null;
    
    private bool _transformMeteorCached = false;
    private bool _transformButtonCached = false;
    
    public Transform cachedMeteorTransform
    {
        get
        {
            if ( !_transformMeteorCached)
            {
                _transformMeteorCached = true;
                _transformMeteor = objectMeteor.GetComponent<Transform>();
            }
            return _transformMeteor;
            
        }
    }

    public Transform cachedButtonTransform
    {
        get
        {
            if (!_transformButtonCached)
            {
                _transformButtonCached = true;
                _transformButton = GetComponent<Transform>();
            }
            return _transformButton;

        }
    }
        
    private void Start()
    {
        if (mainSpawner)
        {
            _speed = mainSpawner.speed;
            _tilt = mainSpawner.tilt;
        }
        ChangeTarget();
        _randomAngerActivator = Random.Range(5, 9);
    }

    private void Update()
    {
        cachedButtonTransform.position = Vector3.MoveTowards(cachedButtonTransform.position, _target, Time.deltaTime * _speed);
        cachedMeteorTransform.Rotate(_rotateOrientation * _tilt);
         
    }
    
    private void OnCollisionEnter2D(Collision2D nearObj)
    {
        
        if (nearObj.gameObject.tag == "Wall")
        {
            Debug.Log(nearObj.gameObject.name);
            if (objectMeteor.Anger == true)
            {
                mainSpawner.cntScore += 1;
                objectMeteor.BecameNormal();
                _randomAngerActivator = Random.Range(5, 9);
                ChangeTarget();
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
        if (nearObj.gameObject.tag == "Meteor")
        {
            if (objectMeteor.Anger == true && nearObj.gameObject.GetComponent<MeteorButton>().objectMeteor.Anger == false)
            {
                _localScore += 1;
                mainSpawner.cntScore += 1;
                Destroy(nearObj.gameObject);

            }
            else 
            {
                ChangeTarget();
            }
        }
    }
        
    private void OnMouseDrag()
    {
        
    }

    private void OnMouseDown()
    {
        SoundEffect(_sourceSound);
        if (objectMeteor.Anger)
        {
            Destroy(gameObject);
        }
        else
        {
            if (objectMeteor.GoldState)
                PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + 1);
            mainSpawner.cntScore += 1;
            _localScore += 1;
            ChangeTarget();
        }
    }

    private void OnMouseUp()
    {
        if (_localScore % _randomAngerActivator == 0)
        {
            objectMeteor.BecameAnger();
        }
    }

    private void ChangeTarget()// смена конечной точки
    {
        //Координаты 
        if (gameObject.transform.position.x >= 0 && gameObject.transform.position.y >= 0)
        {
            _target = new Vector3(Random.Range(-10f, 1f) * 100f, Random.Range(-10f,-1f) * 100f, 0);
        }
        else if (gameObject.transform.position.x < 0 && gameObject.transform.position.y >= 0)
        {
            _target = new Vector3(Random.Range(1f, 10f) * 100f, Random.Range(-10f, -1f) * 100f, 0);
        }
        else if (gameObject.transform.position.x < 0 && gameObject.transform.position.y < 0)
        {
            _target = new Vector3(Random.Range(1f, 10f) * 100f, Random.Range(1f, 10f) * 100f, 0f);
        }
        else if (gameObject.transform.position.x >= 0 && gameObject.transform.position.y < 0)
        {
            _target = new Vector3(Random.Range(-10f, -1f) * 100f, Random.Range(1f, 10f) * 100f, 0);
        }
        //Поворот
        _rotateOrientation = RandomRotate();
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
