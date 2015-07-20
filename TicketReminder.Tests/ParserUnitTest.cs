using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DprcParser;
using TicketReminder.DataClasses;

namespace TicketReminder.Tests
{
    [TestClass]
    public class ParserUnitTest
    {
        [TestMethod]
        public void Can_Auth()
        {
            //Act - do authorization
            Parser.Auth("nikita.ru96@mail.ru", "42354646");

            //Assert check cookies count 
            //Need value: 1
            Assert.AreEqual(Parser.cookies.Count, 1);
            
        }

        [TestMethod]
        public void Can_GetPointId()
        {
            //Act - get id of point by name
            string result = Parser.GetPointId("КИЇВ-ПАСАЖИРСЬКИЙ");

            //Assert check id of point
            Assert.AreEqual(result, "22200001");
        }

        [TestMethod]
        public void Can_GetRuntimeNames()
        {
            //Act - get name by part name
            string[] names = Parser.GetRunTimeNames("К");

            //Assert check count of "runtime" names
            Assert.AreEqual(names.Length, 16);
        }


        [TestMethod]
        public void Can_GetAllTrainInfo()
        {
            //CHANGE DATE OR TRAIN NUMBER IN LIST IF TEST IS FAILED
            //Act - get train info
            Train train = Parser.GetAllTrainInfo("КИЇВ-ПАСАЖИРСЬКИЙ", "ЗАПОРІЖЖЯ 1", "2015-07-02", "072К", 2,false,CarType.Coupe);

            //Assert check train object if it is null
            Assert.IsNotNull(train);
        }
    }
}
