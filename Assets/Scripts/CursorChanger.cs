using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CursorType { aim, point };
public class CursorChanger : MonoBehaviour
{
    //[SerializeField] private Vector2 hotspot;
    //[Space]
    [SerializeField] private Texture2D aim;
    [SerializeField] private Texture2D point;
    private int currentCursor;
    private bool isActive = true;

    //private CursorType cursorType;

    private void Start()
    {
        GameEvents.current.onIsAnyPanelOpened += ChangeIsActive;
    }

    private void OnDisable()
    {
        GameEvents.current.onIsAnyPanelOpened -= ChangeIsActive;
        Change(0);
    }

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButton(1))
            {
                Change(1);
            }
            else
            {
                Change(0);
            }
        }
        else
        {
            Change(0);
        }
    }

    public void Change(int cursorType) //CursorType cursorType
    {
        if (currentCursor == cursorType) return;

        switch(cursorType)
        {
            case 0:
                SetCursor(point); //, new Vector2(5, 5)
                break;

            case 1:
                SetCursor(aim); //, new Vector2(11, 11)
                break;
        }
        currentCursor = cursorType;
    }

    private void ChangeIsActive(bool newValue)
    {
        Debug.Log("ChangeIsActive");
        isActive = !newValue;
    }

    private void SetCursor(Texture2D cursor)
    {
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
    }
}
