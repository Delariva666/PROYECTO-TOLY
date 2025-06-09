using UnityEngine;

public class Toly : MonoBehaviour
{
    Mover mover;
    Collisiones collisiones;
    Animaciones animaciones;
    Rigidbody2D rb2D;
    PlayerHealth playerHealth; // 👈 Nuevo

    private void Awake()
    {
        mover = GetComponent<Mover>();
        collisiones = GetComponent<Collisiones>();
        animaciones = GetComponent<Animaciones>();
        rb2D = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>(); // 👈 Aquí también
    }

    public void Hit()
    {
        Debug.Log("Recibió un golpe");
        Dead();
        animaciones.Hurt();
    }

    private void Dead()
    {
        if (collisiones.IsDead)
        {
            mover.InputMoveEnable = true;
            Debug.Log("Movimiento deshabilitado");
            animaciones.Death();

        }
    }
}
