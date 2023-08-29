using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// A script that allows the player to click on a GameObject to move it around with their mouse, then
/// click again to let go of it. All Game Objects involved must have a BoxCollider (but a RigidBody is not required)
/// </summary>
public class ObjectScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject draggedObject;
    public GameObject clone;

    public Tool tool;

    private const int UILayer = 5;

    private Transform cloneContainer;

    private void Start()
    {
        AddPhysicsRaycaster();

        cloneContainer = GameObject.FindGameObjectWithTag("Background").transform;
    }

    private void Update()
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

    private List<RaycastResult> RaycastAtMouse()
    {
        // Courtesy of : https://stackoverflow.com/a/74744617/12462601
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results;
    }

    private bool TrySelling()
    {
        if (!draggedObject.transform.parent.gameObject.TryGetComponent<PlotScript>(out var plot)) { return false; }

        var results = RaycastAtMouse();
        var sellBox = results.Where(r =>
            r.gameObject.layer == UILayer &&
            r.gameObject == GameObject.FindGameObjectWithTag("Sell"));

        if (sellBox.Count() == 0) { return false; }

        plot.SellCrop();

        return true;
    }

    private void CheckForPlots()
    {
        var results = RaycastAtMouse();

        var plots = results.Where(r =>
            r.gameObject.layer == UILayer &&
            r.gameObject.GetComponent<PlotScript>() != null);

        if (plots.Count() > 0 && tool != null)
        {
            PlotScript plot = plots.First().gameObject.GetComponent<PlotScript>();
            tool.OnUse(plot);
        }
    }

    private void GenerateClone()
    {
        clone = Instantiate(draggedObject, cloneContainer);
        
        Image image = clone.GetComponent<Image>();
        if (image != null)
        {
            image.rectTransform.sizeDelta = draggedObject.GetComponent<RectTransform>().sizeDelta;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GenerateClone();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(clone);
        if (!TrySelling())
        {
            CheckForPlots();
        }
    }
}