using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyServer : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> OnOff = new List<GameObject>();
    public bool turnOn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distanciaAlPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if (distanciaAlPlayer < 6f)
            {
                turnOn = !turnOn;
            }
        }

        if (turnOn)
        {
            TurnOnEnemy();
        }
        else
        {
            TurnOffEnemy();
        }
    }

    void TurnOnEnemy()
    {
        OnOff[0].SetActive(true);
        OnOff[1].SetActive(false);
        foreach (GameObject enemy in enemyList)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }
    }

    void TurnOffEnemy()
    {
        OnOff[1].SetActive(true);
        OnOff[0].SetActive(false);
        foreach (GameObject enemy in enemyList)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
            }
        }
    }
}
