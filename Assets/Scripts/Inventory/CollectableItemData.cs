using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Data/Items/CollectableTest", order = 51)]
public class CollectableItemData : BaseItemData
{
    // Коллекционируемые предметы, это те, 
    //   которые складываются в одну ячейку инвентаря 
    //   и постепенно увеличивается счетчик предметов в ячейке

    // Максимальное кол-во предметов в одной пачке
    public int MaxCollectionCount;
}

