using Pixelplacement;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableBehaviour : MonoBehaviour
{
    protected Color color = new Color(0.7f, 0.7f, 0.7f, 0.1f);

    protected new Renderer renderer;
    protected new Collider collider;

    protected static bool isPressed = false;
    protected bool isSelected = false;

    public Action OnDeselected;
    public Action<SelectableBehaviour> OnSelected;

    protected ShowroomController showroomController => ShowroomController.Instance;
    protected InterfaceController interfaceController => InterfaceController.Instance;

    public virtual void Awake()
    {
        OnSelected -= interfaceController.ShowMaterialOptions;
        OnSelected += interfaceController.ShowMaterialOptions;

        OnDeselected -= interfaceController.HideMaterialOptions;
        OnDeselected += interfaceController.HideMaterialOptions;

        showroomController.BackTrigger.OnTriggered -= Deselect;
        showroomController.BackTrigger.OnTriggered += Deselect;
    }

    public virtual void OnDestroy()
    {
        showroomController.BackTrigger.OnTriggered -= Deselect;
    }

    public void OnMouseEnter()
    {
        if (!enabled)
        {
            return;
        }

        isPressed = Input.GetMouseButton(1);

        if (!interfaceController.HasSelection && !isPressed)
        {
            ChangeColor(showroomController.SelectionColor);
        };
    }

    public void OnMouseExit()
    {
        if (!enabled)
        {
            return;
        }

        if (!interfaceController.HasSelection && !isPressed)
        {
            ResetColor();
        };
    }

    public void OnMouseUpAsButton()
    {
        if (!enabled)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (isSelected)
        {
            Deselect();
        }
        else if (!interfaceController.HasSelection)
        {
            Select();
        }
    }

    public void Select()
    {
        ChangeColor(showroomController.SelectionColor);
        OnSelected.Invoke(this);
        isSelected = true;
    }

    public void Deselect()
    {
        if (!enabled)
        {
            return;
        }

        if (!gameObject.activeSelf | !isSelected)
        {
            return;
        }

        ResetColor();
        OnDeselected.Invoke();
        isSelected = false;
    }

    public virtual void ChangeColor(Color newColor)
    {
        Tween.ShaderColor(renderer.material, "_BaseColor", newColor, 0.2f, 0f);
    }

    public virtual void ResetColor()
    {
        Tween.ShaderColor(renderer.material, "_BaseColor", color, 0.2f, 0f);
    }

    public virtual void ChangeMaterial(Material newMaterial)
    {
        renderer.material = newMaterial;
        renderer.material.SetTextureScale("_BaseMap", renderer.transform.localScale * 0.5f);
        color = newMaterial.color;
        isSelected = false;
    }
}
