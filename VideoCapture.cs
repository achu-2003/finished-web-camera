using System;

namespace webcamproject
{
    internal class VideoCapture : IDisposable
    {
        public bool IsOpened { get; internal set; }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}