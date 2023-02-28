using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio.Setup.Configuration;
using Xunit;

public class DynamicReflectionTests
{
    [Fact]
    public void WhenAsPrivateDynamicOfNullType_ThenReturnsNull()
    {
        var obj = default(Type);
        dynamic target = obj.AsDynamicReflection();

        Assert.Null(target);
    }

    [Fact]
    public void WhenAccessingPrivateField_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        target.field = 5;

        Assert.Equal(5, target.field);
    }

    [Fact]
    public void WhenAccessingPrivateProperty_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        target.Property = "hello";

        Assert.Equal("hello", target.Property);
    }

    [Fact]
    public void WhenInvokingMethod_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        var result = target.Echo("hello");

        Assert.Equal("hello", result);
    }

    [Fact]
    public void WhenInvokingMethod2_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        var result = target.Echo("hello {0}", "world");

        Assert.Equal("hello world", result);
    }

    [Fact]
    public void WhenInvokingMethod_ThenResolvesOverload()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        var result = target.Echo("hello", 2);

        Assert.Equal("hellohello", result);
    }

    [Fact]
    public void WhenInvokingMethodWithRef_ThenResolvesOverload()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();
        var value = default(string);

        var result = target.Echo("hello ", ref value);

        Assert.True(result);
    }

    [Fact]
    public void WhenInvokingMethodWithRef_ThenReturnsRefValue()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();
        var value = default(string);

        var r1 = RefValue.Create(() => value, s => value = s);

        var result = target.Echo("hello ", r1);

        Assert.True(result);
        Assert.Equal("hello world", value);
    }

    [Fact]
    public void WhenInvokingMethodWithOut_ThenReturnsOutValue()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();
        var value1 = default(string);
        var value2 = default(int);

        var r1 = OutValue.Create<string>(s => value1 = s);
        var r2 = OutValue.Create<int>(s => value2 = s);

        var result = target.Echo("hello ", true, out r1, out r2);

        Assert.True(result);
        Assert.Equal("hello world", value1);
        Assert.Equal(25, value2);
    }

    [Fact]
    public void WhenInvokingMethodWithTwoOut_ThenReturnsOutValue()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();
        var value = default(string);
        var i = 0;

        var out1 = OutValue.Create<string>(s => value = s);
        var out2 = OutValue.Create<int>(x => i = x);

        var result = target.Echo("hello ", true, out1, out2);

        Assert.True(result);
        Assert.Equal("hello world", value);
        Assert.Equal(25, i);
    }

    [Fact]
    public void WhenInvokingIndexerOverload1_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        var result = target[9];

        Assert.Equal("9", result);
    }

    [Fact]
    public void WhenInvokingIndexerOverload2_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        var result = target["9"];

        Assert.Equal(9, result);
    }

    [Fact]
    public void WhenSettingIndexedProperty_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        target[9] = "kzu";
    }

    [Fact]
    public void WhenSettingNonExistingIndexedProperty_ThenThrows()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        Assert.Throws<RuntimeBinderException>(() => target[Guid.NewGuid()] = 23);
    }

    [Fact]
    public void WhenInvokingIndexerTwoArgs_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        var result = target["hello", 2];

        Assert.Equal("llo", result);
    }

    [Fact]
    public void WhenNullObject_ThenAsPrivateDinamicReturnsNull()
    {
        var target = default(object);

        Assert.Null(target.AsDynamicReflection());
    }

    [Fact]
    public void WhenInvokingExplicitlyImplementedMethod_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        var clone = target.Clone();

        Assert.Equal(target.Id, clone.Id);
    }

    [Fact]
    public void WhenInvokingExplicitlyImplementedProperty_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        target.Name = "foo";

        Assert.Equal("foo", target.Name);
    }

    [Fact]
    public void WhenInvokingNonExistingMethod_ThenFails()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        Assert.Throws<RuntimeBinderException>(() => target.Do());
    }

    [Fact]
    public void WhenGettingNonExistingProperty_ThenFails()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        Assert.Throws<RuntimeBinderException>(() => target.Blah);
    }

    [Fact]
    public void WhenGettingNonExistingIndex_ThenFails()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        Assert.Throws<RuntimeBinderException>(() => target[true, 24]);
    }

    [Fact]
    public void WhenSettingNonExistingProperty_ThenFails()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        Assert.Throws<RuntimeBinderException>(() => target.Blah = true);
    }

    [Fact]
    public void WhenConverting_ThenConvertsTargetObject()
    {
        var inner = new PrivateObject();
        dynamic target = inner.AsDynamicReflection();

        PrivateObject obj = target;

        Assert.Same(inner, obj);
    }

    [Fact]
    public void WhenConvertingToImplementedInterface_ThenConvertsTargetObject()
    {
        var inner = new PrivateObject();
        dynamic target = inner.AsDynamicReflection();

        ICloneable obj = target;

        Assert.Same(inner, obj);
    }

    [Fact]
    public void WhenInvokingGenericMethod_ThenSucceeds()
    {
        dynamic target = new PrivateObject().AsDynamicReflection();

        var value = target.Get<ICloneable>(23);
    }

    [Fact]
    public void WhenInvokingConstructorSecondTime_ThenChangesId()
    {
        var inner = new PrivateObject();
        var id = inner.Id;
        dynamic target = inner.AsDynamicReflection();

        target.ctor();

        Assert.NotEqual(id, inner.Id);
    }

    [Fact]
    public void WhenInvokingStaticMembers_ThenSucceeds()
    {
        var target = typeof(PrivateObject).AsDynamicReflection();
        // Can invoke static ctor directly
        target.cctor();

        var value1 = target.StaticProp;
        var value2 = target.StaticProp;

        Assert.Equal(value1, value2);

        target.StaticField = "foo";

        Assert.Equal("foo", PrivateObject.StaticField);

        var value = target.StaticMethod("hello");

        Assert.Equal("hello", value);

        var refvalue = default(string);
        value = target.StaticMethod("hello", ref refvalue);

        Assert.Equal("hello", value);
    }

    [Fact]
    public void WhenInvokingStaticMembersWithDerivedTypeArgument_ThenSucceeds()
    {
        var target = typeof(PrivateObject).AsDynamicReflection();
        target.cctor();

        List<int> d = new List<int>();

        List<int> value = target.StaticMethod(d);

        Assert.Equal(d, value);
    }


    [Fact]
    public void WhenInvokingCtorForType_ThenSucceeds()
    {
        var target = typeof(PrivateObject).AsDynamicReflection();
        var id = Guid.NewGuid();
        var obj = target.ctor(id);

        Assert.Equal(id, obj.Id);
    }

    [Fact]
    public void WhenConvertingToIncompatible_ThenThrows()
    {
        var target = new PrivateObject().AsDynamicReflection();

        int id = 0;

        Assert.Throws<RuntimeBinderException>(() => id = target);
    }

    [Fact]
    public void WhenConvertingToIConvertibleCompatibleBuiltInType_ThenSucceeds()
    {
        var target = new ConvertibleObject().AsDynamicReflection();

        int id = target;

        Assert.Equal(25, id);
    }

    [Fact]
    public void WhenConvertingToIConvertibleCompatibleCustomType_ThenSucceeds()
    {
        var target = new ConvertibleObject().AsDynamicReflection();

        PrivateObject converted = target;

        Assert.NotNull(converted);
    }

    [Fact]
    public void WhenConvertingToIConvertibleIncompatibleCustomType_ThenSucceeds()
    {
        var target = new ConvertibleObject().AsDynamicReflection();

        ICloneable converted = null;

        Assert.Throws<RuntimeBinderException>(() => converted = target);
    }

    [Fact]
    public void WhenPassingTypeParameter_ThenResolves()
    {
        var foo = new PrivateObject().AsDynamicReflection();
        var type = typeof(IFormattable);

        var result = foo.Get(typeof(IFormattable).AsGenericTypeParameter(), 5);

        Assert.Equal(typeof(IFormattable).Name, result);
    }

    [Fact]
    public void WhenPassingTypeParameterAtEnd_ThenResolves()
    {
        var foo = new PrivateObject().AsDynamicReflection();
        var type = typeof(IFormattable);

        var result = foo.Get(5, typeof(IFormattable).AsGenericTypeParameter());

        Assert.Equal("IFormattable", result);
    }

    [Fact]
    public void WhenPassingMultipleTypeParameterCanMixGenericAndTypeParam_ThenResolves()
    {
        var foo = new PrivateObject().AsDynamicReflection();
        var type = typeof(IFormattable);

        var result = foo.Get<IFormattable>(5, typeof(bool).AsGenericTypeParameter());

        Assert.Equal("IFormattable|Boolean", result);
    }

    [Fact]
    public void WhenInvokingWithAssignableType_ThenSucceeds()
    {
        var foo = new PrivateObject().AsDynamicReflection();

        var result = foo.Echo("foo", (byte)2);

        Assert.Equal("foofoo", result);

        result = foo.Echo("foo", (double)2);

        Assert.Equal("foofoo", result);

        result = foo.Echo("foo", (decimal)2);

        Assert.Equal("foofoo", result);
    }

    [Fact]
    public void WhenSpecifyingGenericAsParameterTypeHint_ThenFindsComOverload()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;

        var target = new ComUser().AsDynamicReflection();

        var setup = (SetupConfiguration)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("177F0C4A-1CD3-4DE7-A32C-71DBBB9FA36D")));
        var e = setup.EnumAllInstances();
        int fetched;
        var instances = new ISetupInstance[1];
        do
        {
            // Call e.Next to query for the next instance (single item or nothing returned).
            e.Next(1, instances, out fetched);
            if (fetched <= 0)
            {
                continue;
            }
            var instance = instances[0];
            var state = ((ISetupInstance2)instance).GetState();
            Version version;
            try
            {
                version = new Version(instance.GetInstallationVersion());
            }
            catch (FormatException)
            {
                continue;
            }
            // If the install was complete and a valid version, consider it.
            if (state == InstanceState.Complete)
            {
                var type = Type.GetTypeFromProgID($"VisualStudio.DTE.{version.Major}.0");
                if (type == null)
                    continue;

                var dte = (DTE)Activator.CreateInstance(type);

                try
                {
                    dte.MainWindow.Visible = true;

                    var result = (bool)target.Do<DTE>(dte);

                    Assert.True(result);
                }
                finally
                {
                    dte.Quit();
                }

                return;
            }
        } while (fetched > 0);
    }

    public class ComUser
    {
        public bool Do(DTE dte)
        {
            return true;
        }
    }

    class PrivateObject : ICloneable, IPrivate
    {
        static PrivateObject()
        {
            StaticProp = Guid.NewGuid().ToString();
            StaticField = null;
        }

        PrivateObject(Guid id)
        {
            this.Id = id;
        }

        public PrivateObject()
        {
            this.Id = Guid.NewGuid();
        }

        public static string StaticProp { get; set; }
        public static string StaticField;
        public static string StaticMethod(string value)
        {
            return value;
        }

        public static IEnumerable<int> StaticMethod(IEnumerable<int> value)
        {
            return value;
        }

        public static string StaticMethod(string value, ref string refstring)
        {
            return value;
        }

        public Guid Id { get; set; }

#pragma warning disable 0169
        int field;
#pragma warning restore 0169

        string Property { get; set; }

        string Echo(string value)
        {
            return value;
        }

        string Echo(string value, string format)
        {
            return string.Format(value, format);
        }

        string Echo(string value, int count)
        {
            return Enumerable.Range(0, count)
                .Aggregate("", (s, i) => s += value);
        }

        string Echo(string value, IConvertible convertible)
        {
            return Enumerable.Range(0, convertible.ToInt32(CultureInfo.CurrentCulture))
                .Aggregate("", (s, i) => s += value);
        }

        bool Echo(string value, ref string result)
        {
            result = value + "world";

            return true;
        }

        bool Echo(string value, bool valid, out string result, out int count)
        {
            result = value + "world";
            count = 25;

            return true;
        }

        string this[int index]
        {
            get { return index.ToString(); }
            set { }
        }

        int this[string index]
        {
            get { return int.Parse(index); }
        }

        string this[string value, int index]
        {
            get { return value.Substring(index); }
        }

        string Get<T>(int id)
        {
            return typeof(T).Name;
        }

        string Get<T, R>(int id)
        {
            return typeof(T).Name + "|" + typeof(R).Name;
        }

        object ICloneable.Clone()
        {
            return this;
        }

        string IPrivate.Name { get; set; }
    }

    class ConvertibleObject : IConvertible
    {
        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            return 25;
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == typeof(PrivateObject))
                return new PrivateObject();

            throw new NotSupportedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPrivate
    {
        string Name { get; set; }
    }
}