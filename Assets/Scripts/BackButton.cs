using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    void OnMouseDown() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
