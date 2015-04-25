using System;
using System.Collections.Generic;

namespace AsyncDisablingScopeSample
{
    public class DisablingScope : IDisposable
    {
        private readonly IEnumerable<IDisableable> _scopeObjects;

        // Instatiate and Disable all Commands
        public DisablingScope(IEnumerable<IDisableable> scopeObjects)
        {
            _scopeObjects = scopeObjects;

            foreach (var scopeObject in _scopeObjects)
            {
                scopeObject.Disable();
            }
        }

        // Enable Commands when disposing
        public void Dispose()
        {
            foreach (var scopeObject in _scopeObjects)
            {
                scopeObject.Enable();
            }
        }
    }
}
