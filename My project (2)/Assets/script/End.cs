using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            EndGame();
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene("menu");
        Debug.Log("Игра завершена!");
       
    }
}

