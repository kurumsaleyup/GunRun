using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject particlePosition;
    [SerializeField] private GameObject bullet;
    private RectTransform _crosshair;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        _crosshair = FindObjectOfType<CrosshairController>().GetComponent<RectTransform>();
    }

    void Update()
    {
        /*RaycastHit hitInfo;*/
        Ray ray = new Ray(particlePosition.transform.position, particlePosition.transform.forward);

        /*
        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            var spawnBullet = Instantiate(bullet, particlePosition.transform.position,
                particlePosition.transform.localRotation);
            spawnBullet.GetComponent<Rigidbody>().AddRelativeForce(ray.direction * 3000f);
            Destroy(spawnBullet, 2f);
        }
    }
}