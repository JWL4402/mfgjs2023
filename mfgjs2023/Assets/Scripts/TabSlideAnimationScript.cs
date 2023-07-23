using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabSlideAnimationScript : MonoBehaviour
{
    public GameObject srcObject;
    public GameObject destObject;

    public float duration;

    private bool inView = false;
    private bool isMoving = false;

    private Vector3 srcPos;
    private Vector3 destPos;

    private void Start()
    {
        srcPos = srcObject.transform.position;
        destPos = destObject.transform.position;
    }

    [ContextMenu("Toggle Slide")]
    public void ToggleSlide()
    {
        StartCoroutine(SlideTab(
            inView ? destPos : srcPos,
            inView ? srcPos : destPos,
            duration));
        inView = !inView;
    }

    [ContextMenu("Slide IN")]
    private void SlideIntoView()
    {
        inView = true;
        StartCoroutine(SlideTab(srcPos, destPos, duration));
    }

    [ContextMenu("Slide OUT")]
    private void SlideOutOfView()
    {
        inView = false;
        StartCoroutine(SlideTab(destPos, srcPos, duration));
    }

    IEnumerator SlideTab(Vector3 src, Vector3 dest, float duration)
    {
        //Make sure there is only one instance of this function running
        if (isMoving)
        {
            yield break; ///exit if this is still running
        }
        isMoving = true;

        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            srcObject.transform.position = CustomInterpolate(src, dest, counter, duration);
            yield return null;
        }

        isMoving = false;
    }

    private Vector3 CustomInterpolate(Vector3 a, Vector3 b, float counter, float duration)
    {
        float t = MapFunction(counter) / MapFunction(duration);
        return Vector3.Lerp(a, b, t);
    }

    private float MapFunction(float x)
    {
        return x * x * x;
    }
}
