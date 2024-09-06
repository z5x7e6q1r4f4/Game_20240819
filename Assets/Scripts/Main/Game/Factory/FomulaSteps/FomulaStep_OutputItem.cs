using Main.RXs;
using UnityEngine;

namespace Main.Game.FomulaSteps
{
    public class FomulaStep_OutputItem : FomulaStep_ComponentBase<InventoryOutput>
    {
        [field: SerializeField] public ObservableCollection_SerializeField<Item> Items { get; private set; } = new();
        protected override void OnGameComponentAwake()
        {
            base.OnGameComponentAwake();
            OnEnterStep.Subscribe(CheckOutput);
        }
        private void CheckOutput()
        {
            foreach (var inventory in BodyPartComponents)
            {
                if (inventory.Test().AddRange(Items).Result())
                {
                    inventory.AddRange(Items);
                    Fomula.FomulaNext();
                    return;
                }
            }
            RXsSubscription.FromList(
                BodyComponents.AfterAdd.Subscribe(CheckOutput),
                BodyComponents.AfterAdd.Immediately().Subscribe(e =>
                {
                    e.Item.AfterRemove.Subscribe(CheckOutput).Until(OnExitStep);
                    e.Item.Capacity.AfterSet.Subscribe(CheckOutput).Until(OnExitStep);
                })).
            Until(OnExitStep);
        }
    }
}
