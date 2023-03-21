using System;
using System.Collections.Generic;
using DTO;
namespace GeneralImplements
{
    public interface IHittable
    {
        void Hit(HitData data);
    }
}