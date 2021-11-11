using Pixelplacement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InterfaceController : SingletonBehaviour<InterfaceController>
{
    private const float offset = 270f;

    [SerializeField]
    private RectTransform materialMenu;
    [SerializeField]
    private List<RectTransform> spawnerMenus;

    private SelectableBehaviour selected;

    public bool HasSelection => selected != null;

    public void ShowMaterialOptions(SelectableBehaviour selectable)
    {
        selected = selectable;

        Tween.AnchoredPosition(materialMenu, Vector2.zero, 0.2f, 0f);
    }

    public void HideMaterialOptions()
    {
        Tween.AnchoredPosition(materialMenu, Vector2.right * offset, 0.2f, 0f);

        selected = null;
    }

    public void ShowSpawnerOptions(SelectableBehaviour selectable)
    {
        selected = selectable;

        Tween.AnchoredPosition(spawnerMenus[((int)selected.transform.localScale.x) - 1], Vector2.zero, 0.2f, 0f);
    }

    public void HideSpawnerOptions()
    {
        Tween.AnchoredPosition(spawnerMenus[((int)selected.transform.localScale.x) - 1], Vector2.right * offset, 0.2f, 0f);

        selected = null;
    }

    public void SetMaterialColor(Material material)
    {
        selected.ChangeMaterial(material);

        HideMaterialOptions();
    }

    public void SetSpawnerObject(GameObject template)
    {
        Spawner spawner = selected as Spawner;
        spawner.ChangeObject(template);

        HideSpawnerOptions();
    }
}
