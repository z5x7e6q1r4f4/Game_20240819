﻿using Main.RXs;
using UnityEngine;
using Main.Game.FomulaSteps;

namespace Main.Game
{
    public class Fomula : GameComponent
    {
        [field: SerializeField] public RXsProperty_SerializeField<Factory> Factory { get; private set; } = new();
        [field: SerializeField] public RXsCollection_SerializeField_SubClassSelector<FomulaStep> FomulaSteps { get; private set; } = new();
        public IRXsProperty<int> Index => AwakeSelf<Fomula>().index;
        [SerializeField] private RXsProperty_SerializeField<int> index = new(-1);
        public IRXsProperty_Readonly<FomulaStep> CurrentStep => AwakeSelf<Fomula>().currentStep;
        private RXsProperty_SerializeField<FomulaStep> currentStep;
        protected override void OnGameComponentAwake()
        {
            Factory.LinkCollection(this, factory => factory.Fomulas);
            index.BeforeSet.Subscribe(e =>
            {
                var count = FomulaSteps.Count;
                if (count == 0)
                {
                    e.Modified = -1;
                    return;
                }
                e.Modified %= count;
                if (e.Modified < 0) e.Modified += FomulaSteps.Count;
            });
            index.AfterSet.Immediately().Subscribe(e =>
            {
                currentStep.Value = FomulaSteps.GetAt(e.Current, true);
            });
            CurrentStep.AfterSet.Immediately().Subscribe(e =>
            {
                if (e.Previous != null) e.Previous.ExitStep();
                if (e.Current != null) e.Current.EnterStep();
            });
        }
        public void FomulaNext() => Index.Value += 1;
        public void FomulaStop() => Index.SetValue(-1, false);
    }
}
