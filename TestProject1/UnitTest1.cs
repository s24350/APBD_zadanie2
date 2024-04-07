using LegacyApp;
using Xunit;

//Example from lectue:
// Assert.Equal("Some value", res);

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void AddUser_Should_Retutn_False_When_Missing_Name()
        {
            //Arrange
            var service = new UserService();

            //Act
            var res1 = service.AddUser(null, null, "Kowalski@wp.pl", new DateTime(1980, 1, 1), 1);
            //zwoci boola - wynik ma byc false

            //Assert
            Assert.False(res1);
            //jak wy¿ej lub Assert.Equal(EXPECTED_VALUE, ACTUAL_VALUE);

        }

        [Fact]
        public void AddUser_Should_Retutn_False_When_Email_Missing_At_And_Dot()
        {
            //Arrange
            var service = new UserService();

            //walidacja maila
            var res2 = service.AddUser("Jan", "Kowalsi", "Kowalskiwppl", new DateTime(1980, 1, 1), 1);
            Assert.False(res2);


        }
        
        [Fact]
        public void AddUser_Should_Retutn_False_When_Missing_Age()
        {
            var service = new UserService();

            //walidacja wieku
            var res3 = service.AddUser("Jan", "Kowalski", "Kowalskiwp.pl", new DateTime(2010, 1, 1), 1);
            Assert.False(res3);

        }
    }
}