using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGateController : GateController
{
    private ParticleSystem dust;

    private void Start()
    {
        dust = transform.Find("DustEffect").GetComponent<ParticleSystem>();
    }

    void SpreadDust()
    {
        dust.Play();
    }

    // Lower front harrow
    public override IEnumerator LowerFrontHarrow(float Ymovement)
    {
        Vector3 frontHarrowNewPos = frontHarrow.transform.localPosition;
        frontHarrowNewPos.y += Ymovement;

        while (Vector3.Distance(frontHarrow.transform.localPosition, frontHarrowNewPos) > maxError)
        {
            frontHarrow.transform.localPosition = Vector3.MoveTowards(frontHarrow.transform.localPosition, frontHarrowNewPos,
                harrowMovementSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        if (!dust.isEmitting)
        {
            SpreadDust();
        }
    }

    // Lower back harrow
    public override IEnumerator LowerBackHarrow(float Ymovement)
    {
        Vector3 backHarrowNewPos = backHarrow.transform.localPosition;
        backHarrowNewPos.y += Ymovement;

        while (Vector3.Distance(backHarrow.transform.localPosition, backHarrowNewPos) > maxError)
        {
            backHarrow.transform.localPosition = Vector3.MoveTowards(backHarrow.transform.localPosition, backHarrowNewPos,
                harrowMovementSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        if (!dust.isEmitting)
        {
            SpreadDust();
        }
    }
}
