using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Textsequence can display a sequence of text in the middle of the screen
/// </summary>
public class RM_TextSequence : MonoBehaviour {
    [SerializeField]
    private List<string> textSequences = new List<string> (); /** List of sequences to go through must match waitseconds timing*/

    [SerializeField]
    private TMP_Text text; /** The text component reference */

    [SerializeField]
    private int fadeinSeconds = 2; /**The seconds of the sequence to fade in*/

    [SerializeField]
    private int fadeoutSeconds = 2; /**The seconds of the sequence to fade out*/

    [SerializeField]
    private int waitTime = 10; /** The time to wait until stopping sequence*/

    [SerializeField]
    private int preWaitTime = 2; /** Pre wait time before playing*/

    private bool isPlaying; /**If true the textsequence is playing*/

    private void Start() {
        isPlaying = false;
    }

    /**
     * @brief Plays the text sequence
     */
    public void Play() {
        isPlaying = true;
        StartCoroutine(PlaySequence());
    }

    /**
     * @brief Plays the text sequence internal
     */
    private IEnumerator PlaySequence() {
        yield return new WaitForSeconds(preWaitTime);
        for (int i = 0; i < textSequences.Count; i++) {
            text.text = textSequences[i];
            StartCoroutine(FadeTextToFullAlpha(fadeinSeconds, text));
            yield return new WaitForSeconds(waitTime - fadeoutSeconds);
            StartCoroutine(FadeTextToZeroAlpha(fadeoutSeconds, text));
            yield return new WaitForSeconds(fadeoutSeconds); // Wait for waittime to complete
        }
        //Set text to empty string
        text.text = "";
        isPlaying = false;

    }

    /**
     * @brief Fades text to full alpha
     */
    private IEnumerator FadeTextToFullAlpha(int t, TMP_Text i) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }


    /**
     * @brief Fades text to zero alpha
     */
    private IEnumerator FadeTextToZeroAlpha(int t, TMP_Text i) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    /**
     * @return isPlaying
     */
    public bool IsPlaying() { return isPlaying; }
}