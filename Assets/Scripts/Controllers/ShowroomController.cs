using Pixelplacement;
using System;
using UnityEngine;

public enum StructureParts
{
    None     = 0x00,
    Extras   = 0x01,
    Spawners = 0x10,
}

public class ShowroomController : SingletonBehaviour<ShowroomController>
{
    [SerializeField]
    private EventTriggerBehaviour backTrigger;
    public EventTriggerBehaviour BackTrigger => backTrigger;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    private float inputSensitivity = 10f;

    [SerializeField]
    private Color selectionColor = new Color(0.3f, 1.0f, 1.0f, 0.1f);
    public Color SelectionColor => selectionColor;

    public Action<bool> OnToggleExtras;
    public Action<bool> OnToggleSpawner;

    private StructureParts mode = StructureParts.None;
    public StructureParts Mode => mode;

    private Vector2 previousInput;
    private Vector2 inputDelta;

    public void Start()
    {
        Input.simulateMouseWithTouches = true;
    }

    public void Update()
    {
        Rotate();
    }

    public void ToogleExtras() => ToogleParts(StructureParts.Extras);

    public void EnableExtras(bool isDisabled)
    {
        mode = !isDisabled ? mode | StructureParts.Extras : mode & ~StructureParts.Extras;
        Show();
    }

    public void ToogleSpawners() => ToogleParts(StructureParts.Spawners);

    public void EnableSpawners(bool isDisabled)
    {
        mode = !isDisabled ? mode | StructureParts.Spawners : mode & ~StructureParts.Spawners;
        Show();
    }

    private void Show()
    {
        BackTrigger.OnTriggered.Invoke();
        OnToggleExtras.Invoke(mode.HasFlag(StructureParts.Extras));
        OnToggleSpawner.Invoke(mode.HasFlag(StructureParts.Spawners));
    }

    private void ToogleParts(StructureParts flags)
    {
        mode = mode.HasFlag(flags) ? mode & ~flags : mode | flags;
        Show();
    }

    private void Rotate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            previousInput = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            Vector2 inputPosition = Input.mousePosition;
            inputDelta = inputPosition - previousInput;

            transform.Rotate(Vector3.right, Time.deltaTime * inputDelta.y * inputSensitivity * 0.1f, Space.World);
            transform.Rotate(Vector3.up, Time.deltaTime * -inputDelta.x * inputSensitivity, Space.World);

            Vector3 rotation = transform.eulerAngles;
            rotation.x = ClampAngle(rotation.x, -15f, 15f);
            rotation.z = 0f;
            transform.eulerAngles = rotation;

            previousInput = Input.mousePosition;
        }
    }

    float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f)
        {
            angle = 360 + angle;
        }
        if (angle > 180f)
        {
            return Mathf.Max(angle, 360 + from);
        }
        return Mathf.Min(angle, to);
    }
}