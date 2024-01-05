using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using System;


// [AddComponentMenu("Everhood/EventCommands/PlayAudio")]
// [EventCommandInfo("FX", "Play audio")]
// public class PlayAudioEventCommand : EventCommand
// {
//     [SerializeField]
//     private AudioClip audioClip;
//     [SerializeField]
//     [Range(0, 256)]
//     private int priority = 128;
//     [SerializeField]
//     [Range(0, 1)]
//     private float volume = 1f;
//     [SerializeField]
//     [Range(-3, 3)]
//     private float pitch = 1f;

//     private AudioSource _source;

//     private void Awake(){
//         _source = new GameObject("[AudioSourceInstance]").AddComponent<AudioSource>();
//         _source.hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInBuild;
//         _source.gameObject.hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInBuild;

//         _source.priority = priority;
//         _source.playOnAwake = false;
//         _source.volume = volume;
//         _source.pitch = pitch;
//     }

//     public override void Execute(){
//         _source.PlayOneShot(audioClip);
//     }
// }

// [AddComponentMenu("Everhood/EventCommands/PlayAudioSource")]
// [EventCommandInfo("FX", "Play AudioSource")]
// public class PlayAudioSourceEventCommand : EventCommand
// {
//     [SerializeField]
//     private AudioSource source;

//     public override void Execute()
//     {
//         source.Play();
//     }
// }

// [AddComponentMenu("Everhood/EventCommands/PlayParticles")]
// [EventCommandInfo("FX", "Play particles")]
// public class PlayParticleEventCommand : EventCommand
// {
//     [SerializeField]
//     private ParticleSystem particleSystem;

//     public void SetParticle(ParticleSystem particle) => particleSystem = particle;

//     public override void Execute()
//     {
//         particleSystem.Play();
//     }
// }

// [AddComponentMenu("Everhood/EventCommands/SetActiveEvent")]
// [EventCommandInfo("Script", "Set Active")]
// public class SetActiveEventCommand : EventCommand
// {
//     public void SetParameters(GameObject target, bool state)
//     {
//         this.target = target;
//         this.state = state;
//     }
//     [SerializeField] private GameObject target;
//     [SerializeField] private bool state;
//     public override void Execute()
//     {
//         target.SetActive(state);
//     }
// }

// [AddComponentMenu("Everhood/EventCommands/SetAnimatorBoolEvent")]
// [EventCommandInfo("Animator", "Set bool")]
// public class SetAnimatorBoolEvent : EventCommand
// {
//     public void SetParameters(Animator animator, string boolVariableName, bool state)
//     {
//         this.animator = animator;
//         this.boolVariableName = boolVariableName;
//         this.state = state;
//     }

//     [SerializeField] private Animator animator;
//     [SerializeField] private string boolVariableName;
//     [SerializeField] private bool state;

//     public override void Execute()
//     {
//         animator.SetBool(boolVariableName, state);
//     }
// }

// [AddComponentMenu("Everhood/EventCommands/SetAnimatorRandomInteger")]
// [EventCommandInfo("Animator", "Set random integer")]
// public class SetAnimatorRandomInteger : EventCommand
// {
//     public void SetParameters(Animator animator, string integerVariableName, int min, int max)
//     {
//         this.animator = animator;
//         this.integerVariableName = integerVariableName;
//         this.min = min;
//         this.max = max;
//     }

//     [SerializeField] private Animator animator;

//     [SerializeField] private string integerVariableName;

//     [SerializeField]  private int min, max;

//     private int _previousNumber;

//     public override void Execute()
//     {
//         animator.SetInteger(integerVariableName, GetRandomNumber());
//     }

//     private int GetRandomNumber()
//     {

//         int randomNumber = 10; //Random.Range(min, max);
//         if (randomNumber == _previousNumber)
//         {
//             randomNumber++;
//             randomNumber = randomNumber >= max ? 0 : randomNumber;
//         }
//         _previousNumber = randomNumber;
//         return randomNumber;
//     }
// }


// [AddComponentMenu("Everhood/EventCommands/SetAnimatorTriggerEvent")]
// [EventCommandInfo("Animator", "Set trigger")]
// public class SetAnimatorTriggerEventCommand : EventCommand
// {
//     public void SetParameters(Animator animator, string triggerVariableName)
//     {
//         this.animator = animator;
//         this.triggerVariableName = triggerVariableName;
//     }

//     [SerializeField]
//     private Animator animator;

//     [SerializeField]
//     private string triggerVariableName;         

//     public override void Execute()
//     {
//         animator.SetTrigger(triggerVariableName);
//     }
// }



// [AddComponentMenu("Everhood/EventCommands/UnityEventEventCommand")]
// [EventCommandInfo("Script", "UnityEvent")]
// public class UnityEventEventCommand : EventCommand
// {
//     public UnityEvent unityEvent;

//     public override void Execute()
//     {
//         unityEvent?.Invoke();
//     }


// }


