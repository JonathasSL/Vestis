using System;
using Vestis._03_Domain.Entities;
using Xunit;

namespace Vestis._03_Domain.Tests.Entities
{
    public class UserEntityTests
    {
        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void UserEntity_HappyPath_InstantiationWithAllParameters()
        {
            // Arrange
            var name = "Test User";
            var email = "test@example.com";
            var password = "Password123!";
            var profileImg = "profile.png";

            // Act
            var user = new UserEntity(name, email, password, profileImg);

            // Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Equal(profileImg, user.ProfileImg);
            Assert.False(user.IsEmailConfirmed);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void UserEntity_HappyPath_InstantiationWithoutProfileImg()
        {
            // Arrange
            var name = "Test User";
            var email = "test@example.com";
            var password = "Password123!";

            // Act
            var user = new UserEntity(name, email, password);

            // Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Null(user.ProfileImg);
            Assert.False(user.IsEmailConfirmed);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void ConfirmEmail_HappyPath_ChangesIsEmailConfirmedToTrueAndCallsSetAsUpdated()
        {
            // Arrange
            var user = new UserEntity("Test User", "test@example.com", "Password123!");
            var initialUpdatedDate = user.UpdatedDate;

            // Act
            user.ConfirmEmail();

            // Assert
            Assert.True(user.IsEmailConfirmed);
            Assert.NotNull(user.UpdatedDate);
            Assert.NotEqual(initialUpdatedDate, user.UpdatedDate);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void ConfirmEmail_EdgeCase_NoChangesIfAlreadyConfirmed()
        {
            // Arrange
            var user = new UserEntity("Test User", "test@example.com", "Password123!");
            user.ConfirmEmail(); // Confirm first time
            var updatedDateAfterFirstConfirm = user.UpdatedDate;

            // Act
            user.ConfirmEmail(); // Try to confirm again

            // Assert
            Assert.True(user.IsEmailConfirmed);
            Assert.Equal(updatedDateAfterFirstConfirm, user.UpdatedDate);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ChangeName_ExceptionExpected_ArgumentExceptionWhenNameIsNullOrWhiteSpace(string invalidName)
        {
            // Arrange
            var user = new UserEntity("Test User", "test@example.com", "Password123!");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => user.ChangeName(invalidName));
            Assert.Equal("Name cannot be null or empty.", exception.Message);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void ChangeName_HappyPath_UpdatesNameAndCallsSetAsUpdated()
        {
            // Arrange
            var user = new UserEntity("Test User", "test@example.com", "Password123!");
            var newName = "New Name";
            var initialUpdatedDate = user.UpdatedDate;

            // Act
            user.ChangeName(newName);

            // Assert
            Assert.Equal(newName, user.Name);
            Assert.NotNull(user.UpdatedDate);
            Assert.NotEqual(initialUpdatedDate, user.UpdatedDate);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void ChangeName_EdgeCase_NoChangesIfNameIsSame()
        {
            // Arrange
            var name = "Test User";
            var user = new UserEntity(name, "test@example.com", "Password123!");
            var initialUpdatedDate = user.UpdatedDate;

            // Act
            user.ChangeName(name);

            // Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(initialUpdatedDate, user.UpdatedDate);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void ChangeEmail_HappyPath_UpdatesEmailAndCallsSetAsUpdated()
        {
            // Arrange
            var user = new UserEntity("Test User", "old@example.com", "Password123!");
            var newEmail = "new@example.com";
            var initialUpdatedDate = user.UpdatedDate;

            // Act
            user.ChangeEmail(newEmail);

            // Assert
            Assert.Equal(newEmail, user.Email);
            Assert.NotNull(user.UpdatedDate);
            Assert.NotEqual(initialUpdatedDate, user.UpdatedDate);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void ChangeEmail_EdgeCase_NoChangesIfEmailIsSame()
        {
            // Arrange
            var email = "test@example.com";
            var user = new UserEntity("Test User", email, "Password123!");
            var initialUpdatedDate = user.UpdatedDate;

            // Act
            user.ChangeEmail(email);

            // Assert
            Assert.Equal(email, user.Email);
            Assert.Equal(initialUpdatedDate, user.UpdatedDate);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void ChangePassword_HappyPath_UpdatesPasswordAndCallsSetAsUpdated()
        {
            // Arrange
            var user = new UserEntity("Test User", "test@example.com", "OldPassword123!");
            var newPassword = "NewPassword123!";
            var initialUpdatedDate = user.UpdatedDate;

            // Act
            user.ChangePassword(newPassword);

            // Assert
            Assert.Equal(newPassword, user.Password);
            Assert.NotNull(user.UpdatedDate);
            Assert.NotEqual(initialUpdatedDate, user.UpdatedDate);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void ChangePassword_EdgeCase_NoChangesIfPasswordIsSame()
        {
            // Arrange
            var password = "Password123!";
            var user = new UserEntity("Test User", "test@example.com", password);
            var initialUpdatedDate = user.UpdatedDate;

            // Act
            user.ChangePassword(password);

            // Assert
            Assert.Equal(password, user.Password);
            Assert.Equal(initialUpdatedDate, user.UpdatedDate);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void ChangeProfileImg_HappyPath_UpdatesProfileImgAndCallsSetAsUpdated()
        {
            // Arrange
            var user = new UserEntity("Test User", "test@example.com", "Password123!", "old.png");
            var newProfileImg = "new.png";
            var initialUpdatedDate = user.UpdatedDate;

            // Act
            user.ChangeProfileImg(newProfileImg);

            // Assert
            Assert.Equal(newProfileImg, user.ProfileImg);
            Assert.NotNull(user.UpdatedDate);
            Assert.NotEqual(initialUpdatedDate, user.UpdatedDate);
        }

        // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
        [Fact]
        public void ChangeProfileImg_EdgeCase_NoChangesIfProfileImgIsSame()
        {
            // Arrange
            var profileImg = "profile.png";
            var user = new UserEntity("Test User", "test@example.com", "Password123!", profileImg);
            var initialUpdatedDate = user.UpdatedDate;

            // Act
            user.ChangeProfileImg(profileImg);

            // Assert
            Assert.Equal(profileImg, user.ProfileImg);
            Assert.Equal(initialUpdatedDate, user.UpdatedDate);
        }
    }
}
