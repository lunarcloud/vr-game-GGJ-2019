using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnPoint : MonoBehaviour
{
    private Renderer _renderer;

    private Color initialColor;
    
    void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        initialColor = _renderer.material.color;
    }

    public void OnPointerEnter()
    {
        _renderer.material.color = Color.red;
    }

    public void OnPointerExit()
    {
        _renderer.material.color = initialColor;
    }
}
