
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;

    //GFX
    [SerializeField]
    RectTransform boxVisual;

    //logical
    Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;

    void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    // Update is called once per frame
    void Update()
    {
        //when clicked
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
        }
        //when dragging
        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        //when release click
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }


    }
    void DrawVisual()
    {
        //creating UI graphic of selection box
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;
        Vector2 boxCenter=(boxStart + boxEnd)/2;
        boxVisual.position = boxCenter;
        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x),Mathf.Abs(boxStart.y-boxEnd.y));

        boxVisual.sizeDelta = boxSize;


    }
    void DrawSelection()
    {
        //UI part of unit selection, creating data to draw select box
        //do X calc
        if(Input.mousePosition.x < startPosition.x)
        {
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }
        //do Y calc
        if (Input.mousePosition.y < startPosition.y)
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }
    void SelectUnits()
    {
        foreach (var unit in UnitSelections.Instance.unitList)
        {
            //if unit 
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }
}
