using UnityEngine;

public class Theater : MonoBehaviour
{
    [SerializeField]
    private GameObject extras;

    private ShowroomController showroomController => ShowroomController.Instance;

    public void Awake()
    {
        showroomController.OnToggleExtras -= Switch;
        showroomController.OnToggleExtras += Switch;

        Switch(showroomController.Mode.HasFlag(StructureParts.Extras));
    }

    public void OnDestroy()
    {
        showroomController.OnToggleExtras -= Switch;
    }

    private void Switch(bool isVisible)
    {
        extras.SetActive(isVisible);
    }
}
