using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Main
{
    public class UnitTest_Objects_Clone
    {
        [Test]
        [TestCaseSource(nameof(Test_Clone_Source))]
        public void Test_Clone(bool isError, object item, Objects.CustomCloneAttribute customClone, object[] args)
        {
            if (isError) Assert.Catch(() => Objects.Clone(item, customClone, args:args));
            else Assert.IsNotNull(Objects.Clone(item, customClone, args: args));
        }
        public static IEnumerable<object[]> Test_Clone_Source()
        {
            yield return new object[] { true, new NormalClass(), null, null };
            yield return new object[] { false, new ClassWithCloneable(), null, null };
            yield return new object[] { true, new ClassWithCustom(), null, null };
            yield return new object[] { false, new ClassWithCustom(), new ClassWithCustomAttribute(), null };
        }
        public class NormalClass { }
        //
        public class ClassWithCloneable : ICloneable
        { public object Clone() => new ClassWithCloneable(); }
        public class ClassWithAttributeAttribute : Objects.CustomCloneAttribute
        {
            public override T Clone<T>(T item, params object[] args)
            {
                if (new ClassWithAttribute() is T t) return t;
                throw new NotImplementedException();
            }
        }
        [ClassWithAttribute]
        public class ClassWithAttribute { }
        //
        public class ClassWithCustomAttribute : Objects.CustomCloneAttribute
        {
            public override T Clone<T>(T item, params object[] args)
            {
                if (new ClassWithCustomAttribute() is T t) return t;
                throw new NotImplementedException();
            }
        }
        public class ClassWithCustom
        { }
    }
}