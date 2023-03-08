using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCameraController : MonoBehaviour
{
    public Transform cannonTower;
    public Transform panCameraTransform;

    Transform targetBulletTransform;
    Camera camera;

    Quaternion startRotation;
    float startFieldOfView;


    void Start()
    {
        targetBulletTransform = null;
        camera = panCameraTransform.GetComponent<Camera>();

        startRotation = panCameraTransform.transform.localRotation;
        startFieldOfView = camera.fieldOfView;
    }


    void Update()
    {
        transform.rotation = cannonTower.rotation;

        if( targetBulletTransform != null )
        {
            // Orientamos la cámara hacia la bala;

            Vector3 lookDirection =
                targetBulletTransform.position - panCameraTransform.position;

            float distance = lookDirection.magnitude;
            panCameraTransform.rotation =
                Quaternion.LookRotation(lookDirection);

            camera.fieldOfView = 5000 / distance;
        }
    }

    public void FollowBullet( Transform bullet )
    {
        targetBulletTransform = bullet;

        // Necesitamos el gameObject que es el parámetro del delegate
        BulletCs bulletComponent = bullet.GetComponent<BulletCs>();

        // if( bulletComponent != null )
        if( bulletComponent.OnBulletDestroyed != null )
        {
            // Se asigna una función a la variable de tipo delegate
            bulletComponent.OnBulletDestroyed += OnTargetBulletDestroyed;
        }
    }

    void OnTargetBulletDestroyed( GameObject bullet )
    {
        Debug.Log("PanCamera.OnTargetBulletDestroyed");

        Invoke( "RestoreStartValues", 2f );

        // Necesitamos el gameObject que es el parámetro del delegate
        BulletCs bulletComponent = bullet.GetComponent<BulletCs>();

        // if( bulletComponent != null )
        if( bulletComponent.OnBulletDestroyed != null )
        {
            // Se asigna una función a la variable de tipo delegate
            bulletComponent.OnBulletDestroyed -= OnTargetBulletDestroyed;
        }
    }

    private void RestoreStartValues()
    {
        panCameraTransform.localRotation = startRotation;
        camera.fieldOfView = startFieldOfView;
    }
}
