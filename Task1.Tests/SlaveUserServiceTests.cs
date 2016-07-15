﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using Moq;
using NUnit.Framework;
using Task1.StorageSystem.Concrete;
using Task1.StorageSystem.Concrete.Services;
using Task1.StorageSystem.Entities;
using Task1.StorageSystem.Interfaces;
using Task1.StorageSystem.Interfaces.Repository;

namespace Task1.Tests
{
    [TestFixture]
    public class SlaveUserServiceTests
    {
        //Using Moq to create fake entities
        private INumGenerator FakeNumGenerator { get; set; }
        private IRepository<User> FakeRepository { get; set; }
        private EmptyUserValidator FakeValidator { get; set; }
        public User SimpleUser { get; set; } = new User
        {
            FirstName = "Ivan2",
            LastName = "Ivanov2",
            PersonalId = "MP12345",
            BirthDate = DateTime.Now,
        };
        public SlaveUserServiceTests()
        {
            int fakeId = 1;
            var moqGenerator = new Moq.Mock<INumGenerator>();
            moqGenerator.Setup(g => g.GenerateId()).Returns(fakeId);
            FakeNumGenerator = moqGenerator.Object;

            var moqRepository = new Moq.Mock<IRepository<User>>();
            // stab for repository
            moqRepository.Setup(r => r.Add(It.IsAny<User>()));
            FakeRepository = moqRepository.Object;
            FakeValidator = new EmptyUserValidator();
        }
        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void Add_AddSimpleUser_ThrownedNotSupportedException()
        {
            var service = new SlaveUserService(FakeNumGenerator, FakeValidator, FakeRepository);
            service.Add(SimpleUser);
        }

        [Test]
        public void MasterServiceAdd_CreateSlaveAndSubscribeToMasterEditEvent_EventReceivedTwiceAfterAddAndDelete()
        {
            var userRepository = new UserRepository(null, null);
            var master = new MasterUserService(FakeNumGenerator, FakeValidator, userRepository);
            var slave = new SlaveUserService(FakeNumGenerator, FakeValidator, userRepository);
            slave.Subscribe(master);
            int eventsNumber = 2;
            int receivedEvents = 0;

            master.WasEdited += delegate (object sender, EventArgs e)
            {
                receivedEvents++;
            };

            master.Add(SimpleUser);
            master.Delete(SimpleUser);

            Assert.AreEqual(eventsNumber, receivedEvents);
        }



        [Test]
        public void BolleanSwitch_Test()
        {

            var value = ConfigurationManager.AppSettings["test"];
            Debug.WriteLine(value);
        }
    }
}