using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTerrain : MonoBehaviour
{
    public string terrainName;
    public string terrainPrice;
    public string terrainSize;
    public bool isTerrainAvailable;
    public string Owner;
    public List<Outline> terrainObjects;
    // Start is called before the first frame update
    public bool outlinesEnabled;

    private void Start()
    {
        outlinesEnabled = false;
    }

    public void ActiveOutlines()
    {
        if (outlinesEnabled) return;
        
        if (isTerrainAvailable)
        {
            foreach (var terrainOutline in terrainObjects)
            {
                terrainOutline.OutlineColor = Color.green;
                terrainOutline.enabled = true;
                outlinesEnabled = true;
            }
        }
        else
        {
            foreach (var terrainOutline in terrainObjects)
            {
                terrainOutline.OutlineColor = Color.red;
                terrainOutline.enabled = true;
                outlinesEnabled = true;
            }
        }
        
      
        
    }

    public void HideOutlines()
    {
        
        if (!outlinesEnabled) return;
        foreach (var terrainOutline in terrainObjects)
        {
            terrainOutline.enabled = false;
            outlinesEnabled = false;
        }
      
    }
}
