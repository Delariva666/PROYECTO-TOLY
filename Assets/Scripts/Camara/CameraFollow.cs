using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // El objeto que va a ser seguido (el jugador)
    public float smooth = 0.125f;  // Suavidad del movimiento de la cámara
    public Vector3 offset;  // Desplazamiento de la cámara con respecto al objetivo

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // Calcular la posición deseada de la cámara solo en el eje X, manteniendo la posición Y y Z
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);

        // Mover la cámara suavemente a la posición deseada
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smooth * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
