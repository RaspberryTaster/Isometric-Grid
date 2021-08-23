using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorld : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 offSet;
    public Camera cam;
    public CameraController cameraRig;
    public RectTransform rect;
    public float minScale = 0.2f;
    public float maxScale = 1.5f;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        //float x = Mathf.Lerp(cameraRig.minimumZoom.y, cameraRig.maximumZoom.y, cameraRig.minimumZoom.)

    }

    // Update is called once per frame
    void Update()
    {


    }
    public float newZoom;
	private void LateUpdate()
	{
        float perc = cameraRig.newZoom.y / cameraRig.maximumZoom.y;
        newZoom = Mathf.Lerp(cameraRig.minimumZoom.y, cameraRig.maximumZoom.y, perc);
        float zoomPerc = newZoom / cameraRig.maximumZoom.y;
        float newScale = Mathf.Lerp(minScale, maxScale, zoomPerc);
        Vector3 vectorScale = new Vector3(newScale, newScale, newScale);
        rect.localScale = vectorScale;

        Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offSet);
        if (transform.position != pos)
        {
            transform.position = pos;
        }
    }
}
