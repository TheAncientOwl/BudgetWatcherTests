using Microsoft.VisualStudio.TestTools.UnitTesting;

using BudgetDjinni.Database.Schemas;

namespace BudgetDjinniTests.Database.Schemas
{
    [TestClass]
    public class IncomeTests
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
        public void LoadIncomeFromId()
        {
            Income income1 = new Income("Salariu", 4000);
            income1.Save();

            Income income2 = new Income(income1.Id);

            Assert.AreEqual(income1.Id, income2.Id);
            Assert.AreEqual(income1.Name, income2.Name);
            Assert.AreEqual(income1.Value, income2.Value);
        }

        [TestMethod]
        public void UpdateIncome()
        {

        }

        [TestMethod]
        public void DeleteIncome()
        {

        }
    }
}
