using Pixelplacement;
using UnityEngine;

public class Focus : MonoBehaviour
{
    [SerializeField]
    private Transform close;
    [SerializeField]
    private Transform far;

    private ShowroomController showroomController => ShowroomController.Instance;

    public void Awake()
    {
        showroomController.OnToggleExtras -= Zoom;
        showroomController.OnToggleExtras += Zoom;

        Zoom(showroomController.Mode.HasFlag(StructureParts.Extras));
    }

    public void OnDestroy()
    {
        showroomController.OnToggleExtras -= Zoom;
    }

    private void Zoom(bool hasExtras)
    {
        if(hasExtras)
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }

    private void ZoomIn()
    {
        Tween.Position(transform, close.position, 0.2f, 0);
        Tween.Rotation(transform, close.rotation, 0.2f, 0);
    }

    private void ZoomOut()
    {
        Tween.Position(transform, far.position, 0.2f, 0);
        Tween.Rotation(transform, far.rotation, 0.2f, 0);
    }
}
