using System;

namespace eUseControl.Domain.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EnumToStringAttribute : Attribute
    {
    }
}