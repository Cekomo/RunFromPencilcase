using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    [SerializeField] private GameObject pencilSource;
    private readonly Vector3 pencilOffset = new(0.2f, 0.2f, 0f);

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        Instantiate(pencilSource, transform.position + pencilOffset, Quaternion.identity);
    }

    
}
