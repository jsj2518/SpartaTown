using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.CanInteractWithTutor = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.Instance.CanInteractWithTutor = false;
    }
}
