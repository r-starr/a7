using System;
using UnityEngine;

public class SteerToCenter : MonoBehaviour {
    GameObject cam;
    Vector3 prevForwardVector;
    float prevYawRelativeToCenter;
    GameObject trackingSpace;

    void Start() {
        cam = GameObject.Find("CenterEyeAnchor");
        trackingSpace = GameObject.Find("TrackingSpace");
        prevForwardVector = cam.transform.forward;
        prevYawRelativeToCenter = angleBetweenVectors(cam.transform.forward, trackingSpace.transform.position - cam.transform.position);
    }

    void Update() {

        float howMuchUserRotated = angleBetweenVectors(prevForwardVector, cam.transform.forward);
        float directionUserRotated = (d(cam.transform.position + prevForwardVector, cam.transform.position, cam.transform.position + cam.transform.forward) < 0) ? -1f: 1f;
        float deltaYawRelativeToCenter = prevYawRelativeToCenter - angleBetweenVectors(cam.transform.forward, trackingSpace.transform.position - cam.transform.position);
        float distanceFromCenter = cam.transform.localPosition.magnitude;
        float longestDimensionOfPE = 1f;

        float howMuchToAccelerate = (deltaYawRelativeToCenter < 0) ? -0.13f : 0.3f * howMuchUserRotated * directionUserRotated * Mathf.Clamp(distanceFromCenter/longestDimensionOfPE/2, 0, 1);

        if(Mathf.Abs(howMuchToAccelerate) > 0) {
            trackingSpace.transform.RotateAround(cam.transform.position, new Vector3(0, 1, 0), howMuchToAccelerate);
        }

        prevForwardVector = cam.transform.forward;
        prevYawRelativeToCenter = angleBetweenVectors(cam.transform.forward, trackingSpace.transform.position-cam.transform.position);

    }


// converts Vector3 to Vector2 without height param
    Vector2 to2D(Vector3 vector) {
        return new Vector2(vector.x, vector.z);
    }

// determines if a vector is to the left or the right
// < 0 means right, > 0 means left
    float d(Vector3 A, Vector3 B, Vector3 C) {
        return ((A.x - B.x)*(C.z - B.z)) - ((A.z - B.z)*(C.x - B.x));
    }

    float angleBetweenVectors(Vector3 A, Vector3 B) {
        Vector2 a2D = to2D(A);
        Vector2 b2D = to2D(B);
        a2D.Normalize();
        b2D.Normalize();
        float dotProd = Mathf.Clamp(Vector2.Dot(a2D, b2D), -1, 1);
        return Mathf.Acos(dotProd)*(180/Mathf.PI);
    }
}