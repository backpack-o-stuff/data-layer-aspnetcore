using Moq;
using Moq.AutoMock;

namespace DL.Tests.Infrastructure.TestBases
{
    public class MockedFor<T> : ArrangeActAssertOn
        where T : class
    {
        protected AutoMocker Mocker;
        protected T SUT;

        public MockedFor()
        {
            Mocker = new AutoMocker(MockBehavior.Loose, DefaultValue.Empty);

            SharedBeforeAll();

            SUT = Mocker.CreateInstance<T>();
        }

        private void SharedBeforeAll()
        {
            // NOTE: for any setup that is shared among all tests
        }
    }
}