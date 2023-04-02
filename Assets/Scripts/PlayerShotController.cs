using System.Collections;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator playerAnimator;
    
    [SerializeField] private GameObject pencilSource;
    private readonly Vector3 pencilOffset = new(0f, 0.2f, 0f);
    
    private static readonly int Throwing = Animator.StringToHash("Throwing");

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var playerPos = transform.position;
        var mousePosScreen = Input.mousePosition;
        mousePosScreen.z = mainCamera.transform.position.z - playerPos.z;
        var mousePosWorld = mainCamera.ScreenToWorldPoint(mousePosScreen);

        transform.rotation = mousePosWorld.x < playerPos.x ? 
            transform.rotation = Quaternion.Euler(0, 180, 0) : transform.rotation = Quaternion.identity;
        
        StartCoroutine(ThrowPencil());
    }

    private IEnumerator ThrowPencil()
    {
        playerAnimator.SetTrigger(Throwing);
        yield return new WaitForSeconds(0.15f);
        Instantiate(pencilSource, transform.position + pencilOffset, Quaternion.identity);
    }
}
