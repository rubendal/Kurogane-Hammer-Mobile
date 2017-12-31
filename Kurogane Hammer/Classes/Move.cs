using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurogane_Hammer.Classes
{
    public enum MoveType
    {
        Aerial,
        Ground,
        Special,
        Throw,
        Evasion,
        Any = -1
    }

    public class Move
    {
        public MoveType moveType;
        public string name, hitboxActive, FAF, baseDamage, angle, bkb, kbg;

        public Move()
        {

        }

        public Move(MoveType moveType, string name, string hitboxActive, string FAF, string baseDamage, string angle, string bkb, string kbg)
        {
            this.moveType = moveType;
            this.name = name;
            this.hitboxActive = hitboxActive;
            this.FAF = FAF;
            this.baseDamage = baseDamage.Replace("&#215;","x");
            this.angle = angle;
            this.bkb = bkb;
            this.kbg = kbg;
        }

        public override string ToString()
        {
            return name;
        }

        public static Move Convert(MoveData data)
        {
            MoveType moveType = MoveType.Ground;

            if (data.MoveType == "aerial")
                moveType = MoveType.Aerial;
            else if (data.MoveType == "throw")
                moveType = MoveType.Throw;
            else if (data.MoveType == "special")
                moveType = MoveType.Special;
            else if (data.Name == "Forward Roll" || data.Name == "Back Roll" || data.Name == "Spotdodge" || data.Name == "Airdodge")
                moveType = MoveType.Evasion;

            switch (moveType)
            {
                case MoveType.Aerial:
                    return new AerialMove(moveType, data.Name, data.HitboxActive, data.FirstActionableFrame, data.BaseDamage, data.Angle, data.BaseKnockBackSetKnockback, data.KnockbackGrowth, data.LandingLag, data.AutoCancel);
                case MoveType.Throw:
                    return new ThrowMove(moveType, data.Name, data.HitboxActive, data.FirstActionableFrame, data.BaseDamage, data.Angle, data.BaseKnockBackSetKnockback, data.KnockbackGrowth, data.IsWeightDependent);
                default:
                    return new Move(moveType, data.Name, data.HitboxActive, data.FirstActionableFrame, data.BaseDamage, data.Angle, data.BaseKnockBackSetKnockback, data.KnockbackGrowth);
            }
        }
    }

    public class AerialMove : Move
    {
        public string landingLag, autoCancel;

        public AerialMove()
        {

        }

        public AerialMove(MoveType moveType, string name, string hitboxActive, string FAF, string baseDamage, string angle, string bkb, string kbg, string landingLag, string autoCancel) : base(moveType, name, hitboxActive, FAF, baseDamage, angle, bkb, kbg)
        {
            this.landingLag = landingLag;
            this.autoCancel = autoCancel;
        }
    }

    public class ThrowMove : Move
    {

        public bool weightDependent;

        public ThrowMove()
        {

        }

        public ThrowMove(MoveType moveType, string name, string hitboxActive, string FAF, string baseDamage, string angle, string bkb, string kbg, bool weightDependent) : base(moveType, name, hitboxActive, FAF, baseDamage, angle, bkb, kbg)
        {
            this.weightDependent = weightDependent;
        }
    }

    public class MoveData
    {
        public string Name, Owner, HitboxActive, FirstActionableFrame, BaseDamage, Angle, BaseKnockBackSetKnockback, LandingLag, AutoCancel, KnockbackGrowth, MoveType;
        public int OwnerId;
        public bool IsWeightDependent;

        public MoveData()
        {

        }
    }
}
