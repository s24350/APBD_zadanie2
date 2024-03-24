using LegacyApp;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void AddUser_Should_Retutn_False_When_Missing ()
        {
            //Arrange
            var service = new UserService();

            //Act
            var res1 = service.AddUser(null, null, "Kowalski@wp.pl", new DateTime(1980, 1, 1), 1);
            //zwoci boola - wynik ma byc false

            //Assert
            Assert.False(res1);


        }

        [Fact]
        public void AddUser_Should_Retutn_False_When_Missing_mail()
        {
            //Arrange
            var service = new UserService();

            //walidacja maila
            var res2 = service.AddUser("Jan", "Kowalsi", "Kowalskiwp.pl", new DateTime(1980, 1, 1), 1);
            Assert.False(res2);


        }
        
        [Fact]
        public void AddUser_Should_Retutn_False_When_Missing_Age()
        {
            var service = new UserService();

            //walidacja wieku
            var res3 = service.AddUser("Jan", "Kowalsi", "Kowalskiwp.pl", new DateTime(2010, 1, 1), 1);
            Assert.False(res3);

        }
    }
}