using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionSchedule : MonoBehaviour
{
    IAction currentAction;

    public void StartAction(IAction action)
    {
        if (currentAction == action) return;

        if(currentAction != null)
        {
            currentAction.Cancel();
        }

        currentAction = action;
    }

    public void CancelCurrentAction()
    {
        StartAction(null);
    }
}
