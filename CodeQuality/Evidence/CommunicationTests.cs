
using LaughingMonkey.CodeQuality.Communication;
using LaughingMonkey.CodeQuality.Design;
using NUnit.Framework;

namespace Evidence
{
    [TestFixture]
    public class EvidenceTests
    {
        [Test]
        public void Example1_HumanReadable()
        {
            Example1_HumanReadable t = new Example1_HumanReadable();
            Assert.AreEqual(t.MachineOnly(), t.HumanReadable());
        }

        [Test]
        public void Example2_DeveloperDialect()
        {
            Example2_DeveloperDialect.FirstDialect_SensorXmlPacket t = new Example2_DeveloperDialect.FirstDialect_SensorXmlPacket();
            Example2_DeveloperDialect.SecondDialect_SensorXmlPacket u = new Example2_DeveloperDialect.SecondDialect_SensorXmlPacket();
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

        [Test]
        public void Example3_DevelopersSometimesMumble()
        {
            Example3_DevelopersSometimesMumble t = new LaughingMonkey.CodeQuality.Communication.Example3_DevelopersSometimesMumble();
            string s = t.TrivialMumblingExample();
            Assert.AreEqual("World", s);
        }

        [Test]
        public void Example4_DevelopersCanBeVague()
        {
            Example4_DevelopersCanBeVague t = new LaughingMonkey.CodeQuality.Communication.Example4_DevelopersCanBeVague();
            Example4_DevelopersCanBeVague.Envelope testEnvelope = new LaughingMonkey.CodeQuality.Communication.Example4_DevelopersCanBeVague.Envelope();
            testEnvelope.EnvelopeId = 1;
            testEnvelope.Content = "This is the envelope from the database.";
            Assert.IsTrue(t.Validate(testEnvelope));
        }

        [Test]
        public void Example5_DevelopersTalkALot()
        {
            Example5_DevelopersTalkALot.UnfortunatelyLongClass t = new LaughingMonkey.CodeQuality.Design.Example5_DevelopersTalkALot.UnfortunatelyLongClass();
            string actual = t.AddTwoNumbersRepresentedAsStringsAndConvertBases("1000", "2020", "10", "10");
            Assert.AreEqual("3020", actual);

            Example5_DevelopersTalkALot.RefactoredToShorterClass t2 = new LaughingMonkey.CodeQuality.Design.Example5_DevelopersTalkALot.RefactoredToShorterClass();
            actual = t2.AddTwoNumbersRepresentedAsStringsAndConvertBases("1000", "2020", "10", "10");
            Assert.AreEqual("3020", actual);
        }
    }
}
