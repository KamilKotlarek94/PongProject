using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCamController : MonoBehaviour
{
    public int CameraMode; // 1 - Menu is visible, 2 - Game started
    public float speedFactor = 0.1f;
    public float zoomFactor = 1.0f;
    public Transform currentMount;
    public Transform Table;
    public Camera cameraComp;

    private Vector3 lastPosition;
    private float[] t;
    private float[,] CameraRotationOffsets;
    private Vector3[] CameraPositionOffsets;

    void Start()
    {
        t = new float[2] { 0.01f, 0.07f };
        //CameraRotationOffsets = new float[2,3] { { 5, -90, 0 }, { 90, -90, 0.7f } };
        CameraPositionOffsets = new Vector3[2] { new Vector3(85, 15, 0), new Vector3(0, 55, 0) };
        lastPosition = transform.position;
        CameraMode = 1;
    }

    void Update()
    {
        ControlTheCamera(CameraMode, CameraPositionOffsets[CameraMode-1], t[CameraMode - 1]);
    }

    private void ControlTheCamera(int Mode, Vector3 PositionOffset, float t)
    {
        if(Mode == 1)
            cameraComp.orthographic = false;
        else if(Mode == 2)
        {
            if (transform.position.y > 55)
                cameraComp.orthographic = true;
            cameraComp.orthographicSize = 9.84f;
        }
        transform.position = Vector3.Lerp(transform.position, Table.position + PositionOffset, t);
        if(Mode == 1)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Table.rotation.x + 5, Table.rotation.y - 90, Table.rotation.z), 0.01f);
        if(Mode == 2)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Table.rotation.x + 90, Table.rotation.y - 90, Table.rotation.z + 0.7f), 0.07f);
        var velocity = Vector3.Magnitude(transform.position - lastPosition);
        cameraComp.fieldOfView = 60 + velocity * zoomFactor;
        lastPosition = transform.position;

    }
}
