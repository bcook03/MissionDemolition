using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class CloudCover : MonoBehaviour
{ 
    [Header("Incscribed")]
    public Sprite[] cloudSprites;
    public int numClouds = 40;
    public Vector3 minPOs = new Vector3(-20,5,5);
    public Vector3 maxPow = new Vector3(300,40,5);
    [Tooltip("For scaleRange, x is the min valuse and y is the max value.")]
    public Vector2 scaleRange = new Vector2(1,4);
    void Start()
    {
        Transform parentTrans = this.transform;
        GameObject cloudGO;
        Transform cloudTrans;
        SpriteRenderer sRend;
        float scaleMult;
        for (int i = 0; i < numClouds; i++) {
            // Create a new GameObject (from scratch!) and get its Transform
            cloudGO = new GameObject();
            cloudTrans = cloudGO.transform;
            
        }
    }

    Vector3 RandomPos() {
        Vector3 pos = new Vector3();
        pos.x = Random.Range(minPOs.x, maxPow.x);
        pos.y = Random.Range(minPOs.y, maxPow.y);
        pos.z = Random.Range(minPOs.z, maxPow.z);
        return pos;
    }

    
}
