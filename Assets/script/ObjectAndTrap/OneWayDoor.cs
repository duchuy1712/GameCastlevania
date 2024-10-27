using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OneWayDoor : MonoBehaviour
{
    public GameObject CamV;
    public BoxCollider2D Wall;
    public Transform PosPoint;
    public float TransitionDuration = 1f;
    public GameObject DoorSr;
    public float DoorAnimDuration = 2f;
    [SerializeField] private Animator anim;
    [Header("SafeRoom")]
    public bool SafeRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Move(collision.gameObject));
        }
    }
    IEnumerator Move(GameObject player)
    {
        Time.timeScale = 0;
        RoomController.Instance.TurnOffPlayerInput();
        Wall.isTrigger = true;
        anim.Play("Open",0);
        yield return new WaitForSecondsRealtime(DoorAnimDuration);
        RoomController.Instance.EnableTransition(CamV);
        player.transform.DOMoveX(PosPoint.position.x, TransitionDuration).SetUpdate(true);
        yield return new WaitForSecondsRealtime(TransitionDuration);
        Wall.isTrigger = false;
        anim.Play("Close", 0);
        yield return new WaitForSecondsRealtime(DoorAnimDuration);
        RoomController.Instance.ActiveObjectRoom();
        RoomController.Instance.TurnOnPlayerInput();
        Time.timeScale = 1;
        if (SafeRoom)
             player.GetComponent<DataControl>().CheckPointPos = PosPoint.position;
    }
}
