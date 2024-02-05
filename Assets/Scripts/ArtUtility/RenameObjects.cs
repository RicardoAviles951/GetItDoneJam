using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> a simple script to take a list of objects and rename them</para>
/// </summary>
public class RenameObjects : MonoBehaviour
{
    // renaming 80 items by hand is not ideal

    public List<Transform> listOfObjectsToChangeName;
    public string stringToReplace, stringToPutInPlace;

    void Start()
    {
        foreach(Transform objs in listOfObjectsToChangeName)
        {
            print("name changing");
            if (objs.name.Contains(stringToReplace))
                objs.name = objs.name.Replace(stringToReplace, stringToPutInPlace);
        }
    }


}// end of RenameObjects class
