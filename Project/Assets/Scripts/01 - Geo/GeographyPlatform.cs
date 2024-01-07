using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeographyPlatform : MonoBehaviour
{
    public bool isActive = true;
    private Collider2D collider;
    private Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        // Récupérer les composants nécessaires
        collider = GetComponent<Collider2D>();
        renderers = GetComponentsInChildren<Renderer>();

        SetPlatformActive(isActive);
    }


    public void SetPlatformActive(bool isActive)
    {
        // Activer ou désactiver le Collider2D
        if (collider != null)
        {
            collider.enabled = isActive;
        }

        foreach (Renderer renderer in renderers)
        {
            // Activer ou désactiver le Renderer
            if (renderer != null)
            {
                renderer.enabled = isActive;
            }
        }

    }
}
