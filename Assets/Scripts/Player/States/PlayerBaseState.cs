using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    public abstract void OnCollisionEnter(PlayerStateManager player, Collision collision);
    public abstract void FixedUpdateState(PlayerStateManager player);

    public abstract void LateUpdateState(PlayerStateManager player);
    public abstract void OnTriggerEnter(PlayerStateManager player, Collider other);
}
