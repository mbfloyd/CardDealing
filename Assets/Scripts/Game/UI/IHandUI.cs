using System.Collections.Generic;
using UnityEngine;

public interface IHandUI
{
    public void ClearSpots();

    public List<SpotUI> GetSpots();
    public void CreateSpots(int dealAmount);
    public SpotUI GetNextAvailableSpot();
}
