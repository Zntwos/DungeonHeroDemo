using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace DungeonHero
{
    /// <summary>
    /// ��Ϸ���ؽ�������
    /// </summary>
    public class GamePassCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<INumModal>().GoldScore.Value = 0;
        }
    }
    /// <summary>
    ///  ��һ�ȡ��ҷ������� 
    /// </summary>
    public class GoldScoreCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<INumModal>().GoldScore.Value++;
            this.SendEvent<GoldScoreAddEvent>();
        }
    }
    /// <summary>
    /// �õ������¼�
    /// </summary>
    public class GetWeaponCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<GetWeaponEvent>();
        }
    }
    public class loseWeaponCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<LoseWeaponEvent>();
        }
    }
    public class WeaponSwitchCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<WeaponSwitchEvent>();
        }
    }
    public class WeaponFire : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<WeaponFireEvent>();
        }
    }
}