using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public Slider healthSlider;
    public GameObject gameOverText; // 👈 arrastra tu texto Game Over desde la jerarquía
    public GameObject gameOverPanel; // 👈 Asigna aquí el panel con botones en el Inspector
    public float restartDelay = 2f;
    public HeartManager heartManager; // 👈 conecta aquí tu script de corazones

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        Time.timeScale = 1f; // ⚠️ Asegura que el juego comience a velocidad normal


        //if (gameOverText != null)
        //  gameOverText.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        healthSlider.value = currentHealth;

        // Actualizar corazones visualmente ANTES de morir
        if (heartManager != null)
            heartManager.UpdateHearts(currentHealth);

        // Forzar actualización de UI
        Canvas.ForceUpdateCanvases();

        if (currentHealth <= 0)
        {
            // Esperar un momento antes de morir para que se vea el corazón vacío
            StartCoroutine(DieWithDelay());
        }
    }

    void Die()
    {
        Debug.Log("¡Jugador sin vidas!");
        //if (gameOverText != null)
        // gameOverText.SetActive(true);

        // Ocultar al jugador
        //gameObject.SetActive(false);
        //StartCoroutine(DelayedDeactivate());
        // Reiniciar escena después de un delay
        //Invoke(nameof(RestartLevel), restartDelay);

        // Activa el panel Game Over con botones
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        // Desactiva al jugador
        gameObject.SetActive(false);
    }
    
    private IEnumerator DieWithDelay()
{
    yield return new WaitForSeconds(4f); // Ajustable si es necesario
    Die();
}

/*
            void RestartLevel()
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            private IEnumerator DelayedDeactivate()
            {
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            */
}





