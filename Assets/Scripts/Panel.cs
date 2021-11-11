using Pixelplacement;
using UnityEngine;

public class Panel : SelectableBehaviour
{
    [SerializeField]
    private GameObject panel;

    public void Start()
    {
        renderer = panel.GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }
}
