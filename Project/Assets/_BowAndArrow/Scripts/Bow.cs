using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class Bow : XRGrabInteractable
{
    private Notch notch = null;

    [SerializeField] private XRDirectInteractor leftController;
    [SerializeField] private XRDirectInteractor rightController;

    protected override void Awake()
    {
        base.Awake();
        notch = GetComponentInChildren<Notch>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // Only notch an arrow if the bow is held
        selectEntered.AddListener(notch.SetReady);
        selectExited.AddListener(notch.SetReady);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        selectEntered.RemoveListener(notch.SetReady);
        selectExited.RemoveListener(notch.SetReady);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Player.PlayerInstance.quiver.SetActive(true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Player.PlayerInstance.quiver.SetActive(false);
    }
}
