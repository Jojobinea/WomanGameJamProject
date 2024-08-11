using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicDetail", menuName ="ScriptableObjects/MagicDetail")]
public class MagicProjectileDetail : ScriptableObject
{
    [Header("References")]
    public Color spriteColor;

    [Header("Variables")]
    public int primeDamage;
    public float speed;
    public int currentDamage;
    public float coolDownTimer;
}
