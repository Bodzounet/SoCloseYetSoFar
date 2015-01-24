using UnityEngine;
using System.Collections;

public class KeyBehaviour : MonoBehaviour {

  private Transform _target;

  void Start ()
  {
    _target = null;
  }

  void Update()
  {
    if (_target != null)
    {
      transform.position = new Vector2(_target.position.x - 0.5f, _target.position.y + 0.7f);
    }
  }

  void OnTriggerEnter2D(Collider2D col)
  {
    _target = col.transform;
  }
}
