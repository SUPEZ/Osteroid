using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Meteor : MonoBehaviour
{

    //public UnityEvent OnAngryEvent;

    [SerializeField] private bool _anger;
    [SerializeField] private bool _goldState;

    public bool Anger
    {
        get
        {
            return _anger;
        }

        set
        {
            _anger = value;
        }
    }

    public bool GoldState
    {
        get
        {
            return _goldState;
        }

        set
        {
            _goldState = value;
        }
    }

    private Material _materialMeteor = null;
    private bool _materialMeteorCached = false;

    public Material cachedMeteorMaterial
    {
        get
        {
            if (!_materialMeteorCached)
            {
                _materialMeteorCached = true;
                _materialMeteor = GetComponent<MeshRenderer>().material;
            }
            return _materialMeteor;
        }
    }

    private void Start()
    {
        
    }

    public void BecameAnger()
    {
        _anger = true;
        cachedMeteorMaterial.SetColor("_Color", Color.red + Color.black);
        //OnAngryEvent.Invoke();
    }

    public void BecameNormal()
    {
        if (!GoldState)
        {
            _anger = false;
            cachedMeteorMaterial.SetColor("_Color", Color.gray + Color.black);
        }
        else
        {
            BecameGold();
        }
    }

    public void BecameGold()
    {
        _anger = false;
        _goldState = true;
        cachedMeteorMaterial.SetColor("_Color", Color.yellow);
    }

}
