using UnityEngine;
using System.Collections;

public class LeverActivate : MonoBehaviour
{

  public GameObject[] affectedBlock;
  private bool        _isTriggered;

  void Start()
  {
    _isTriggered = false;
  }

  IEnumerator reActivate()
  {
    yield return new WaitForSeconds(0.5f);
    _isTriggered = false;
  }

  void OnTriggerStay2D(Collider2D col)  {
    if (_isTriggered == false && col.gameObject.tag == "Player" && Input.GetButton("Fire1"))
    {
      _isTriggered = true;
      for (int i = 0; i < affectedBlock.Length; i++)
      {
        affectedBlock[i].SetActive(!affectedBlock[i].activeSelf);
      }
      StartCoroutine("reActivate");
    }
  }
}