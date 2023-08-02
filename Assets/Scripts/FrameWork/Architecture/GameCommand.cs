using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace DungeonHero
{
    /// <summary>
    /// 游戏过关结束命令
    /// </summary>
    public class GamePassCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<INumModal>().GoldScore.Value = 0;
        }
    }
    /// <summary>
    ///  玩家获取金币分数命令 
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
    /// 得到武器事件
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