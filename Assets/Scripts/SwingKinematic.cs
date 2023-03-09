using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingKinematic : MonoBehaviour
{
    float restorationTorqueRate;

    // Un vector de velocidad angular (ω) es un vector axial
    // paralelo al eje de rotación. Es una medida de velocidad de rotación.

    // Se define como el ángulo girado por una unidad de tiempo​
    // en radianes por segundo (rad/s).

    Vector3 angularVelocity;


    void Start()
    {
        restorationTorqueRate = 1;

        // Omega se inicia a 0 rad/s
        angularVelocity = Vector3.zero;
    }


    void Update()
    {
        // Ángulo de inclinación del barco respecto de la vertical mundial
        // en radianes
        float leanAngle = Vector3.Angle(Vector3.up, transform.up);

        // La aceleración es un vector que indica la variación de velocidad
        // Necesitamos una aceleracion para cambiar la velocidad en cada frame
        // en m/s2

        // El producto vectorial nos da el vector de balanceo normalizado

        Vector3 angularAcceleration =
            Vector3.Cross(transform.up, Vector3.up)
            * restorationTorqueRate * leanAngle;

        // Conociento la aceleración podemos calcular la velocidad en cada frame
        // en m/s
        // m/s = m/s2 * s = m * s / s * s
        angularVelocity += angularAcceleration * Time.deltaTime;

        // Rotamos los metros resultantes en cada frame
        transform.Rotate( angularVelocity * Time.deltaTime );
    }
}
