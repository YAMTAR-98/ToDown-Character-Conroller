using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void LoadScene(){
        SceneManager.LoadScene("Mechanics Scene", LoadSceneMode.Single);
    }
    public void ExitGame(){
        Application.Quit();
    }
}
