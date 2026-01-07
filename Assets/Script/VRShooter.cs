using UnityEngine;
using UnityEngine.InputSystem;

public class VRShooter : MonoBehaviour
{
    [Header("References")]
    // Point where the projectile is created
    public Transform muzzle; 
    // Prefab of the laser projectile to shoot
    public GameObject projectilePrefab;

    [Header("Input Actions")]
    // Action reference for the VR trigger
    public InputActionProperty shootAction; 

    void Update()
    {
        // Input detection for VR trigger and Mouse Left Click
        bool vrTriggerPressed = shootAction.action != null && shootAction.action.WasPressedThisFrame();
        bool mousePressed = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        if (vrTriggerPressed || mousePressed)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && muzzle != null)
        {
            // Creation of the projectile at muzzle position and rotation
            Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        }
    }
}
