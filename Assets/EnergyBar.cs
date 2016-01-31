using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    private GameObject fillBar;
    private bool animate;
    private float scaleTo;

    public float animationSpeed = 1f;

    float timer = 0f;


    void Start()
    {
        fillBar = GameObject.Find("Energy fill");
    }

    void Update()
    {
        if (animate)
        {
            fillBar.transform.localScale = Vector3.MoveTowards(fillBar.transform.localScale, GetScaleVector(scaleTo), animationSpeed * Time.deltaTime);
            if (Mathf.Approximately(fillBar.transform.localScale.x, scaleTo))
                animate = false;
        }
    }

    private Vector3 GetScaleVector(float size)
    {
        return new Vector3(size, 0.7f, 1f);
    }

    public void SetEnergyBar(float size)
    {
        if (size < 0)
            size = 0f;
        if (size > 1)
            size = 1f;
        scaleTo = size;
        animate = true;
    }

    public void SetEnergyBarNoAnim(float size)
    {
        if (size < 0)
            size = 0f;
        if (size > 1)
            size = 1f;
        scaleTo = size;
        fillBar.transform.localScale = GetScaleVector(size);
    }

}
