using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private CheckPointSO checkPointSo;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pencilCase;
    
    private void Update()
    {
        if (!PencilcaseController.IsGameOver) return;

        SetCheckPointPositions();

        PencilcaseController.IsGameOver = false;
    }

    private void SetCheckPointPositions()
    {
        if (Respawner.IsPositionSaved)
        {
            player.transform.position = checkPointSo.playerPosition;
            pencilCase.transform.position = checkPointSo.pencilCasePosition;
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
