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

public class AlbumControllerTests
{
    private readonly Mock<IRepository<Album>> _mockAlbumRepository;
    private readonly AlbumController _albumController;
    private readonly List<Artist> _testArtists;
    private readonly List<Format> _testFormats;
    private readonly List<Album> _testAlbums;
    private readonly Dictionary<string, string> _testFiltersWithData;
    private readonly Dictionary<string, string> _testFiltersWithoutData;

    public AlbumControllerTests()
    {
        _mockAlbumRepository = new Mock<IRepository<Album>>();
        _albumController = new AlbumController(_mockAlbumRepository.Object);

        _testFormats = new List<Format>() {
            new Format() { Id = 1, Name = "CD" },
            new Format() { Id = 2, Name = "DVD" }
        };

        _testArtists = new List<Artist>()
        {
            new Artist() { Id = Guid.NewGuid(), Name = "First" },
            new Artist() { Id = Guid.NewGuid(), Name = "First" }
        };

        _testAlbums = new List<Album>() {
            new Album() { Id = Guid.NewGuid(), Title = "First", ArtistId = Guid.NewGuid(), FormatId = 1, Stock = 1 },
            new Album() { Id = Guid.NewGuid(), Title = "Second", ArtistId = Guid.NewGuid(), FormatId = 2, Stock = 1 }
        };

        _testAlbums[0].FormatIdNavigation = _testFormats[0];
        _testAlbums[1].FormatIdNavigation = _testFormats[1];

        _testAlbums[0].Artist = _testArtists[0];
        _testAlbums[1].Artist = _testArtists[1];

        _testFiltersWithData = new Dictionary<string, string>
        {
            { "Title", "ir" },
            { "Artist", "ir" }
        };

        _testFiltersWithoutData = new Dictionary<string, string>
        {
            { "Title", "ir" },
            { "Artist", "ec" }
        };
    }

    [Fact]
    public void GetAsync_ReturnsIEnumerableTaskOfAlbums()
    {
        _mockAlbumRepository.Setup(r => r.GetAsync(false, new Dictionary<string, string>())).ReturnsAsync(_testAlbums);

        var result = _albumController.GetAsync();

        Assert.IsType<Task<IEnumerable<Album>>>(result);
    }

    [Fact]
    public void GetAsync_ReturnsCorrectNumberOfAlbums()
    {
        _mockAlbumRepository.Setup(r => r.GetAsync(false, new Dictionary<string, string>())).ReturnsAsync(_testAlbums);

        var result = _albumController.GetAsync(false).Result.Count();

        Assert.Equal(2, result);
    }

    [Fact]
    public void GetAsync_ReturnsCorrectNumberOfFilteredAlbumsWithData()
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(_testFiltersWithData)));

        var ctx = new HttpContextMock();
        ctx.RequestMock.Mock.Setup(r => r.Method).Returns("GET");
        ctx.RequestMock.Mock.Setup(r => r.Body).Returns(stream);
        ctx.RequestMock.Mock.Setup(r => r.ContentType).Returns("application/json");
        ctx.RequestMock.Mock.Setup(r => r.ContentLength).Returns(stream.Length);

        _albumController.ControllerContext.HttpContext = ctx;

        _mockAlbumRepository.Setup(r => r.GetAsync(true, _testFiltersWithData)).ReturnsAsync(new List<Album>() { _testAlbums[0] });

        var result = _albumController.GetAsync().Result.Count();

        Assert.Equal(1, result);
    }

    [Fact]
    public void GetAsync_ReturnsCorrectNumberOfFilteredAlbumsWithoutData()
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(_testFiltersWithoutData)));

        var ctx = new HttpContextMock();
        ctx.RequestMock.Mock.Setup(r => r.Method).Returns("GET");
        ctx.RequestMock.Mock.Setup(r => r.Body).Returns(stream);
        ctx.RequestMock.Mock.Setup(r => r.ContentType).Returns("application/json");
        ctx.RequestMock.Mock.Setup(r => r.ContentLength).Returns(stream.Length);

        _albumController.ControllerContext.HttpContext = ctx;

        _mockAlbumRepository.Setup(r => r.GetAsync(true, _testFiltersWithData)).ReturnsAsync(new List<Album>());

        var result = _albumController.GetAsync().Result.Count();

        Assert.Equal(0, result);
    }

    [Fact]
    public void GetByIdAsync_ReturnsCorrectAlbum()
    {
        _mockAlbumRepository.Setup(r => r.GetByIdAsync(_testAlbums[0].Id)).ReturnsAsync(_testAlbums[0]);

        var result = _albumController.GetByIdAsync(_testAlbums[0].Id).Result;

        Assert.Equal(_testAlbums[0], result.Value);
    }

    [Fact]
    public void InsertAsync_CorrectlyInsertsAlbum()
    {
        var newAlbum = new Album() { Id = Guid.NewGuid(), Title = "Third", ArtistId = Guid.NewGuid(), FormatId = 3, Stock = 1 };

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newAlbum)));

        var ctx = new HttpContextMock();
        ctx.RequestMock.Mock.Setup(r => r.Method).Returns("POST");
        ctx.RequestMock.Mock.Setup(r => r.Body).Returns(stream);
        ctx.RequestMock.Mock.Setup(r => r.ContentType).Returns("application/json");
        ctx.RequestMock.Mock.Setup(r => r.ContentLength).Returns(stream.Length);

        _albumController.ControllerContext.HttpContext = ctx;

        _mockAlbumRepository.Setup(r => r.InsertAsync(It.IsAny<Album>())).ReturnsAsync(new ObjectResult(newAlbum));

        var result = _albumController.InsertAsync().Result;

        Assert.Equal(new ObjectResult(newAlbum).Value, (result as ObjectResult).Value);
    }

    [Fact]
    public void UpdateAsync_CorrectlyUpdatesAlbum()
    {
        // Add one unit of stock
        var newAlbum = _testAlbums[0];

        newAlbum.Stock++;

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newAlbum)));

        var ctx = new HttpContextMock();
        ctx.RequestMock.Mock.Setup(r => r.Method).Returns("PUT");
        ctx.RequestMock.Mock.Setup(r => r.Body).Returns(stream);
        ctx.RequestMock.Mock.Setup(r => r.ContentType).Returns("application/json");
        ctx.RequestMock.Mock.Setup(r => r.ContentLength).Returns(stream.Length);

        _albumController.ControllerContext.HttpContext = ctx;

        _mockAlbumRepository.Setup(r => r.UpdateAsync(It.IsAny<Album>())).ReturnsAsync(new ObjectResult(newAlbum));

        var result = _albumController.UpdateAsync().Result;

        Assert.Equal(new ObjectResult(newAlbum).Value, (result as ObjectResult).Value);
     }

    [Fact]
    public void DeleteAsync_CorrectlyDeletesAlbum()
    {
        var ctx = new HttpContextMock();
        ctx.RequestMock.Mock.Setup(r => r.Method).Returns("DELETE");

        _albumController.ControllerContext.HttpContext = ctx;

        _mockAlbumRepository.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(new ObjectResult(_testAlbums[0].Id));

        var result = _albumController.DeleteAsync(_testAlbums[0].Id).Result;

        Assert.Equal(new ObjectResult(_testAlbums[0].Id).Value, (result as ObjectResult).Value);
    }
}