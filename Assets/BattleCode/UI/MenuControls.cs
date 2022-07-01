using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MenuControls : MonoBehaviour
{
    public TacticsControls tc;

    private void Awake()
    {
        tc = new TacticsControls();
    }

    private void OnEnable()
    {
        tc.RegularMenu.Enable();
        tc.RegularMenu.ChangeSelection.started += ChangeSelectionMethod;
        tc.RegularMenu.Select.started += SelectedMethod;
        tc.RegularMenu.Cancel.started += CancelMethod;
    }

    private void OnDisable()
    {
        tc.RegularMenu.ChangeSelection.started -= ChangeSelectionMethod;
        tc.RegularMenu.Select.started -= SelectedMethod;
        tc.RegularMenu.Cancel.started -= CancelMethod;
        tc.RegularMenu.Disable();
    }

    #region Selected Event
    public delegate void SelectedDelegate();

    public event SelectedDelegate SelectedEvent;

    private void SelectedMethod(InputAction.CallbackContext obj)
    {
        if (SelectedEvent != null)
        {
            SelectedEvent();
        }
    }
    #endregion

    #region Canceled Event
    public delegate void CanceledDelegate();

    public event CanceledDelegate CanceledEvent;

    private void CancelMethod(InputAction.CallbackContext obj)
    {
        if (CanceledEvent != null)
        {
            CanceledEvent();
        }
    }
    #endregion

    #region Change Selection Event
    public delegate void ChangeSelectionDelegate(float direction);

    public event ChangeSelectionDelegate ChangeSelectionEvent;

    private void ChangeSelectionMethod(InputAction.CallbackContext obj)
    {
        if(ChangeSelectionEvent != null)
        {
            ChangeSelectionEvent(obj.ReadValue<float>());
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
