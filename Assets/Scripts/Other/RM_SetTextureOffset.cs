using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_SetTextureOffset : MonoBehaviour {
    private Renderer r;

    [SerializeField]
    private float scrollSpeed = 0.5f;

    void Start() { 
        r = GetComponent<Renderer>();
    }

    private void Update() {
        float offset = Time.time + scrollSpeed;

        r.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
