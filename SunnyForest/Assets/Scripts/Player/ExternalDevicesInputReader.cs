using Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExternalDevicesInputReader : IEntityInputSource
{
    public Vector2 Direction => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    public bool Attack { get; private set; }

    public void OnUpdate()
    {
        if (!IsPointerOverUI() && Input.GetButtonDown("Fire1"))
            Attack = true;
    }

    private bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    public void ResetOneTimeActions()
    {
        Attack = false;
    }
}