using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RespawnManager respawnManager;

    public void ReturnCheckpoint()
    {
        respawnManager.SetCheckPointPositions();
    }
}
