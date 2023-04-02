using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private CheckPointSO checkPointSo;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pencilCase;
    [SerializeField] private GameObject checkPoint;

    public static bool[] IsPortalReachedList;
    public static int CurrentPortalIndex;

    public static Vector3[] PencilCaseLocations;
    
    private void Start()
    {
        CurrentPortalIndex = -1;
        IsPortalReachedList = new bool[7] { false, false, false, false, false, false, false };
        PencilCaseLocations = new Vector3[7]
        {
            Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero
        };
    }

    private void Update()
    {
        if (!PencilcaseController.IsGameOver) return;

        SetCheckPointPositions();

        PencilcaseController.IsGameOver = false;
    }

    private void SetCheckPointPositions()
    {
        for (var i = 6; i >= 0; i--)
        {
            if (!IsPortalReachedList[i]) continue;
            CurrentPortalIndex = i;
            break;
        }

        if (CurrentPortalIndex == -1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        else
        {
            player.transform.position = checkPoint.transform.GetChild(CurrentPortalIndex).transform.position;
            pencilCase.transform.position = PencilCaseLocations[CurrentPortalIndex];
        }
    }
}
