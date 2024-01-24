using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public static EnemyPath instance;

    private void Awake()
    {
        instance = this;
    }

    public List<GameObject> enemyPath= new List<GameObject>();
}
