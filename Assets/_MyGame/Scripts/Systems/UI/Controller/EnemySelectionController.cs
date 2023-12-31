using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectionController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private CustomEventSystem.GameEvent enemiesSelectedFeedbackEvent;

    public void OnEnemySelected(Component sender, object data)
    {
        if (data is List<CharacterInformationSO> characterInformations)
        {
            try
            {
                EnemyManager.Instance.SetSpawnableEnemies(characterInformations);
                enemiesSelectedFeedbackEvent.Raise(this, "Enemies selected");
            }
            catch (System.Exception e)
            {
                enemiesSelectedFeedbackEvent.Raise(this, e.Message);
            }
        }
    }
}
