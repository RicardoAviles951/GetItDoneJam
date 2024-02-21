using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBurnable : IAffectable
{
    bool isBurning { get; set; }
    void Burn();
}
