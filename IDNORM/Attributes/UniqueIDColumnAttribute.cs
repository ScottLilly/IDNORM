using System;

namespace IDNORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class UniqueIDColumnAttribute : Attribute
    {
    }
}
