using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform    cible;
    private bool        _sourisDown = false;
    private Vector3     _positionSourisDepart;
    private Vector3     _positionDepart;
    public bool         cevenirSurSaPosition;

    public void OnPointerDown(PointerEventData dt)
    {
        _sourisDown =           true;
        _positionDepart =       cible.position;
        _positionSourisDepart = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData dt)
    {
        _sourisDown = false;
        if (cevenirSurSaPosition)
            cible.position = _positionDepart;
    }

    // Update is called once per frame
    void Update()
    {
        if (_sourisDown)
        {
            Vector3 positionCourante =  Input.mousePosition;
            Vector3 diff =              positionCourante - _positionSourisDepart;
            Vector3 pos =               _positionDepart + diff;
            cible.position =            pos;
        }
    }
}