using System.IO;
using NUnit.Framework;
using WSEI_parser_planu;

namespace TestProject {
    public class Tests {
        string sampleSchedule;
        [SetUp]
        public void Setup() {
            sampleSchedule = File.ReadAllText("../../../../sampleSchedule.htm");
        }

        [Test] 
        public void Test1() {
            var schedule = new Schedule(sampleSchedule);
            Assert.Pass();
        } 
    }
}