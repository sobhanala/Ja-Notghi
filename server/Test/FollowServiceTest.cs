using Application.ApplicationResult;
using Application.interfaces.ApplicationServices;
using Application.interfaces.ExternalServices;
using Application.interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Domain.Utils;
using Microsoft.AspNetCore.Http.Features;
using Moq;
using Xunit.Abstractions;

namespace Test
{
    public class FollowServiceTest
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IFollowRepository> _mockFollowRepo;
        private readonly FollowService _followService;

        public FollowServiceTest()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockFollowRepo = new Mock<IFollowRepository>();
            _followService = new FollowService(_mockFollowRepo.Object, _mockUserRepo.Object);
        }

        [Fact]
        public async Task FollowAsync_Fail_FollowYourself_ReturnStatusFollowYourSelf()
        {
            var followerUser = new User()
            {
                UserName = "testUser",
            };
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followerUser.UserName)).ReturnsAsync(followerUser);

            var actualResult = await _followService.FollowAsync(followerUser.UserName, followerUser.UserName);
            var expectedResult = Result.Failure(ResultStatus.FollowYourself);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task FollowAsync_Fail_FolloweeNotExist_UserNotFound()
        {
            var followerUser = new User()
            {
                UserName = "testUser",
            };
            var followeeUser = new User()
            {
                UserName = "testUser2",
            };
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followerUser.UserName)).ReturnsAsync(followerUser);
            var actualResult = await _followService.FollowAsync(followerUser.UserName, followeeUser.UserName);
            var expectedResult = Result.Failure(ResultStatus.UserNotFound);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task FollowAsync_Fail_FollowSomeOneWhoFollowedBefore_FollowAlreadyExist()
        {
            var followerUser = new User()
            {
                UserName = "testUser",
                FollowingsCount = 1,
            };
            var followeeUser = new User()
            {
                UserName = "testUser2",
                FollowersCount = 1,
            };
            Follow follow = new Follow
            {
                FollowedAt = DateTime.UtcNow,
                FollowedUserName = followerUser.UserName,
                FollowerUserName = followerUser.UserName,
            };
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followerUser.UserName))
                .ReturnsAsync(followerUser);
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followeeUser.UserName))
                .ReturnsAsync(followeeUser);

            _mockFollowRepo.Setup(x => x.GetFollowAsync(It.IsAny<string?>(), It.IsAny<string?>()))
                .ReturnsAsync(follow);

            var actualResult = await _followService.FollowAsync(followerUser.UserName, followeeUser.UserName);
            var expectedResult = Result.Failure(ResultStatus.FollowAlreadyExist);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task FollowAsync_Success_ReturnSuccessResult()
        {
            var followerUser = new User()
            {
                UserName = "testUser",
            };
            var followeeUser = new User()
            {
                UserName = "testUser2",
            };

            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followerUser.UserName))
                .ReturnsAsync(followerUser);
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followeeUser.UserName))
                .ReturnsAsync(followeeUser);

            _mockFollowRepo.Setup(x => x.GetFollowAsync(It.IsAny<string?>(), It.IsAny<string?>()))
             .ReturnsAsync((Follow?)null);

            var actualResult = await _followService.FollowAsync(followerUser.UserName, followeeUser.UserName);
            var expectedResult = Result.Success();
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(1, followerUser.FollowingsCount);
            Assert.Equal(1, followeeUser.FollowersCount);
        }

        [Fact]
        public async Task UnfollowAsync_Fail_UnfollowSomeoneNotFollowed_FollowNotFound()
        {
            var followerUser = new User()
            {
                UserName = "testUser",
            };
            var followeeUser = new User()
            {
                UserName = "testUser2",
            };
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followerUser.UserName))
                .ReturnsAsync(followerUser);
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followeeUser.UserName))
                .ReturnsAsync(followeeUser);
            _mockFollowRepo.Setup(x => x.GetFollowAsync(It.IsAny<string?>(), It.IsAny<string?>()))
                .ReturnsAsync((Follow?)null);
            var actualResult = await _followService.UnfollowAsync(followerUser.UserName, followeeUser.UserName);
            var expectedResult = Result.Failure(ResultStatus.FollowNotFound);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task UnfollowAsync_Fail_FolloweeNotExist_UserNotFound()
        {
            var followerUser = new User()
            {
                UserName = "testUser",
            };
            var followeeUser = new User()
            {
                UserName = "testUser2",
            };
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followerUser.UserName)).ReturnsAsync(followerUser);
            var actualResult = await _followService.FollowAsync(followerUser.UserName, followeeUser.UserName);
            var expectedResult = Result.Failure(ResultStatus.UserNotFound);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task UnFollowAsync_Fail_UnfollowYourself_UnfollowYourself()
        {
            var followerUser = new User()
            {
                UserName = "testUser",
            };
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followerUser.UserName))
                .ReturnsAsync(followerUser);

            var actualResult = await _followService.UnfollowAsync(followerUser.UserName, followerUser.UserName);
            var expectedResult = Result.Failure(ResultStatus.UnfollowYourself);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task UnFollowAsync_Success_ReturnSuccessResult()
        {

            var followerUser = new User()
            {
                UserName = "testUser",
                FollowingsCount = 1,
            };
            var followeeUser = new User()
            {
                UserName = "testUser2",
                FollowersCount = 1,
            };
            Follow follow = new Follow
            {
                FollowedAt = DateTime.UtcNow,
                FollowedUserName = followerUser.UserName,
                FollowerUserName = followerUser.UserName,
            };
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followerUser.UserName))
                .ReturnsAsync(followerUser);
            _mockUserRepo.Setup(x => x.GetByUsernameAsync(followeeUser.UserName))
                .ReturnsAsync(followeeUser);

            _mockFollowRepo.Setup(x => x.GetFollowAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(follow);

            var actualResult = await _followService.UnfollowAsync(followerUser.UserName, followeeUser.UserName);
            var expectedResult = Result.Success();
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(0, followerUser.FollowingsCount);
            Assert.Equal(0, followeeUser.FollowersCount);
        }

        [Fact]
        public async Task GetFollowersAsync_ValidUsername_ReturnsPaginatedList()
        {
            // Arrange
            string username = "testuser";
            int pageNumber = 1;
            int pageSize = 10;

            var user = new User {UserName = username, FollowersCount = 15};
            var followersUsernames = new List<string> {"follower1", "follower2"};
            var followerUsers = new List<User>
            {
                new User {UserName = "follower1"},
                new User {UserName = "follower2"}
            };


            _mockUserRepo.Setup(repo => repo.GetByUsernameAsync(username)).ReturnsAsync(user);
            _mockFollowRepo.Setup(repo => repo.GetFollowersAsync(username, pageNumber, pageSize))
                .ReturnsAsync(followersUsernames);
            _mockUserRepo.Setup(repo => repo.GetByUsernameAsync(It.Is<string>(u => followersUsernames.Contains(u))))
                .ReturnsAsync((string u) => followerUsers.Find(f => f.UserName == u));
            var expectedPaginatedList = new PaginatedList<User>
            {
                Items = followerUsers,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = user.FollowersCount
            };

            // Act
            var result = await _followService.GetFollowersAsync(username, pageNumber, pageSize);

            // Assert
            Assert.Equal(ResultStatus.Succeeded, result.Status);

            var resultPaginatedList = result.Value;

            Assert.Equal(expectedPaginatedList.PageNumber, resultPaginatedList.PageNumber);
            Assert.Equal(expectedPaginatedList.PageSize, resultPaginatedList.PageSize);
            Assert.Equal(expectedPaginatedList.TotalCount, resultPaginatedList.TotalCount);
            Assert.Equal(expectedPaginatedList.Items.Count(), resultPaginatedList.Items.Count());

            for (int i = 0; i < expectedPaginatedList.Items.Count(); i++)
            {
                Assert.Equal(expectedPaginatedList.Items.ElementAt(i).UserName,
                    resultPaginatedList.Items.ElementAt(i).UserName);
            }
        }

        [Fact]
        public async Task GetFollowingsAsync_ValidUsername_ReturnsPaginatedList()
        {
            // Arrange
            string username = "testuser";
            int pageNumber = 1;
            int pageSize = 10;

            var user = new User {UserName = username, FollowingsCount = 20};
            var followingsUsernames = new List<string> {"following1", "following2"};
            var followingUsers = new List<User>
            {
                new User {UserName = "following1"},
                new User {UserName = "following2"}
            };

            _mockUserRepo.Setup(repo => repo.GetByUsernameAsync(username)).ReturnsAsync(user);
            _mockFollowRepo.Setup(repo => repo.GetFollowingsAsync(username, pageNumber, pageSize))
                .ReturnsAsync(followingsUsernames);
            _mockUserRepo.Setup(repo => repo.GetByUsernameAsync(It.Is<string>(u => followingsUsernames.Contains(u))))
                .ReturnsAsync((string u) => followingUsers.Find(f => f.UserName == u));
            // Act
            var result = await _followService.GetFollowingsAsync(username, pageNumber, pageSize);
            var expectedPaginatedList = new PaginatedList<User>
            {
                Items = followingUsers,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = user.FollowingsCount,
            };

            // Assert
            Assert.Equal(ResultStatus.Succeeded, result.Status);
            Assert.Equal(pageNumber, expectedPaginatedList.PageNumber);
            Assert.Equal(pageSize, expectedPaginatedList.PageSize);
            Assert.Equal(expectedPaginatedList.TotalCount, result.Value.TotalCount);
            Assert.Equal(expectedPaginatedList.Items.Count(), result.Value.Items.Count());
            for (int i = 0; i < expectedPaginatedList.Items.Count(); i++)
            {
                Assert.Equal(expectedPaginatedList.Items.ElementAt(i).UserName,
                    actual: result.Value.Items.ElementAt(i).UserName);
            }
        }

        [Fact]
        public async Task GetFollowingsAsync_InValidUsername_ReturnsUserNotFound()
        {
            // Arrange
            string username = "nonexistentuser";
            int pageNumber = 1;
            int pageSize = 10;

            _mockUserRepo.Setup(repo => repo.GetByUsernameAsync(username)).ReturnsAsync((User?) null);

            // Act
            var result = await _followService.GetFollowingsAsync(username, pageNumber, pageSize);

            // Assert 
            Assert.Equal(ResultStatus.UserNotFound, result.Status);
            Assert.Null(result.Value);
        }
    }
}
