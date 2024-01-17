using NUnit.Framework;

namespace AutomationFramework.Sports
{
    
    internal static class TestFixture
    {
        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            Base.Base.KafkaConnection.Close();
        }
    }
}
