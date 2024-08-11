using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    public void DestroyGameObject()
    {
        GameObject parent = gameObject.GetComponentInParent<EnemyController>().gameObject;
        Destroy(parent);
    }
}
