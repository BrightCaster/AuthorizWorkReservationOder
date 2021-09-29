using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkersOrder.Service;
using System;
using System.Collections.Generic;
using System.Text;
using WorkersOrder.Models;
using WorkersOrder.Models.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using WorkersOrder.Service;

namespace WorkersOrder.Service.Tests
{
    [TestClass()]
    public class ServiceTests
    {

        [TestMethod()]
        public async Task FindAccountModelTest()
        {
            Context context = new Context();
            Service service = new Service(context);
            Employee e = await service.FindAccountModel(null, new LoginModel { Login = "admin", Password = "11111" });
            Assert.AreEqual("admin", e.Login);
            Assert.AreEqual("11111", e.Password);
            e = await service.FindAccountModel(null, new LoginModel { Login = "Figname", Password = "123456" });
            Assert.AreEqual("Figname", e.Login);
            Assert.AreNotEqual("1234456", e.Password);
        }

        [TestMethod()]
        public void TrueRolesTest()
        {
            Context context = new Context();
            Service service = new Service(context);
            Assert.AreEqual(true, service.TrueRoles(new LoginModel { Login = "admin", Password = "11111" }));
            Assert.AreEqual(false, service.TrueRoles(new LoginModel { Login = "Figname", Password = "123456" }));
        }

        [TestMethod()]
        public void LatinTest()
        {
            Context context = new Context();
            Service service = new Service(context);
            Assert.AreEqual(true, service.Latin("asd"));
            Assert.AreEqual(false, service.Latin("sadрнп"));
            Assert.AreEqual(false, service.Latin("выаыва"));
        }

        [TestMethod()]
        public void RemoveSpaceTest()
        {
            Context context = new Context();
            Service service = new Service(context);
            Assert.AreEqual("asdas",service.RemoveSpace("asdas  "));
            Assert.AreEqual("asdas", service.RemoveSpace("asdas"));


        }
    }
}