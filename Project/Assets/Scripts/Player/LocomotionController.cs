using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    [SerializeField] private XRController teleportRay;
    [SerializeField] private InputHelpers.Button teleportButton;
    [SerializeField] private float activationTreshold = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.menuActive)
        {
            if (teleportRay)
            {
                teleportRay.gameObject.SetActive(CheckIfTeleportIsPressed(teleportRay));
            }
        }
        else
        {
            teleportRay.gameObject.SetActive(false);
        }
    }

    private bool CheckIfTeleportIsPressed(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportButton, out bool isActivated, activationTreshold);
        return isActivated;
    }
}
