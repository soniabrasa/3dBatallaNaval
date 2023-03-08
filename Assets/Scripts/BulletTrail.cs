using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    public float ttl;


    void Start()
    {
        Invoke( "SelfDestroy", ttl * Random.Range(0.3f, 2.5f) );
        transform.localScale = transform.localScale * Random.Range(0.6f, 1.4f);
    }

    private void SelfDestroy()
    {
        Destroy( gameObject );
    }
}
