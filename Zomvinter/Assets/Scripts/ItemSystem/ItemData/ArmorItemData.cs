using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Equipment_", menuName = "Inventory System/Item Data/Equipment/Armor", order = 3)]
public class ArmorItemData : EquipmentData
{
    /// <summary> ���� </summary>
    public int Defence => _defence;

    [SerializeField] private int _defence = 1;
    public override Item CreateItem()
    {
        return new ArmorItem(this);
    }
}
