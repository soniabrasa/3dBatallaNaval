using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCs : MonoBehaviour
{
    public GameObject bulletTrailPrefab;
    public GameObject explosionEffect;
    Rigidbody rb;

    // Creamos un Delegate para informar a quien esté suscrito
    // de la explosión de la bala
    // P.ej. para que la cámara deje de seguirla

    // Firma del Delegate con parámetros
    public delegate void OnBulletDestroyedDelegate( GameObject bullet );
    // Creación del Delegate con esa firma
    public OnBulletDestroyedDelegate OnBulletDestroyed;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        SpawnTrail();

        // La velocidad es el cambio de posición respecto al tiempo
        // Trabajando con vectores incluye la dirección del movimiento

        Vector3 movementDirection = rb.velocity;

        // Girando el Transform para que apunte
        // en la misma dirección que su velocidad
        transform.LookAt( movementDirection );


        // Por si la bala sale del océano
        // la destruímos en caso de que baje de -100 Y

        if( transform.position.y < -100 )
        {
        // Es importante publicar un delegate sólo si hay suscritos
        // Es buena práctica comprobar siempre si hay listeners

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

    // Estela tras el espaneo de la bala
    void SpawnTrail()
    {
        Vector3 onda = transform.position - transform.forward * 2;
        Vector3 estela = Random.insideUnitCircle * 0.3f;

        for( int i = 0; i < 20; i++ )
        {
            // Instantiate(
            //     bulletTrailPrefab,
            //     transform.position - transform.forward * 2 + (Vector3)Random.insideUnitCircle * 0.3f,
            //     Quaternion.identity);

            // Instantiate(
            //     bulletTrailPrefab,
            //     onda + estela,
            //     Quaternion.identity);

            Instantiate(
                bulletTrailPrefab,
                transform.position - transform.forward*(Random.Range(2, 4f)) + (Vector3)Random.insideUnitCircle*0.3f,
                Quaternion.identity);
        }
    }
}
