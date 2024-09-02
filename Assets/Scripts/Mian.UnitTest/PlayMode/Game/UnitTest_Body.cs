using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Main.Game
{
    public class UnitTest_Body
    {
        [Test]
        public void Test_Body_BodyPart_Link()
        {
            var body = new GameObject().AddComponent<Body>();
            Assert.IsNotNull(body);
            var bodyPart = new GameObject().AddComponent<BodyPart>();
            Assert.IsNotNull(bodyPart);
            //
            body.BodyParts.Add(bodyPart);
            Assert.AreEqual(body, bodyPart.Body.Value);
            //
            body.BodyParts.Remove(bodyPart);
            Assert.AreEqual(null, bodyPart.Body.Value);
            //
            bodyPart.Body.Value = body;
            Assert.IsTrue(body.BodyParts.Contains(bodyPart));
            //
            bodyPart.Body.Value = null;
            Assert.IsFalse(body.BodyParts.Contains(bodyPart));
        }
        [Test]
        public void Test_Body_BodyPart_Component()
        {
            var body = new GameObject().AddComponent<Body>();
            var bodyPart = new GameObject().AddComponent<BodyPart>();
            //
            body.BodyParts.Add(bodyPart);
            Assert.IsTrue(bodyPart.BodyComponents.Contains(body));
            //
            body.BodyParts.Remove(bodyPart);
            Assert.IsFalse(bodyPart.BodyComponents.Contains(body));
            //
            body.BodyParts.Add(bodyPart);
            var test = body.AddComponent<TestBodyPartComponent>();
            Assert.IsTrue(bodyPart.BodyComponents.Contains(test));
        }
        public class TestBodyPartComponent : GameComponent { }
    }
}
