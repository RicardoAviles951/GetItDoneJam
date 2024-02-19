using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoader : MonoBehaviour
{
    public enum LoadState
    {
        load,
        unload
    }
    public LoadState state;
    public GameObject Area;
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")//Check if player walks into triggerbox
        {
            switch (state)
            {
                case LoadState.load:

                    if (!Area.gameObject.activeSelf)//Check if it is not loaded, load it in
                    {
                        Area.gameObject.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning(Area.ToString() + " is Loaded already!");
                    }


                    break;


                case LoadState.unload:

                    if (Area.gameObject.activeSelf)//Check if it is loaded, unload it
                    {
                        Area.gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.LogWarning(Area.ToString()+ " is unloaded already");
                    }

                    break;

                default: Debug.LogWarning("There is not load state specified."); break;
            }
        }
        
    }
}
