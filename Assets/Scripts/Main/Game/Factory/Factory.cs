using Main.RXs;
using UnityEngine;
namespace Main.Game
{
    public class Factory : BodyPartComponent
    {
        [field: SerializeField] public ObservableCollection_SerializeField<Fomula> Fomulas { get; private set; } = new();
        protected override void OnGameComponentAwake()
        {
            Fomulas.LinkItem(this, fomula => fomula.Factory);
            Fomulas.AfterAdd.Immediately().Subscribe(e => e.Item?.FomulaNext());
            Fomulas.AfterRemove.Subscribe(e => e.Item?.FomulaStop());
        }
    }
}
