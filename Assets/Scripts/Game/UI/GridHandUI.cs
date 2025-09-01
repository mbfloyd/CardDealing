using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridHandUI : MonoBehaviour, IHandUI
{
    public RowUI rowPrefab; 

    private int maxRows = 3;
    private int maxPerRowSpots = 4;
    private List<RowUI> rowList = new List<RowUI>();

    public void CreateSpots(int dealAmount)
    {
        Clear();

        int numRows = Mathf.CeilToInt(((float)dealAmount / maxPerRowSpots));

        int cardsLeft = dealAmount;
        for (int loop = 0; loop < numRows; loop++)
        {
            int spotsNeeded = Mathf.Min(cardsLeft, maxPerRowSpots);
            RowUI row = CreateRow(spotsNeeded);
            cardsLeft -= spotsNeeded;
        }

        //needed to force the ui position calculation for the Vert/Hor Layout Groups
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
    }

    private RowUI CreateRow(int spotsNeeded)
    {
        RowUI row = Instantiate<RowUI>(rowPrefab, transform);
        row.CreateSpots(spotsNeeded);
        rowList.Add(row);
        return row;
    }

    public List<SpotUI> GetSpots()
    {
        List<SpotUI> spots = new List<SpotUI>();
        for (int loop=0; loop < rowList.Count; loop++) {
            spots.AddRange(rowList[loop].GetSpots());
        }
        return spots;
    }

    public SpotUI GetNextAvailableSpot()
    {
        List<SpotUI> spotList = GetSpots();
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
        for (int loop = 0; loop < rowList.Count; loop++)
        {
            rowList[loop].Remove();
        }
        rowList.Clear();
    }
    

}
