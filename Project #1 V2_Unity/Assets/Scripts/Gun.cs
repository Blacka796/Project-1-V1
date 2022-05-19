using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour
{
    public LayerMask collisionMask;
    public Transform spawn;
    public Transform shellEjectionPoint;
    public Rigidbody shell;
    private LineRenderer tracer;

    public enum GunType {Semi, Burst, Auto};

    public GunType gunType;
    public float rpm;
    public float damage = 1;

    private float secondsBetweenShots;
    private float nextPossibleShootTime;


    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shotDistance = 20;

            if (Physics.Raycast(ray, out hit, shotDistance, collisionMask))
            {
                shotDistance = hit.distance;

                if (hit.collider.GetComponent<Entity>())
                {
                    hit.collider.GetComponent<Entity>().TakeDamage(damage);
                }
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;

            GetComponent<AudioSource>().Play();

            if (tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }

            Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity) as Rigidbody;
            newShell.AddForce(shellEjectionPoint.forward * Random.Range(150f, 200f) + spawn.forward * Random.Range(-10f, 10f));
        }
    }

    public void ShootContinuous()
    {
        if (gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        secondsBetweenShots = 60/rpm;
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanShoot()
    {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime)
        {
            canShoot = false;
        }

        return canShoot;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + hitPoint);
        yield return null;
        tracer.enabled = false;
    }
}
