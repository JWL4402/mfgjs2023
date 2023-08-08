using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public float slideDuration;

    private GameObject activeTab;
    [SerializeField] private GameObject[] tabs;

    private enum TabState { OPEN, CLOSED };

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void SwitchActiveTab(GameObject tab)
    {
        if (tab == activeTab)
        {
            StartCoroutine(SlideTab(tab, TabState.CLOSED));
            activeTab = null;
            return;
        }

        activeTab = tab;
        StartCoroutine(OpenActiveTab());
    }

    private IEnumerator OpenActiveTab()
    {
        yield return StartCoroutine(CloseTabs());
        yield return StartCoroutine(SlideTab(activeTab, TabState.OPEN));
    }

    private IEnumerator CloseTabs()
    {
        foreach (GameObject tab in tabs)
        {
            yield return StartCoroutine(SlideTab(tab, TabState.CLOSED));
        }
    }

    private bool inMotion;
    IEnumerator SlideTab(GameObject tab, TabState state)
    {
        TabScript tabScript = tab.GetComponent<TabScript>();
        if (tabScript.open == (state == TabState.OPEN) ? true : false)
        {
            yield break;
        }

        //Make sure there is only one instance of this function running
        if (inMotion)
        {
            yield break; ///exit if this is still running
        }
        inMotion = true;

        float counter = 0;
        
        Button tabButton = tabScript.button;

        Vector3 tabSrc = (state == TabState.OPEN) ? tabScript.initPos : gameObject.transform.position;
        Vector3 tabDest = (state == TabState.CLOSED) ? tabScript.initPos : gameObject.transform.position;

        Vector3 btnSrc = tabButton.transform.position;
        Vector3 btnDest = new Vector3(tabButton.transform.position.x - tabSrc.x + tabDest.x,
            tabButton.transform.position.y, 0);

        while (counter < slideDuration)
        {
            counter += Time.deltaTime;
            tab.transform.position = CustomInterpolate(tabSrc, tabDest, counter, slideDuration);
            tabButton.transform.position = CustomInterpolate(btnSrc, btnDest, counter, slideDuration);
            yield return null;
        }

        inMotion = false;
        tabScript.open = !tabScript.open;
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
