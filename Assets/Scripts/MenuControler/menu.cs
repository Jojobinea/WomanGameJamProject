using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class menu : MonoBehaviour
{

    public void jogar(){
        SceneManager.LoadScene("TestScene");
    }

    public void exit(){
        Application.Quit();
    }
}
