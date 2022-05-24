using Auth.Infrastructure.Repositories;
using Auth.Model;
using Auth.Model.Exceptions;
using Auth.Model.InfrastructureInterfaces;
using Auth.Service;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Auth.unitTests
{
    public class Test
    {      
        [Theory]
        [MemberData(nameof(CreateUsers))]
        public void CreateUsers_should_throw_exception(RegistrationRequest r)
        {
            bool exceptionThrown = false;

            var stub = new Mock<IUserInfrastructureService>();

            List < User > registered = new List<User>();

            stub.Setup(s => s.GetAll()).Returns(registered);

            UserService us = new UserService(stub.Object);

            try
            {
                us.Register(r);
            }
            catch (EmailIsTakenException)
            {
                exceptionThrown = true;
            }
            catch (UsernameIsTakenException)
            {
                exceptionThrown = true;
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }
            Assert.False(exceptionThrown);
        }

        public static IEnumerable<object[]> CreateUsers()
        {
            List<object[]> retVal = new List<object[]>();

            //User u1 = new User("ime 1", "prz 1", "usrnm1", "mail1", "pass1", "pass1", true);

           // User u2 = new User("ime 2", "prz 2", "usrnm2", "mail1", "pass2", "pass2", false);

            User u3 = new User("ime 3", "prz 3", "usrnm1", "mail2", "pass3",  false, "salt");

            User u4 = new User("", "prz 3", "usrnm 4", "mail4", "pass3",  false, "salt");

            User u5 = new User("ime 4", "", "usrnm 5", "mail5", "pass3",  false, "salt");

            User u6 = new User("ime 3", "prz 3", "", "mail6", "pass3",  false, "salt");

            User u7 = new User("ime 3", "prz 3", "usrnm7", "", "pass", false, "salt");

            User u8 = new User("ime 3", "prz 3", "usrnm 9", "mail9", "",  false, "salt");

            User u9 = new User("ime 3", "prz 3", "usrnm 9", "mail9", "pass1", false, "salt");

            

            //RegistrationRequest r1 = new RegistrationRequest(u1.Email, u1.FirstName, u1.LastName, u1.UserName, u1.Password, u1.Confirm, u1.IsAdmin);

           // RegistrationRequest r2 = new RegistrationRequest(u2.Email, u2.FirstName, u2.LastName, u2.UserName, u2.Password, u2.Confirm, u2.IsAdmin);

            RegistrationRequest r3 = new RegistrationRequest(u3.Email, u3.FirstName, u3.LastName, u3.UserName, u3.Password, u3.Confirm, u3.IsAdmin);

            RegistrationRequest r4 = new RegistrationRequest(u4.Email, u4.FirstName, u4.LastName, u4.UserName, u4.Password, u4.Confirm, u4.IsAdmin);

            RegistrationRequest r5 = new RegistrationRequest(u5.Email, u5.FirstName, u5.LastName, u5.UserName, u5.Password, u5.Confirm, u5.IsAdmin);

            RegistrationRequest r6 = new RegistrationRequest(u6.Email, u6.FirstName, u6.LastName, u6.UserName, u6.Password, u6.Confirm, u6.IsAdmin);

            RegistrationRequest r7 = new RegistrationRequest(u7.Email, u7.FirstName, u7.LastName, u7.UserName, u7.Password, u7.Confirm, u7.IsAdmin);

            RegistrationRequest r8 = new RegistrationRequest(u8.Email, u8.FirstName, u8.LastName, u8.UserName, u8.Password, u8.Confirm, u8.IsAdmin);

            RegistrationRequest r9 = new RegistrationRequest(u9.Email, u9.FirstName, u9.LastName, u9.UserName, u9.Password, u9.Confirm, u9.IsAdmin);

           /* retVal.Add(new object[]
            {
                r1
            });

            retVal.Add(new object[]
            {
                r2
            });*/

            retVal.Add(new object[]
            {
                r3
            });

            retVal.Add(new object[]
            {
                r4
            });

            retVal.Add(new object[]
            {
                r5
            });

            retVal.Add(new object[]
            {
                r6
            });

            retVal.Add(new object[]
            {
                r7
            });

            retVal.Add(new object[]
            {
                r8
            });

            retVal.Add(new object[]
            {
                r9
            });

            return retVal;
        }

        [Theory]
        [MemberData(nameof(ValidateUsers))]
        public void ValidateUsers_should_throw_exception(AuthenticateRequest a)
        {
            bool exceptionThrown = false;

            var stub = new Mock<IUserInfrastructureService>();

            List<User> registered = new List<User>();

            stub.Setup(s => s.GetAll()).Returns(registered);

            UserService us = new UserService(stub.Object);

            try
            {
                us.Authenticate(a);
            }
            catch (LogInException)
            {
                exceptionThrown = true;
            }
            catch (ArgumentException)
            {
                exceptionThrown = true;
            }
            Assert.True(exceptionThrown);
        }

        public static IEnumerable<object[]> ValidateUsers()
        {
            List<object[]> retVal = new List<object[]>();

            User u1 = new User("ime 1", "prz 1", "usrnm 1", "mail1", "pass1", true, "salt");

            User u2 = new User("ime 2", "prz 2", "usrnm 2", "mail2", "pass2", false, "salt");

            User u3 = new User("ime 3", "prz 3", "", "mail3", "pass3", false, "salt");

            User u4 = new User("ime 3", "prz 3", "usrnm 4", "mail4", "", false, "salt");

            User u5 = new User("ime 3", "prz 3", "usrnm 5", "mail5", "pass5", false, "salt");

            AuthenticateRequest a1 = new AuthenticateRequest(u1.UserName, "nevazeci");

            AuthenticateRequest a2 = new AuthenticateRequest("nevazeci", u2.Password);

            AuthenticateRequest a3 = new AuthenticateRequest(u3.UserName, u3.Password);

            AuthenticateRequest a4 = new AuthenticateRequest(u4.UserName, u4.Password);

            AuthenticateRequest a5 = new AuthenticateRequest(u5.UserName, u5.Password);

            retVal.Add(new object[]
            {
                a1
            });

            retVal.Add(new object[]
            {
                a2
            });

            retVal.Add(new object[]
            {
                a3
            });

            retVal.Add(new object[]
            {
                a4
            });

            retVal.Add(new object[]
            {
                a5
            });

            return retVal;
        }
    }
}
