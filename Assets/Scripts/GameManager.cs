using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera panCamera;


    void Start()
    {
    }


    void Update()
    {
        if( Input.GetKeyDown(KeyCode.C) )
        {
            if( mainCamera != null && panCamera != null )
            {
                // Intercambiamos las cámaras
                Rect auxViewPort = mainCamera.rect;
                float auxDepth = mainCamera.depth;

                mainCamera.rect = panCamera.rect;
                mainCamera.depth = panCamera.depth;

                panCamera.rect = auxViewPort;
                panCamera.depth = auxDepth;
            }
        }
    }
}
