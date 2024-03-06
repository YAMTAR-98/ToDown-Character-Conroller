using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Joystick;
    public bool isMobile;
    private void Awake() {
        if(isMobile)
            Joystick.SetActive(true);
        else
            Joystick.SetActive(false);
        
    }
}
