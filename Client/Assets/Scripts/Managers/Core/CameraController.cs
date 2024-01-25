using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : ManagerBase
{
    [SerializeField]
    private GameObject _map = null;

    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    

    public void SetMap(GameObject Map)
    {   
        
        _map = Map;
    }
}
