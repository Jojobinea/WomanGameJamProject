using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public void RetorneMenu()
    {
        SceneManager.LoadScene("menuScene");
    }

    public void RetorneJogo()
    {
        SceneManager.LoadScene("TestScene");
    }
}
