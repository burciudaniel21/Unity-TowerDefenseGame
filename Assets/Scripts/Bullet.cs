using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [SerializeField] float speed = 70f;
    [SerializeField] GameObject impactEffect;
    [SerializeField] int damage = 50;
    public float explosionRadius = 0f;

    public void Seek(Transform _target) 
    {  
        target = _target; 
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceTravelThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceTravelThisFrame) //if the length of the direction vector is less than the distance we are supposed to move this frame than we have hit something
            //distance from a bullet to the target = direction.magnitude
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceTravelThisFrame, Space.World);
        transform.LookAt(target.position);
    }

    private void HitTarget()
    {
        GameObject impactEffectVariable = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(impactEffectVariable, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }

    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if(e != null)
        {
            e.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
