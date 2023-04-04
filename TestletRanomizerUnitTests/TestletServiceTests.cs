using TestletRandomizer.Constants;
using TestletRandomizer.Extensions;
using TestletRandomizer.Interfaces;
using TestletRandomizer.Models;
using TestletRandomizer.Services;

namespace TestletRanomizerUnitTests
{
    public class Tests
    {
        private ITestletService _testletService;

        [SetUp]
        public void Setup()
        {
            _testletService = new TestletService();
        }

        [TestCase(TesletConstants.PretestItemsCount, TesletConstants.OperationalItemsCount, false)]
        [TestCase(TesletConstants.PretestItemsCount, 7, true)]
        [TestCase(3, TesletConstants.OperationalItemsCount, true)]
        [TestCase(1, 9, true)]
        [TestCase(0, 0, true)]
        public void TestletItemsCountValidationTests(int pretestItemsCount, int operationalItemsCount, bool isFailing)
        {
            // Arrange
            var items = TestletHelpers.GenerateItemsByRule(pretestItemsCount, operationalItemsCount);

            // Act, Assert
            if (isFailing)
            {
                Assert.Throws<ArgumentException>(() => _testletService.CreateTestlet("Id", items));
            }
            else
            {
                Assert.DoesNotThrow(() => _testletService.CreateTestlet("Id", items));
            }
        }

        [Test]
        public void TestletNullValidationTest()
        {
            // Arrange
            var items = TestletHelpers.GenerateItemsByRule(TesletConstants.PretestItemsCount, TesletConstants.OperationalItemsCount);
            
            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => _testletService.CreateTestlet(null, items));

            Assert.Throws<ArgumentNullException>(() => _testletService.CreateTestlet("Id", null));
        }

        [Test]
        public void TestletItemsWithDuplicatesTest()
        {
            // Arrange, Act
            var testlet = _testletService.CreateTestlet("Id", TestletHelpers.ItemsWithDuplicates());

            // Assert
            AssertTestlet(testlet);
        }

        [Test]
        public void TestletDefaultTest()
        {
            // Arrange, Act
            var testlet = _testletService.CreateTestlet("Id", TestletHelpers.DefaultItems());

            // Assert
            AssertTestlet(testlet);
        }

        [Test]
        public void TestletPretestItemsUniformDistributionTest()
        {
            // Arrange
            var testRunsCount = 1_000_000;
            var itemsCount = TesletConstants.PretestItemsCount + TesletConstants.OperationalItemsCount; 
            var pretestPositionCount = new long[itemsCount];

            // Act
            for (long i = 0; i < testRunsCount; i++)
            {
                var items = TestletHelpers.GenerateItemsByRule(TesletConstants.PretestItemsCount, TesletConstants.OperationalItemsCount).Shuffle();
                var testlet = _testletService.CreateTestlet($"testlet{i}", items);
                for (int position = 0; position < itemsCount; position++)
                {
                    if (testlet.Items[position].ItemType == ItemTypeEnum.Pretest)
                    {
                        pretestPositionCount[position]++;
                    }
                }
            }

            // Assert
            var standardDeviation = 0.01;
            var pretestProbability = (double)(TesletConstants.PretestItemsCount - TesletConstants.PretestItemsToBeFirst) 
                / (itemsCount - TesletConstants.PretestItemsToBeFirst);
            for (int position = 0; position < itemsCount; position++)
            {
                if (position < TesletConstants.PretestItemsToBeFirst)
                {
                    Assert.That(pretestPositionCount[position], Is.EqualTo(testRunsCount));
                }
                else
                {
                    Assert.That((double)pretestPositionCount[position] / testRunsCount, 
                        Is.InRange(pretestProbability - standardDeviation, pretestProbability + standardDeviation));
                }
            }
        }

        private void AssertTestlet(Testlet testlet)
        {
            Assert.That(testlet, Is.Not.Null);
            Assert.That(testlet.Items.Count, Is.EqualTo(TesletConstants.OperationalItemsCount + TesletConstants.PretestItemsCount));
            Assert.That(testlet.Items.Where(t => t.ItemType == ItemTypeEnum.Pretest).Count, Is.EqualTo(TesletConstants.PretestItemsCount));
            Assert.That(testlet.Items.Where(t => t.ItemType == ItemTypeEnum.Operational).Count, Is.EqualTo(TesletConstants.OperationalItemsCount));
            for (int i = 0; i < TesletConstants.PretestItemsToBeFirst; i++)
            {
                Assert.That(testlet.Items[i].ItemType, Is.EqualTo(ItemTypeEnum.Pretest));
            }
        }
    }
}