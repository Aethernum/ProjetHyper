using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSensor : MonoBehaviour {

  public float speedThreshold = 0.1f;

  public delegate void SpeedReachedHandler(float speed);
  public event SpeedReachedHandler OnSpeedReached;

  private Rigidbody rb;

  void Start() {
    rb = GetComponent<Rigidbody>();
  }
  
  void Update() {

    float speed = rb.velocity.magnitude;

    if(speed >= speedThreshold && OnSpeedReached != null) {
      // Evènement déclenché si le seuil est dépassé
      OnSpeedReached(speed); 
    }

  }

}
