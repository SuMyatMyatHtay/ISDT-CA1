using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class InventoryDestroy : MonoBehaviour
{

    public GameObject InventoryManager;
    private void Start()
    {
        DontDestroyOnLoad(InventoryManager);
    }
    }
