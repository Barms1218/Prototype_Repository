using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Fields

    Rigidbody2D rb2d;
    CapsuleCollider2D capsuleCollider;

    // laser 
    LineRenderer lineRenderer;
    [SerializeField] Transform laserPoint;
    [SerializeField] float defaultRayDistance = 100f;
    Transform targetTransform;
    bool facingRight = true;

    #endregion


    #region Unity Methods


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        AimLaser();
    }

    #endregion


    #region Private Methods

    /// <summary>
    /// Take two vector2 positions for the line renderer component
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    void SetLinePositions(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    /// <summary>
    /// Enable line renderer and aim it at a raycast2d hit point
    /// </summary>
    void AimLaser()
    {
        lineRenderer.enabled = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - laserPoint.position;


        if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }


        if (Physics2D.Raycast(laserPoint.position, direction))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserPoint.position, direction);
            SetLinePositions(laserPoint.position, hit.point);

            if (hit.collider.gameObject.tag == "Target")
            {
                targetTransform = hit.collider.transform;

                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 tempPosition = transform.position;
                    transform.position = targetTransform.position;
                    hit.collider.transform.position = tempPosition;
                    targetTransform = null;
                }
            }
        }
        else
        {
            SetLinePositions(laserPoint.position, direction * defaultRayDistance);
        }
    }

    /// <summary>
    /// make the player face the direction they wish to fire the swazer
    /// </summary>
    void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale; 
    }

    #endregion
}
