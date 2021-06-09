using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FloodControl
{
    class RotatingPiece: GamePiece
    {
        public bool clockwise;
        public static float rotationRate = (MathHelper.PiOver2 / 20);
        private float rotationAmount = 0;
        public int rotationTicksRemaining = 20;

        public float RotationAmount
        {
            get
            {
                if (clockwise)
                    return rotationAmount;
                else
                    return (MathHelper.Pi * 2) - rotationAmount;
            }
        }

        public RotatingPiece(string pieceType, bool clockwise): base(pieceType)
        {
            this.clockwise = clockwise;
        }

        public void UpdatePiece()
        {
            rotationAmount += rotationRate;
            rotationTicksRemaining = (int)MathHelper.Max(0, rotationTicksRemaining - 1);
        }
    }
}
