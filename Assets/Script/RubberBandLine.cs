using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RubberBandLine : MonoBehaviour
{
    private LineRenderer _line;
    private bool _drawing = true;
    private Projectile _projectile;
    private Vector3 _launchPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
        _line.SetPosition(0, transform.position);

        _projectile = GetComponentInParent<Projectile>();

        
    }

    public void FixedUpdate()
    {
        if(_drawing) {
            _line.SetPosition(0, _launchPos);
            _line.SetPosition(1, transform.position);
            if(_projectile == null || !_projectile.awake) {
                    _drawing = false;
                    _line.enabled = false;
            }
        }
    }
    public void SetLaunchPosition(Vector3 launchPos) {
        _launchPos = launchPos;
    }
    public void StopDrawing() {
        _drawing = false;
        _line.enabled = false;
    }
}

