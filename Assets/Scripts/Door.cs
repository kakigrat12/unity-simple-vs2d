using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Sprite closedDoor;
    [SerializeField] private Sprite openedDoor;
    [SerializeField] private KeyCode keyToOpen;
    private bool isOpened = false;

    public UnityEvent OnOpened;
    public UnityEvent OnClosed;

    //private void Start()
    //{
    //    //ChangeState();
    //}

    private void Update()
    {
        if (Input.GetKeyDown(keyToOpen)) 
        {
            var objectsNear = Physics2D.OverlapCircleAll(transform.position, 0.02f);
            for (int i = 0; i < objectsNear.Length; i++)
            {
                if (objectsNear[i] == ((Transform)PhotonNetwork.LocalPlayer.TagObject).GetChild(0).GetComponent<Collider2D>())
                {
                    photonView.RPC("ChangeState", RpcTarget.AllBuffered);
                    break;
                }
            }
        }
    }

    [PunRPC]
    private void ChangeState()
    {
        if (isOpened)
        {
            spriteRenderer.sprite = closedDoor;
            OnClosed?.Invoke();
        }
        else
        {
            spriteRenderer.sprite = openedDoor;
            OnOpened?.Invoke();
        }
        isOpened = !isOpened;
    }
}
