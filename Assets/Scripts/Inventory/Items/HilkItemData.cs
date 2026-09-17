using UnityEngine;

[CreateAssetMenu(fileName = "newItemHilk", menuName = "Data/Items/Hilk", order = 51)]
public class HilkItemData : AssetItem, IHandsItem
{
    [Space]

    public int Value;
    public float TimeToUseing;
    public RuntimeAnimatorController AnimatorController;
    public AudioClip Sound;

    public RuntimeAnimatorController _animatorController()
    {
        return AnimatorController;
    }

    public AudioClip[] Audio()
    {
        return new AudioClip[1] { Sound };
    }
}
