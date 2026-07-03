using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Laywelin {

  public class DependencyManager : MonoBehaviour {
    public static DependencyManager Instance { get; private set; }

    [SerializeField] private InputHandler _inputHandler;
    public InputHandler InputHandler => _inputHandler;
  }
}