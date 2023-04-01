using UnityEngine;

[CreateAssetMenu(fileName = "CheckpointData", menuName = "Checkpoint")]
public class CheckPointSO : ScriptableObject
{
    [HideInInspector] public Vector3 playerPosition;
    [HideInInspector] public Vector3 pencilCasePosition;
    
    public void SetPositions(Vector3 newPlayerPosition, Vector3 newCasePosition)
    {
        playerPosition = newPlayerPosition;
        pencilCasePosition = newCasePosition;
    }
}
