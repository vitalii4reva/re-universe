using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    public GridLayoutGroup grid;
    public TextMeshProUGUI name;
    public Image water;
    public Image green;
    public Image earth;
    public Image hot;

    // Start is called before the first frame update
    void Start()
    {
        grid.cellSize = new Vector2(grid.cellSize.x * ((float)Screen.width / 1440f), grid.cellSize.y * ((float)Screen.height / 2960f));
        grid.spacing = new Vector2(grid.spacing.x * ((float)Screen.width / 1440f), grid.spacing.y * ((float)Screen.height / 2960f));
        grid.padding.left = Mathf.RoundToInt((float)grid.padding.left * ((float)Screen.width / 1440f));
        grid.padding.right = Mathf.RoundToInt((float)grid.padding.right * ((float)Screen.width / 1440f));
        grid.padding.top = Mathf.RoundToInt((float)grid.padding.top * ((float)Screen.height / 2960f));
        grid.padding.bottom = Mathf.RoundToInt((float)grid.padding.bottom * ((float)Screen.height / 2960f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
