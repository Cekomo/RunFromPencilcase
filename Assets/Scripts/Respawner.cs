using UnityEngine;

public class Respawner : MonoBehaviour
{
    public CheckPointSO checkPointSO;
    [SerializeField] private GameObject pencilCase;

    public static bool IsPositionSaved;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player") || IsPositionSaved) return;
        checkPointSO.SetPositions(transform.position, pencilCase.transform.position);
        print("Position saved!");
        IsPositionSaved = true;
    }
}
