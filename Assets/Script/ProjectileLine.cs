using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileLine : MonoBehaviour
{
    private LineRenderer _line;
    private bool _drawing = true;
    private Projectile _projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 1;
        _line.SetPosition(0, transform.position);

        _projectile = GetComponentInParent<Projectile>();
    }

    
    void FixedUpdate()
    {
        if (_drawing) {
            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, transform.position);
            // If the Projectile Rigidbody is sleeping, stop drawing
            if (_projectile != null) {
                if (!_projectile.awake) {
                    _drawing = true;
                    _projectile = null;
                }
            }
        }
    }
}
