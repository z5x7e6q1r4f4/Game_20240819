using Main.RXs;
using UnityEngine;

namespace Main.Game.FomulaSteps
{
    public class FomulaStep_InputItem : FomulaStep_ComponentBase<InventoryInput>
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
                if (inventory.Test().RemoveRange(Items).Result())
                {
                    inventory.RemoveRange(Items);
                    Fomula.FomulaNext();
                    return;
                }
            }
            Disposable.Create(
                BodyComponents.AfterAdd.Subscribe(CheckOutput),
                BodyComponents.AfterAdd.Immediately().Subscribe(e => e.Item.AfterAdd.Subscribe(CheckOutput).Until(OnExitStep))).
                Until(OnExitStep);
        }
    }
}
