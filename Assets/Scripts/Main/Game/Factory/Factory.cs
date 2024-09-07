using UnityEngine;
namespace Main.Game
{
    public class Factory : BodyPartComponent
    {
        [field: SerializeField] public CollectionSerializeField<Fomula> Fomulas { get; private set; } = new();
        protected override void OnGameComponentAwake()
        {
            Fomulas.LinkItem(this, fomula => fomula.Factory);
            Fomulas.AfterAdd.Immediately().Subscribe(e => e.Item?.FomulaNext());
            Fomulas.AfterRemove.Subscribe(e => e.Item?.FomulaStop());
        }
    }
}
