using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquippedMagicDetail", menuName ="ScriptableObjects/EquipedMagicDetail")]
public class EquippedProjectileStruct : ScriptableObject
{
    public int equippedMagic;
    public GameObject[] magicList;
}
