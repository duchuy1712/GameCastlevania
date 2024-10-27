using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public Transform Pos1,Pos2;
    public GameObject CamV1, CamV2;
    private Transform PosPoint;
    private GameObject CamV;
    public float TransitionDuration = 1f;
    public bool Isvertical;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 Direction = collision.transform.position - transform.position;
            if (Isvertical)
                SetVerticalDirection(Direction);
            else
                SetHorizontalDirection(Direction);
            StartCoroutine(Move(collision.gameObject));
        }
    }
    private void SetVerticalDirection(Vector3 _Direction)
    {
        if (_Direction.y < 0)
        {
            CamV = CamV1;
            PosPoint = Pos1;
        }
        else
        {
            CamV = CamV2;
            PosPoint = Pos2;
        }
    }
    private void SetHorizontalDirection(Vector3 _Direction)
    {
        if (_Direction.x > 0)
        {
            CamV = CamV1;
            PosPoint = Pos1;
        }
        else
        {
            CamV = CamV2;
            PosPoint = Pos2;
        }
    }
    IEnumerator Move(GameObject Player)
    {
        Time.timeScale = 0;
        RoomController.Instance.TurnOffPlayerInput();
        RoomController.Instance.EnableTransition(CamV);
        if(!Isvertical)
            Player.transform.DOMoveX(PosPoint.position.x, TransitionDuration).SetUpdate(true);
        else
            Player.transform.DOMoveY(PosPoint.position.y, TransitionDuration).SetUpdate(true);
        yield return new WaitForSecondsRealtime(TransitionDuration);
        Player.transform.DOKill();
        Time.timeScale = 1;
        RoomController.Instance.ActiveObjectRoom();
        RoomController.Instance.TurnOnPlayerInput();
    }
}
