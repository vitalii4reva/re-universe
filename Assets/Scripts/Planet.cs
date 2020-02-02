using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet:MonoBehaviour
{
    public PlanetManager planetManager;
    public StatsPanel statsPanel;
    public GameObject main;
    public SpriteRenderer Shadow;
    public SpriteRenderer Dummy;
    public SpriteRenderer Water;
    public SpriteRenderer overWater;
    public SpriteRenderer Earth;
    public SpriteRenderer Green;
    public SpriteRenderer Mountains;
    //public SpriteRenderer Hot;
    public SpriteRenderer Atmoshpere;
    public bool hotNeutralized;
    public bool done;
    [SerializeField]
    float speed;
    public bool swipingOut;

    private void Start()
    {
        main = this.transform.GetChild(2).gameObject;
        Atmoshpere.color= new Color(Atmoshpere.color.r, Atmoshpere.color.g, Atmoshpere.color.b, 0f);
        planetManager = FindObjectOfType<PlanetManager>();
        done = false;
    }
    void Update()
    {
        main.transform.Rotate(0, 0, 0.2f);
        if (!done && statsPanel.water.fillAmount == 1f && statsPanel.earth.fillAmount == 1f && statsPanel.green.fillAmount == 1f && statsPanel.hot.fillAmount == 1f )
        {
            done = true;
        }
        if (!done)
        {
            if (!swipingOut)
            {
                UpdateStats();
            }
            
        }
        else
        {
            done = false;
            swipingOut = true;
            StartCoroutine(ShowAtmosphere());

        }
        
    }
    public void UpdateStats()
    {
        if (statsPanel.hot.fillAmount == 1f && !hotNeutralized)
        {
            hotNeutralized = true;
        }

        Water.color = new Color(Water.color.r, Water.color.g, Water.color.b, statsPanel.water.fillAmount*0.7f);
        Earth.color = new Color(Earth.color.r, Earth.color.g, Earth.color.b, statsPanel.earth.fillAmount);
        Green.color = new Color(Green.color.r, Green.color.g, Green.color.b, statsPanel.green.fillAmount);
        if (!hotNeutralized)
        {
            overWater.color = new Color(overWater.color.r, overWater.color.g, overWater.color.b, 1-statsPanel.hot.fillAmount);
        }

        float temp = (statsPanel.water.fillAmount + statsPanel.earth.fillAmount + statsPanel.green.fillAmount + statsPanel.hot.fillAmount) / 4f;
        Atmoshpere.color = new Color(Atmoshpere.color.r, Atmoshpere.color.g, Atmoshpere.color.b, temp);
        
        //Water.color = new Color(Water.color.r, Water.color.g, Water.color.b, statsPanel.water.fillAmount);
        //Water.color = new Color(Water.color.r, Water.color.g, Water.color.b, statsPanel.water.fillAmount);
    }

    public IEnumerator ShowAtmosphere()
    {
        
        yield return new WaitForSeconds(2f);
        FindObjectOfType<PlanetManager>().SwipeOut();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Blast")
        {
            Blast blast = collision.gameObject.GetComponent<Blast>();
            float score = blast.power;
            switch (blast.blastMode)
            {
                case BlastManager.BlastMode.Green:
                    planetManager.AddStat(0, score, 0, 0);
                    break;
                case BlastManager.BlastMode.Water:
                    planetManager.AddStat(score, 0, 0, 0);
                    break;
                case BlastManager.BlastMode.Earth:
                    planetManager.AddStat(0, 0, score, 0);
                    break;
                case BlastManager.BlastMode.Hot:
                    planetManager.AddStat(0, 0, 0, score);
                    break;
            }
            Destroy(blast.gameObject);
        }
    }

}
