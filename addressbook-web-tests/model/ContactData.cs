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
        public string Address2 { get; set; }
        public string Phone2 { get; set; }
        public string Notes { get; set; }


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
                    return 
                        (
                        (FullName(CleanName(FirstName), CleanName(MiddleName), CleanName(LastName))
                        
                        + CleanSubInfo(NickName)
                        + CleanImage(Image)

                        + Block1(CleanSubInfo(Title), CleanSubInfo(Company), CleanSubInfo(Address)))

                        + AllPhone(CleanPhone("H", HomePhone), CleanPhone("M", MobilePhone), CleanPhone("W", WorkPhone), CleanPhone("F", FaxPhone))

                        + Block2(AllEmail(CleanEmail(Email), CleanEmail(Email2), CleanEmail(Email3))
                        , CleanHomepage("Homepage", HomePage))

                        + Block3(CleanDate("Birthday", CleanDMY(Bday), CleanDMY(Bmonth), CleanDMY(Byear))
                        , CleanDate("Anniversary", CleanDMY(Aday), CleanDMY(Amonth), CleanDMY(Ayear)))

                        + BlockSecondary(CleanSec(Address2).Trim(), CleanPhone("P", Phone2), CleanSec(Notes))
                        ).Trim();
                }
            }
            set
            {
                allDetails = value;
            }
        }

        private string FullName(string n1, string n2, string n3)
        {
            if (n1 == "" && n2 == "" && n3 == "")
            {
                return "";
            }
            return (n1 + n2 + n3).Trim() + "\r\n";
        }
        private string CleanName(string name)
        {
            if (name == null || name == "")
            {
                return "";
            }
            return name + " ";
        }

        private string CleanSubInfo(string info)
        {
            if (info == null || info == "")
            {
                return "";
            }
            return info.Trim() + "\r\n";
        }
        private string CleanImage(bool img)
        {
            if (img)
            {
                return "\r\n";
            }
            return "";
        }

        private string Block1(string s1, string s2, string s3)
        {
            if (s1 == "" && s2 == "" && s3 == "")
            {
                return "\r\n";
            }
            return s1 + s2 + s3 + "\r\n";
        }
        private string AllPhone(string p1, string p2, string p3, string p4)
        {
            if (p1 == "" && p2 == "" & p3 == "" && p4 == "")
            {
                return "";
            }
            return (p1 + p2 + p3 + p4).Trim() + "\r\n\r\n";
        }
        private string CleanPhone(string info, string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return info + ": " + phone + "\r\n";
        }

        private string Block2(string s1, string s2)
        {
            if(s1 == "" && s2 == "")
            {
                return "";
            }
            if(s2 == "")
            {
                s2 = "\r\n";
            }
            return s1 + s2;
        }
        private string AllEmail(string e1, string e2, string e3)
        {
            if (e1 == "" && e2 == "" & e3 == "")
            {
                return "";
            }
            return (e1 + e2 + e3).Trim() + "\r\n";
        }
        private string CleanEmail(string email)
        {

            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";
        }
        private string CleanHomepage(string info, string homePage)
        {
            if (homePage == null || homePage == "")
            {
                return "";
            }
            return info + ":\r\n" + homePage + "\r\n\r\n";
        }

        private string Block3(string s1, string s2)
        {
            if(s1 == "" && s2 == "")
            {
                return "\r\n";
            }
            return (s1 + s2).Trim() + "\r\n\r\n";
        }
        private string CleanDate(string info, string bday, string bmounth, string byear)
        {
            if (bday == "" && bmounth == "" && byear == "")
            {
                return "";
            }

            string ageout = "";
            if (byear != "" && byear != null && Convert.ToInt32(byear) > 1872)
            {
                string dateString;
                if (bmounth == "")
                {
                    dateString = ("1 1 " + byear).Trim();
                }
                else
                {
                    dateString = (bday + bmounth + byear).Trim();
                }
                var dateTime = DateTime.Parse(dateString);
                if (dateTime.Year >= 1873)
                {
                    int age = DateTime.Now.Year - dateTime.Year;
                    if (info == "Birthday")
                    {
                        if (dateTime > DateTime.Now.AddYears(-age)) age--;
                    }
                    ageout = System.String.Format("({0})", age);
                }
            }

            if (bday != "" && bday != null)
            {
                bday = bday.Trim() + ". ";
            }
            else
            {
                bday = "";
            }

            return info + " " + (bday + bmounth + byear + ageout).Trim() + "\r\n";
        }

        private string CleanDMY(string b)
        {
            if (b == null || b == "" || b == "-")
            {
                return "";
            }
            return b + " ";
        }

        private string BlockSecondary(string s1, string s2, string s3)
        {
            if (s1 == "" && s2 == "" && s3 == "")
            {
                return "";
            }
            if (s1 == "" && s2 == "")
            {
                return s3.Trim();
            }
            if (s1 == "")
            {
                return "\r\n" + s2 + "\r\n" + s3.Trim();
            }
            if (s2 == "")
            {
                return s1 + "\r\n\r\n" + s3;
            }
            return s1 + "\r\n\r\n" + s2 + "\r\n" + s3;
        }
        private string CleanSec(string address)
        {
            if (address == null || address == "")
            {
                return "";
            }
            return address.Trim();
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
