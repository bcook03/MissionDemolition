using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 9.25f;
    public GameObject projLinePrefab;

    [Header("Dynamic")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive( false );
        launchPos = launchPointTrans.position;
    }

    void OnMouseEnter(){
        launchPoint.SetActive(true);
    }

    void OnMouseExit(){
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        // The player has pressed the mouse button while over Slingshot
        aimingMode = true;
        // Instantiate a Projectile
        projectile = Instantiate(projectilePrefab) as GameObject;
        // Start it at the launchPoint
        projectile.transform.position = launchPos;
        // Set it to isKinematic for now
        projectile.GetComponent<Rigidbody>().isKinematic = true;

    }

    void Update()
    {
        // If Slingshot is not in aimingMode, don't run this code
        if(!aimingMode) return;

        // Get the current mouse position in 2D screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );

        // Find the delta from the launchPos to the mousePos3D
        Vector3 mouseDelta =  mousePos3D - launchPos;
        // Limit mouseData to the radius of the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude > maxMagnitude) {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // Move the projectile to this position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButton(0)) {
            aimingMode = false;
            Rigidbody projRb = projectile.GetComponent<Rigidbody>();
            projRb.isKinematic = false;
            projRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRb.linearVelocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile; // Set the _MainCamera POI
            Instantiate<GameObject>(projLinePrefab, projectile.transform);
            projectile = null;
        }




    }
} 


