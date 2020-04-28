namespace Validatum
{
    /// <summary>
    /// Encapsulates options used for validation.
    /// </summary>
    public class ValidationOptions
    {
        private bool _locked = false;
        private SetOnce<bool> _stopWhenInvalid = new SetOnce<bool>(false);
        private SetOnce<bool> _addBrokenRuleForException = new SetOnce<bool>(true);
        private SetOnce<bool> _throwWhenInvalid = new SetOnce<bool>(false);

        /// <summary>
        /// Indicates to stop validation when the first invalid rule occurs.
        /// </summary>
        public bool StopWhenInvalid 
        { 
            get => _stopWhenInvalid.Value;
            set
            {
                if (!_locked)
                {
                    _stopWhenInvalid.Value = value;
                }
            } 
        }

        /// <summary>
        /// Indicates to add exceptions as broken rules when they occur.
        /// </summary>
        public bool AddBrokenRuleForException
        {
            get => _addBrokenRuleForException.Value;
            set
            {
                if (!_locked)
                {
                    _addBrokenRuleForException.Value = value;
                }
            }
        }

        /// <summary>
        /// Indicates to throw <see cref="ValidationException"/> when validation fails.
        /// </summary>
        public bool ThrowWhenInvalid
        {
            get => _throwWhenInvalid.Value;
            set
            {
                if (!_locked)
                {
                    _throwWhenInvalid.Value = value;
                }
            }
        }

        internal void Lock()
        {
            _locked = true;
        }

        private struct SetOnce<T>
        {
            public SetOnce(T defaultValue)
            {
                _value = defaultValue;
                _isSet = false;
            }

            private bool _isSet;
            private T _value;

            public T Value
            {
                get => _value;
                set
                {
                    if (!_isSet)
                    {
                        _value = value;
                        _isSet = true;
                    }
                }
            }
        }
    }
}