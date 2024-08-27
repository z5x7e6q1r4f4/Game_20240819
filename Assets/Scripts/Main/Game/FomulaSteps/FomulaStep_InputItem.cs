using Main.RXs;
using UnityEngine;

namespace Main.Game.FomulaSteps
{
    public class FomulaStep_InputItem : FomulaStep_ComponentBase<InventoryOutput>
    {
        private readonly DisposableList disposableList = new();
        [field: SerializeField] public RXsCollection_SerializeField<Item> Items { get; private set; } = new();
        public override void EnterStep() => CheckOutput();
        public override void ExitStep() => disposableList.Dispose();
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
            if (disposableList.Count != 0) return;
            disposableList.Add(BodyComponents.AfterAdd.Subscribe(CheckOutput));
            disposableList.Add(BodyComponents.AfterAdd.Immediately().Subscribe(e =>
            {
                disposableList.Add(e.Item.AfterAdd.Subscribe(CheckOutput));
                //disposableList.Add(e.Item.Capacity.AfterSet.Subscribe(CheckOutput));
            }));
        }
    }
}
