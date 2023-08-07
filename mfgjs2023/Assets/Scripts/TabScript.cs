using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabScript : MonoBehaviour
{
    public Button button;
    public bool open;
    public Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }
}
