using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace DungeonHero
{
    public interface INumModal : IModel
    {
        BindableProperty<int> GoldScore { get; set; }
    }
    public interface ISpriteModal : IModel
    {
        BindableProperty<Sprite> weaponIcon { get; set; }
    }
    public interface IWeaponModal : IModel
    {
        BindableProperty<List<GameObject>> weaponInventory { get; set; }
    }
    public class Modal : AbstractModel , INumModal, ISpriteModal, IWeaponModal
    {
        BindableProperty<int> INumModal.GoldScore { get; set; } = new BindableProperty<int>(0);
        BindableProperty<Sprite> ISpriteModal.weaponIcon { get; set; } = new BindableProperty<Sprite>(null);
        BindableProperty<List<GameObject>> IWeaponModal.weaponInventory { get; set; } = new BindableProperty<List<GameObject>>(new List<GameObject>(3) { null, null, null });


        protected override void OnInit() 
        {
            
        }
    }
}
