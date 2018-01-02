using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {
    private Transform _cameraTarget;

    private float x = 0.0f; // Rotation x
    private float y = 0.0f; // Rotation y

    private int mouseXSpeedMod = 5; // Vitesse de caméra pour l'axe X
    private int mouseYSpeedMod = 5; // Vitesse de caméra pour l'axe Y

    public float MaxViewDistance = 8f; // Distance maximale de la caméra par rapport au joueur
    public float MinViewDistance = 1f; // Distance minimale de la caméra par rapport au joueur

    private int ZoomRate = 20;  // Vitesse du zoom/dezoom
    private int lerpRate = 5;   // Pour des vérifications de valeur X de rotation

    private float distance = 3f;        //
    private float desireDistance;       //
    private float correctedDistance;    //
    private float currentDistance;      //

    public float cameraTargetHeight = 22.0F; // Hauteur de la caméra par rapport au joueur

    private bool FPSMode = false; // Checks if first person mode is on
    private float curDist = 0; // Stores cameras distance from player

    void Awake()
    {
        _cameraTarget = transform.parent;
        Vector3 Angles = transform.eulerAngles;
        x = Angles.x;
        y = Angles.y;
        currentDistance = distance;
        desireDistance = distance;
        correctedDistance = distance;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {//0 mouse btn izq, 1 mouse btn der
            x += Input.GetAxis("Mouse X") * mouseXSpeedMod;
            y += Input.GetAxis("Mouse Y") * mouseYSpeedMod;
        }
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            float targetRotantionAngle = _cameraTarget.eulerAngles.y;
            float cameraRotationAngle = transform.eulerAngles.y;
            x = Mathf.LerpAngle(cameraRotationAngle, targetRotantionAngle, lerpRate * Time.deltaTime);
        }

        y = ClampAngle(y, -15, 25);
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        desireDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomRate * Mathf.Abs(desireDistance);
        desireDistance = Mathf.Clamp(desireDistance, MinViewDistance, MaxViewDistance);
        correctedDistance = desireDistance;

        Vector3 position = _cameraTarget.position - (rotation * Vector3.forward * desireDistance);

        RaycastHit collisionHit;
        Vector3 cameraTargetPosition = new Vector3(_cameraTarget.position.x, _cameraTarget.position.y + cameraTargetHeight, _cameraTarget.position.z);

        bool isCorrected = false;
        if (Physics.Linecast(cameraTargetPosition, position, out collisionHit))
        {
            position = collisionHit.point;
            correctedDistance = Vector3.Distance(cameraTargetPosition, position);
            isCorrected = true;
        }

        // -----------------------------------------------
        // TODO: Cette ligne cause des problèmes de caméra
        // -----------------------------------------------
        //?
        // condition ? first_expresion : second_expresion;
        // (input > 0) ? isPositive : isNegative;
        // TODO: Has to be done
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * ZoomRate) : correctedDistance;

        position = _cameraTarget.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -cameraTargetHeight, 0));

        transform.rotation = rotation;
        transform.position = position;

        // CameraTarget.rotation = rotation;

        float cameraX = transform.rotation.x;
        // Checks if right mouse button is pushed
        if (Input.GetMouseButton(1))
        {
            // Sets CHARACTERS x rotation to match cameras x rotation
            _cameraTarget.eulerAngles = new Vector3(cameraX, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        // Checks if middle mouse button is pushed down
        if (Input.GetMouseButtonDown(2))
        {
            print("MOUSE 3, oui");
            // If middle mouse button is pressed 1st time set click to true and camera in front of player and save cameras position before mmb.
            // If mmb is pressed again set camera back to it's position before we clicked mmb 1st time and set click to false
            if (FPSMode == false)
            {
                FPSMode = true;
                curDist = distance;
                distance = distance - distance - 1;
            }
            else
            {
                distance = curDist;
                FPSMode = false;
            }
        }

    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
