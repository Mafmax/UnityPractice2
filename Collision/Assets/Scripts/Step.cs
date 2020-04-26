using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step
{
    public WayCell CurrentCell { get; set; }

    private Step PreviewsStep { get; set; }

    private float StepDistance { get; set; }

    public Step(Step previewsStep, WayCell currentCell /*,float stepDistance*/)
    {
        CurrentCell = currentCell;
        PreviewsStep = previewsStep;
        //StepDistance = stepDistance;
    }

   
}
