using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using LeroGames.Tools;
using MoreMountains.Tools;

namespace LeroGames.PrestigeHell
{
    public class ResourceLootDrop : PickableItem
    {
        [MMInspectorGroup("Overrides", true, 10)]
        public FloatVariable resourceAmount;
        public ResourceType resourceType;
        public FloatVariable collectRange;
        [SerializeField] private LeroGames.Tools.GameEvent resourceLootEvent;

        protected override void Pick(GameObject picker)
        {
            resourceLootEvent?.Raise(this, new ResourceAmount(resourceType, (int)resourceAmount.Value));
        }

        protected virtual void Update()
        {
            if (collectRange == null)
            {
                return;
            }
            if (_collider2D is CircleCollider2D cirlceCollider)
            {
                cirlceCollider.radius = collectRange.Value;
            }
            if (_collider2D is BoxCollider2D boxCollider)
            {
                boxCollider.size = new Vector2(collectRange.Value, collectRange.Value);
            }
            if (_collider2D is CapsuleCollider2D capsuleCollider)
            {
                capsuleCollider.size = new Vector2(collectRange.Value, collectRange.Value);
            }
        }

    }
}