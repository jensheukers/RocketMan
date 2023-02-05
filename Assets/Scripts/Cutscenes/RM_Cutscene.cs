using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles cutscenes for Rocketman, cutscenes are gameobjects in the scene that have a animator to controll their child objects.
/// </summary>
public class RM_Cutscene : MonoBehaviour {
    [SerializeField]
    private Camera cam; /** Reference to the camera to use*/
    private Camera mainCam; /** Reference to main camera*/

    [SerializeField]
    private UnityEvent onCutsceneStart; /** Event gets called when cutscene starts*/

    [SerializeField]
    private UnityEvent onCutsceneStop; /** Event gets called when cutscene stop*/

    private Animator animator; /** Refernce to Animator component*/

    private bool isPlaying; /** IsPlaying Bool*/

    [SerializeField]
    private float animationLenght = 5; /** The lenght of the animation*/

    [SerializeField]
    private float fadeOutDuration = 2; /** The duration of the fade out*/
    
    private void Start() {
        if (!cam) Debug.LogError("Camera needs to be set in order for cutscene to work");

        mainCam = Camera.main;

        animator = GetComponent<Animator>();
        cam.depth = Camera.main.depth - 1;

        isPlaying = false;
    }

    /**
     * Plays the cutscene
     */
    public void Play() {
        Debug.Log("Playing Cutscene: " + transform.name);
        isPlaying = true;
        cam.depth = mainCam.depth + 1;

        animator.SetTrigger("PlayAnimation");

        if (onCutsceneStart != null) {
            onCutsceneStart.Invoke();
        }

        StartCoroutine("Stop");
    }

    /**
     * Stops the cutscene from playing, will wait for animation lenght to stop
     */
    public IEnumerator Stop() {
        yield return new WaitForSeconds(animationLenght);

        Debug.Log("Stopping Cutscene: " + transform.name);
        isPlaying = false;
        cam.depth = mainCam.depth - 1;

        onCutsceneStop.Invoke();

        //Fade out to scene
        RM_GameState.FadeScreen(new Color(0,0,0,1), new Color(0,0,0,0), fadeOutDuration);
    }

    /**
     * Adds a UnityAction on cutscenestop event.
     * @param UnityAction function or lambda code
     */
    public void AddOnCutsceneStop(UnityAction func) {
        onCutsceneStop.AddListener(func);
    }
}
