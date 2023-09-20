using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformationTooltipTrigger : TooltipTrigger
{
    private CharacterInformationSO characterInformation;

    public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GetCharacterInformation();
        header = characterInformation.characterName;
        // content = characterInformation.characterDescription;
        base.OnPointerEnter(eventData);
    }

    private void GetCharacterInformation()
    {
        characterInformation = GetComponent<ICharacterInformation>()?.characterInformation;
    }
}
