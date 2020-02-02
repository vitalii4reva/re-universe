using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FailPanel : MonoBehaviour
{
    public float blinkSpeed;
    public List<Image> blinks;
    public Image alienImage;
    public Image backgroundImage;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (Image img in blinks)
        {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
        }
        for (float i = 0; i < 1; i += 0.01f * blinkSpeed)
        {
            foreach (Image img in blinks)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);

        foreach (Image img in blinks)
        {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
        }
        alienImage.enabled = false;
        for (float i = 1; i >=0; i -= 0.01f * blinkSpeed)
        {
            foreach (Image img in blinks)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
            }
            yield return null;
        }
        foreach (Image img in blinks)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
        }

        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(2);
        this.gameObject.SetActive(false);
    }

}
