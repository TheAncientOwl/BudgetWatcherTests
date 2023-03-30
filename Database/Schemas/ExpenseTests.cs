using BudgetWatcher.Database.Schemas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BudgetWatcherTests.Database.Schemas
{
    [TestClass]
    public class ExpenseTests
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            BudgetWatcher.Database.Manager.Instance.OpenOrCreateDatabase();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            BudgetWatcher.Database.Manager.Instance.CloseDatabase();
        }

        [TestMethod]
        public void LoadFromId()
        {
            ExpenseCategory category = new ExpenseCategory("Music", "Lorem ipsum dolor sit amet");
            category.Insert();

            ExpenseFrequency frequency = new ExpenseFrequency("Monthly", 30);
            frequency.Insert();

            Expense expense1 = new Expense("Spotify", 2.5, DateTime.Now, category.Id, frequency.Id);
            expense1.Insert();

            Expense expense2 = new Expense(expense1.Id);
            
            Assert.AreEqual(expense1, expense2);
        }

        [TestMethod]
        public void Update()
        {
            // create and save expense1
            ExpenseCategory expenseCategory = new ExpenseCategory("Music", "Lorem ipsum dolor sit amet");
            expenseCategory.Insert();

            ExpenseFrequency expenseFrequency = new ExpenseFrequency("Monthly", 30);
            expenseFrequency.Insert();

            Expense expense1 = new Expense("Spotify", 2.5, DateTime.Now, expenseCategory.Id, expenseFrequency.Id);

            expense1.Insert();

            // modify and update expense1
            expense1.Name = "Water";
            expense1.Value = 1;

            ExpenseCategory expenseCategory2 = new ExpenseCategory("House", "Lorem ipsum");
            expenseCategory2.Insert();
            expense1.Category = expenseCategory2;

            ExpenseFrequency expenseFrequency2 = new ExpenseFrequency("Daily", 1);
            expenseFrequency2.Insert();
            expense1.Frequency = expenseFrequency2;

            expense1.Update();

            // load expense2 from db with expense1.Id
            Expense expense2 = new Expense(expense1.Id);

            // asserts
            Assert.AreEqual(expense1, expense2);
        }

        [TestMethod]
        public void Delete()
        {
            ExpenseCategory expenseCategory = new ExpenseCategory("Music", "Lorem ipsum dolor sit amet");
            expenseCategory.Insert();

            ExpenseFrequency expenseFrequency = new ExpenseFrequency("Monthly", 30);
            expenseFrequency.Insert();

            Expense expense1 = new Expense("Spotify", 2.5, DateTime.Now, expenseCategory.Id, expenseFrequency.Id);

            expense1.Insert();

            expense1.Delete();

            Assert.ThrowsException<Exception>(() =>
            {
                Expense expense2 = new Expense(expense1.Id);
            });
        }
    }
}
