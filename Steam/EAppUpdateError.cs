﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam
{
    public enum EAppUpdateError : int
    {
        k_EAppErrorNone                            =  0,
        k_EAppErrorUnspecified                     =  1,
        k_EAppErrorPaused                          =  2,
        k_EAppErrorCanceled                        =  3,
        k_EAppErrorSuspended                       =  4,
        k_EAppErrorNoSubscription                  =  5,
        k_EAppErrorNoConnection                    =  6,
        k_EAppErrorTimeout                         =  7,
        k_EAppErrorMissingKey                      =  8,
        k_EAppErrorMissingConfig                   =  9,
        k_EAppErrorDiskReadFailure                 = 10,
        k_EAppErrorDiskWriteFailure                = 11,
        k_EAppErrorCorruptContent                  = 13,
        k_EAppErrorWaitingForDisk                  = 14,
        k_EAppErrorInvalidInstallPath              = 15,
        k_EAppErrorApplicationRunning              = 16,
        k_EAppErrorDependencyFailure               = 17,
        k_EAppErrorNotInstalled                    = 18,
        k_EAppErrorUpdateRequired                  = 19,
        k_EAppErrorStillBusy                       = 20,
        k_EAppErrorNoConnectionToContentServers    = 21,
        k_EAppErrorInvalidApplicationConfiguration = 22,
        k_EAppErrorInvalidContentConfiguration     = 23,
        k_EAppErrorMissingManifest                 = 24,
        k_EAppErrorNotReleased                     = 25,
        k_EAppErrorRegionRestricted                = 26,
        k_EAppErrorCorruptDepotCache               = 27,
        k_EAppErrorMissingExecutable               = 28,
        k_EAppErrorInvalidPlatform                 = 29,
        k_EAppErrorInvalidFileSystem               = 30,
        k_EAppErrorCorruptUpdateFiles              = 31,
        k_EAppUpdateErrorDownloadCorrupt           = 32,
        k_EAppUpdateErrorDownloadDisabled          = 33,
        k_EAppUpdateErrorSharedLibraryLocked       = 34,
        k_EAppUpdateErrorPurchasePending           = 35,
        k_EAppUpdateErrorOtherSessionPlaying       = 36,
    }
}
