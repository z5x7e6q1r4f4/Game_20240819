using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Main
{
    public class UnitTest_Objects_New
    {
        [Test]
        [TestCaseSource(nameof(Test_New_Source))]
        public void Test_New(bool isError, Type type, Objects.CustomNewAttribute customNew, object[] args)
        {
            if (isError) { Assert.Catch(() => Objects.New<object>(type, customNew, args)); return; }
            else Assert.IsNotNull(Objects.New<object>(type, customNew, args));
        }
        public static IEnumerable<object[]> Test_New_Source()
        {
            yield return new object[] { false, typeof(NormalClass), null, null };

            yield return new object[] { false, typeof(ClassWithAttribute), null, null };

            yield return new object[] { true, typeof(ClassWithCustom), null, null };
            yield return new object[] { false, typeof(ClassWithCustom), null, new object[] { 0, 0 } };
            yield return new object[] { false, typeof(ClassWithCustom), new ClassWithCustomAttribute(), null };
        }
        //Nomal
        public class NormalClass { }
        //WithAttribute
        public class ClassWithAttributeAttribute : Objects.CustomNewAttribute
        {
            public override T New<T>(Type type, params object[] args)
            {
                if (new ClassWithAttribute(0, 0) is T t) return t;
                throw new NotImplementedException();
            }
        }
        [ClassWithAttribute]
        public class ClassWithAttribute
        {
            public ClassWithAttribute(int a, int b) { }
        }
        //WithCustom
        public class ClassWithCustomAttribute : Objects.CustomNewAttribute
        {
            public override T New<T>(Type type, params object[] args)
            {
                if (new ClassWithCustom(0, 0) is T t) return t;
                throw new NotImplementedException();
            }
        }
        public class ClassWithCustom
        {
            public ClassWithCustom(int a, int b) { }
        }
    }
}
