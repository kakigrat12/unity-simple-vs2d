using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandsItem
{
    public RuntimeAnimatorController _animatorController();
    public AudioClip[] Audio();
}
