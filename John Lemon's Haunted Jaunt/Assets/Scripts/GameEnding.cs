﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    bool m_HasAudioPlayed;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            m_IsPlayerAtExit = true;
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerAtExit)
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        else if (m_IsPlayerCaught)
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audio)
    {
        if(!m_HasAudioPlayed)
        {
            audio.Play();
            m_HasAudioPlayed = true;
        }
        m_Timer = m_Timer + Time.deltaTime;

        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if(doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
                Application.Quit();
        }
    }
}