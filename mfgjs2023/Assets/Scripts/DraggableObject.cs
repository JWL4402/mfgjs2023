using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A script that allows the player to click on a GameObject to move it around with their mouse, then
/// click again to let go of it. All Game Objects involved must have a BoxCollider (but a RigidBody is not required)
/// </summary>
public class DraggableObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool generator;
    public GameObject copiedObject;
    public GameObject clone;

    void Start()
    {
        AddPhysicsRaycaster();
    }

    void Update()
    {
        if (clone == null) { return;  }

        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clone.transform.position = mouse_pos;
    }

    private void AddPhysicsRaycaster()
    {
        Physics2DRaycaster raycast = FindObjectOfType<Physics2DRaycaster>();
        if (raycast == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }

        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    /// <summary>
    /// Generates a clone of the Clickable object to be moved. Used for Generator functionality.
    /// </summary>
    private void GenerateClone()
    {
        clone = Object.Instantiate(copiedObject, gameObject.transform.parent);

        DraggableObject script = clone.GetComponent<DraggableObject>();
        if (script == null) { Debug.LogError("Generator generated an object without script."); }
        // if it's copy of object with this script, should have this script

        script.generator = false;
    }

    /// <summary>
    /// Trashes an object if it is dropped onto its Trash Object.
    /// </summary>
    /// <param name="event_pos">The 2D position of the click.</param>
    /// <returns>True if trashed, false otherwise.</returns>
    //private bool CheckTrashed(Vector2 event_pos)
    //{
    //    if (Trash_Object == null) { return false; }

    //    Vector3 click_pos = Camera.main.ScreenToWorldPoint(event_pos);
    //    RaycastHit2D[] ray_hits = Physics2D.GetRayIntersectionAll(new Ray(click_pos, transform.forward));

    //    foreach (RaycastHit2D hit in ray_hits)
    //    {
    //        if (hit.collider.gameObject == Trash_Object)
    //        {
    //            Destroy(gameObject);
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (generator) { GenerateClone(); }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (generator) { Destroy(clone); }
    }
}