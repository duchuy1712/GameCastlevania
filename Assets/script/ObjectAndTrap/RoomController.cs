using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class RoomController : Singleton<RoomController>
{
    public CinemachineBrain Brain;
    public PlayerInput Input;
    [SerializeField] private GameObject LevelObject;
    [System.Serializable]
    public struct RoomList
    {
        public GameObject VCam;
        public GameObject RoomObjectGroub;
    }
    [System.Serializable]
    public struct SaveRoomList
    {
        public GameObject VCam;
        public GameObject RoomObjectGroub;
        public Transform RespawnPoint;
    }
    public RoomList[] roomlist;
    public SaveRoomList[] saveRoomLists;
    public void EnableTransition(GameObject vCam)
    {
        Brain.m_DefaultBlend.m_Time = 1;
        if (vCam.activeInHierarchy)
            return;
        for(int i =0; i < roomlist.Length;i++)
        {
            if (roomlist[i].VCam.activeSelf)
            {
                roomlist[i].VCam.SetActive(false);
                roomlist[i].RoomObjectGroub.SetActive(false);
            }
        }
        vCam.SetActive(true);
    }
    public void ReturnToSafeRoom(Vector3 _RespawnPoint)
    {
        Brain.m_DefaultBlend.m_Time = 0;
        for (int i = 0; i < roomlist.Length; i++)
        {
            if (roomlist[i].VCam.activeSelf)
            {
                roomlist[i].VCam.SetActive(false);
                roomlist[i].RoomObjectGroub.SetActive(false);
            }
        }
        for (int i =0; i < saveRoomLists.Length;i++)
        {
                if (_RespawnPoint == saveRoomLists[i].RespawnPoint.position)
                {
                    saveRoomLists[i].VCam.SetActive(true);
                }
        }
    }
        //yield return new WaitForSecondsRealtime(Brain.m_DefaultBlend.BlendTime);
    public void ActiveObjectRoom()
    {
        for (int i = 0; i < roomlist.Length; i++)
        {
            if (roomlist[i].VCam.activeSelf)
            {
                roomlist[i].RoomObjectGroub.SetActive(true);
            }
        }
    }
    public void TurnOffPlayerInput()
    {
        Input.actions.Disable();
    }
    public void TurnOnPlayerInput()
    {
        Input.actions.Enable();
    }
    public void ResetLevel(Vector3 _RespawnPoint)
    {
        for (int i = 0; i < LevelObject.transform.childCount; i++)
        {
            LevelObject.transform.GetChild(i).gameObject.SetActive(true);
        }
        ReturnToSafeRoom(_RespawnPoint);
    }
}
