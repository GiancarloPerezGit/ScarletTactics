using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileControls : MonoBehaviour
{
    public TacticsControls tc;

    private void Awake()
    {
        tc = new TacticsControls();
    }

    private void OnEnable()
    {
        tc.TileSelection.Enable();
        tc.TileSelection.MoveCursor.started += ChangeTile;
        tc.TileSelection.ChangeHeight.started += ChangeElevation;
        tc.TileSelection.Select.started += Select;
        tc.TileSelection.Cancel.started += Cancel;
    }

    private void OnDisable()
    {
        tc.TileSelection.MoveCursor.started -= ChangeTile;
        tc.TileSelection.Disable();
    }

    public delegate void DirectionDelegate();

    public event DirectionDelegate UpEvent;
    public event DirectionDelegate RightEvent;
    public event DirectionDelegate DownEvent;
    public event DirectionDelegate LeftEvent;

    public void ChangeTile(InputAction.CallbackContext obj)
    {
        
        Vector2 direction = obj.ReadValue<Vector2>();
        if(direction == Vector2.up)
        {
            if (UpEvent != null)
            {
                UpEvent();
            }
        }
        else if(direction == Vector2.right)
        {
            if (RightEvent != null)
            {
                RightEvent();
            }
        }
        else if(direction == Vector2.down)
        {
            if (DownEvent != null)
            {
                DownEvent();
            }
        }
        else if(direction==Vector2.left)
        {
            if (LeftEvent != null)
            {
                LeftEvent();
            }
        }
    }

    public delegate void ElevationDelegate();

    public event DirectionDelegate AscendEvent;
    public event DirectionDelegate DescendEvent;
    public void ChangeElevation(InputAction.CallbackContext obj)
    {
        if(obj.ReadValue<float>() > 0)
        {
            if(AscendEvent != null)
            {
                AscendEvent();
            }
        }
        else if(obj.ReadValue<float>() < 0)
        {
            if(DescendEvent != null)
            {
                DescendEvent();
            }
        }
    }

    public delegate void SelectDelegate();

    public event SelectDelegate SelectEvent;

    public void Select(InputAction.CallbackContext obj)
    {
        if(SelectEvent != null)
        {
            SelectEvent();
        }
    }

    public delegate void CancelDelegate();

    public event CancelDelegate CancelEvent;

    public void Cancel(InputAction.CallbackContext obj)
    {
        if(CancelEvent != null)
        {
            CancelEvent();
        }
    }
}
