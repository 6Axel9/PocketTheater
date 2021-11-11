using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    private GameObject small;
    [SerializeField]
    private GameObject big;

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

    private void Switch(bool hasExtras)
    {
        if (hasExtras)
        {
            ShowBig();
        }
        else
        {
            ShowSmall();
        }
    }

    private void ShowSmall()
    {
        small.SetActive(true);
        big.SetActive(false);
    }

    private void ShowBig()
    {
        big.SetActive(true);
        small.SetActive(false);
    }
}
