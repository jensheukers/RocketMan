using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RM_Cutscene : MonoBehaviour {
    [SerializeField]
    private Camera cam;
    private Camera mainCam;

    [SerializeField]
    private UnityEvent onCutsceneStop;

   
    private Animator animator;

    private bool isPlaying;

    [SerializeField]
    private float animationLenght = 5;

    [SerializeField]
    private float fadeOutDuration = 2;
    
    private void Start() {
        if (!cam) Debug.LogError("Camera needs to be set in order for cutscene to work");

        mainCam = Camera.main;

        animator = GetComponent<Animator>();
        cam.depth = Camera.main.depth - 1;

        isPlaying = false;
    }

    public void Play() {
        Debug.Log("Playing Cutscene: " + transform.name);
        isPlaying = true;
        cam.depth = mainCam.depth + 1;

        animator.SetTrigger("PlayAnimation");

        StartCoroutine("Stop");
    }

    public IEnumerator Stop() {
        yield return new WaitForSeconds(animationLenght);

        Debug.Log("Stopping Cutscene: " + transform.name);
        isPlaying = false;
        cam.depth = mainCam.depth - 1;

        onCutsceneStop.Invoke();

        //Fade out to scene
        RM_GameState.FadeScreen(new Color(0,0,0,1), new Color(0,0,0,0), fadeOutDuration);
    }

    public void AddOnCutsceneStop(UnityAction func) {
        onCutsceneStop.AddListener(func);
    }
}
