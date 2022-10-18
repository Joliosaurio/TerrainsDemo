using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    //private RaycastHit hit;
    public Camera fpsCamera;
    private Transform selectionRef;
    private Transform targetRef;
    private RaycastHit [] hits = new RaycastHit [1];
    private string selectableTag = "Selectable";
    public GameObject targetInfo;
    public TextMeshProUGUI targetName;
    public TextMeshProUGUI targetPrice;
    public TextMeshProUGUI targetAvailable;
    public TextMeshProUGUI targetSize;
    public TextMeshProUGUI OwnerName;
    private bool isTargetOnSightAvailable;
    public Tweening targetInfoTweening;
    
    public string name = "Player";
    public LayerMask layerToHit;
    
    private bool isTargetInfoActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
        var ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
        int numHits = Physics.RaycastNonAlloc(ray, hits, layerToHit);
        
        if (numHits >= 1)
        {
            if (targetRef != hits[0].collider.transform && targetRef != null)
            {
                //agregar validación de si el nuevo objeto es un custom terrain, si es dejar activo el pop up, si no es, desactivarlo
                var targetRefOutlines = targetRef.GetComponent<CustomTerrain>();
                if (targetRefOutlines != null)
                {
                    targetRefOutlines.HideOutlines();
                    targetInfoTweening.PopOut();
                    isTargetInfoActive = false;
                    targetRef = null;
                }
            }
            
            
            var seenTarget = hits[0].collider.transform.GetComponent<CustomTerrain>();
            if (seenTarget)
            {
                seenTarget.ActiveOutlines();
                targetInfo.SetActive(true);
                isTargetInfoActive = true;
                var targetAvailable = " ";
                if (seenTarget.isTerrainAvailable)
                {
                    targetAvailable = "Sí";
                    isTargetOnSightAvailable = true;
                }
                else
                {
                    targetAvailable = "No";
                    isTargetOnSightAvailable = false;
                    foreach (var outline in seenTarget.terrainObjects)
                    {
                        outline.OutlineColor = Color.red;
                    }
                }
                
                SetTargetInfo(seenTarget.terrainName, seenTarget.terrainPrice, seenTarget.terrainSize,targetAvailable, seenTarget.Owner);
                targetRef = seenTarget.transform;
                isTargetOnSightAvailable = true;
            }
            else
            {
                isTargetOnSightAvailable = false;
            }
            
            if (Input.GetKeyDown(KeyCode.Return) && isTargetOnSightAvailable)
            {
                PurchaseTerrain(seenTarget);
            }
            else
            {
                Debug.Log("No terrain on sight");
            }
        }

        
        
    }

    public void PurchaseTerrain(CustomTerrain terrain)
    {
        terrain.isTerrainAvailable = false;
        terrain.Owner = name;
    }
    
    public void SetTargetInfo(string targetName, string targetPrice, string targetSize, string targetAvailable, string ownerName)
    {
        this.targetName.text = targetName;
        this.targetPrice.text = targetPrice;
        this.targetSize.text = targetSize;
        this.targetAvailable.text = targetAvailable;
        this.OwnerName.text = ownerName;
    }

    
    public void AppearTargetInfo()
    {
        targetInfo.SetActive(true);
    }

    public void HideTargetInfo()
    {
        targetInfoTweening.PopOut();
    }
    
}

