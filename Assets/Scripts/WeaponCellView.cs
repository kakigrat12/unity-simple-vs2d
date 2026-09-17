using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCellView : MonoBehaviour
{
    [SerializeField] private Image _weapon;
    [SerializeField] private Image _background;

    public void Render(Sprite weapon, Sprite background)
    {
        _weapon.sprite = weapon;
        _background.sprite = background;
    }
}
