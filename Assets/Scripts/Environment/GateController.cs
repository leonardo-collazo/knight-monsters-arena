using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] protected float harrowMovementSpeed;

    [SerializeField] protected GameObject backHarrow;
    [SerializeField] protected GameObject frontHarrow;

    protected float maxError = 0.001f;

    #region Lower harrows methods

    // Lower the harrows
    public void LowerHarrows()
    {
        const float Ymovement = -0.3850229f;

        StartCoroutine(LowerFrontHarrow(Ymovement));
        StartCoroutine(LowerBackHarrow(Ymovement));
    }

    // Lower front harrow
    public virtual IEnumerator LowerFrontHarrow(float Ymovement)
    {
        Vector3 frontHarrowNewPos = frontHarrow.transform.localPosition;
        frontHarrowNewPos.y += Ymovement;

        while (Vector3.Distance(frontHarrow.transform.localPosition, frontHarrowNewPos) > maxError)
        {
            frontHarrow.transform.localPosition = Vector3.MoveTowards(frontHarrow.transform.localPosition, frontHarrowNewPos,
                harrowMovementSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }

    // Lower back harrow
    public virtual IEnumerator LowerBackHarrow(float Ymovement)
    {
        Vector3 backHarrowNewPos = backHarrow.transform.localPosition;
        backHarrowNewPos.y += Ymovement;

        while (Vector3.Distance(backHarrow.transform.localPosition, backHarrowNewPos) > maxError)
        {
            backHarrow.transform.localPosition = Vector3.MoveTowards(backHarrow.transform.localPosition, backHarrowNewPos,
                harrowMovementSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }

    #endregion

    #region Raise harrows methods

    // Raise the harrows
    public void RaiseHarrows()
    {
        const float Ymovement = 0.3850229f;

        StartCoroutine(RaiseFrontHarrow(Ymovement));
        StartCoroutine(RaiseBackHarrow(Ymovement));
    }

    // Raise front harrow
    IEnumerator RaiseFrontHarrow(float Ymovement)
    {
        Vector3 frontHarrowNewPos = frontHarrow.transform.localPosition;
        frontHarrowNewPos.y += Ymovement;

        while (Vector3.Distance(frontHarrow.transform.localPosition, frontHarrowNewPos) > maxError)
        {
            frontHarrow.transform.localPosition = Vector3.MoveTowards(frontHarrow.transform.localPosition, frontHarrowNewPos,
                harrowMovementSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }

    // Raise back harrow
    IEnumerator RaiseBackHarrow(float Ymovement)
    {
        Vector3 backHarrowNewPos = backHarrow.transform.localPosition;
        backHarrowNewPos.y += Ymovement;

        while (Vector3.Distance(backHarrow.transform.localPosition, backHarrowNewPos) > maxError)
        {
            backHarrow.transform.localPosition = Vector3.MoveTowards(backHarrow.transform.localPosition, backHarrowNewPos,
                harrowMovementSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }

    #endregion
}
