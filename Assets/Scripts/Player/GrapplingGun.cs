using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask hookableLayer;
    public Transform gunTip, player;
    private float maxDistance = 20f;
    private SpringJoint joint;

    private Rigidbody connectedObject;
    public float reelSpeed = 1f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private Vector3 currentReelPoint;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }
        if (connectedObject)
        {
            currentReelPoint = Vector3.Lerp(connectedObject.position, player.position, Time.deltaTime * reelSpeed);
            connectedObject.MovePosition(currentReelPoint);
        }
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, maxDistance, hookableLayer))
        {
            grapplePoint = hit.point;
            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            if (hit.rigidbody != null)
            {
                connectedObject = hit.rigidbody;
                currentReelPoint = hit.point;
            }
            else 
            {
                joint = player.gameObject.AddComponent<SpringJoint>();
                //The distance grapple will try to keep from grapple point. 
                joint.maxDistance = distanceFromPoint * 0.8f;
                joint.minDistance = distanceFromPoint * 0.25f;

                //Adjust these values to fit your game.
                joint.damper = 7f;
                joint.spring = 10f;
                joint.massScale = 4.5f;

                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grapplePoint;
            }

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }

    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        connectedObject = null;
    }

    private Vector3 currentGrapplePosition;
    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint && !connectedObject) return;

        if (connectedObject == null)
        {
            currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, currentGrapplePosition);
        }
        else
        {
            currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, connectedObject.position);
        }
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
