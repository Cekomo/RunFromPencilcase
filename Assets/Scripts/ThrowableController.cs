using UnityEngine;

public class ThrowableController : MonoBehaviour
{
    private Rigidbody2D _rbPencil;

    private bool isStuck;

    private Vector3 _direction;

    private const float PENCIL_SPEED = 12.5f;

    private void Start()
    {
        _rbPencil = GetComponent<Rigidbody2D>();
        
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        _direction = (mousePos - transform.position).normalized;
        
        var rotation = Quaternion.LookRotation(Vector3.forward, _direction) * Quaternion.Euler(0f, 0f, -90f);
        transform.rotation = rotation;
    }

    private void Update()
    {
        if (isStuck) return;
        
        _rbPencil.velocity = _direction * PENCIL_SPEED;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Wall")) return;
        
        var colliderPencil = GetComponent<Collider2D>();
        colliderPencil.isTrigger = false;
        
        isStuck = true;
        _rbPencil.velocity = Vector2.zero;
        _rbPencil.angularVelocity = 0f;
        _rbPencil.isKinematic = true;
        gameObject.layer = LayerMask.NameToLayer("Platform");

        // when collision happens, to stabilize the object, make it kinetic and usable as platform for player
    }
}
