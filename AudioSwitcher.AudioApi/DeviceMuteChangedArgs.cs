﻿namespace AudioSwitcher.AudioApi
{
    public sealed class DeviceMuteChangedArgs : DeviceChangedArgs
    {

        public bool IsMuted
        {
            get;
            private set;
        }

        public DeviceMuteChangedArgs(IDevice device, bool isMuted) 
            : base(device, DeviceChangedType.MuteChanged)
        {
            IsMuted = isMuted;
        }
    }
}