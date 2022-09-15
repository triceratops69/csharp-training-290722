using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            app.Groups.CreateGroupIfNotExist("modifyme");

            GroupData newData = new GroupData("qqq");
            newData.Header = "www";
            newData.Footer = "eee";

            List<GroupData> oldGroups = GroupData.GetAll();

            GroupData oldData = oldGroups[0];

            app.Groups.Modify(oldData.Id, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups); 
            
            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(group.Name, newData.Name);
                }
            }
        }
    }
}