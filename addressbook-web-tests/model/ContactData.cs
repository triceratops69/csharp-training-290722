using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allDetails;

        public ContactData() { }
        public ContactData(string firstName)
        {
            FirstName = firstName;
        }
        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string NickName { get; set; }
        public bool Image { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public string AllPhones 
        { 
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }

            } set
            {
                allPhones = value;
            } 
        }

        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string FaxPhone { get; set; }
        public string AllEmails
        { 
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                } 
                else
                {
                    return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            } 
        }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string HomePage { get; set; }
        public string Bday { get; set; }
        public string Bmonth { get; set; }
        public string Byear { get; set; }
        public string Aday { get; set; }
        public string Amonth { get; set; }
        public string Ayear { get; set; }


        public string AllDetails
        {
            get
            {
                if (allDetails != null)
                {
                    return allDetails;
                }
                else
                {
                    return FullName(CleanName(FirstName), CleanName(MiddleName), CleanName(LastName))
                        + CleanNickTitleComp(NickName) + CleanImage(Image) + CleanNickTitleComp(Title) + CleanNickTitleComp(Company)
                        + CleanAddress(Address)
                        + AllPhone(CleanPhone("H", HomePhone), CleanPhone("M", MobilePhone), CleanPhone("W", WorkPhone), CleanPhone("F", FaxPhone))
                        + CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3) + CleanHomepage("Homepage", HomePage)
                        + CleanBirthday("Birthday", CleanB(Bday), CleanB(Bmonth), CleanB(Byear))
                        + CleanBirthday("Anniversary", CleanB(Aday), CleanB(Amonth), CleanB(Ayear));
                }
            }
            set
            {
                allDetails = value;
            }
        }

        private string FullName(string n1, string n2, string n3)
        {
            if(n1 == "" && n2 == "" && n3 == "")
            {
                return "";
            }
            return (n1 + n2 + n3).Trim() + "\r\n";
        }

        private string CleanBirthday(string info, string bday, string bmounth, string byear)
        {
            if (bday == "" && bmounth == "" && byear == "")
            {
                return "";
            }

            string ageout = "";
            if (byear != "" && byear != null)
            {
                string dateString = (bday + bmounth + byear).Trim();
                var dateTime = DateTime.Parse(dateString);

                int age = DateTime.Now.Year - dateTime.Year;
                if(info == "Birthday")
                {
                    if (dateTime > DateTime.Now.AddYears(-age)) age--;
                }
                ageout = System.String.Format("({0})", age);
            }

            if (bday != "" && bday != null)
            {
                bday = bday.Trim() + ". ";
            }
            else
            {
                bday = "";
            }
            
            return "\r\n" + info + " " + (bday + bmounth + byear + ageout).Trim();
        }

        private string CleanB(string b)
        {
            if (b == null || b == "" || b == "-")
            {
                return "";
            }
            return b + " ";
        }

        private object CleanHomepage(string info, string homePage)
        {
            if (homePage == null || homePage == "")
            {
                return "";
            }
            return info + ":\r\n" + homePage + "\r\n";
        }

        private string CleanImage(bool img)
        {
            if (img)
            {
                return "\r\n";
            }
            return "";
        }

        private string CleanNickTitleComp(string nickName)
        {
            if (nickName == null || nickName == "")
            {
                return "";
            }
            return nickName + "\r\n";
        }

        private string CleanAddress(string address)
        {
            if (address == null || address == "")
            {
                return "";
            }
            return address + "\r\n";
        }
        private string AllPhone(string p1, string p2, string p3, string p4)
        {
            if (p1 == "" && p2 == "" & p3 == "" && p4 == "")
            {
                return "";
            }
            return p1 + p2 + p3 + p4 + "\r\n";

        }

        private string CleanPhone(string info, string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return info + ": " + phone + "\r\n";
        }

        private string CleanName(string name)
        {
            if (name == null || name == "")
            {
                return "";
            }
            return name.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + " ";
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")","") + "\r\n";
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return LastName.GetHashCode() ^ FirstName.GetHashCode();
        }

        public override string ToString()
        {
            return "LastName = " + LastName + " FirstName = " + FirstName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (LastName.CompareTo(other.LastName) == 0)
            {
                return FirstName.CompareTo(other.FirstName);
            }
            else
            {
                return LastName.CompareTo(other.LastName);
            }
        }
    }
}
