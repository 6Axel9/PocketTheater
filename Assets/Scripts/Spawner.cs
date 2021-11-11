using Pixelplacement;
using UnityEngine;

public class Spawner : SelectableBehaviour
{
    [SerializeField]
    private GameObject element;

    public override void Awake()
    {
        showroomController.OnToggleSpawner -= Show;
        showroomController.OnToggleSpawner += Show;

        Show(showroomController.Mode.HasFlag(StructureParts.Spawners));

        OnSelected -= interfaceController.ShowSpawnerOptions;
        OnSelected += interfaceController.ShowSpawnerOptions;

        OnDeselected -= interfaceController.HideSpawnerOptions;
        OnDeselected += interfaceController.HideSpawnerOptions;

        showroomController.BackTrigger.OnTriggered -= Deselect;
        showroomController.BackTrigger.OnTriggered += Deselect;
    }
   
    public void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public override void OnDestroy()
    {
        showroomController.OnToggleSpawner -= Show;

        base.OnDestroy();
    }

    public void ChangeObject(GameObject template)
    {
        Tween.ShaderColor(renderer.material, "_BaseColor", color, 0.2f, 0f);

        if (element.transform.childCount > 0)
        {
            Destroy(element.transform.GetChild(0).gameObject);
        }
        Instantiate(template, element.transform);

        isSelected = false;
    }

    private void Show(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}