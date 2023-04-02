using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RespawnManager respawnManager;
    public static bool AreLettersCheckable;

    [SerializeField] private GameObject wordPanel; 

    private void Update()
    {
        if (!AreLettersCheckable) return;
        
        wordPanel.gameObject.transform.GetChild(Respawner.CheckPointIndex).transform.gameObject.SetActive(true);
        
        AreLettersCheckable = false;
    }

    public void ReturnCheckpoint()
    {
        respawnManager.SetCheckPointPositions();
    }
}
