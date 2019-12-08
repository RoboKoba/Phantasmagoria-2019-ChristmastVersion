using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SkullController : MonoBehaviour

{
    private List<Fadeable> fadeables;
    public Fadeable Wig;

    void Start()
    {
        fadeables = gameObject.GetComponentsInChildren<Fadeable>().ToList();
        fadeables.Add(Wig);
        fadeables.ForEach(i => i.FadeOut());
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fadeables.ForEach(i => i.FadeIn());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            fadeables.ForEach(i => i.FadeOut());
        }
    }
}