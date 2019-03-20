using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Vector3 localScale;

    private void Start()
    {
        localScale = transform.localScale;
    }

    public void SetSize(float size)
    {
        localScale.x = size / 100;
        transform.localScale = localScale;
    }


}
