using BudgetDjinni.Database.Schemas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BudgetDjinniTests.Database.Schemas
{
    [TestClass]
    public class ExpenseCategoryTests
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
            ExpenseCategory cat1 = new ExpenseCategory("Lorem ipsum", "Lorem ipsum");
            cat1.Save();

            ExpenseCategory cat2 = new ExpenseCategory(cat1.Id);

            Assert.AreEqual(cat1, cat2);
        }

        [TestMethod]
        public void Update()
        {
            ExpenseCategory cat1 = new ExpenseCategory("Lorem ipsum", "Lorem ipsum");
            cat1.Save();

            cat1.Name = "random";
            cat1.Description = "something";
            cat1.Update();

            ExpenseCategory cat2 = new ExpenseCategory(cat1.Id);
            Assert.AreEqual(cat2.Name, "random");
            Assert.AreEqual(cat2.Description, "something");
        }

        [TestMethod]
        public void DeleteIncome()
        {
            ExpenseCategory cat1 = new ExpenseCategory("Lorem ipsum", "Lorem ipsum");
            cat1.Save();

            ExpenseCategory cat2 = new ExpenseCategory(cat1.Id);
            cat2.Delete();

            Assert.ThrowsException<Exception>(() =>
            {
                ExpenseCategory freq = new ExpenseCategory(cat1.Id);
            });
        }
    }
}
