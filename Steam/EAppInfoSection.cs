using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EAppInfoSection : uint
    {
        k_EAppInfoSectionUnknown = 0,
        k_EAppInfoSectionAll,
        k_EAppInfoSectionCommon,
        k_EAppInfoSectionExtended,
        k_EAppInfoSectionConfig,
        k_EAppInfoSectionStats,
        k_EAppInfoSectionInstall,
        k_EAppInfoSectionDepots,
        k_EAppInfoSectionVac,
        k_EAppInfoSectionDrm,
        k_EAppInfoSectionUfs,
        k_EAppInfoSectionOgg,
        k_EAppInfoSectionItems,
        k_EAppInfoSectionPolicies,
        k_EAppInfoSectionSysreqs,
        k_EAppInfoSectionCommunity
    }
}
