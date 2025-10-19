using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Contadores y Objetivos")]
    public int dulcesRecolectados = 0;
    private const int DULCES_PARA_LLAVE = 50;

    [Header("Referencias de Objetos (Arrastrar en Inspector)")]
    public GameObject llavePrefab;
    public Transform puntoDeSpawnLlave;
    public GameObject puerta;

    private bool llaveGenerada = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDulce(int cantidad)
    {
        dulcesRecolectados += cantidad;
        Debug.Log("Dulces recolectados: " + dulcesRecolectados);

        if (dulcesRecolectados >= DULCES_PARA_LLAVE && !llaveGenerada)
        {
            GenerarLlave();
        }
    }

    private void GenerarLlave()
    {
        if (llavePrefab != null && puntoDeSpawnLlave != null)
        {
            Instantiate(llavePrefab, puntoDeSpawnLlave.position, Quaternion.identity);
            llaveGenerada = true;
            Debug.Log("¡La Llave ha sido GENERADA!");
        }
        else
        {
            Debug.LogError("Faltan referencias en GameManager.");
        }
    }

    public void DestruirPuertaYCompletarJuego()
    {
        if (puerta != null)
        {
            Destroy(puerta);
            Debug.Log("¡La Puerta ha sido DESTRUIDA!");
        }

        CompletarJuego();
    }

    private void CompletarJuego()
    {
        Debug.Log("¡Juego Completado!");
        Time.timeScale = 0f;
    }
}