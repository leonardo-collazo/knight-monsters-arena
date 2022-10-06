using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGateController : GateController
{
    private ParticleSystem dust;

    private void Start()
    {
        dust = transform.Find("Dust").GetComponent<ParticleSystem>();
    }

    // Lower front harrow
    public override IEnumerator LowerFrontHarrow(float Ymovement)
    {
        Vector3 frontHarrowNewPos = frontHarrow.transform.localPosition;
        frontHarrowNewPos.y += Ymovement;

        while (Vector3.Distance(frontHarrow.transform.localPosition, frontHarrowNewPos) > maxDistance)
        {
            frontHarrow.transform.localPosition = Vector3.MoveTowards(frontHarrow.transform.localPosition, frontHarrowNewPos,
                harrowLoweringSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        if (!dust.isEmitting)
        {
            dust.Play();
        }

        StopHarrowSound();
    }

    // Lower back harrow
    public override IEnumerator LowerBackHarrow(float Ymovement)
    {
        Vector3 backHarrowNewPos = backHarrow.transform.localPosition;
        backHarrowNewPos.y += Ymovement;

        while (Vector3.Distance(backHarrow.transform.localPosition, backHarrowNewPos) > maxDistance)
        {
            backHarrow.transform.localPosition = Vector3.MoveTowards(backHarrow.transform.localPosition, backHarrowNewPos,
                harrowLoweringSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        if (!dust.isEmitting)
        {
            dust.Play();
        }

        StopHarrowSound();
    }
}
