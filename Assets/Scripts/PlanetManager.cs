using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<Planet> planets;
    
    public List<GameObject> planets;
    public Transform startPlanet;
    public Transform endPlanet;
    public List<PlanetStats> stats;
    public StatsPanel statsPanel;
    //public GameObject planet;
    public Trajectory trajectory;
    public float speed;
    public GameObject planetDestroyPos;
    public bool planetMoved;
    public GameObject curPlanet;
    public PlanetStats curPlanetStats;

    public float startTime;
    public float endTime;
    public float scoreKoef;
    public float addStatsSpeed;

    public float sizeSpeed;
    public GameObject curBlast;
    public float speedForce;

    public int ind;

    public bool sizing;
    void Start()
    {
        curPlanetStats = new PlanetStats();
        SpawnPlanet();
        //StartCoroutine(MovePlanetToScreen(planets[0], stats[0]));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlanet()
    {
        if (ind < planets.Count)
        {
            GameObject temp = Instantiate(planets[ind], startPlanet.position, startPlanet.rotation);
            temp.transform.localScale = startPlanet.localScale;
            temp.GetComponent<Planet>().statsPanel = statsPanel;
            StartCoroutine(MovePlanetToScreen(temp.GetComponent<Planet>(), stats[ind]));
            ind++;
        }
        
    }

    public IEnumerator MovePlanetToScreen(Planet planet, PlanetStats curStats)
    {
        statsPanel.water.fillAmount = 0f;
        statsPanel.green.fillAmount = 0f;
        statsPanel.earth.fillAmount = 0f;
        statsPanel.hot.fillAmount = 0f;
        statsPanel.name.text = "";
        planetMoved = false;
        curPlanet = planet.gameObject;
        curPlanetStats.water = curStats.water/100f;
        curPlanetStats.green = curStats.green / 100f;
        curPlanetStats.earth = curStats.earth / 100f;
        curPlanetStats.hot = curStats.hot / 100f;
        curPlanetStats.name = curStats.name;
        planet.transform.localScale = startPlanet.localScale;


            for (float i = 0f; i <= 1; i += 0.01f * speed)
            {
                planet.transform.localScale = Vector3.Lerp(startPlanet.localScale, endPlanet.localScale, i);
                yield return null;
            }
            

        statsPanel.water.fillAmount = curPlanetStats.water;
        statsPanel.green.fillAmount = curPlanetStats.green;
        statsPanel.earth.fillAmount = curPlanetStats.earth;
        statsPanel.hot.fillAmount = curPlanetStats.hot;
        statsPanel.name.text = curPlanetStats.name;
        planetMoved = true;

    }

    public void SwipeOut()
    {
        Debug.Log("Swiping out");
        curPlanet.GetComponent<Planet>().swipingOut = true;
        StartCoroutine(MovePlanetOut(curPlanet));
        
    }

    public IEnumerator MovePlanetOut(GameObject planet)
    {
        if (planetMoved)
        {
            statsPanel.water.fillAmount = 0f;
            statsPanel.green.fillAmount = 0f;
            statsPanel.earth.fillAmount = 0f;
            statsPanel.hot.fillAmount = 0f;
            statsPanel.name.text = "";
            Vector3 StartPos = planet.transform.position;
            Vector3 EndPos = planetDestroyPos.transform.position;


            for (float i = 0f; i <= 1; i += 0.01f * speed)
            {
                if (planet != null)
                {
                    planet.transform.position = Vector3.Lerp(StartPos, EndPos, i);
                    yield return null;
                }
                else
                    break;
                
            }
            if (planet != null)
            {
                yield return new WaitForSeconds(0.1f);
                SpawnPlanet();
                Destroy(planet);
            }
            planetMoved = false;
        }
        
    }
    public void AddStat(float addWater, float addGreen, float addEarth, float addHot)
    {
        StartCoroutine(AddStatIE(addWater, addGreen, addEarth, addHot));
    }

    public IEnumerator AddStatIE(float addWater, float addGreen, float addEarth, float addHot)
    {

        float startWater = curPlanetStats.water;
        float startGreen = curPlanetStats.green;
        float startEarth = curPlanetStats.earth;
        float startHot = curPlanetStats.hot;
        float endWater = curPlanetStats.water + addWater;
        float endGreen = curPlanetStats.green + addGreen;
        float endEarth = curPlanetStats.earth + addEarth;
        float endHot = curPlanetStats.hot + addHot;
        for (float i=0; i <= 1; i += 0.01f*addStatsSpeed)
        {
            curPlanetStats.water = Mathf.Lerp(startWater, endWater, i);
            curPlanetStats.green = Mathf.Lerp(startGreen, endGreen, i);
            curPlanetStats.earth = Mathf.Lerp(startEarth, endEarth, i);
            curPlanetStats.hot = Mathf.Lerp(startHot, endHot, i);
            statsPanel.water.fillAmount = curPlanetStats.water;
            statsPanel.green.fillAmount = curPlanetStats.green;
            statsPanel.earth.fillAmount = curPlanetStats.earth;
            statsPanel.hot.fillAmount = curPlanetStats.hot;
            yield return null;
        }
    }

    public void AddStatsBtnStart(GameObject blast)
    {
        startTime = Time.time;
        curBlast = blast;
        StartCoroutine(SizeBlast(blast));
    }
    public IEnumerator BackGroundChange()
    {
        yield return null;
    }

    public IEnumerator SizeBlast(GameObject blast)
    {
        sizing = true;
        float deltasize = 0.01f;
        
        float size = blast.transform.localScale.x;
        float startSize=size;
        while (sizing)
        {
            size += deltasize*sizeSpeed;
            size = Mathf.Clamp(size, startSize, startSize * 1.5f);
            if (blast != null)
            blast.transform.localScale = new Vector3(size, size, size);

            yield return null;
        }
    }


    public void AddStatsBtnEnd()
    {
        sizing = false;
        endTime = Time.time;
        float score = (endTime - startTime) * scoreKoef;
        score = Mathf.Clamp(score, 0f, 0.4f);
        Debug.Log("score = " + score);
        curBlast.GetComponent<Blast>().power = score;
        //curBlast.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speedForce, ForceMode2D.Impulse);
        StartCoroutine(BlastMover());
        curBlast = null;
        
    }

    IEnumerator BlastMover()
    {
        GameObject temp = curBlast;
        while (temp != null)
        {
            temp.GetComponent<Rigidbody2D>().velocity = Vector2.up * speedForce;
            yield return null;
        }
    }
}
