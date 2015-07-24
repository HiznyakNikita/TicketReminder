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
            Parser.Auth("nikita.ru96@mail.ru", "mpo5yy70");

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
            Parser.Auth("nikita.ru96@mail.ru", "mpo5yy70");

            //CHANGE DATE OR TRAIN NUMBER IN LIST IF TEST IS FAILED
            //Act - get train info
            Train train = Parser.GetAllTrainInfo("КИЇВ-ПАСАЖИРСЬКИЙ", "ЗАПОРІЖЖЯ 1", "2015-10-12", "072К", 2,false,CarType.Coupe);

            //Assert check train object if it is null
            Assert.AreNotEqual(train.Cars,0);
        }

        [TestMethod]
        public void Can_ReserveTicket()
        {
            Parser.Auth("nikita.ru96@mail.ru", "mpo5yy70");

            //Act - get train with reservation
            Train train1 = Parser.GetAllTrainInfo("КИЇВ-ПАСАЖИРСЬКИЙ", "ЗАПОРІЖЖЯ 1", "2015-10-12", "072К", 2, true, CarType.Coupe);

            //Assert check train reserved place
            //If we had one reservation we couldn't get a train info again
            Train train2 = Parser.GetAllTrainInfo("КИЇВ-ПАСАЖИРСЬКИЙ", "ЗАПОРІЖЖЯ 1", "2015-10-12", "072К", 2, false, CarType.Coupe);

            Assert.Equals(train2.Cars, 0);
        }
    }
}
