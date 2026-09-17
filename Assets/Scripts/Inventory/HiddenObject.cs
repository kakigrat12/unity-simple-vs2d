using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HiddenObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private KeyCode keyCode;

    //public UnityEvent OnActive;
    //public UnityEvent OnDisactive;

    private void Start()
    {
        //GameEvents.current.onInvIsFull += Activing;
        _object.SetActive(false);
    }

    //private void OnDisable()
    //{
    //    GameEvents.current.onInvIsFull -= Activing;
    //}

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            Activing();
            //GameEvents.current.PatronsUpdate();
        }
    }

    private void Activing()
    {
        _object.gameObject.SetActive(!_object.gameObject.activeSelf);
        //if (_object.gameObject.activeSelf)
        //{
        //    OnActive?.Invoke();
        //}
        //else
        //{
        //    OnDisactive?.Invoke();
        //}
    }
}
