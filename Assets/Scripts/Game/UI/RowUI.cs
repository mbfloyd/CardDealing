using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowUI : MonoBehaviour
{
    public SpotUI spotprefab;

    private List<SpotUI> spotList = new List<SpotUI>();

    public void CreateSpots(int spotsNeeded)
    {
        for (int loop = 0; loop < spotsNeeded; loop++)
        {
            SpotUI spot = CreateSpot();
        }
    }

    private SpotUI CreateSpot()
    {
        SpotUI spot = Instantiate<SpotUI>(spotprefab, transform);
        spotList.Add(spot);
        return spot;
    }

    public void Remove() 
    {
        for (int loop = 0; loop < spotList.Count; loop++)
        {
            spotList[loop].Remove();
        }
        spotList.Clear();
        GameObject.DestroyImmediate(gameObject);
    }

    public List<SpotUI> GetSpots()
    {
        return spotList;
    }
    
}
