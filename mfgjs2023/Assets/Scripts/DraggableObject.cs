using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private const int UILayer = 5;

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

    private void CheckForPlots()
    {
        // Courtesy of : https://stackoverflow.com/a/74744617/12462601
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        if (results.Where(r => r.gameObject.layer == UILayer).Count() > 0)
        {
            Debug.Log(results[0].gameObject.name);
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
        CheckForPlots();
    }
}