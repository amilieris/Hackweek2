using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Controllers;

namespace RoomsTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestGetRooms()
        {
            var controller = new ExchangeController();
            var param = new BookRoomParam
            {
                LifeSize = true,
                Time = 30,
                Location = "1"
            };
            var res = controller.BookRoom(param);
        }
    }
}
