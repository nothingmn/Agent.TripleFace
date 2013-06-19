using System;
using System.Collections;
using Microsoft.SPOT;

namespace Agent.TripleFace.Faces
{
    public interface IFace
    {
        void Render(Bitmap screen, bool military, ArrayList forecast);
    }
}
