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
    public GameObject draggedObject;
    public GameObject clone;

    void Start()
    {
        AddPhysicsRaycaster();
    }

    void Update()
    {
        if (clone == null) { return; }

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

    private void GenerateClone()
    {
        clone = Object.Instantiate(draggedObject, gameObject.transform.parent);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GenerateClone();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(clone);
    }
}