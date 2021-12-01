using HttpContextMoq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicalogAPI.Controllers.Musicalog;
using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Models.Musicalog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MusicalogAPITests;

public class ArtistControllerTests
{
    private readonly Mock<IRepository<Artist>> _mockArtistRepository;
    private readonly ArtistController _artistController;
    private readonly List<Artist> _testArtists;

    public ArtistControllerTests()
    {
        _mockArtistRepository = new Mock<IRepository<Artist>>();
        _artistController = new ArtistController(_mockArtistRepository.Object);

        _testArtists = new List<Artist>() {
            new Artist() { Id = Guid.NewGuid(), Name = "First" },
            new Artist() { Id = Guid.NewGuid(), Name = "Second" }
        };
    }

    [Fact]
    public void GetAsync_ReturnsIEnumerableTaskOfArtists()
    {
        _mockArtistRepository.Setup(r => r.GetAsync(false, new Dictionary<string, string>())).ReturnsAsync(_testArtists);

        var result = _artistController.GetAsync();

        Assert.IsType<Task<IEnumerable<Artist>>>(result);
    }

    [Fact]
    public void GetAsync_ReturnsCorrectNumberOfArtists()
    {
        _mockArtistRepository.Setup(r => r.GetAsync(false, new Dictionary<string, string>())).ReturnsAsync(_testArtists);

        var result = _artistController.GetAsync(false).Result.Count();

        Assert.Equal(2, result);
    }

    [Fact]
    public void GetByIdAsync_ReturnsCorrectArtist()
    {
        _mockArtistRepository.Setup(r => r.GetByIdAsync(_testArtists[0].Id)).ReturnsAsync(_testArtists[0]);

        var result = _artistController.GetByIdAsync(_testArtists[0].Id).Result;

        Assert.Equal(_testArtists[0], result.Value);
    }

    [Fact]
    public void GetByIdAsync_CorrectlyInsertsArtist()
    {
        var newArtist = new Artist() { Id = Guid.NewGuid(), Name = "Third" };

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newArtist)));

        var ctx = new HttpContextMock();
        ctx.RequestMock.Mock.Setup(r => r.Method).Returns("POST");
        ctx.RequestMock.Mock.Setup(r => r.Body).Returns(stream);
        ctx.RequestMock.Mock.Setup(r => r.ContentType).Returns("application/json");
        ctx.RequestMock.Mock.Setup(r => r.ContentLength).Returns(stream.Length);

        _artistController.ControllerContext.HttpContext = ctx;

        _mockArtistRepository.Setup(r => r.InsertAsync(It.IsAny<Artist>())).ReturnsAsync(new ObjectResult(newArtist));

        var result = _artistController.InsertAsync().Result;

        Assert.Equal(new ObjectResult(newArtist).Value, (result as ObjectResult).Value);
    }

    [Fact]
    public void GetByIdAsync_CorrectlyUpdatesArtist()
    {
        // CHange Artist's Name
        var newArtist = _testArtists[0];

        newArtist.Name = "Steve";

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newArtist)));

        var ctx = new HttpContextMock();
        ctx.RequestMock.Mock.Setup(r => r.Method).Returns("PUT");
        ctx.RequestMock.Mock.Setup(r => r.Body).Returns(stream);
        ctx.RequestMock.Mock.Setup(r => r.ContentType).Returns("application/json");
        ctx.RequestMock.Mock.Setup(r => r.ContentLength).Returns(stream.Length);

        _artistController.ControllerContext.HttpContext = ctx;

        _mockArtistRepository.Setup(r => r.UpdateAsync(It.IsAny<Artist>())).ReturnsAsync(new ObjectResult(newArtist));

        var result = _artistController.UpdateAsync().Result;

        Assert.Equal(new ObjectResult(newArtist).Value, (result as ObjectResult).Value);
    }

    [Fact]
    public void GetByIdAsync_CorrectlyDeletesArtist()
    {
        var ctx = new HttpContextMock();
        ctx.RequestMock.Mock.Setup(r => r.Method).Returns("DELETE");

        _artistController.ControllerContext.HttpContext = ctx;

        _mockArtistRepository.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(new ObjectResult(_testArtists[0].Id));

        var result = _artistController.DeleteAsync(_testArtists[0].Id).Result;

        Assert.Equal(new ObjectResult(_testArtists[0].Id).Value, (result as ObjectResult).Value);
    }
}