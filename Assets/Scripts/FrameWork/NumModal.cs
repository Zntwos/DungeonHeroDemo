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
    public class NumModal : AbstractModel , INumModal
    {
        BindableProperty<int> INumModal.GoldScore { get; set; } = new BindableProperty<int>(0);

        protected override void OnInit() 
        {
            
        }
    }
}
