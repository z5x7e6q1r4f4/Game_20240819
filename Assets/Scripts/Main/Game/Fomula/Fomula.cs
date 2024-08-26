using Main.RXs;
using UnityEngine;

namespace Main.Game
{
    public class Fomula : GameComponent
    {
        [field: SerializeField] public RXsCollection_SubClassSelector<FomulaStep> FomulaSteps { get; private set; } = new();
        protected override void OnGameComponentAwake()
        {
            FomulaSteps.AfterAdd.Immediately().Subscribe(e => e.Item.Fomula = this);
        }
    }
}
