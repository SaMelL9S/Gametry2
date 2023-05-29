using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Игра завершена!");
    }
}
