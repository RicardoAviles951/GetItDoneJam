using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private LineRenderer beam;
    [SerializeField] private Transform firePoint;

    [SerializeField] private ParticleSystem StartFX;
    [SerializeField] private ParticleSystem EndFX;
    public Rotate rotationAlas;
    public Vector3 offset;
    private bool soundPlayed = false;

    public AK.Wwise.Event laserBlastSound;
    public AK.Wwise.Event laserImpactSound;
    public void ActivateLaser(float attackSpeed, Vector3 targetPos, float radius)
    {
        //Sounds
        if(!soundPlayed)
        {
            laserBlastSound.Post(gameObject);
            laserImpactSound.Post(gameObject);
            soundPlayed = true;
        }
        beam.enabled = true;
        StartFX.Emit(1);
        EndFX.Emit(1);
        rotationAlas.speed = attackSpeed;

        Vector3 directionToPlayer = (targetPos - firePoint.position).normalized;

        Ray ray = new Ray(firePoint.position, directionToPlayer);

        bool cast = Physics.Raycast(ray, out RaycastHit hit, radius);

        Vector3 hitPosition = cast ? hit.point : firePoint.position + directionToPlayer * radius;

        beam.SetPosition(0, firePoint.position);
        beam.SetPosition(1, hitPosition);
        EndFX.transform.position = hitPosition - offset;
    }
    public void DeactivateLaser(float speed)
    {
        rotationAlas.speed = speed;
        beam.enabled = false;
        beam.SetPosition(0, firePoint.position);
        beam.SetPosition(1, firePoint.position);
        StartFX.Stop();
        EndFX.Stop();
        laserBlastSound.Stop(gameObject);
        laserImpactSound.Stop(gameObject);
        soundPlayed = false;
    }
}
