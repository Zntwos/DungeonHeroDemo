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
        BindableProperty<GameObject> weaponHeld { get; set; }
    }
    public class Modal : AbstractModel , INumModal, ISpriteModal, IWeaponModal
    {
        BindableProperty<int> INumModal.GoldScore { get; set; } = new BindableProperty<int>(0);
        BindableProperty<Sprite> ISpriteModal.weaponIcon { get; set; } = new BindableProperty<Sprite>(null);
        BindableProperty<GameObject> IWeaponModal.weaponHeld { get; set; } = new BindableProperty<GameObject>(null);
        protected override void OnInit() 
        {
            
        }
    }
}
