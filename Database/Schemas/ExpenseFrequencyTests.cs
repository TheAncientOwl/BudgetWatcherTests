using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BudgetDjinni.Database.Schemas;

namespace BudgetDjinniTests.Database.Schemas
{
    [TestClass]
    public class ExpenseFrequencyTests
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            BudgetDjinni.Database.Manager.Instance.OpenOrCreateDatabase();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            BudgetDjinni.Database.Manager.Instance.CloseDatabase();
        }

        [TestMethod]
        public void LoadFromId()
        {
            ExpenseFrequency freq1 = new ExpenseFrequency("Lunara", 30);
            freq1.Save();

            ExpenseFrequency freq2 = new ExpenseFrequency(freq1.Id);

            Assert.AreEqual(freq1, freq2);
        }

        [TestMethod]
        public void Update()
        {
            ExpenseFrequency freq1 = new ExpenseFrequency("Random", 15);
            freq1.Save();

            freq1.Name = "Lunara";
            freq1.Days = 30;
            freq1.Update();

            ExpenseFrequency freq2 = new ExpenseFrequency(freq1.Id);
            Assert.AreEqual(freq2.Name, "Lunara");
            Assert.AreEqual(freq2.Days, 30);
        }

        [TestMethod]
        public void Delete()
        {
            ExpenseFrequency freq1 = new ExpenseFrequency("Random", 10);
            freq1.Save();

            ExpenseFrequency freq2 = new ExpenseFrequency(freq1.Id);
            freq2.Delete();

            Assert.ThrowsException<Exception>(() =>
            {
                ExpenseFrequency freq = new ExpenseFrequency(freq1.Id);
            });
        }
    }
}
