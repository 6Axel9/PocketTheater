using Pixelplacement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CoverSide
{
    Back,
    Front,
}

public class Cover : SelectableBehaviour
{
    [SerializeField]
    protected List<Renderer> renderers;

    public void Start()
    {
        renderers = GetComponentsInChildren<Renderer>().ToList();
    }

    public override void ChangeColor(Color newColor)
    {
        foreach(Renderer renderer in renderers)
        {
            Tween.ShaderColor(renderer.material, "_BaseColor", newColor, 0.2f, 0f);
        }
    }

    public override void ResetColor()
    {
        foreach (Renderer renderer in renderers)
        {
            Tween.ShaderColor(renderer.material, "_BaseColor", color, 0.2f, 0f);
        }
    }

    public override void ChangeMaterial(Material newMaterial)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material = newMaterial;
            renderer.material.SetTextureScale("_BaseMap", renderer.transform.localScale * 0.5f);
            isSelected = false;
        }
        color = newMaterial.color;
    }
}