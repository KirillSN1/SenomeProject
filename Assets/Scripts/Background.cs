using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    public GameObject background;
    public Camera MainCamera;
    [Range(0.1f,0.5f)]
    public float Height;
    [Range(0.1f, 0.5f)]
    public float Width;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float H = MainCamera.scaledPixelHeight * Height/100;
        float W = MainCamera.scaledPixelWidth * Width/100;
        background.transform.position = new Vector3(MainCamera.transform.position.x,MainCamera.transform.position.y,background.transform.position.z);
        background.transform.localScale = new Vector2(W, H);
    }
}
