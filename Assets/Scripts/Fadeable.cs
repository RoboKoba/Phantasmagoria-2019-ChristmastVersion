using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fadeable : MonoBehaviour
{   
    private MeshRenderer mr;
    private Material mat;
    public float speed = 0.01f;
    public float StartOpacity = 0f;
    public float EndOpacity = 1f;
    public string ColorPropertyName = "_Color";

    private bool fadingIn;
    private bool fadingOut;
    

    void Start()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
        mat = mr.material;
    }

    public void FadeIn()
    {
        fadingIn = true;
        fadingOut = false;
    }

    public void FadeOut()
    {
        fadingIn = false;
        fadingOut = true;
    }
    
    void Update()
    {
        if (!fadingIn && !fadingOut) return;
        var col = mat.GetColor(ColorPropertyName);
        if (fadingIn)
        {
            mr.enabled = true;
            if (col.a >= EndOpacity)
            {
                fadingIn = false;
                return;
            }
            col.a += speed;
            mat.SetColor(ColorPropertyName, col);
        }
        else if (fadingOut)
        {
            if (col.a <= StartOpacity)
            {
                fadingOut = false;
                mr.enabled = false;
                return;
            }
            col.a -= speed;
            mat.SetColor(ColorPropertyName, col);
        }
    }
}
