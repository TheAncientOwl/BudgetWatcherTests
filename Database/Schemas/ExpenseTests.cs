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

            Expense exp1 = new Expense("Spotify", 2.5, DateTime.Now, "no details", category.Id, frequency.Id);
            exp1.Insert();

            Expense exp2 = new Expense(exp1.Id);

            Assert.IsTrue(
                (exp1.Id == exp2.Id) && (exp1.Name == exp2.Name) &&
                (exp1.Value == exp2.Value) && (exp1.Details == exp2.Details) &&
                (exp1.Date.Day == exp2.Date.Day) &&
                (exp1.Date.Month == exp2.Date.Month) &&
                (exp1.Date.Year == exp2.Date.Year) && 
                (exp1.Category.Name == exp2.Category.Name) && (exp1.Category.Id == exp2.Category.Id) &&
                (exp1.Frequency.Name == exp2.Frequency.Name) && (exp1.Frequency.Id == exp2.Frequency.Id));
        }

        [TestMethod]
        public void Update()
        {
            // create and save expense1
            ExpenseCategory expenseCategory = new ExpenseCategory("Music", "Lorem ipsum dolor sit amet");
            expenseCategory.Insert();

            ExpenseFrequency expenseFrequency = new ExpenseFrequency("Monthly", 30);
            expenseFrequency.Insert();

            Expense exp1 = new Expense("Spotify", 2.5, DateTime.Now, "no details", expenseCategory.Id, expenseFrequency.Id);

            exp1.Insert();

            // modify and update expense1
            exp1.Name = "Water";
            exp1.Value = 1;
            exp1.Details = "Some details";

            ExpenseCategory expenseCategory2 = new ExpenseCategory("House", "Lorem ipsum");
            expenseCategory2.Insert();
            exp1.Category = expenseCategory2;

            ExpenseFrequency expenseFrequency2 = new ExpenseFrequency("Daily", 1);
            expenseFrequency2.Insert();
            exp1.Frequency = expenseFrequency2;

            exp1.Update();

            // load expense2 from db with expense1.Id
            Expense exp2 = new Expense(exp1.Id);

            // asserts
            Assert.IsTrue(
                (exp1.Id == exp2.Id) && (exp1.Name == exp2.Name) &&
                (exp1.Value == exp2.Value) && (exp1.Details == exp2.Details) &&
                (exp1.Date.Day == exp2.Date.Day) &&
                (exp1.Date.Month == exp2.Date.Month) &&
                (exp1.Date.Year == exp2.Date.Year) &&
                (exp1.Category.Name == exp2.Category.Name) && (exp1.Category.Id == exp2.Category.Id) &&
                (exp1.Frequency.Name == exp2.Frequency.Name) && (exp1.Frequency.Id == exp2.Frequency.Id));
        }

        [TestMethod]
        public void Delete()
        {
            ExpenseCategory expenseCategory = new ExpenseCategory("Music", "Lorem ipsum dolor sit amet");
            expenseCategory.Insert();

            ExpenseFrequency expenseFrequency = new ExpenseFrequency("Monthly", 30);
            expenseFrequency.Insert();

            Expense expense1 = new Expense("Spotify", 2.5, DateTime.Now, "some details", expenseCategory.Id, expenseFrequency.Id);

            expense1.Insert();

            expense1.Delete();

            Assert.ThrowsException<Exception>(() =>
            {
                Expense expense2 = new Expense(expense1.Id);
            });
        }
    }
}
