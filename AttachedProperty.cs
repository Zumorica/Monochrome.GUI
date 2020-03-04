using System;
using Monochrome.GUI.Controls;

namespace Monochrome.GUI
{
    /// <param name="owner">Control on which the property was changed.</param>
    /// <param name="eventArgs"></param>
    public delegate void AttachedPropertyChangedCallback(Control owner, AttachedPropertyChangedEventArgs eventArgs);

    /// <summary>
    ///     An attached property is a property that can be assigned to any control,
    ///     without having to modify the base <see cref="Control" /> class to add it.
    ///     This is useful for storing data for specific controls like <see cref="LayoutContainer" />
    /// </summary>
    public sealed class AttachedProperty
    {
        /// <summary>
        ///     The name of the property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The type that defines the attached property.
        /// </summary>
        public Type OwningType { get; }

        /// <summary>
        ///     The type of the value stored in the property.
        /// </summary>
        public Type PropertyType { get; }

        /// <summary>
        ///     The default value of the property.
        ///     This is returned if no value is set and <see cref="Control.GetValue"/> is called.
        /// </summary>
        public object DefaultValue { get; }

        /// <summary>
        ///     An optional validation function.
        ///     If the value to <see cref="Control.SetValue"/> fails this check, an exception will be thrown.
        /// </summary>
        public Func<object, bool> Validate { get; }

        /// <summary>
        ///     A callback to run whenever this property changes on a control.
        /// </summary>
        public AttachedPropertyChangedCallback Changed { get; }

        private AttachedProperty(string name, Type owningType, Type propertyType,
            object defaultValue = null,
            Func<object, bool> validate = null,
            AttachedPropertyChangedCallback changed = null)
        {
            Name = name;
            OwningType = owningType;
            PropertyType = propertyType;
            DefaultValue = defaultValue;
            Validate = validate;
            Changed = changed;
        }

        /// <remarks>
        ///     Parameters correspond to properties on this class.
        /// </remarks>
        public static AttachedProperty Create(
            string name, Type owningType, Type propertyType,
            object defaultValue = null,
            Func<object, bool> validate = null,
            AttachedPropertyChangedCallback changed = null)
        {
            if (propertyType.IsValueType && defaultValue == null)
            {
                // Use activator to create uninitialized version of value type.
                defaultValue = Activator.CreateInstance(propertyType);
            }

            return new AttachedProperty(name, owningType, propertyType, defaultValue, validate, changed);
        }
    }

    /// <summary>
    ///     Event args for when an attached property on a control changes.
    /// </summary>
    public struct AttachedPropertyChangedEventArgs
    {
        public AttachedPropertyChangedEventArgs(object newValue, object oldValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }

        public object NewValue { get; }
        public object OldValue { get; }
    }
}
