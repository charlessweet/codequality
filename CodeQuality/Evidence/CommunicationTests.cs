
using LaughingMonkey.CodeQuality.Communication;
using NUnit.Framework;

namespace Evidence
{
    [TestFixture]
    public class CommunicationTests
    {
        [Test]
        public void Example1()
        {
            ExampleOne t = new ExampleOne();
            Assert.AreEqual(t.MachineOnly(), t.HumanReadable());
        }

        [Test]
        public void Example2()
        {
            ExampleTwo.FirstDialect_SensorXmlPacket t = new ExampleTwo.FirstDialect_SensorXmlPacket();
            ExampleTwo.SecondDialect_SensorXmlPacket u = new ExampleTwo.SecondDialect_SensorXmlPacket();
            for (int i = 0; i < 10; i++)
            {
                string nodeId = "Node" + i;
                string state = "Active";
                t.AddHeader(nodeId, state);
                u.addHeader(nodeId, state);
                Assert.IsFalse(u.addPayload("FAILTOADD"));
            }
            string payload = "53545345,1364643535,6235443,24353,66543352,325445,3456346,58563462,23457457,23452346";
            t.AddPayload(payload);
            u.setReadyForPayload();
            Assert.IsFalse(u.addHeader("Node", "Fail this add"));

            u.addPayload(payload);
            u.setReadyForRender();
            Assert.IsFalse(u.addPayload("Fail to add this too"));

            Assert.AreEqual(u.render(), t.RenderPacket());
        }
    }
}
