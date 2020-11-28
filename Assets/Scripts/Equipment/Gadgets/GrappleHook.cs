using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GrappleHook : MonoBehaviour, IGadget
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask hookableLayer;
    public Transform startPoint, player;
    private float maxDistance = 20f;
    private SpringJoint joint;

    private Rigidbody connectedObject;
    public float reelSpeed = 1.5f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        player = GetComponentInParent<PlayerController>().transform;
    }

    private Vector3 currentReelPoint;
    private void Update()
    {
        if (connectedObject)
        {
            currentReelPoint = Vector3.Lerp(connectedObject.position, player.position, Time.deltaTime * reelSpeed);
            connectedObject.MovePosition(currentReelPoint);
        }
        if (!Input.GetMouseButton(1))
        {
            SpringJoint spring = GetComponentInParent<SpringJoint>();
            if (joint != null)
            {
                Destroy(joint);
            }
            else if (spring)
            {
                Destroy(spring);
            }
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
    public void StartUse()
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
                AudioManager.instance.StartPlayingAtPosition("Grapple Hit", transform.position);
            }
            else 
            {
                joint = player.gameObject.AddComponent<SpringJoint>();
                //The distance grapple will try to keep from grapple point. 
                joint.maxDistance = 0.01f;
                joint.minDistance = distanceFromPoint * 0.1f;

                //Adjust these values to fit your game.
                joint.damper = 7f;
                joint.spring = 25f;
                joint.massScale = 4.5f;

                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grapplePoint;
                AudioManager.instance.StartPlayingAtPosition("Grapple Hit", transform.position);
            }

            lr.positionCount = 2;
            currentGrapplePosition = startPoint.position;
        }
    }

    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    public void EndUse()
    {
        if (joint != null || connectedObject != null)
        {
            AudioManager.instance.StartPlaying("Grapple Reel");
        }
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

            lr.SetPosition(0, startPoint.position);
            lr.SetPosition(1, currentGrapplePosition);
        }
        else
        {
            currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

            lr.SetPosition(0, startPoint.position);
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

    public void DisableGadget()
    {
        gameObject.SetActive(false);
    }

    public void EnableGadget()
    {
        gameObject.SetActive(true);
    }
}
