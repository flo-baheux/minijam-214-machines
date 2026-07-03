using System;
using DG.Tweening;
using UnityEngine;

public class MusicController : MonoBehaviour {
  [SerializeField] private MusicNote[] musicNotes;

  private void Awake() {
    foreach (var musicNote in musicNotes) 
      musicNote.Reset();
  }

  private void Update() { 
    
  }
}
