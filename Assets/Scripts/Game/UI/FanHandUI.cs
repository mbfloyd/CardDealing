using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FanHandUI : MonoBehaviour, IHandUI
{ 
    public SpotUI spotprefab;
    private List<SpotUI> spotList = new List<SpotUI>();


    public void CreateSpots(int dealAmount)
    {
        Clear();
        for (int loop = 0; loop < dealAmount; loop++)
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

    public List<SpotUI> GetSpots()
    {
        return spotList;
    }
    
    public SpotUI GetNextAvailableSpot()
    {
        foreach (SpotUI spot in spotList)
        {
            if (spot.transform.childCount == 0)
            {
                return spot;
            }
        }
        return null;
    }

    public void Clear()
    {
        for (int loop = 0; loop < spotList.Count; loop++)
        {
            spotList[loop].Remove();
        }
        spotList.Clear();
    }
    

}
