using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComicScene : MonoBehaviour
{

    public Image first;
    public Image second;
    // Start is called before the first frame update
    public void ShowSecond()
    {
        first.gameObject.SetActive(false);
        second.gameObject.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(2);
    }
}
