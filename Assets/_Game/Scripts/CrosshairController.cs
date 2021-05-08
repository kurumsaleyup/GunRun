using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private GameObject _crosshair;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            _crosshair.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            _crosshair.SetActive(false);
        }
    }
}
