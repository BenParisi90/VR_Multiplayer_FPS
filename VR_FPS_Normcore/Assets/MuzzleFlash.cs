using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + transform.forward * 100);
        StartCoroutine(DestroyAfterSeconds(.25f));
    }

    IEnumerator DestroyAfterSeconds(float numSeconds)
    {
        yield return new WaitForSeconds(numSeconds);
        Destroy(gameObject);
    }
}
