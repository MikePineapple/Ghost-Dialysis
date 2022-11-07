using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CuteBoy : MonoBehaviour
{
    [SerializeField] GameObject angryFace;
    [SerializeField] GameObject neutralFace;
    [SerializeField] GameObject happyFace;
    [SerializeField] GameObject staticLiquid;
    [SerializeField] GameObject flowLiquid;

    [SerializeField] GameObject video;
    [SerializeField] GameObject videoTexture;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject chooseBanner;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject fbiAgent;
    [SerializeField] GameObject fbiSound;
    [SerializeField] GameObject winScreen;

    private bool isAngry;
    public float notAngerCounter;
    public float notAngerCounterLimit;
    public float angerCounter;
    public float angerLimit;
    public float endCounter;
    public float endLimit;
    public float safeCounter;
    public float safeLimit;

    public float enterCounter;
    public float enterLimit;

    public float winCounter;
    public float winLimit;

    public byte alpha = 0;
    private Color color;

    [SerializeField] States currentState = States.NEUTRAL;
    public enum States
    {
        NEUTRAL,
        HAPPY,
        ANGER
    }

    private void Start()
    { 
        video.SetActive(false);
        videoTexture.SetActive(false);
    }
    private void Update()
    {
        safeCounter += 1 * Time.deltaTime;
        if (safeCounter > safeLimit)
        {
            audioSource.volume = 0;
            fbiSound.SetActive(true);
            buttons.SetActive(false);
            GettingNeutral();
            chooseBanner.SetActive(false);
            enterCounter += 1 * Time.deltaTime;

            if (enterCounter > enterLimit)
            {
                fbiAgent.SetActive(true);
                winCounter += 1 * Time.deltaTime;

                if (winCounter > winLimit)
                {
                    flowLiquid.SetActive(false);
                    staticLiquid.SetActive(false);
                    this.gameObject.SetActive(false);
                    fbiAgent.SetActive(false);
                    winScreen.SetActive(true);
                }
            }
        }
        
        if (currentState != States.ANGER)
        {
            notAngerCounter += 1 * Time.deltaTime;
            if (notAngerCounter >= notAngerCounterLimit)
            {
                GettingAngry();
                notAngerCounter = 0;
            }
                
        }
        else
            notAngerCounter = 0;

        if (isAngry)
            angerCounter += 1 * Time.deltaTime;
        else
            angerCounter = 0;
        if (angerCounter > angerLimit)
        {
            video.SetActive(true);
            videoTexture.SetActive(true);
            buttons.SetActive(false);
            audioSource.volume = 0;
            endCounter += 1 * Time.deltaTime;
            if (endCounter > endLimit)
                Menu();
        }
        Debug.Log(angerCounter);

    }

    public void SetCurrentState (States state)
    {
        if (currentState != state)
        {
            currentState = state;
            switch (currentState)
            {
                case States.NEUTRAL:
                    Debug.Log("is neutral");
                    angryFace.SetActive(false);
                    happyFace.SetActive(false);
                    neutralFace.SetActive(true);
                    staticLiquid.SetActive(true);
                    animator.SetTrigger("neutral");
                    isAngry = false;
                    break;
                case States.HAPPY:
                    Debug.Log("is happy");
                    angryFace.SetActive(false);
                    happyFace.SetActive(true);
                    neutralFace.SetActive(false);
                    staticLiquid.SetActive(true);
                    animator.SetTrigger("happy");
                    isAngry = false;
                    break;
                case States.ANGER:
                    Debug.Log("is angry");
                    angryFace.SetActive(true);
                    happyFace.SetActive(false);
                    neutralFace.SetActive(false);
                    staticLiquid.SetActive(false);
                    animator.SetTrigger("angry");
                    isAngry = true;
                    break;
            }
        }
    }

    public void GettingAngry()
    {
        SetCurrentState(States.ANGER);
    }
    public void GettingHappy()
    {
        SetCurrentState(States.HAPPY);
    }
    public void GettingNeutral()
    {
        SetCurrentState(States.NEUTRAL);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
