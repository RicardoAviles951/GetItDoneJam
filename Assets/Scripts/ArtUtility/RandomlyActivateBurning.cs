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
    private int curBoxID = 0, lastBoxID = 0;

    public float acceptableRange = 20;
    public LineRenderer lazer_LineRen;
    public Transform originOfLaser;
    private Transform targetOfLaser;

    private float timeStamp;

    public bool optimizeFromPlayerDist;
    private Transform playerObj;
    private string playerTag = "Player";
    public bool playerWithin_40Meters;

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
        if (optimizeFromPlayerDist)
        {
            FindPlayer();
            if (!playerWithin_40Meters)
                return;
        }

        if(Time.time > timeStamp + burnTime)
        {
            BurnABox();
            timeStamp = Time.time;
        }

    }

    private void FindPlayer()
    {
        if(playerObj == null)
        {
            GameObject[] listOfPlayerTags = GameObject.FindGameObjectsWithTag(playerTag);
            if(listOfPlayerTags.Length > 0)
            {
                foreach(GameObject playerTag in listOfPlayerTags)
                {
                    if(playerTag.transform.name == "PlayerCapsule")
                    {
                        playerObj = playerTag.transform;
                        return;
                    }
                }
            }
        }
        else
        {
            //print("Player is found and checkable");
            float dist = CheckDistance(originOfLaser.position, playerObj.position);
            playerWithin_40Meters = IsInRange(dist, 40);
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
            // pick a new target
            StartCoroutine(LazerVisual(0.05f));
            burnableBoxes[lastBoxID].StopBurning();            
        }

        if (burnableBoxes.Count > 0 && burnableBoxes[curBoxID].gameObject.activeSelf == true)
        {
            targetOfLaser = burnableBoxes[curBoxID].transform;
            float dist = CheckDistance(originOfLaser.position, targetOfLaser.position);
            if (IsInRange(dist, acceptableRange))
            {
                // fire the lazer
                StartCoroutine(LazerVisual(0.5f));
                lazer_LineRen.SetPosition(0, originOfLaser.position);
                lazer_LineRen.SetPosition(1, targetOfLaser.position);
                burnableBoxes[curBoxID].Burn();                
            }
        }
        lastBoxID = curBoxID;
        curBoxID = Random.Range(0, burnableBoxes.Count);
        burnTime = Random.Range(burnTimeRange.x, burnTimeRange.y);
    }  

    private IEnumerator LazerVisual(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);
        lazer_LineRen.enabled = !lazer_LineRen.enabled;
    }

}// end of RandomlyActivateBurning class
