using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private GameObject pencilCase;
    private static readonly int Erasing = Animator.StringToHash("Erasing");
    [SerializeField] private Animator playerAnimator;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var objectName = gameObject.name;
        var checkPointChar = objectName[objectName.Length - 1];
        var checkPointIndex = (int)checkPointChar - (int)'0';

        if (!col.gameObject.CompareTag("Player") || RespawnManager.IsPortalReachedList[checkPointIndex]) return;

        playerAnimator.SetTrigger(Erasing);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        RespawnManager.IsPortalReachedList[checkPointIndex] = true;
        RespawnManager.PencilCaseLocations[checkPointIndex] = pencilCase.transform.position;
    }
}
