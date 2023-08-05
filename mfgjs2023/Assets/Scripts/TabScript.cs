using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabScript : MonoBehaviour
{
    //public GameObject srcObject;
    //public GameObject destObject;
    public Button button;
    public bool open;
    public Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    //public float duration;

    //private bool inView = false;
    //private bool isMoving = false;

    //private Vector3 srcPos;
    //private Vector3 destPos;

    //private void Start()
    //{
    //    srcPos = srcObject.transform.position;
    //    destPos = destObject.transform.position;
    //}

    //[ContextMenu("Toggle Slide")]
    //public void ToggleTab()
    //{
    //    StartCoroutine(SlideTab(
    //        inView ? destPos : srcPos,
    //        inView ? srcPos : destPos,
    //        duration));
    //    inView = !inView;
    //}

    //[ContextMenu("Slide IN")]
    //private void OpenTab()
    //{
    //    inView = true;
    //    StartCoroutine(SlideTab(srcPos, destPos, duration));
    //}

    //[ContextMenu("Slide OUT")]
    //private void CloseTab()
    //{
    //    inView = false;
    //    StartCoroutine(SlideTab(destPos, srcPos, duration));
    //}

    //IEnumerator SlideTab(Vector3 src, Vector3 dest, float duration)
    //{
    //    //Make sure there is only one instance of this function running
    //    if (isMoving)
    //    {
    //        yield break; ///exit if this is still running
    //    }
    //    isMoving = true;

    //    float counter = 0;
    //    //Vector3 tabOffset = new Vector3(tabButton.gameObject.GetComponent<RectTransform>().sizeDelta.x / 2 +
    //    //    srcObject.GetComponent<RectTransform>().sizeDelta.x / 2, 0);

    //    Vector3 btnSrc = tabButton.transform.position;
    //    Vector3 btnDest = new Vector3(tabButton.transform.position.x - src.x + dest.x,
    //        tabButton.transform.position.y, 0);

    //    while (counter < duration)
    //    {
    //        counter += Time.deltaTime;
    //        srcObject.transform.position = CustomInterpolate(src, dest, counter, duration);
    //        tabButton.transform.position = CustomInterpolate(btnSrc, btnDest, counter, duration);
    //        yield return null;
    //    }

    //    isMoving = false;
    //}

    //private Vector3 CustomInterpolate(Vector3 a, Vector3 b, float counter, float duration)
    //{
    //    float t = MapFunction(counter) / MapFunction(duration);
    //    return Vector3.Lerp(a, b, t);
    //}

    //private float MapFunction(float x)
    //{
    //    return x * x * x;
    //}
}
