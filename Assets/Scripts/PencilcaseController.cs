using UnityEngine;

public class PencilcaseController : MonoBehaviour
{
    private readonly float _caseSpeed = 4f;
    private Rigidbody2D _rbCase;

    public static bool IsGameOver;

    private void Start()
    {
        _rbCase = GetComponent<Rigidbody2D>();
        _rbCase.velocity = new Vector2(_caseSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        IsGameOver = true;
        print("you died!");
    }
}
