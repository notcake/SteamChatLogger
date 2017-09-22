using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EOverlayToStoreFlag : uint
    {
        k_EOverlayToStoreFlag_None             = 0,
        k_EOverlayToStoreFlag_AddToCart        = 1,
        k_EOverlayToStoreFlag_AddToCartAndShow = 2
    }
}
