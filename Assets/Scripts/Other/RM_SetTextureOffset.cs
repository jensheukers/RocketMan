using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets texture offset by script
/// </summary>
public class RM_SetTextureOffset : MonoBehaviour {
    private Renderer r; /** Reference to renderer*/

    [SerializeField]
    private float scrollSpeed = 0.5f; /**The speed to scroll the texture */

    void Start() { 
        r = GetComponent<Renderer>();
    }

    private void Update() {
        float offset = Time.time + scrollSpeed;

        r.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
