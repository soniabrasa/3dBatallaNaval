using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCs : MonoBehaviour
{
    public GameObject bulletTrailPrefab;
    public GameObject explosionEffect;

    // Creamos un Delegate para informar a quien esté suscrito
    // de la explosión de la bala
    // P.ej. para que la cámara deje de seguirla

    // Delegate con parámetros
    public delegate void OnBulletDestroyedDelegate( GameObject bullet );
    // Variable de tipo :delegate
    public OnBulletDestroyedDelegate OnBulletDestroyed;



    void Start()
    {
    }


    void Update()
    {
        SpawnTrail();

        // Destruímos la bala en caso de que baje a -100 Y

        if( transform.position.y < -100 )
        {
        // Es importante llamar a un delegate sólo si tiene función asignada
        // Es buena práctica comprobar siempre antes de llamarlo

            if( OnBulletDestroyed != null )
            {
                OnBulletDestroyed( gameObject );
            }

            Destroy( gameObject );
        }
    }

    void OnCollisionEnter( Collision other )
    {
        GameObject newEffect = Instantiate(
            explosionEffect,
            transform.position,
            Quaternion.identity );

        Destroy(
            newEffect,
            newEffect.GetComponent<ParticleSystem>().main.duration );

        if( OnBulletDestroyed != null )
        {
            OnBulletDestroyed( gameObject );
        }

        Destroy( gameObject );
    }

    void SpawnTrail()
    {
        for( int i = 0; i < 20; i++ )
        {
            Instantiate(
                bulletTrailPrefab,
                transform.position - transform.forward * 2 + (Vector3)Random.insideUnitCircle * 0.3f,
                Quaternion.identity);
        }
    }
}
