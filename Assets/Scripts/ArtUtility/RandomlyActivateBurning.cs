using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Just for a small visual of an ai burning different boxes </para>
/// </summary>
public class RandomlyActivateBurning : MonoBehaviour
{

    public List<BurnDestruct> burnableBoxes = new List<BurnDestruct>();
    private float burnTime = 3.0f;
    public Vector2 burnTimeRange;
    private int curBoxID = 0;

    public float acceptableRange = 20;
    public LineRenderer lazer_LineRen;
    public Transform originOfLaser;
    private Transform targetOfLaser;

    private float timeStamp;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (burnTimeRange.x == 0)
            burnTimeRange.x = 1.25f;
        if (burnTimeRange.y == 0)
            burnTimeRange.y = burnTime;
        //lazer_LineRen.useWorldSpace = true;
        lazer_LineRen.enabled = false;
    }

    private void LateUpdate()
    {
        if(Time.time > timeStamp + burnTime)
        {
            BurnABox();
            timeStamp = Time.time;
        }

    }


    public float CheckDistance(Vector3 _pos2, Vector3 _pos1)
    {
        float dist = Vector3.Distance(_pos2, _pos1);
        return dist;
    }

    public bool IsInRange(float _dist, float _range)
    {
        return _dist < _range;
    }

    private void BurnABox()
    {
        if (lazer_LineRen.enabled)
        {
            lazer_LineRen.enabled = false;
            burnableBoxes[curBoxID].StopBurning();
        }

        if (burnableBoxes.Count > 0 && burnableBoxes[curBoxID].gameObject.activeSelf == true)
        {
            targetOfLaser = burnableBoxes[curBoxID].transform;
            float dist = CheckDistance(originOfLaser.position, targetOfLaser.position);
            if (IsInRange(dist, acceptableRange))
            {
                //print($"LAZER FIRE: {curBoxID}\n {burnableBoxes[curBoxID].transform.name}");
                lazer_LineRen.enabled = true;
                lazer_LineRen.SetPosition(0, originOfLaser.position);
                lazer_LineRen.SetPosition(1, targetOfLaser.position);
                burnableBoxes[curBoxID].Burn();                
            }
        }
        
        curBoxID = Random.Range(0, burnableBoxes.Count);
        //print("LAZER Targetting");
        //print($"LAZER Targetting: {curBoxID}\n {burnableBoxes[curBoxID].transform.name}");
        burnTime = Random.Range(burnTimeRange.x, burnTimeRange.y);
    }  

}// end of RandomlyActivateBurning class
