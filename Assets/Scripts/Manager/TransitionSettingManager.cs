using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;


public class TransitionSettingManager : StaticInstance<TransitionSettingManager>
{
    public Dictionary<string, TransitionSettings> transitionSettings = new Dictionary<string, TransitionSettings>();
    public TransitionSettings DefaultTransition;
    public float startDelay = 0;

    private List<TransitionSettings> transitionSettingList = new List<TransitionSettings>();
    void Awake()
    {
        base.Awake();
        transitionSettingList.AddRange(Resources.LoadAll<TransitionSettings>(""));
        if (DefaultTransition == null && transitionSettingList.Count != 0) DefaultTransition = transitionSettingList[0];
        foreach (var transition in transitionSettingList)
        {
            transitionSettings.Add(transition.name.ToLower(), transition);
        }
    }

    public void LoadScene(string _sceneName)
    {
        Debug.Log(_sceneName);
        TransitionManager.Instance().Transition(_sceneName, DefaultTransition, startDelay);
    }

    public void LoadScene(string _sceneName, TransitionSettings transition)
    {
        TransitionManager.Instance().Transition(_sceneName, transition, startDelay);
    }

    public void LoadScene(TransitionSettings transition, System.Action onTransitionCutPointReached = null)
    {
        TransitionManager.Instance().Transition(transition, startDelay);
        TransitionManager.Instance().onTransitionCutPointReached = () =>
        {
            onTransitionCutPointReached?.Invoke();
        };
    }

    public void LoadScene(float startDelay, System.Action onTransitionCutPointReached = null)
    {
        TransitionManager.Instance().Transition(DefaultTransition, startDelay);
        TransitionManager.Instance().onTransitionCutPointReached = () =>
        {
            onTransitionCutPointReached?.Invoke();
        };
    }

    public void LoadScene(System.Action onTransitionCutPointReached = null)
    {
        TransitionManager.Instance().Transition(DefaultTransition, startDelay);
        TransitionManager.Instance().onTransitionCutPointReached = () =>
        {
            onTransitionCutPointReached?.Invoke();
        };
    }


    public void LoadScene(TransitionSettings transition, float startDelay, System.Action onTransitionCutPointReached = null)
    {
        TransitionManager.Instance().Transition(transition, startDelay);
        TransitionManager.Instance().onTransitionCutPointReached = () =>
        {
            onTransitionCutPointReached?.Invoke();
        };
    }
}
