using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            if (! app.Groups.IsGroupIn())
            {
                GroupData group = new GroupData("modifyme");
                app.Groups.Create(group);
            }

            GroupData newData = new GroupData("qqq");
            newData.Header = "www";
            newData.Footer = "eee";

            app.Groups.Modify(1, newData);
        }
    }
}