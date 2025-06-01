using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Garantindo que haja um Collider2D
public class ResetZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // --- LINHA CORRIGIDA AQUI ---
        // Era: FindObjectOfType<GameManager>().Miss();
        // Agora:
        FindFirstObjectByType<GameManager>().Miss();
    }

}