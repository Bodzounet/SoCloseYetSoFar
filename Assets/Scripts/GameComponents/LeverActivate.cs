using UnityEngine;
using System.Collections;

public class LeverActivate : MonoBehaviour
{


    public  GameObject[]  affectedBlock;
    public  bool          startupState;
    private bool          _isTriggered;
    private Animator      _anim;

  void  changeState()
    {
      _isTriggered = true;
      _anim.SetBool("isActive", !_anim.GetBool("isActive"));
      for (int i = 0; i < affectedBlock.Length; i++)
      {
        affectedBlock[i].SetActive(!affectedBlock[i].activeSelf);
      }
      StartCoroutine("reActivate");
    }

    void Start()
    {
        _isTriggered = false;
        _anim = GetComponent<Animator>();
        if (startupState)
          changeState();
    }

    IEnumerator reActivate()
    {
        yield return new WaitForSeconds(0.5f);
        _isTriggered = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (_isTriggered == false && col.gameObject.tag == "Player" && Input.GetButton("Fire1"))
          changeState();
    }
}