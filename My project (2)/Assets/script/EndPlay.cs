using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndPlay : MonoBehaviour
{
    public GameObject player; // —сылка на объект игрока

    private void Update()
    {
        if (player == null)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("»гра завершена!");
        SceneManager.LoadScene("menu");
    }
}


