using UnityEngine.SceneManagement;
using UnityEngine;
public class menu : MonoBehaviour
{

    public void jogar()
    {
        SceneManager.LoadScene("TestScene");
        AudioManager.instance.PlaySFX(0);
        AudioManager.instance.StopMusic();
    }

    public void exit()
    {
        Application.Quit();
    }
}
