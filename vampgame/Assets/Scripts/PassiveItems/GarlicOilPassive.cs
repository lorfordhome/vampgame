using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicOilPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.currentMight *= 1 + passiveData.Multiplier / 100f; //eg. if multiplier is set to 50. the total multiplier will be calculated as 1.5.
    }
}
