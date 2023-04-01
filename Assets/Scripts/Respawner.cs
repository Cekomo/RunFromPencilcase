using UnityEngine;

public class Respawner : MonoBehaviour
{
    public CheckPointSO checkPointSO;
    [SerializeField] private GameObject pencilCase;

    private bool isPositionSaved;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player") || isPositionSaved) return;
        checkPointSO.SetPositions(transform.position, pencilCase.transform.position);
        print("Position saved!");
        isPositionSaved = true;
    }
}
