using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float hp;
    public Transform progress;

    public void SetHp(float percent)
    {
        progress.localPosition = new Vector2(percent, 0);
    }

}
