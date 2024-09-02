using NUnit.Framework;
using UnityEngine;

namespace Main.Game
{
    public class UnitTest_Inventory
    {
        private InventoryInput inventoryInput;
        private InventoryOutput inventoryOutput;
        private TestItemA testItemA;
        private TestItemB testItemB;
        [SetUp]
        public void SetUp()
        {
            inventoryInput = new GameObject().AddComponent<InventoryInput>();
            inventoryOutput = new GameObject().AddComponent<InventoryOutput>();
            testItemA = new GameObject().AddComponent<TestItemA>();
            testItemB = new GameObject().AddComponent<TestItemB>();
        }
        [Test]
        public void Test_Capacity()
        {
            inventoryInput.Capacity.Value = 1;
            Assert.AreEqual(0, inventoryInput.Add(testItemA));
            Assert.AreEqual(-1, inventoryInput.Add(testItemB));
            inventoryInput.Capacity.Value = 2;
            Assert.AreEqual(1, inventoryInput.Add(testItemB));
        }
        [Test]
        public void Test_Test()
        {
            inventoryInput.Capacity.Value = 1;
            Assert.AreEqual(true, inventoryInput.Test().Add(testItemA).Result());
            Assert.AreEqual(false, inventoryInput.Test().Add(testItemA).Add(testItemB).Result());
            inventoryInput.Add(testItemA);
            Assert.AreEqual(true, inventoryInput.Test().Remove(testItemA).Result());
            Assert.AreEqual(false, inventoryInput.Test().Remove(testItemB).Result());
        }
        [Test]
        public void Test_InputFilter()
        {
            inventoryInput.Capacity.Value = 10;
            inventoryInput.Filter.Items.Add(testItemA);
            //
            inventoryInput.Filter.Mode.Value = InventoryFilter.FilterMode.Disable;
            Assert.AreEqual(-1, inventoryInput.Add(testItemA));
            //
            inventoryInput.Filter.Mode.Value = InventoryFilter.FilterMode.Enable;
            Assert.AreEqual(-1, inventoryInput.Add(testItemB));
            //
            inventoryInput.Filter.Mode.Value = InventoryFilter.FilterMode.None;
            Assert.AreEqual(0, inventoryInput.Add(testItemA));
            Assert.AreEqual(1, inventoryInput.Add(testItemB));
        }
        [Test]
        public void Test_InputFilter_Test()
        {
            inventoryInput.Capacity.Value = 10;
            inventoryInput.Filter.Items.Add(testItemA);
            //
            inventoryInput.Filter.Mode.Value = InventoryFilter.FilterMode.Disable;
            Assert.AreEqual(false, inventoryInput.Test().Add(testItemA).Result());
            //
            inventoryInput.Filter.Mode.Value = InventoryFilter.FilterMode.Enable;
            Assert.AreEqual(false, inventoryInput.Test().Add(testItemB).Result());
            //
            inventoryInput.Filter.Mode.Value = InventoryFilter.FilterMode.None;
            Assert.AreEqual(true, inventoryInput.Test().Add(testItemA).Add(testItemB).Result());
        }
        [Test]
        public void Test_OutputFilter()
        {
            inventoryOutput.Capacity.Value = 10;
            inventoryOutput.Add(testItemA);
            inventoryOutput.Add(testItemB);
            inventoryOutput.Filter.Items.Add(testItemA);
            //
            inventoryOutput.Filter.Mode.Value = InventoryFilter.FilterMode.Disable;
            Assert.AreEqual(-1, inventoryOutput.Remove(testItemA));
            //
            inventoryOutput.Filter.Mode.Value = InventoryFilter.FilterMode.Enable;
            Assert.AreEqual(-1, inventoryOutput.Remove(testItemB));
            //
            inventoryOutput.Filter.Mode.Value = InventoryFilter.FilterMode.None;
            Assert.AreEqual(0, inventoryOutput.Remove(testItemA));
            Assert.AreEqual(0, inventoryOutput.Remove(testItemB));
        }
        [Test]
        public void Test_OutputFilter_Test()
        {
            inventoryOutput.Capacity.Value = 10;
            inventoryOutput.Add(testItemA);
            inventoryOutput.Add(testItemB);
            inventoryOutput.Filter.Items.Add(testItemA);
            //
            inventoryOutput.Filter.Mode.Value = InventoryFilter.FilterMode.Disable;
            Assert.AreEqual(false, inventoryOutput.Test().Remove(testItemA).Result());
            //
            inventoryOutput.Filter.Mode.Value = InventoryFilter.FilterMode.Enable;
            Assert.AreEqual(false, inventoryOutput.Test().Remove(testItemB).Result());
            //
            inventoryOutput.Filter.Mode.Value = InventoryFilter.FilterMode.None;
            Assert.AreEqual(true, inventoryOutput.Test().Remove(testItemA).Remove(testItemB).Result());
        }
        private class TestItemA : Item { }
        private class TestItemB : Item { }
    }
}