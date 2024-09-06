using System.Collections;
using Main.Game.FomulaSteps;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Main.Game
{
    public class UnitTest_Fomula
    {
        Item item;
        Body body;
        //TimeNode timeNode;
        BodyPart bodyPart;
        Factory factory;
        InventoryInput inventoryInput;
        InventoryOutput inventoryOutput;
        Fomula fomula;
        FomulaStep_InputItem inputItem;
        FomulaStep_OutputItem outputItem;
        FomulaStep_Stop stop;
        //FomulaStep_Timer timer;
        [SetUp]
        public void SetUp()
        {
            //Item
            item ??= new GameObject("Item").AddComponent<Item>();
            //Body
            if (body == null)
            {
                body = new GameObject("Body").AddComponent<Body>();
                //timeNode = body.AddComponent<TimeNode>();
                bodyPart = new GameObject("BodyPart").AddComponent<BodyPart>();
                inventoryInput = bodyPart.AddComponent<InventoryInput>();
                inventoryOutput = bodyPart.AddComponent<InventoryOutput>();
                factory = bodyPart.AddComponent<Factory>();
                body.BodyParts.Add(bodyPart);
                Assert.IsTrue(bodyPart.BodyComponents.Contains(factory));
                Assert.IsTrue(bodyPart.BodyComponents.Contains(inventoryInput));
                Assert.IsTrue(bodyPart.BodyComponents.Contains(inventoryOutput));
                Assert.IsTrue(bodyPart.BodyComponents.Contains(factory));
            }
            inventoryInput.Clear();
            inventoryInput.Capacity.Value = 0;
            inventoryInput.Filter.Mode.Value = InventoryFilter.FilterMode.None;
            inventoryInput.Filter.Items.Clear();
            //
            inventoryOutput.Clear();
            inventoryOutput.Capacity.Value = 0;
            inventoryOutput.Filter.Mode.Value = InventoryFilter.FilterMode.None;
            inventoryOutput.Filter.Items.Clear();
            //
            factory.Fomulas.Clear();
            //Fomula
            if (fomula == null)
            {
                fomula = new GameObject("Fomula").AddComponent<Fomula>();
                inputItem = fomula.AddComponent<FomulaStep_InputItem>();
                outputItem = fomula.AddComponent<FomulaStep_OutputItem>();
                stop = fomula.AddComponent<FomulaStep_Stop>();
                //timer = fomula.AddComponent<FomulaStep_Timer>();
            }
            fomula.FomulaSteps.Clear();
            fomula.FomulaStop();
            inputItem.Items.Clear();
            outputItem.Items.Clear();
        }
        [Test]
        public void Test_Input()
        {
            inventoryInput.Capacity.Value = 1;
            inventoryInput.Add(item);
            Assert.IsTrue(inventoryInput.Contains(item));
            //
            inputItem.Items.Add(item);
            fomula.FomulaSteps.Add(inputItem);
            fomula.FomulaSteps.Add(stop);
            //
            factory.Fomulas.Add(fomula);
            //
            Assert.AreEqual(-1, fomula.Index.Value);
            Assert.IsFalse(inventoryInput.Contains(item));
        }
        [Test]
        public void Test_Output()
        {
            inventoryOutput.Capacity.Value = 1;
            Assert.IsFalse(inventoryInput.Contains(item));
            //
            outputItem.Items.Add(item);
            fomula.FomulaSteps.Add(outputItem);
            fomula.FomulaSteps.Add(stop);
            //
            factory.Fomulas.Add(fomula);
            //
            Assert.AreEqual(-1, fomula.Index.Value);
            Assert.IsTrue(inventoryOutput.Contains(item));
        }
        //[Test]
        //public void Test_Timer()
        //{
        //    fomula.FomulaSteps.Add(timer);
        //    timer.Target.Value = 10f;
        //    fomula.FomulaSteps.Add(stop);
        //    //
        //    factory.Fomulas.Add(fomula);
        //    timeNode.UpdateTime(5f);
        //    Assert.AreEqual(0, fomula.Index.Value);
        //    timeNode.UpdateTime(5f);
        //    Assert.AreEqual(-1, fomula.Index.Value);
        //}
    }
}
